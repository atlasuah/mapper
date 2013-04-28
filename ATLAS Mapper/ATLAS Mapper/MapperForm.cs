using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;
using SlimDX.DirectInput;
using Microsoft.VisualBasic;

namespace ATLAS_Mapper
{
    public partial class MapperForm : Form
    {
        private const int HEART_RATE = 40;
        private const double GYRO_OFFSET_Z = 0.013;
        private const double SONAR_CONVERSION = (5.0 / 1024.0);
        private const int COUNT_CONVERSION = 268;
        private const double CONVERSION_FEET_TO_METER = 3.28084;
        private const float ZOOM_MAX = 0.060f;
        private const float ZOOM_MIN = 0.0005f;
        private DateTime timeOld = DateTime.Now,
                         timeNew = DateTime.Now,
                         timeOldS = DateTime.Now,
                         timeNewS = DateTime.Now;
        private TimeSpan timeDif;
        private double convFact = (5.0 / 512.0);     // Defaults to inches
        private SerialPort sPort;
        private volatile int sendCmdCount = 8;          // Number of commands to send with every heartbeat
        private volatile bool joystickActive = false;
        private volatile bool initialData = true;
        private volatile bool calibrating = false;
        private int packetSentCount = 0;
        private int packetRecvCount = 0;
        private int totalEncoderCnt = 0;

        private int calibrationCount = 0;
        private int maxCalibrations = 100;
        private List<double> listCalibrationValues;

        // Rover Variables
        private int curRoverPosX = 350,
                    curRoverPosY = 350,
                    newRoverPosX = 350,
                    newRoverPosY = 350,
                    driveDir = 0,
                    driveCnt = 0;
        private double accelX = 0.0,
                    accelY = 0.0,
                    accelZ = 0.0,
                    gyroX = 0.0,
                    gyroY = 0.0,
                    gyroZ = 0.0,
                    gyroZprev = 0.0;
        private double gyroZOffset = 0.46000;
        private double sonarLeft = 0.0,
                       sonarRight = 0.0,
                       sonarFront = 0.0;
        private int newRoverGyroPosX = 450,
                    newRoverGyroPosY = 450;
        private double roverGyroDir = 0;

        // Joystick Variables
        private int jsRangeUpper = 940,
                    jsRangeLower = -940,
                    jsCurrX = 0,
                    jsCurrY = 0,
                    jsTolX = 250,               // Tolerance for Turning
                    jsTolY = 250,               // Tolerance for Driving
                    jsScaleX = 75,
                    jsScaleY = 75;
        private char jsSignX = '+',
                     jsSignY = '+';
        private string jsCharX = "",
                       jsCharY = "";
        private bool jsNegX = false,
                     jsNegY = false;
        public Thread jsThread;
        private Joystick jStick;
        private DirectInput dInput;
        private JoystickState jsState;

        // Mapping Variables
        private const double SONAR_DISTANCE_MAX = 150;
        Bitmap mapBitmap;
        private List<Point> listRoverPoints;
        private List<Point> listRoverGyroPoints;
        private List<Point> listSonarPoints;
        private int mapShiftX = 0,
                    mapShiftY = 0,
                    prevMapShiftX = 0,
                    prevMapShiftY = 0,
                    prevMapScaleShiftX = 0,
                    prevMapScaleShiftY = 0;
        private int prevMouseX = 0,
                    prevMouseY = 0;
        private float mapZoom = 0.04f;
        private bool mouseDown = false;

        public MapperForm()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(pbMap_MouseWheel);
        }

