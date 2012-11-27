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

namespace ATLAS_Mapper
{
    public partial class MapperForm : Form
    {
        bool keyWHandled = false, keySHandled = false,
            keyAHandled = false, keyDHandled = false;
        bool acceptKeys = false;
        private SerialPort sPort;
        private volatile string driveDirection;
        private volatile string turnDirection;

        public MapperForm()
        {
            InitializeComponent();
        }

        private void MapperForm_Load(object sender, EventArgs e)
        {
            sPort = new SerialPort();
            driveDirection = "Halted";
            turnDirection = "Straight";
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
                acceptKeys = true;
                btnStartStop.Text = "Stop Driving";
            }
            else if (btnStartStop.Text == "Stop Driving")
            {
                acceptKeys = false;
                btnStartStop.Text = "Start Driving";
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
                                { tbSensorFront.Text = (Convert.ToDouble(data.Substring(2)) / 57.89).ToString(); }));
                        else if (data[1] == 'l')
                            this.BeginInvoke(new MethodInvoker(delegate()
                                { tbSensorLeft.Text = (Convert.ToDouble(data.Substring(2)) / 57.89).ToString(); }));
                        else if (data[1] == 'r')
                            this.BeginInvoke(new MethodInvoker(delegate()
                                { tbSensorRight.Text = (Convert.ToDouble(data.Substring(2)) / 57.89).ToString(); }));
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
    }
}
