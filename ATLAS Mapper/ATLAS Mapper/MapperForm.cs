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

namespace ATLAS_Mapper
{
    public partial class MapperForm : Form
    {
        double convFact = 57.89;     // Defaults to cm
        private SerialPort sPort;
        private volatile int sendCmdCount = 8;
        private volatile bool joystickActive = false;
        private int curRoverPosX = 200,
                    curRoverPosY = 200,
                    newRoverPosX = 200,
                    newRoverPosY = 200,
                    driveDir = 0,
                    driveCnt = 0;
        private int mapScale = 10;         // Larger number = smaller scale
        private int jsRangeUpper = 940,
                    jsRangeLower = -940,
                    jsUpdateDelay = 30,
                    jsCurrX = 0,
                    jsCurrY = 0,
                    jsTolX = 250,               // Tolerance for Turning
                    jsTolY = 250,               // Tolerance for Driving
                    jsScaleX = 75,
                    jsScaleY = 100;
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

        public MapperForm()
        {
            InitializeComponent();
        }

        private void MapperForm_Load(object sender, EventArgs e)
        {
            sPort = new SerialPort();
            dInput = new DirectInput();
            jsThread = new Thread(new ThreadStart(this.UpdateJoystick));
            btnStartStop.Enabled = false;

            try
            {
                btnAcquireJs_Click(sender, e);
                if (jStick != null)
                    btnAcquireJs.Enabled = false;
            }
            catch (Exception){}
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
                MessageBox.Show(this, "Unable to open COM port:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception)
            {
                // prevent crash
            }

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
                //acceptKeys = true;
                joystickActive = true;
                if (jsThread.ThreadState == System.Threading.ThreadState.Aborted)
                    jsThread = new Thread(new ThreadStart(this.UpdateJoystick));
                jsThread.Start();
                btnStartStop.Text = "Stop Driving";
            }
            else if (btnStartStop.Text == "Stop Driving")
            {
                //acceptKeys = false;
                joystickActive = false;
                jsThread.Abort();
                btnStartStop.Text = "Start Driving";
            }
        }
        private void btnUnits_Click(object sender, EventArgs e)
        {
            if (btnUnits.Text == "cm")
            {
                convFact = 147.04;
                btnUnits.Text = "inches";
            }
            else if (btnUnits.Text == "inches")
            {
                convFact = 57.89;
                btnUnits.Text = "cm";
            }
        }
        private void btnRequestUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                sPort.Write("u");
            }
            catch (Exception)
            {
                // prevent crash
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

        // Joystick Update Thread
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

                        sendCmd = "<d" + jsSignY + jsCharY + "t" + jsSignX + jsCharX + ">";
                        tbSentCmd.Text = sendCmd;
                        SendData(sendCmd);

                        //tbDrive.Text = "d" + jsCurrY.ToString("+0;-0;0");
                        //tbTurn.Text = "t" + jsCurrX.ToString("+0;-0;0");
                    });
                    
                    Thread.Sleep(jsUpdateDelay);
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
                
                switch (data[0])
                {
                    case 'd':
                        this.BeginInvoke(new MethodInvoker(delegate()
                            {
                                driveDir = Convert.ToInt16(data.Substring(1));
                                driveCnt = Convert.ToInt16(sPort.ReadLine());
                                UpdateMap(driveDir, driveCnt);
                            }));
                        break;
                    case 's':
                        if (data[1] == 'f')
                            this.BeginInvoke(new MethodInvoker(delegate()
                                { tbSensorFront.Text = (Double.Parse(data.Substring(2)) / convFact).ToString("F2"); }));
                        else if (data[1] == 'l')
                            this.BeginInvoke(new MethodInvoker(delegate()
                                { tbSensorLeft.Text = (Double.Parse(data.Substring(2)) / convFact).ToString("F2"); }));
                        else if (data[1] == 'r')
                            this.BeginInvoke(new MethodInvoker(delegate()
                                { tbSensorRight.Text = (Double.Parse(data.Substring(2)) / convFact).ToString("F2"); }));
                        break;
                    default:
                        this.BeginInvoke(new MethodInvoker(delegate()
                            { rtbDataIn.AppendText(data); }));
                        break;
                }
                
            }
            catch (Exception)
            {
                // prevent crash
            }
        }

        private void UpdateMap(int pDir, int pCounts)
        {
            newRoverPosX += (int)(Math.Cos(pDir) * pCounts / mapScale);
            newRoverPosY += (int)(Math.Sin(pDir) * pCounts / mapScale);

            if (pbMap.Image == null)
            {
                Bitmap bmp = new Bitmap(pbMap.Width, pbMap.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                }
                pbMap.Image = bmp;
            }
            using (Graphics g = Graphics.FromImage(pbMap.Image))
            {
                g.DrawLine(Pens.Black, curRoverPosX, curRoverPosY, newRoverPosX, newRoverPosY);
                //rtbDataIn.AppendText("driveDir: " + pDir + "\r\ndriveCnt: " + pCounts + "\r\n");
                //rtbDataIn.AppendText("newX: " + newRoverPosX + "\r\nnewY: " + newRoverPosY + "\r\n");
            }

            curRoverPosX = newRoverPosX;
            curRoverPosY = newRoverPosY;
            pbMap.Invalidate();
        }

        private void MapperForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sPort.IsOpen)
                sPort.Close();
            try
            {
                jsThread.Abort();
            }
            catch (Exception) { }
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
    }
}
