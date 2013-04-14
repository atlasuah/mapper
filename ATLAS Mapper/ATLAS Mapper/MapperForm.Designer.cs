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
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbSensorRight = new System.Windows.Forms.TextBox();
            this.tbSensorLeft = new System.Windows.Forms.TextBox();
            this.tbSensorFront = new System.Windows.Forms.TextBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnUnits = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnAcquireJs = new System.Windows.Forms.Button();
            this.tbSentCmd = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.pbMap = new System.Windows.Forms.PictureBox();
            this.sonarGroupBox = new System.Windows.Forms.GroupBox();
            this.encoderDelta = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.compassDirection = new System.Windows.Forms.TextBox();
            this.accelGroupBox = new System.Windows.Forms.GroupBox();
            this.accelBoxY = new System.Windows.Forms.TextBox();
            this.accelBoxX = new System.Windows.Forms.TextBox();
            this.accelBoxZ = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.gyroGroupBox = new System.Windows.Forms.GroupBox();
            this.gyroBoxY = new System.Windows.Forms.TextBox();
            this.gyroBoxX = new System.Windows.Forms.TextBox();
            this.gyroBoxZ = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnClearMap = new System.Windows.Forms.Button();
            this.btnSaveMap = new System.Windows.Forms.Button();
            this.tbTotalEncCnt = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).BeginInit();
            this.sonarGroupBox.SuspendLayout();
            this.accelGroupBox.SuspendLayout();
            this.gyroGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(104, 26);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(38, 20);
            this.tbPort.TabIndex = 0;
            this.tbPort.Text = "5";
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
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 394);
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
            this.tbDataBits.Enabled = false;
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
            this.rtbDataIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDataIn.BackColor = System.Drawing.SystemColors.Window;
            this.rtbDataIn.Location = new System.Drawing.Point(348, 391);
            this.rtbDataIn.Name = "rtbDataIn";
            this.rtbDataIn.ReadOnly = true;
            this.rtbDataIn.Size = new System.Drawing.Size(282, 287);
            this.rtbDataIn.TabIndex = 13;
            this.rtbDataIn.Text = "";
            this.rtbDataIn.TextChanged += new System.EventHandler(this.rtbDataIn_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.groupBox1.Controls.Add(this.btnClosePort);
            this.groupBox1.Location = new System.Drawing.Point(12, 391);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 230);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Port";
            // 
            // btnClosePort
            // 
            this.btnClosePort.Enabled = false;
            this.btnClosePort.Location = new System.Drawing.Point(63, 194);
            this.btnClosePort.Name = "btnClosePort";
            this.btnClosePort.Size = new System.Drawing.Size(75, 23);
            this.btnClosePort.TabIndex = 15;
            this.btnClosePort.Text = "Close Port";
            this.btnClosePort.UseVisualStyleBackColor = true;
            this.btnClosePort.Click += new System.EventHandler(this.btnClosePort_Click);
            // 
            // btnClearText
            // 
            this.btnClearText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearText.Location = new System.Drawing.Point(267, 653);
            this.btnClearText.Name = "btnClearText";
            this.btnClearText.Size = new System.Drawing.Size(75, 23);
            this.btnClearText.TabIndex = 16;
            this.btnClearText.Text = "Clear";
            this.btnClearText.UseVisualStyleBackColor = true;
            this.btnClearText.Click += new System.EventHandler(this.btnClearText_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(117, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Front";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(40, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Left";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(200, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Right";
            // 
            // tbSensorRight
            // 
            this.tbSensorRight.BackColor = System.Drawing.SystemColors.Window;
            this.tbSensorRight.Location = new System.Drawing.Point(185, 42);
            this.tbSensorRight.Name = "tbSensorRight";
            this.tbSensorRight.ReadOnly = true;
            this.tbSensorRight.Size = new System.Drawing.Size(62, 20);
            this.tbSensorRight.TabIndex = 19;
            // 
            // tbSensorLeft
            // 
            this.tbSensorLeft.BackColor = System.Drawing.SystemColors.Window;
            this.tbSensorLeft.Location = new System.Drawing.Point(21, 42);
            this.tbSensorLeft.Name = "tbSensorLeft";
            this.tbSensorLeft.ReadOnly = true;
            this.tbSensorLeft.Size = new System.Drawing.Size(62, 20);
            this.tbSensorLeft.TabIndex = 18;
            // 
            // tbSensorFront
            // 
            this.tbSensorFront.BackColor = System.Drawing.SystemColors.Window;
            this.tbSensorFront.Location = new System.Drawing.Point(101, 32);
            this.tbSensorFront.Name = "tbSensorFront";
            this.tbSensorFront.ReadOnly = true;
            this.tbSensorFront.Size = new System.Drawing.Size(62, 20);
            this.tbSensorFront.TabIndex = 17;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartStop.Location = new System.Drawing.Point(116, 654);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(100, 23);
            this.btnStartStop.TabIndex = 25;
            this.btnStartStop.Text = "Start Driving";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnUnits
            // 
            this.btnUnits.Location = new System.Drawing.Point(101, 75);
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
            this.label11.Location = new System.Drawing.Point(115, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Units";
            // 
            // btnAcquireJs
            // 
            this.btnAcquireJs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAcquireJs.Location = new System.Drawing.Point(9, 654);
            this.btnAcquireJs.Name = "btnAcquireJs";
            this.btnAcquireJs.Size = new System.Drawing.Size(101, 23);
            this.btnAcquireJs.TabIndex = 29;
            this.btnAcquireJs.Text = "Acquire Joystick";
            this.btnAcquireJs.UseVisualStyleBackColor = true;
            this.btnAcquireJs.Click += new System.EventHandler(this.btnAcquireJs_Click);
            // 
            // tbSentCmd
            // 
            this.tbSentCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbSentCmd.Location = new System.Drawing.Point(227, 627);
            this.tbSentCmd.Name = "tbSentCmd";
            this.tbSentCmd.Size = new System.Drawing.Size(100, 20);
            this.tbSentCmd.TabIndex = 33;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(166, 631);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "Last Sent:";
            // 
            // pbMap
            // 
            this.pbMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMap.BackColor = System.Drawing.SystemColors.Window;
            this.pbMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbMap.Location = new System.Drawing.Point(12, 12);
            this.pbMap.Name = "pbMap";
            this.pbMap.Size = new System.Drawing.Size(824, 358);
            this.pbMap.TabIndex = 35;
            this.pbMap.TabStop = false;
            this.pbMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMap_MouseClick);
            this.pbMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMap_MouseDown);
            this.pbMap.MouseEnter += new System.EventHandler(this.pbMap_MouseEnter);
            this.pbMap.MouseLeave += new System.EventHandler(this.pbMap_MouseLeave);
            this.pbMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMap_MouseMove);
            this.pbMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMap_MouseUp);
            // 
            // sonarGroupBox
            // 
            this.sonarGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sonarGroupBox.Controls.Add(this.btnUnits);
            this.sonarGroupBox.Controls.Add(this.tbSensorFront);
            this.sonarGroupBox.Controls.Add(this.tbSensorLeft);
            this.sonarGroupBox.Controls.Add(this.tbSensorRight);
            this.sonarGroupBox.Controls.Add(this.label10);
            this.sonarGroupBox.Controls.Add(this.label11);
            this.sonarGroupBox.Controls.Add(this.label9);
            this.sonarGroupBox.Controls.Add(this.label8);
            this.sonarGroupBox.Location = new System.Drawing.Point(659, 391);
            this.sonarGroupBox.Name = "sonarGroupBox";
            this.sonarGroupBox.Size = new System.Drawing.Size(271, 106);
            this.sonarGroupBox.TabIndex = 36;
            this.sonarGroupBox.TabStop = false;
            this.sonarGroupBox.Text = "Sonar Data";
            // 
            // encoderDelta
            // 
            this.encoderDelta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.encoderDelta.Location = new System.Drawing.Point(761, 589);
            this.encoderDelta.Name = "encoderDelta";
            this.encoderDelta.Size = new System.Drawing.Size(61, 20);
            this.encoderDelta.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(754, 573);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Encoder Delta";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(766, 642);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 13);
            this.label13.TabIndex = 38;
            this.label13.Text = "Compass";
            // 
            // compassDirection
            // 
            this.compassDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.compassDirection.Location = new System.Drawing.Point(761, 658);
            this.compassDirection.Name = "compassDirection";
            this.compassDirection.Size = new System.Drawing.Size(61, 20);
            this.compassDirection.TabIndex = 39;
            // 
            // accelGroupBox
            // 
            this.accelGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accelGroupBox.Controls.Add(this.accelBoxY);
            this.accelGroupBox.Controls.Add(this.accelBoxX);
            this.accelGroupBox.Controls.Add(this.accelBoxZ);
            this.accelGroupBox.Controls.Add(this.label15);
            this.accelGroupBox.Controls.Add(this.label14);
            this.accelGroupBox.Controls.Add(this.label16);
            this.accelGroupBox.Location = new System.Drawing.Point(659, 573);
            this.accelGroupBox.Name = "accelGroupBox";
            this.accelGroupBox.Size = new System.Drawing.Size(95, 103);
            this.accelGroupBox.TabIndex = 37;
            this.accelGroupBox.TabStop = false;
            this.accelGroupBox.Text = "Accelerometer";
            // 
            // accelBoxY
            // 
            this.accelBoxY.BackColor = System.Drawing.SystemColors.Window;
            this.accelBoxY.Location = new System.Drawing.Point(21, 45);
            this.accelBoxY.Name = "accelBoxY";
            this.accelBoxY.ReadOnly = true;
            this.accelBoxY.Size = new System.Drawing.Size(62, 20);
            this.accelBoxY.TabIndex = 17;
            // 
            // accelBoxX
            // 
            this.accelBoxX.BackColor = System.Drawing.SystemColors.Window;
            this.accelBoxX.Location = new System.Drawing.Point(21, 19);
            this.accelBoxX.Name = "accelBoxX";
            this.accelBoxX.ReadOnly = true;
            this.accelBoxX.Size = new System.Drawing.Size(62, 20);
            this.accelBoxX.TabIndex = 18;
            // 
            // accelBoxZ
            // 
            this.accelBoxZ.BackColor = System.Drawing.SystemColors.Window;
            this.accelBoxZ.Location = new System.Drawing.Point(21, 71);
            this.accelBoxZ.Name = "accelBoxZ";
            this.accelBoxZ.ReadOnly = true;
            this.accelBoxZ.Size = new System.Drawing.Size(62, 20);
            this.accelBoxZ.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 13);
            this.label15.TabIndex = 21;
            this.label15.Text = "Z";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 13);
            this.label14.TabIndex = 21;
            this.label14.Text = "Y";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "X";
            // 
            // gyroGroupBox
            // 
            this.gyroGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gyroGroupBox.Controls.Add(this.gyroBoxY);
            this.gyroGroupBox.Controls.Add(this.gyroBoxX);
            this.gyroGroupBox.Controls.Add(this.gyroBoxZ);
            this.gyroGroupBox.Controls.Add(this.label17);
            this.gyroGroupBox.Controls.Add(this.label18);
            this.gyroGroupBox.Controls.Add(this.label19);
            this.gyroGroupBox.Location = new System.Drawing.Point(835, 573);
            this.gyroGroupBox.Name = "gyroGroupBox";
            this.gyroGroupBox.Size = new System.Drawing.Size(95, 103);
            this.gyroGroupBox.TabIndex = 38;
            this.gyroGroupBox.TabStop = false;
            this.gyroGroupBox.Text = "Gyroscope";
            // 
            // gyroBoxY
            // 
            this.gyroBoxY.BackColor = System.Drawing.SystemColors.Window;
            this.gyroBoxY.Location = new System.Drawing.Point(21, 45);
            this.gyroBoxY.Name = "gyroBoxY";
            this.gyroBoxY.ReadOnly = true;
            this.gyroBoxY.Size = new System.Drawing.Size(62, 20);
            this.gyroBoxY.TabIndex = 17;
            // 
            // gyroBoxX
            // 
            this.gyroBoxX.BackColor = System.Drawing.SystemColors.Window;
            this.gyroBoxX.Location = new System.Drawing.Point(21, 19);
            this.gyroBoxX.Name = "gyroBoxX";
            this.gyroBoxX.ReadOnly = true;
            this.gyroBoxX.Size = new System.Drawing.Size(62, 20);
            this.gyroBoxX.TabIndex = 18;
            // 
            // gyroBoxZ
            // 
            this.gyroBoxZ.BackColor = System.Drawing.SystemColors.Window;
            this.gyroBoxZ.Location = new System.Drawing.Point(21, 71);
            this.gyroBoxZ.Name = "gyroBoxZ";
            this.gyroBoxZ.ReadOnly = true;
            this.gyroBoxZ.Size = new System.Drawing.Size(62, 20);
            this.gyroBoxZ.TabIndex = 19;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 74);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 13);
            this.label17.TabIndex = 21;
            this.label17.Text = "Z";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 48);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 13);
            this.label18.TabIndex = 21;
            this.label18.Text = "Y";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "X";
            // 
            // btnClearMap
            // 
            this.btnClearMap.Location = new System.Drawing.Point(855, 123);
            this.btnClearMap.Name = "btnClearMap";
            this.btnClearMap.Size = new System.Drawing.Size(75, 23);
            this.btnClearMap.TabIndex = 40;
            this.btnClearMap.Text = "Clear Map";
            this.btnClearMap.UseVisualStyleBackColor = true;
            this.btnClearMap.Click += new System.EventHandler(this.btnClearMap_Click);
            // 
            // btnSaveMap
            // 
            this.btnSaveMap.Location = new System.Drawing.Point(855, 165);
            this.btnSaveMap.Name = "btnSaveMap";
            this.btnSaveMap.Size = new System.Drawing.Size(75, 23);
            this.btnSaveMap.TabIndex = 41;
            this.btnSaveMap.Text = "Save Map";
            this.btnSaveMap.UseVisualStyleBackColor = true;
            this.btnSaveMap.Click += new System.EventHandler(this.btnSaveMap_Click);
            // 
            // tbTotalEncCnt
            // 
            this.tbTotalEncCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTotalEncCnt.Location = new System.Drawing.Point(761, 615);
            this.tbTotalEncCnt.Name = "tbTotalEncCnt";
            this.tbTotalEncCnt.Size = new System.Drawing.Size(61, 20);
            this.tbTotalEncCnt.TabIndex = 42;
            // 
            // MapperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 699);
            this.Controls.Add(this.tbTotalEncCnt);
            this.Controls.Add(this.btnSaveMap);
            this.Controls.Add(this.btnClearMap);
            this.Controls.Add(this.gyroGroupBox);
            this.Controls.Add(this.accelGroupBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.compassDirection);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.encoderDelta);
            this.Controls.Add(this.sonarGroupBox);
            this.Controls.Add(this.pbMap);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tbSentCmd);
            this.Controls.Add(this.btnAcquireJs);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnClearText);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtbDataIn);
            this.Controls.Add(this.label2);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(958, 726);
            this.Name = "MapperForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATLAS Mapper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapperForm_FormClosing);
            this.Load += new System.EventHandler(this.MapperForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MapperForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MapperForm_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).EndInit();
            this.sonarGroupBox.ResumeLayout(false);
            this.sonarGroupBox.PerformLayout();
            this.accelGroupBox.ResumeLayout(false);
            this.accelGroupBox.PerformLayout();
            this.gyroGroupBox.ResumeLayout(false);
            this.gyroGroupBox.PerformLayout();
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbSensorRight;
        private System.Windows.Forms.TextBox tbSensorLeft;
        private System.Windows.Forms.TextBox tbSensorFront;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnUnits;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnAcquireJs;
        private System.Windows.Forms.TextBox tbSentCmd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pbMap;
        private System.Windows.Forms.GroupBox sonarGroupBox;
        private System.Windows.Forms.TextBox encoderDelta;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox compassDirection;
        private System.Windows.Forms.GroupBox accelGroupBox;
        private System.Windows.Forms.TextBox accelBoxY;
        private System.Windows.Forms.TextBox accelBoxX;
        private System.Windows.Forms.TextBox accelBoxZ;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox gyroGroupBox;
        private System.Windows.Forms.TextBox gyroBoxY;
        private System.Windows.Forms.TextBox gyroBoxX;
        private System.Windows.Forms.TextBox gyroBoxZ;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnClearMap;
        private System.Windows.Forms.Button btnSaveMap;
        private System.Windows.Forms.TextBox tbTotalEncCnt;
    }
}

