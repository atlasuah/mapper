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
        bool keyWHandled = false, keySHandled = false,
            keyAHandled = false, keyDHandled = false;
        bool acceptKeys = false;
        double convFact = 57.89;     // Defaults to cm
        private SerialPort sPort;
        private volatile string driveDirection;
        private volatile string turnDirection;
        private volatile bool joystickActive = false;
        private int jsRangeUpper = 1000,
                    jsRangeLower = -1000,
                    jsUpdateDelay = 20,
                    jsPrevX = 0,
                    jsPrevY = 0,
                    jsCurrX = 0,
                    jsCurrY = 0,
                    jsTolX = 500,               // Tolerance for Turning
                    jsTolY = 500,               // Tolerance for Driving
                    jsScaleX = 100,
                    jsScaleY = 100;
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
            driveDirection = "Halted";
            turnDirection = "Straight";
            btnStartStop.Enabled = false;
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
                MessageBox.Show("Joystick Error", "Failed to acquire joystick.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            foreach (DeviceObjectInstance deviceObject in jStick.GetObjects())
            {
                if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                    jStick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(jsRangeLower, jsRangeUpper);
            }
            jStick.Acquire();
            btnStartStop.Enabled = true;
        }

        // Joystick Update Thread
        private void UpdateJoystick()
        {
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

                        if (jsNegY)
                            jsCurrY *= -1;
                        if (jsNegX)
                            jsCurrX *= -1;

                        if (jsCurrY != jsPrevY)
                        {
                            sPort.Write("d" + jsCurrY);
                            tbDrive.Text = "d" + jsCurrY;
                        }
                        if (jsCurrX != jsPrevX)
                        {
                            sPort.Write("t" + jsCurrY);
                            tbTurn.Text = "t" + jsCurrX;
                        }

                        jsPrevX = jsCurrX;
                        jsPrevY = jsCurrY;
                    });
                    
                    Thread.Sleep(jsUpdateDelay);
                }
            }
            catch (ThreadAbortException){}
        }

        // This event gets fired on a seperate thread.
        private void sPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = sPort.ReadLine();
                
                switch (data[0])
                {
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
                    case 'd':
                        if (data[1] == 'f')
                            driveDirection = "Forward";
                        else if (data[1] == 'b')
                            driveDirection = "Reverse";
                        else if (data[1] == 'h')
                            driveDirection = "Halted";
                        this.BeginInvoke(new MethodInvoker(delegate()
                            { tbDirection.Text = turnDirection + " " + driveDirection; }));
                        break;
                    case 't':
                        if (data[1] == 'l')
                            turnDirection = "Left";
                        else if (data[1] == 'r')
                            turnDirection = "Right";
                        else if (data[1] == 's')
                            turnDirection = "Straight";
                        this.BeginInvoke(new MethodInvoker(delegate()
                            { tbDirection.Text = turnDirection + " " + driveDirection; }));
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

        // This event gets fired on a seperate thread.
        private void sPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(delegate()
                { rtbDataIn.AppendText("  ***  ERROR RECEIVED " + e.ToString() + "  ***\r\n"); }));
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

        private void MapperForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (acceptKeys && sPort.IsOpen)
            {
                switch (e.KeyData)
                {
                    case Keys.W:
                        if (!keyWHandled)
                        {
                            sPort.Write("f");           // Forward
                            keyWHandled = true;
                        }
                        break;
                    case Keys.S:
                        if (!keySHandled)
                        {
                            sPort.Write("b");           // Back
                            keySHandled = true;
                        }
                        break;
                    case Keys.A:
                        if (!keyAHandled)
                        {
                            sPort.Write("l");           // Left
                            keyAHandled = true;
                        }
                        break;
                    case Keys.D:
                        if (!keyDHandled)
                        {
                            sPort.Write("r");           // Right
                            keyDHandled = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private void MapperForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (acceptKeys && sPort.IsOpen)
            {
                switch (e.KeyData)
                {
                    case Keys.W:
                        keyWHandled = false;
                        sPort.Write("h");           // Halt
                        break;
                    case Keys.S:
                        keySHandled = false;
                        sPort.Write("h");           // Halt
                        break;
                    case Keys.A:
                        keyAHandled = false;
                        sPort.Write("s");           // Straight
                        break;
                    case Keys.D:
                        keyDHandled = false;
                        sPort.Write("s");           // Straight
                        break;
                    default:
                        break;
                }
            }
        }

        private void rtbDataIn_TextChanged(object sender, EventArgs e)
        {
            rtbDataIn.SelectionStart = rtbDataIn.Text.Length; //Set the current caret position to the end
            rtbDataIn.ScrollToCaret(); //Now scroll it automatically
        }
    }
}
