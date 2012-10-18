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
        SerialPort sPort;

        public MapperForm()
        {
            InitializeComponent();
        }

        private void MapperForm_Load(object sender, EventArgs e)
        {
            sPort = new SerialPort();
            sPort.ReadTimeout = 500;
            sPort.WriteTimeout = 500;
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {

        }
    }
}
