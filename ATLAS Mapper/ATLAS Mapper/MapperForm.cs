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

namespace ATLAS_Mapper
{
    public partial class MapperForm : Form
    {
        string inData;

        public MapperForm()
        {
            InitializeComponent();
        }

        private void MapperForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            try
            {
                sPort = new SerialPort();
                sPort.ReadTimeout = 500;
                sPort.WriteTimeout = 500;
                sPort.BaudRate = Convert.ToInt32(tbBaudRate.Text);
                sPort.DataBits = Convert.ToInt32(tbDataBits.Text);
                sPort.StopBits = StopBits.One;
                sPort.Parity = Parity.None;
                sPort.PortName = "COM" + tbPort.Text;

                sPort.Open();
                if (sPort.IsOpen)
                    rtbDataIn.Text = "   **  Connected to COM " + tbPort.Text + "  **   ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Unable to open COM port:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            inData = sPort.ReadLine();
            rtbDataIn.Text += inData;
        }

        private void MapperForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            sPort.Close();
        }
    }
}