        private void MapperForm_Load(object sender, EventArgs e)
        {
            sPort = new SerialPort();
            dInput = new DirectInput();
            jsThread = new Thread(new ThreadStart(this.UpdateJoystick));
            listRoverPoints = new List<Point>();
            listRoverGyroPoints = new List<Point>();
            listSonarPoints = new List<Point>();
            listCalibrationValues = new List<double>();
            btnStartStop.Enabled = false;

            this.WindowState = FormWindowState.Maximized;

            // Initial rover position on map
            newRoverPosX = (int)((pbMap.Width / 2) / mapZoom);
            newRoverPosY = (int)((pbMap.Height / 2) / mapZoom);

            // Initialize the bitmap for the PictureBox
            mapBitmap = new Bitmap(pbMap.Width, pbMap.Height);
            pbMap.Image = mapBitmap;

            try
            {
                btnAcquireJs_Click(sender, e);
                if (jStick != null)
                    btnAcquireJs.Enabled = false;
            }
            catch (Exception){}

            DrawMap();
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            try
            {
                sPort.PortName = "COM" + tbPort.Text;
                sPort.BaudRate = Convert.ToInt32(tbBaudRate.Text);
                sPort.Parity = Parity.None;
                sPort.StopBits = StopBits.One;
                sPort.DataBits = 8;
                sPort.Handshake = Handshake.None;
                sPort.DataReceived += new SerialDataReceivedEventHandler(sPort_DataReceived);
                sPort.ErrorReceived += new SerialErrorReceivedEventHandler(sPort_ErrorReceived);
                sPort.Open();
                rtbDataIn.AppendText("  **  Connected to  " + sPort.PortName + "  **\r\n");

                btnClosePort.Enabled = true;
                btnOpenPort.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to open COM port:\r\n" + ex.Message, "Port Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClosePort_Click(object sender, EventArgs e)
        {
            try
            {
                sPort.Close();
                rtbDataIn.Text += "   **  Disconnected from " + sPort.PortName + "  **   \r\n";
                sPort.Dispose();
                sPort = new SerialPort();
            }
            catch (Exception){}

            btnOpenPort.Enabled = true;
            btnClosePort.Enabled = false;
        }
        private void btnClearText_Click(object sender, EventArgs e)
        {
            rtbDataIn.Clear();
        }
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (btnStartStop.Text == "Start Driving")
            {
                joystickActive = true;
                if (jsThread.ThreadState == System.Threading.ThreadState.Aborted)
                    jsThread = new Thread(new ThreadStart(this.UpdateJoystick));
                jsThread.Start();
                btnStartStop.Text = "Stop Driving";
                calibrating = true;
            }
            else if (btnStartStop.Text == "Stop Driving")
            {
                joystickActive = false;
                jsThread.Abort();
                btnStartStop.Text = "Start Driving";
            }
        }
        private void btnAcquireJs_Click(object sender, EventArgs e)
        {
            // Search for device
            foreach (DeviceInstance device in dInput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                try
                {
                    jStick = new Joystick(dInput, device.InstanceGuid);
                    break;
                }
                catch (DirectInputException){}
            }

            if (jStick == null)
                MessageBox.Show("Failed to acquire joystick.", "Joystick Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                foreach (DeviceObjectInstance deviceObject in jStick.GetObjects())
                {
                    if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                        jStick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(jsRangeLower, jsRangeUpper);
                }
                jStick.Acquire();
                btnStartStop.Enabled = true;
            }
        }

        // Joystick Update Thread. Controls heart beat
        private void UpdateJoystick()
        {
            string sendCmd = "";

            try
            {
                while (joystickActive)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        jsState = jStick.GetCurrentState();
                        jsCurrY = (jsState.Y * -1);
                        jsCurrX = jsState.X;
                        jsNegX = (jsCurrX < 0);
                        jsNegY = (jsCurrY < 0);

                        jsCurrY = Math.Abs(jsCurrY);
                        jsCurrX = Math.Abs(jsCurrX);

                        if (jsCurrY < jsTolY)
                            jsCurrY = 0;
                        else
                            jsCurrY = (jsCurrY - jsTolY) / jsScaleY;

                        if (jsCurrX < jsTolX)
                            jsCurrX = 0;
                        else
                            jsCurrX = (jsCurrX - jsTolX) / jsScaleX;

                        jsCharY = jsCurrY.ToString("0;0;0");

                        if (jsNegY)
                        {
                            jsCurrY *= -1;
                            jsSignY = '-';
                        }
                        else
                            jsSignY = '+';

                        jsCharX = jsCurrX.ToString("0;0;0");

                        if (jsNegX)
                        {
                            jsCurrX *= -1;
                            jsSignX = '-';
                        }
                        else
                            jsSignX = '+';

                        // If we are calibrating the gyroscopes, don't send actual driving data
                        if (!calibrating)
                            sendCmd = "<d" + jsSignY + jsCharY + "t" + jsSignX + jsCharX + ">";
                        else
                            sendCmd = "<d+0t+0>";       // Forces the rover not to drive

                        tbSentCmd.Text = sendCmd;

                        SendData(sendCmd);
                        packetSentCount++;
                    });

                    Thread.Sleep(HEART_RATE);
                }
            }
            catch (ThreadAbortException){}
        }

        private void SendData(string pData)
        {
            try
            {
                string sendData = "";
                for (int i = 0; i < sendCmdCount; i++)
                    sendData += pData;
                sPort.Write(sendData);
            }
            catch (Exception) {}
        }

        // This event gets fired on a seperate thread.
        private void sPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                timeOld = timeNew;
                timeNew = DateTime.Now;
                timeDif = timeNew - timeOld;
                string data = sPort.ReadLine();
                var parts = data.Split('_');

                sonarFront = Convert.ToDouble(parts[0]) * SONAR_CONVERSION / convFact;
                sonarLeft = Convert.ToDouble(parts[1]) * SONAR_CONVERSION / convFact;
                sonarRight = Convert.ToDouble(parts[2]) * SONAR_CONVERSION / convFact;
                driveCnt = Convert.ToInt16(parts[3]);
                driveDir = Convert.ToInt16(parts[4]);
                accelX = Convert.ToInt16(parts[5]);
                accelY = Convert.ToInt16(parts[6]);
                accelZ = Convert.ToInt16(parts[7]);
                gyroX = Convert.ToInt16(parts[8]) / 131.0;
                gyroY = Convert.ToInt16(parts[9]) / 131.0;
                gyroZ = (Convert.ToInt16(parts[10]) / 131.0) * -1;

                // Assign initial value for gyroscope to initial compass heading
                if (initialData)
                {
                    roverGyroDir = driveDir;
                    initialData = false;
                    roverGyroDir = driveDir * -1;
                }

                // Calibrate the gyro offset value on startup
                if (calibrating)
                {
                    if (calibrationCount < maxCalibrations)
                    {
                        listCalibrationValues.Add(gyroZ);
                        calibrationCount++;
                    }
                    else
                    {
                        double calAverage = 0;
                        foreach (double p in listCalibrationValues)
                        {
                            calAverage += p;
                        }
                        calAverage /= maxCalibrations;
                        gyroZOffset = calAverage;
                        jsThread.Abort();                   // Stop communication with Rover for now
                        this.Invoke(new MethodInvoker(delegate
                        {
                            if (DialogResult.No == MessageBox.Show(this, "gyroZOffset = " + gyroZOffset + "\r\nAcceptable?",
                                "Calibration result", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                            {
                                string input = Interaction.InputBox("How many data poitns?", "Calibration", "10", 0, 0);
                                maxCalibrations = Convert.ToInt32(input);
                                listCalibrationValues.Clear();
                                calAverage = 0.0;
                                gyroZOffset = 0.0;
                                calibrationCount = 0;
                                calibrating = true;
                                jsThread = new Thread(this.UpdateJoystick);
                                jsThread.Start();
                            }
                            else
                            {
                                calibrating = false;
                                jsThread = new Thread(this.UpdateJoystick);
                                jsThread.Start();                   // Calibration is accepted, continue communciation
                            }
                        }));
                    }
                }
                else
                {
                    gyroZ -= gyroZOffset;       // Subtract out the noise
                    gyroZ = Math.Round(gyroZ, 5);       // Limit number of digits we're considering
                }

                // Adjust gyroZ value based on the time between heartbeats from the rover
                gyroZ *= ((double)(timeDif.Milliseconds) / 1000);

                // Throw out the bugged data from gyro if recieved
                if (Math.Abs(gyroZ) >= 3.0 && jsCurrX == 0)     // Should not be greater than 3 during normal use
                {
                    gyroZ = gyroZprev;
                    this.BeginInvoke(new MethodInvoker(delegate()
                    { rtbDataIn.AppendText("  GyroZ = " + gyroZ.ToString() + "\r\n"); }));
                }
                else
                {
                    gyroZprev = gyroZ;
                }

                // Update rover heading
                roverGyroDir += gyroZ;

                // Update total counts
                totalEncoderCnt += driveCnt;

                this.BeginInvoke(new MethodInvoker(delegate()       // Make the changes to the GUI
                {
                    tbSensorFront.Text = sonarFront.ToString("F2");
                    tbSensorLeft.Text = sonarLeft.ToString("F2");
                    tbSensorRight.Text = sonarRight.ToString("F2");
                    encoderDelta.Text = driveCnt.ToString();
                    tbTotalEncCnt.Text = totalEncoderCnt.ToString();
                    compassDirection.Text = roverGyroDir.ToString();
                    accelBoxX.Text = accelX.ToString();
                    accelBoxY.Text = accelY.ToString();
                    accelBoxZ.Text = accelZ.ToString();
                    gyroBoxX.Text = gyroX.ToString();
                    gyroBoxY.Text = gyroY.ToString();
                    gyroBoxZ.Text = gyroZ.ToString();

                    UpdateMapPoints(roverGyroDir, driveCnt, sonarLeft, sonarRight, sonarFront);
                }));
                packetRecvCount++;
            }
            catch (IndexOutOfRangeException){}
        }

        private void UpdateMapPoints(double rDir, int pCounts, double sLeft, double sRight, double sFront)
        {
            int tmpSonarX = 0,
                tmpSonarY = 0;

            // Calculate the Rover postion based on Compass data
            //newRoverPosX += (int)(Math.Cos(pDir * Math.PI / 180) * pCounts);
            //newRoverPosY += (int)(Math.Sin(pDir * Math.PI / 180) * pCounts);
            //listRoverPoints.Add(new Point(newRoverPosX, newRoverPosY));

            // Calculate the Rover position point basd on gyro data
            newRoverGyroPosX += (int)(Math.Cos(rDir * Math.PI / 180) * pCounts);
            newRoverGyroPosY += (int)(Math.Sin(rDir * Math.PI / 180) * pCounts);
            listRoverGyroPoints.Add(new Point(newRoverGyroPosX, newRoverGyroPosY));

            // Calculate the sonar points
            if (sonarLeft < SONAR_DISTANCE_MAX && jsCurrY != 0)     // Draw point if closer than max distance and not moving
            {
                tmpSonarX = newRoverGyroPosX + ((int)(Math.Cos((rDir - 90) * Math.PI / 180) * sLeft * COUNT_CONVERSION));
                tmpSonarY = newRoverGyroPosY + ((int)(Math.Sin((rDir - 90) * Math.PI / 180) * sLeft * COUNT_CONVERSION));
                listSonarPoints.Add(new Point(tmpSonarX, tmpSonarY));
            }
            if (sonarRight < SONAR_DISTANCE_MAX && jsCurrY != 0)    // Draw point if closer than max distance and not moving
            {
                tmpSonarX = newRoverGyroPosX + ((int)(Math.Cos((rDir + 90) * Math.PI / 180) * sRight * COUNT_CONVERSION));
                tmpSonarY = newRoverGyroPosY + ((int)(Math.Sin((rDir + 90) * Math.PI / 180) * sRight * COUNT_CONVERSION));
                listSonarPoints.Add(new Point(tmpSonarX, tmpSonarY));
            }
            
            DrawMap();

            curRoverPosX = newRoverPosX;
            curRoverPosY = newRoverPosY;
        }

        private void DrawMap()
        {
            try
            {
                mapBitmap = new Bitmap(pbMap.Width, pbMap.Height);
                using (Graphics g = Graphics.FromImage(mapBitmap))
                {
                    // Draw the COMPASS position
                /*  
                 *   for (int i = 0; i < (listRoverPoints.Count - 1); i++)
                 *   {
                 *       //g.DrawLine(new Pen(Color.Black, (int)(5 * mapZoom)),
                 *       //    (listRoverPoints[i].X + mapShiftX) * mapZoom,
                 *       //    (listRoverPoints[i].Y + mapShiftY) * mapZoo
                 *       //    (listRoverPoints[i + 1].X + mapShiftX) * mapZoom,
                 *       //    (listRoverPoints[i + 1].Y + mapShiftY) * mapZoom);
                 *       g.FillRectangle(Brushes.Black,
                 *           (listRoverPoints[i].X + mapShiftX) * mapZoom,
                 *           (listRoverPoints[i].Y + mapShiftY) * mapZoom,
                 *           2, 2);
                 *   }
                */
                    
                    // Draw the GYRO position
                    for (int i = 0; i < (listRoverGyroPoints.Count - 1); i++)
                    {
                        g.FillRectangle(Brushes.Blue,
                            (listRoverGyroPoints[i].X + mapShiftX) * mapZoom,
                            (listRoverGyroPoints[i].Y + mapShiftY) * mapZoom,
                            2, 2);
                    }

                    // Draw the SONAR points
                    for (int i = 0; i < (listSonarPoints.Count - 1); i++)
                    {
                        g.FillRectangle(Brushes.Black,
                            (listSonarPoints[i].X + mapShiftX) * mapZoom,
                            (listSonarPoints[i].Y + mapShiftY) * mapZoom,
                            2, 2);
                    }

                    // Draw the scale legend
                    g.DrawLine(new Pen(Color.Brown, 2),
                        (pbMap.Width - 35),
                        (pbMap.Height - 30),
                        (pbMap.Width - 35) - (int)((double)COUNT_CONVERSION * (double)mapZoom * 12.0 * CONVERSION_FEET_TO_METER),
                        (pbMap.Height - 30));
                    g.DrawLine(new Pen(Color.Brown, 2),
                        (pbMap.Width - 35),
                        (pbMap.Height - 35),
                        (pbMap.Width - 35),
                        (pbMap.Height - 25));
                    g.DrawLine(new Pen(Color.Brown, 2),
                        (pbMap.Width - 35) - (int)((double)COUNT_CONVERSION * (double)mapZoom * 12.0 * CONVERSION_FEET_TO_METER),
                        (pbMap.Height - 35),
                        (pbMap.Width - 35) - (int)((double)COUNT_CONVERSION * (double)mapZoom * 12.0 * CONVERSION_FEET_TO_METER),
                        (pbMap.Height - 25));
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.DrawString("1 Meter", new Font("Arial", 12.0f, FontStyle.Bold),
                        Brushes.Brown, pbMap.Width - 95, pbMap.Height - 23);
                }
                pbMap.Image = mapBitmap;        // Update screen image
            }
            catch (Exception) { }
        }

        private void MapperForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sPort.IsOpen)
                sPort.Close();
            try
            {
                jsThread.Abort();
            }
            catch (Exception){}
        }
        private void sPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(delegate()
            { rtbDataIn.AppendText("  ***  ERROR RECEIVED " + e.ToString() + "  ***\r\n"); }));
        }
        private void rtbDataIn_TextChanged(object sender, EventArgs e)
        {
            rtbDataIn.SelectionStart = rtbDataIn.Text.Length;   // Set the current caret position to the end
            rtbDataIn.ScrollToCaret();      // Now scroll it automatically
        }

        private void pbMap_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            prevMouseX = (int)(e.X / mapZoom);
            prevMouseY = (int)(e.Y / mapZoom);
            Cursor = Cursors.SizeAll;
        }
        private void pbMap_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            prevMapShiftX = mapShiftX;
            prevMapShiftY = mapShiftY;
            Cursor = Cursors.Default;
        }
        private void pbMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                mapShiftX = (int)((prevMapShiftX - (prevMouseX - (e.X / mapZoom))));
                mapShiftY = (int)((prevMapShiftY - (prevMouseY - (e.Y / mapZoom))));
                DrawMap();
            }
        }
        private void pbMap_MouseWheel(object sender, MouseEventArgs e)
        {
            // Re-centers the map after adjusting the scale
            prevMapScaleShiftX = (int)(((pbMap.Width / 2) - ((pbMap.Width / 2) / mapZoom)));
            prevMapScaleShiftY = (int)(((pbMap.Height / 2) - ((pbMap.Height / 2) / mapZoom)));
            if (e.Delta > 0)
            {
                mapZoom *= 1.25F;
                if (mapZoom > ZOOM_MAX)
                    mapZoom = ZOOM_MAX;
                mapShiftX += (-1 * (int)(((pbMap.Width / 2) - ((pbMap.Width / 2) / mapZoom)))) + prevMapScaleShiftX;
                mapShiftY += (-1 * (int)(((pbMap.Height / 2) - ((pbMap.Height / 2) / mapZoom)))) + prevMapScaleShiftY;
            }
            else
            {
                mapZoom /= 1.25F;
                if (mapZoom < ZOOM_MIN)
                    mapZoom = ZOOM_MIN;
                mapShiftX -= (int)(((pbMap.Width / 2) - ((pbMap.Width / 2) / mapZoom))) - prevMapScaleShiftX;
                mapShiftY -= (int)(((pbMap.Height / 2) - ((pbMap.Height / 2) / mapZoom))) - prevMapScaleShiftY;
            }

            prevMapShiftX = mapShiftX;
            prevMapShiftY = mapShiftY;
            DrawMap();
        }
        private void pbMap_MouseEnter(object sender, EventArgs e)
        {
            pbMap.Focus();
        }
        private void pbMap_MouseLeave(object sender, EventArgs e)
        {
            pbMap.Parent.Focus();
        }

        private void MapperForm_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void MapperForm_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void btnClearMap_Click(object sender, EventArgs e)
        {
            listRoverGyroPoints.Clear();
            listRoverPoints.Clear();
            listSonarPoints.Clear();
            Bitmap tmpImg = new Bitmap(pbMap.Width, pbMap.Height);
            pbMap.Image = tmpImg;
        }

        private void btnSaveMap_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Bitmap Image|*.bmp";
            saveDialog.Title = "Save an Image File";
            saveDialog.FileName = "Map.bmp";
            if (saveDialog.ShowDialog() == DialogResult.OK)
                pbMap.Image.Save(saveDialog.FileName);
        }

        private void MapperForm_Resize(object sender, EventArgs e)
        {
            DrawMap();
        }
    }
}
