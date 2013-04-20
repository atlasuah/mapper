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
        private const int HEART_RATE = 60;
        private const double GYRO_OFFSET_Z = 0.013;
        private const double SONAR_CONVERSION = (5.0 / 1024.0);
        private const int COUNT_CONVERSION = 268;
        private const double CONVERSION_FEET_TO_METER = 3.28084;
        private const float ZOOM_MAX = 0.060f;
        private const float ZOOM_MIN = 0.001f;

        private double convFact = (5.0 / 512.0);     // Defaults to inches
        private SerialPort sPort;
        private volatile int sendCmdCount = 8;          // Number of commands to send with every heartbeat
        private volatile bool joystickActive = false;
        private volatile bool initialData = true;
        private volatile bool calibrating = false;
        private int packetDropCount = 0;
        private int packetSentCount = 0;
        private int packetRecvCount = 0;
        private int totalEncoderCnt = 0;

        private int calibrationCount = 0;
        private int maxCalibrations = 5;
        private List<double> listCalibrationValues;

        // Rover Variables
        private int curRoverPosX = 200,
                    curRoverPosY = 200,
                    newRoverPosX = 200,
                    newRoverPosY = 200,
                    driveDir = 0,
                    driveCnt = 0;
        private double accelX = 0.0,
                    accelY = 0.0,
                    accelZ = 0.0,
                    gyroX = 0.0,
                    gyroY = 0.0,
                    gyroZ = 0.0,
                    gyroZprev = 0.0;
        private double gyroZOffset = 0.0;
        private double sonarLeft = 0.0,
                       sonarRight = 0.0,
                       sonarFront = 0.0;
        private int prevDriveDir = 0;
        private int curRoverGyroPosX = 200,
                    curRoverGyroPosY = 200,
                    newRoverGyroPosX = 200,
                    newRoverGyroPosY = 200;
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
        private int mapScale = 1;         // Larger number = smaller map
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
        private void btnUnits_Click(object sender, EventArgs e)
        {
            if (btnUnits.Text == "cm")
            {
                //convFact = 147.04;
                btnUnits.Text = "inches";
            }
            else if (btnUnits.Text == "inches")
            {
                //convFact = 57.89;
                btnUnits.Text = "cm";
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
                            sendCmd = "<d+0t+0>";

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

                //else
                //    gyroZ = 0.0;
                // Assign initial value for gyroscope to initial compass heading
                if (initialData)
                {
                    roverGyroDir = driveDir;
                    initialData = false;
                    roverGyroDir = driveDir * -1;
                }

                // Adjust gyro values based on HEART_BEAT.
                gyroZ *= ((double)(HEART_RATE) / 1000);

                // Calibrate the gyro offset value
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
                    //gyroZ += GYRO_OFFSET_Z;
                    gyroZ -= gyroZOffset;
                    gyroZ = Math.Round(gyroZ, 5);
                }

                // Throw out the bugged data from gyro if recieved
                if (Math.Abs(gyroZ) >= 3.0)
                {
                    gyroZ = gyroZprev;
                    this.BeginInvoke(new MethodInvoker(delegate()
                    { rtbDataIn.AppendText("  GyroZ = " + gyroZ.ToString() + "\r\n"); }));
                }
                else
                {
                    gyroZprev = gyroZ;
                }
                roverGyroDir += gyroZ;

                // Update total counts
                totalEncoderCnt += driveCnt;

                this.BeginInvoke(new MethodInvoker(delegate()
                {
                    tbSensorFront.Text = sonarFront.ToString("F2");
                    tbSensorLeft.Text = sonarLeft.ToString("F2");
                    tbSensorRight.Text = sonarRight.ToString("F2");
                    encoderDelta.Text = driveCnt.ToString();
                    tbTotalEncCnt.Text = totalEncoderCnt.ToString();
                    compassDirection.Text = (driveDir * -1).ToString();
                    accelBoxX.Text = accelX.ToString();
                    accelBoxY.Text = accelY.ToString();
                    accelBoxZ.Text = accelZ.ToString();
                    gyroBoxX.Text = gyroX.ToString();
                    gyroBoxY.Text = gyroY.ToString();
                    gyroBoxZ.Text = gyroZ.ToString();

                    UpdateMap(driveDir, driveCnt);
                }));
                packetRecvCount++;
            }
            catch (IndexOutOfRangeException){}
        }

        private void UpdateMap(int rDir, int pCounts)
        {
            int pDir = rDir * -1;
            int tmpSonarX = 0,
                tmpSonarY = 0;

            newRoverPosX += (int)(Math.Cos(pDir * Math.PI / 180) * pCounts / mapScale);
            newRoverPosY += (int)(Math.Sin(pDir * Math.PI / 180) * pCounts / mapScale);

            newRoverGyroPosX += (int)(Math.Cos(roverGyroDir * Math.PI / 180) * pCounts / mapScale);
            newRoverGyroPosY += (int)(Math.Sin(roverGyroDir * Math.PI / 180) * pCounts / mapScale);

            listRoverPoints.Add(new Point(newRoverPosX, newRoverPosY));
            listRoverGyroPoints.Add(new Point(newRoverGyroPosX, newRoverGyroPosY));

            // Calculate te sonar points
            tmpSonarX = newRoverPosX + ((int)(Math.Cos((roverGyroDir + 90) * Math.PI / 180) * sonarLeft * COUNT_CONVERSION / mapScale));
            tmpSonarY = newRoverPosY + ((int)(Math.Sin((roverGyroDir + 90) * Math.PI / 180) * sonarLeft * COUNT_CONVERSION / mapScale));
            listSonarPoints.Add(new Point(tmpSonarX, tmpSonarY));
            tmpSonarX = newRoverPosX + ((int)(Math.Cos((roverGyroDir - 90) * Math.PI / 180) * sonarRight * COUNT_CONVERSION / mapScale));
            tmpSonarY = newRoverPosY + ((int)(Math.Sin((roverGyroDir - 90) * Math.PI / 180) * sonarRight * COUNT_CONVERSION / mapScale));
            listSonarPoints.Add(new Point(tmpSonarX, tmpSonarY));
            tmpSonarX = newRoverPosX + ((int)(Math.Cos((roverGyroDir) * Math.PI / 180) * sonarFront * COUNT_CONVERSION / mapScale));
            tmpSonarY = newRoverPosY + ((int)(Math.Sin((roverGyroDir) * Math.PI / 180) * sonarFront * COUNT_CONVERSION / mapScale));
            //listSonarPoints.Add(new Point(tmpSonarX, tmpSonarY));
            
            DrawMap();

            curRoverPosX = newRoverPosX;
            curRoverPosY = newRoverPosY;
            prevDriveDir = pDir;
        }

        private void DrawMap()
        {
            try
            {
                mapBitmap = new Bitmap(pbMap.Width, pbMap.Height);
                using (Graphics g = Graphics.FromImage(mapBitmap))
                {
                    //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    // Draw the COMPASS position
                /*       for (int i = 0; i < (listRoverPoints.Count - 1); i++)
                    {
                        //g.DrawLine(new Pen(Color.Black, (int)(5 * mapZoom)),
                        //    (listRoverPoints[i].X + mapShiftX) * mapZoom,
                        //    (listRoverPoints[i].Y + mapShiftY) * mapZoo
                        //    (listRoverPoints[i + 1].X + mapShiftX) * mapZoom,
                        //    (listRoverPoints[i + 1].Y + mapShiftY) * mapZoom);
                        g.FillRectangle(Brushes.Black,
                            (listRoverPoints[i].X + mapShiftX) * mapZoom,
                            (listRoverPoints[i].Y + mapShiftY) * mapZoom,
                            2, 2);
                    }
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
                pbMap.Image = mapBitmap;
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
            rtbDataIn.SelectionStart = rtbDataIn.Text.Length; //Set the current caret position to the end
            rtbDataIn.ScrollToCaret(); //Now scroll it automatically
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
