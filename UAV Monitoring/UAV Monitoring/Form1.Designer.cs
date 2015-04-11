namespace UAV_Monitoring
{
    partial class Form1
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
            this.btn_connect = new System.Windows.Forms.Button();
            this.txtB_IP = new System.Windows.Forms.TextBox();
            this.label_IP = new System.Windows.Forms.Label();
            this.label_GPS = new System.Windows.Forms.Label();
            this.label_IMU = new System.Windows.Forms.Label();
            this.textBox_latitude = new System.Windows.Forms.TextBox();
            this.label_latitude = new System.Windows.Forms.Label();
            this.label_longitude = new System.Windows.Forms.Label();
            this.textBox_longitude = new System.Windows.Forms.TextBox();
            this.label_accelero = new System.Windows.Forms.Label();
            this.textBox_accelero = new System.Windows.Forms.TextBox();
            this.label_gyro = new System.Windows.Forms.Label();
            this.textBox_gyro = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(556, 301);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(120, 23);
            this.btn_connect.TabIndex = 0;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            // 
            // txtB_IP
            // 
            this.txtB_IP.Location = new System.Drawing.Point(576, 275);
            this.txtB_IP.Name = "txtB_IP";
            this.txtB_IP.Size = new System.Drawing.Size(100, 20);
            this.txtB_IP.TabIndex = 1;
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(553, 278);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(17, 13);
            this.label_IP.TabIndex = 2;
            this.label_IP.Text = "IP";
            // 
            // label_GPS
            // 
            this.label_GPS.AutoSize = true;
            this.label_GPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_GPS.Location = new System.Drawing.Point(3, 9);
            this.label_GPS.Name = "label_GPS";
            this.label_GPS.Size = new System.Drawing.Size(74, 31);
            this.label_GPS.TabIndex = 3;
            this.label_GPS.Text = "GPS";
            // 
            // label_IMU
            // 
            this.label_IMU.AutoSize = true;
            this.label_IMU.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_IMU.Location = new System.Drawing.Point(163, 9);
            this.label_IMU.Name = "label_IMU";
            this.label_IMU.Size = new System.Drawing.Size(67, 31);
            this.label_IMU.TabIndex = 4;
            this.label_IMU.Text = "IMU";
            // 
            // textBox_latitude
            // 
            this.textBox_latitude.Location = new System.Drawing.Point(66, 47);
            this.textBox_latitude.Name = "textBox_latitude";
            this.textBox_latitude.Size = new System.Drawing.Size(66, 20);
            this.textBox_latitude.TabIndex = 5;
            // 
            // label_latitude
            // 
            this.label_latitude.AutoSize = true;
            this.label_latitude.Location = new System.Drawing.Point(6, 50);
            this.label_latitude.Name = "label_latitude";
            this.label_latitude.Size = new System.Drawing.Size(45, 13);
            this.label_latitude.TabIndex = 6;
            this.label_latitude.Text = "Latitude";
            // 
            // label_longitude
            // 
            this.label_longitude.AutoSize = true;
            this.label_longitude.Location = new System.Drawing.Point(6, 85);
            this.label_longitude.Name = "label_longitude";
            this.label_longitude.Size = new System.Drawing.Size(54, 13);
            this.label_longitude.TabIndex = 8;
            this.label_longitude.Text = "Longitude";
            // 
            // textBox_longitude
            // 
            this.textBox_longitude.Location = new System.Drawing.Point(66, 82);
            this.textBox_longitude.Name = "textBox_longitude";
            this.textBox_longitude.Size = new System.Drawing.Size(66, 20);
            this.textBox_longitude.TabIndex = 7;
            // 
            // label_accelero
            // 
            this.label_accelero.AutoSize = true;
            this.label_accelero.Location = new System.Drawing.Point(166, 85);
            this.label_accelero.Name = "label_accelero";
            this.label_accelero.Size = new System.Drawing.Size(75, 13);
            this.label_accelero.TabIndex = 12;
            this.label_accelero.Text = "Accelerometer";
            // 
            // textBox_accelero
            // 
            this.textBox_accelero.Location = new System.Drawing.Point(247, 82);
            this.textBox_accelero.Name = "textBox_accelero";
            this.textBox_accelero.Size = new System.Drawing.Size(66, 20);
            this.textBox_accelero.TabIndex = 11;
            // 
            // label_gyro
            // 
            this.label_gyro.AutoSize = true;
            this.label_gyro.Location = new System.Drawing.Point(166, 50);
            this.label_gyro.Name = "label_gyro";
            this.label_gyro.Size = new System.Drawing.Size(55, 13);
            this.label_gyro.TabIndex = 10;
            this.label_gyro.Text = "Gyrometer";
            // 
            // textBox_gyro
            // 
            this.textBox_gyro.Location = new System.Drawing.Point(247, 47);
            this.textBox_gyro.Name = "textBox_gyro";
            this.textBox_gyro.Size = new System.Drawing.Size(66, 20);
            this.textBox_gyro.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 336);
            this.Controls.Add(this.label_accelero);
            this.Controls.Add(this.textBox_accelero);
            this.Controls.Add(this.label_gyro);
            this.Controls.Add(this.textBox_gyro);
            this.Controls.Add(this.label_longitude);
            this.Controls.Add(this.textBox_longitude);
            this.Controls.Add(this.label_latitude);
            this.Controls.Add(this.textBox_latitude);
            this.Controls.Add(this.label_IMU);
            this.Controls.Add(this.label_GPS);
            this.Controls.Add(this.label_IP);
            this.Controls.Add(this.txtB_IP);
            this.Controls.Add(this.btn_connect);
            this.Name = "Form1";
            this.Text = "UAV Monitor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.TextBox txtB_IP;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.Label label_GPS;
        private System.Windows.Forms.Label label_IMU;
        private System.Windows.Forms.TextBox textBox_latitude;
        private System.Windows.Forms.Label label_latitude;
        private System.Windows.Forms.Label label_longitude;
        private System.Windows.Forms.TextBox textBox_longitude;
        private System.Windows.Forms.Label label_accelero;
        private System.Windows.Forms.TextBox textBox_accelero;
        private System.Windows.Forms.Label label_gyro;
        private System.Windows.Forms.TextBox textBox_gyro;
    }
}

