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
            this.rtbDataIn = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClosePort = new System.Windows.Forms.Button();
            this.btnClearText = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDirection = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbSensorRight = new System.Windows.Forms.TextBox();
            this.tbSensorLeft = new System.Windows.Forms.TextBox();
            this.tbSensorFront = new System.Windows.Forms.TextBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnRequestUpdate = new System.Windows.Forms.Button();
            this.btnUnits = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAcquireJs = new System.Windows.Forms.Button();
            this.tbDrive = new System.Windows.Forms.TextBox();
            this.tbTurn = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
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
            this.tbBaudRate.Text = "57600";
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
            // rtbDataIn
            // 
            this.rtbDataIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rtbDataIn.BackColor = System.Drawing.SystemColors.Window;
            this.rtbDataIn.Location = new System.Drawing.Point(316, 10);
            this.rtbDataIn.Name = "rtbDataIn";
            this.rtbDataIn.ReadOnly = true;
            this.rtbDataIn.Size = new System.Drawing.Size(227, 326);
            this.rtbDataIn.TabIndex = 13;
            this.rtbDataIn.Text = "";
            this.rtbDataIn.TextChanged += new System.EventHandler(this.rtbDataIn_TextChanged);
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
            // btnClosePort
            // 
            this.btnClosePort.Enabled = false;
            this.btnClosePort.Location = new System.Drawing.Point(60, 242);
            this.btnClosePort.Name = "btnClosePort";
            this.btnClosePort.Size = new System.Drawing.Size(75, 23);
            this.btnClosePort.TabIndex = 15;
            this.btnClosePort.Text = "Close Port";
            this.btnClosePort.UseVisualStyleBackColor = true;
            this.btnClosePort.Click += new System.EventHandler(this.btnClosePort_Click);
            // 
            // btnClearText
            // 
            this.btnClearText.Location = new System.Drawing.Point(235, 313);
            this.btnClearText.Name = "btnClearText";
            this.btnClearText.Size = new System.Drawing.Size(75, 23);
            this.btnClearText.TabIndex = 16;
            this.btnClearText.Text = "Clear";
            this.btnClearText.UseVisualStyleBackColor = true;
            this.btnClearText.Click += new System.EventHandler(this.btnClearText_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(627, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Driving:";
            // 
            // tbDirection
            // 
            this.tbDirection.BackColor = System.Drawing.SystemColors.Window;
            this.tbDirection.Location = new System.Drawing.Point(676, 168);
            this.tbDirection.Name = "tbDirection";
            this.tbDirection.ReadOnly = true;
            this.tbDirection.Size = new System.Drawing.Size(100, 20);
            this.tbDirection.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(705, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Front";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(613, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Left";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(806, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Right";
            // 
            // tbSensorRight
            // 
            this.tbSensorRight.BackColor = System.Drawing.SystemColors.Window;
            this.tbSensorRight.Location = new System.Drawing.Point(791, 87);
            this.tbSensorRight.Name = "tbSensorRight";
            this.tbSensorRight.ReadOnly = true;
            this.tbSensorRight.Size = new System.Drawing.Size(62, 20);
            this.tbSensorRight.TabIndex = 19;
            // 
            // tbSensorLeft
            // 
            this.tbSensorLeft.BackColor = System.Drawing.SystemColors.Window;
            this.tbSensorLeft.Location = new System.Drawing.Point(594, 87);
            this.tbSensorLeft.Name = "tbSensorLeft";
            this.tbSensorLeft.ReadOnly = true;
            this.tbSensorLeft.Size = new System.Drawing.Size(62, 20);
            this.tbSensorLeft.TabIndex = 18;
            // 
            // tbSensorFront
            // 
            this.tbSensorFront.BackColor = System.Drawing.SystemColors.Window;
            this.tbSensorFront.Location = new System.Drawing.Point(689, 29);
            this.tbSensorFront.Name = "tbSensorFront";
            this.tbSensorFront.ReadOnly = true;
            this.tbSensorFront.Size = new System.Drawing.Size(62, 20);
            this.tbSensorFront.TabIndex = 17;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(676, 313);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(100, 23);
            this.btnStartStop.TabIndex = 25;
            this.btnStartStop.Text = "Start Driving";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnRequestUpdate
            // 
            this.btnRequestUpdate.Location = new System.Drawing.Point(791, 313);
            this.btnRequestUpdate.Name = "btnRequestUpdate";
            this.btnRequestUpdate.Size = new System.Drawing.Size(86, 23);
            this.btnRequestUpdate.TabIndex = 26;
            this.btnRequestUpdate.Text = "Toggle Update";
            this.btnRequestUpdate.UseVisualStyleBackColor = true;
            this.btnRequestUpdate.Click += new System.EventHandler(this.btnRequestUpdate_Click);
            // 
            // btnUnits
            // 
            this.btnUnits.Location = new System.Drawing.Point(691, 103);
            this.btnUnits.Name = "btnUnits";
            this.btnUnits.Size = new System.Drawing.Size(60, 23);
            this.btnUnits.TabIndex = 27;
            this.btnUnits.Text = "cm";
            this.btnUnits.UseVisualStyleBackColor = true;
            this.btnUnits.Click += new System.EventHandler(this.btnUnits_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(705, 87);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Units";
            // 
            // btnAcquireJs
            // 
            this.btnAcquireJs.Location = new System.Drawing.Point(676, 284);
            this.btnAcquireJs.Name = "btnAcquireJs";
            this.btnAcquireJs.Size = new System.Drawing.Size(101, 23);
            this.btnAcquireJs.TabIndex = 29;
            this.btnAcquireJs.Text = "Acquire Joystick";
            this.btnAcquireJs.UseVisualStyleBackColor = true;
            this.btnAcquireJs.Click += new System.EventHandler(this.btnAcquireJs_Click);
            // 
            // tbDrive
            // 
            this.tbDrive.Location = new System.Drawing.Point(570, 284);
            this.tbDrive.Name = "tbDrive";
            this.tbDrive.Size = new System.Drawing.Size(54, 20);
            this.tbDrive.TabIndex = 30;
            this.tbDrive.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbTurn
            // 
            this.tbTurn.Location = new System.Drawing.Point(570, 311);
            this.tbTurn.Name = "tbTurn";
            this.tbTurn.Size = new System.Drawing.Size(54, 20);
            this.tbTurn.TabIndex = 31;
            this.tbTurn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(549, 234);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 38);
            this.button1.TabIndex = 32;
            this.button1.Text = "Debug Output";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MapperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 348);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbTurn);
            this.Controls.Add(this.tbDrive);
            this.Controls.Add(this.btnAcquireJs);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnUnits);
            this.Controls.Add(this.btnRequestUpdate);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDirection);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbSensorRight);
            this.Controls.Add(this.tbSensorLeft);
            this.Controls.Add(this.tbSensorFront);
            this.Controls.Add(this.btnClearText);
            this.Controls.Add(this.btnClosePort);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtbDataIn);
            this.Controls.Add(this.label2);
            this.KeyPreview = true;
            this.Name = "MapperForm";
            this.Text = "ATLAS Mapper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapperForm_FormClosing);
            this.Load += new System.EventHandler(this.MapperForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MapperForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MapperForm_KeyUp);
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
        private System.Windows.Forms.RichTextBox rtbDataIn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClosePort;
        private System.Windows.Forms.Button btnClearText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDirection;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbSensorRight;
        private System.Windows.Forms.TextBox tbSensorLeft;
        private System.Windows.Forms.TextBox tbSensorFront;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnRequestUpdate;
        private System.Windows.Forms.Button btnUnits;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAcquireJs;
        private System.Windows.Forms.TextBox tbDrive;
        private System.Windows.Forms.TextBox tbTurn;
        private System.Windows.Forms.Button button1;
    }
}

