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
        bool keyWHandled = false, keySHandled = false, keyAHandled = false, keyDHandled = false;
        bool acceptKeys = false;
        private SerialPort sPort;

        public MapperForm()
        {
            InitializeComponent();
        }

        private void MapperForm_Load(object sender, EventArgs e)
        {
            sPort = new SerialPort();
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
            if (btnStartStop.Text == "Start")
            {
                acceptKeys = true;
                btnStartStop.Text = "Stop";
            }
            else if (btnStartStop.Text == "Stop")
            {
                acceptKeys = false;
                btnStartStop.Text = "Start";
            }
        }

        // This event gets fired on a seperate thread.
        private void sPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = sPort.ReadLine();
                this.BeginInvoke(new MethodInvoker(delegate()
                    { rtbDataIn.AppendText(data); }));
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
                            sPort.Write("F");           // Forward
                            keyWHandled = true;
                        }
                        break;
                    case Keys.S:
                        if (!keySHandled)
                        {
                            sPort.Write("B");           // Back
                            keySHandled = true;
                        }
                        break;
                    case Keys.A:
                        if (!keyAHandled)
                        {
                            sPort.Write("L");           // Left
                            keyAHandled = true;
                        }
                        break;
                    case Keys.D:
                        if (!keyDHandled)
                        {
                            sPort.Write("R");           // Right
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
                        sPort.Write("H");           // Halt
                        break;
                    case Keys.S:
                        keySHandled = false;
                        sPort.Write("H");           // Halt
                        break;
                    case Keys.A:
                        keyAHandled = false;
                        sPort.Write("S");           // Straight
                        break;
                    case Keys.D:
                        keyDHandled = false;
                        sPort.Write("S");           // Straight
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
