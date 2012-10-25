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
        SerialPort sPort;

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
                sPort.DataReceived += new SerialDataReceivedEventHandler(realSP_DataReceived);
                sPort.ErrorReceived += new SerialErrorReceivedEventHandler(realSP_ErrorReceived);
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
                sPort.Dispose();
                sPort = new SerialPort();
                rtbDataIn.Text += "   **  Disconnected from " + sPort.PortName + "  **   \r\n";
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

        // This event gets fired on a seperate thread.
        private void realSP_DataReceived(object sender, SerialDataReceivedEventArgs e)
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
        private void realSP_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            this.BeginInvoke(new MethodInvoker(delegate()
                { rtbDataIn.AppendText("  ***  ERROR RECEIVED " + e.ToString() + "  ***\r\n"); }));
        }

        private void MapperForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sPort.IsOpen)
                sPort.Close();
        }
    }
}
