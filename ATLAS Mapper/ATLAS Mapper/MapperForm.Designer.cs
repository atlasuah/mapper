namespace ATLAS_Mapper
{
    partial class MapperForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBaudRate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbParity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDataBits = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbStopBits = new System.Windows.Forms.TextBox();
            this.sPort = new System.IO.Ports.SerialPort(this.components);
            this.rtbDataIn = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(104, 26);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(38, 20);
            this.tbPort.TabIndex = 0;
            this.tbPort.Text = "1";
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(63, 165);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPort.TabIndex = 1;
            this.btnOpenPort.Text = "Open Port";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Serial Data:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Baud Rate:";
            // 
            // tbBaudRate
            // 
            this.tbBaudRate.Location = new System.Drawing.Point(104, 52);
            this.tbBaudRate.Name = "tbBaudRate";
            this.tbBaudRate.Size = new System.Drawing.Size(67, 20);
            this.tbBaudRate.TabIndex = 5;
            this.tbBaudRate.Text = "9600";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Parity:";
            // 
            // tbParity
            // 
            this.tbParity.Enabled = false;
            this.tbParity.Location = new System.Drawing.Point(104, 78);
            this.tbParity.Name = "tbParity";
            this.tbParity.Size = new System.Drawing.Size(48, 20);
            this.tbParity.TabIndex = 7;
            this.tbParity.Text = "None";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Data Bits:";
            // 
            // tbDataBits
            // 
            this.tbDataBits.Location = new System.Drawing.Point(104, 104);
            this.tbDataBits.Name = "tbDataBits";
            this.tbDataBits.Size = new System.Drawing.Size(38, 20);
            this.tbDataBits.TabIndex = 9;
            this.tbDataBits.Text = "8";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Stop Bits:";
            // 
            // tbStopBits
            // 
            this.tbStopBits.Enabled = false;
            this.tbStopBits.Location = new System.Drawing.Point(104, 130);
            this.tbStopBits.Name = "tbStopBits";
            this.tbStopBits.Size = new System.Drawing.Size(38, 20);
            this.tbStopBits.TabIndex = 11;
            this.tbStopBits.Text = "1";
            // 
            // sPort
            // 
            this.sPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.sPort_DataReceived);
            // 
            // rtbDataIn
            // 
            this.rtbDataIn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDataIn.Location = new System.Drawing.Point(316, 10);
            this.rtbDataIn.Name = "rtbDataIn";
            this.rtbDataIn.Size = new System.Drawing.Size(239, 326);
            this.rtbDataIn.TabIndex = 13;
            this.rtbDataIn.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbPort);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnOpenPort);
            this.groupBox1.Controls.Add(this.tbStopBits);
            this.groupBox1.Controls.Add(this.tbBaudRate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbDataBits);
            this.groupBox1.Controls.Add(this.tbParity);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 205);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Port";
            // 
            // MapperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 348);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtbDataIn);
            this.Controls.Add(this.label2);
            this.Name = "MapperForm";
            this.Text = "ATLAS Mapper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapperForm_FormClosing);
            this.Load += new System.EventHandler(this.MapperForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBaudRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbParity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDataBits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbStopBits;
        private System.IO.Ports.SerialPort sPort;
        private System.Windows.Forms.RichTextBox rtbDataIn;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

