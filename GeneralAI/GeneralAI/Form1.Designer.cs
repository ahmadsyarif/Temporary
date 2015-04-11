namespace GeneralAI
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
            this.Connect = new System.Windows.Forms.Button();
            this.serverIP = new System.Windows.Forms.TextBox();
            this.Audio = new System.Windows.Forms.Label();
            this.sendWav = new System.Windows.Forms.Button();
            this.record = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.TextBox();
            this.rest = new System.Windows.Forms.Button();
            this.visual = new System.Windows.Forms.Label();
            this.faceDec = new System.Windows.Forms.Button();
            this.identify = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(115, 290);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 0;
            this.Connect.Text = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // serverIP
            // 
            this.serverIP.Location = new System.Drawing.Point(9, 292);
            this.serverIP.Name = "serverIP";
            this.serverIP.Size = new System.Drawing.Size(100, 20);
            this.serverIP.TabIndex = 1;
            // 
            // Audio
            // 
            this.Audio.AutoSize = true;
            this.Audio.Location = new System.Drawing.Point(13, 13);
            this.Audio.Name = "Audio";
            this.Audio.Size = new System.Drawing.Size(34, 13);
            this.Audio.TabIndex = 2;
            this.Audio.Text = "Audio";
            // 
            // sendWav
            // 
            this.sendWav.Location = new System.Drawing.Point(12, 29);
            this.sendWav.Name = "sendWav";
            this.sendWav.Size = new System.Drawing.Size(75, 23);
            this.sendWav.TabIndex = 3;
            this.sendWav.Text = "Send Wav";
            this.sendWav.UseVisualStyleBackColor = true;
            this.sendWav.Click += new System.EventHandler(this.sendWav_Click);
            // 
            // record
            // 
            this.record.Location = new System.Drawing.Point(12, 58);
            this.record.Name = "record";
            this.record.Size = new System.Drawing.Size(75, 23);
            this.record.TabIndex = 4;
            this.record.Text = "Record";
            this.record.UseVisualStyleBackColor = true;
            this.record.Click += new System.EventHandler(this.record_Click);
            // 
            // result
            // 
            this.result.Location = new System.Drawing.Point(9, 117);
            this.result.Multiline = true;
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(113, 87);
            this.result.TabIndex = 5;
            // 
            // rest
            // 
            this.rest.Location = new System.Drawing.Point(405, 292);
            this.rest.Name = "rest";
            this.rest.Size = new System.Drawing.Size(75, 23);
            this.rest.TabIndex = 6;
            this.rest.Text = "rest";
            this.rest.UseVisualStyleBackColor = true;
            this.rest.Click += new System.EventHandler(this.rest_Click);
            // 
            // visual
            // 
            this.visual.AutoSize = true;
            this.visual.Location = new System.Drawing.Point(173, 13);
            this.visual.Name = "visual";
            this.visual.Size = new System.Drawing.Size(35, 13);
            this.visual.TabIndex = 7;
            this.visual.Text = "Visual";
            // 
            // faceDec
            // 
            this.faceDec.Location = new System.Drawing.Point(176, 28);
            this.faceDec.Name = "faceDec";
            this.faceDec.Size = new System.Drawing.Size(75, 23);
            this.faceDec.TabIndex = 8;
            this.faceDec.Text = "Detect";
            this.faceDec.UseVisualStyleBackColor = true;
            this.faceDec.Click += new System.EventHandler(this.faceDec_Click);
            // 
            // identify
            // 
            this.identify.Location = new System.Drawing.Point(16, 88);
            this.identify.Name = "identify";
            this.identify.Size = new System.Drawing.Size(71, 23);
            this.identify.TabIndex = 9;
            this.identify.Text = "identify";
            this.identify.UseVisualStyleBackColor = true;
            this.identify.Click += new System.EventHandler(this.identify_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 325);
            this.Controls.Add(this.identify);
            this.Controls.Add(this.faceDec);
            this.Controls.Add(this.visual);
            this.Controls.Add(this.rest);
            this.Controls.Add(this.result);
            this.Controls.Add(this.record);
            this.Controls.Add(this.sendWav);
            this.Controls.Add(this.Audio);
            this.Controls.Add(this.serverIP);
            this.Controls.Add(this.Connect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.TextBox serverIP;
        private System.Windows.Forms.Label Audio;
        private System.Windows.Forms.Button sendWav;
        private System.Windows.Forms.Button record;
        private System.Windows.Forms.TextBox result;
        private System.Windows.Forms.Button rest;
        private System.Windows.Forms.Label visual;
        private System.Windows.Forms.Button faceDec;
        private System.Windows.Forms.Button identify;
    }
}

