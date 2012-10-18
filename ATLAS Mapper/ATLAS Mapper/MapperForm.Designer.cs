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
            this.tbSensorOutput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBaudRate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbParity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDataBits = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbStopBits = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(180, 34);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(38, 20);
            this.tbPort.TabIndex = 0;
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(139, 173);
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
            this.label1.Location = new System.Drawing.Point(105, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Port Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sensor Output";
            // 
            // tbSensorOutput
            // 
            this.tbSensorOutput.Location = new System.Drawing.Point(160, 242);
            this.tbSensorOutput.Name = "tbSensorOutput";
            this.tbSensorOutput.Size = new System.Drawing.Size(112, 20);
            this.tbSensorOutput.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Baud Rate:";
            // 
            // tbBaudRate
            // 
            this.tbBaudRate.Location = new System.Drawing.Point(180, 60);
            this.tbBaudRate.Name = "tbBaudRate";
            this.tbBaudRate.Size = new System.Drawing.Size(67, 20);
            this.tbBaudRate.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(138, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Parity:";
            // 
            // tbParity
            // 
            this.tbParity.Location = new System.Drawing.Point(180, 86);
            this.tbParity.Name = "tbParity";
            this.tbParity.Size = new System.Drawing.Size(38, 20);
            this.tbParity.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(121, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Data Bits:";
            // 
            // tbDataBits
            // 
            this.tbDataBits.Location = new System.Drawing.Point(180, 112);
            this.tbDataBits.Name = "tbDataBits";
            this.tbDataBits.Size = new System.Drawing.Size(38, 20);
            this.tbDataBits.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(122, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Stop Bits:";
            // 
            // tbStopBits
            // 
            this.tbStopBits.Location = new System.Drawing.Point(180, 138);
            this.tbStopBits.Name = "tbStopBits";
            this.tbStopBits.Size = new System.Drawing.Size(38, 20);
            this.tbStopBits.TabIndex = 11;
            // 
            // MapperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 283);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbStopBits);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDataBits);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbParity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbBaudRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSensorOutput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.tbPort);
            this.Name = "MapperForm";
            this.Text = "ATLAS Mapper";
            this.Load += new System.EventHandler(this.MapperForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSensorOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbBaudRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbParity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDataBits;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbStopBits;
    }
}

