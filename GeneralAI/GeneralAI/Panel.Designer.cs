namespace GeneralAI
{
    partial class Panel
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
            this.txt_serverIP = new System.Windows.Forms.TextBox();
            this.btn_collect = new System.Windows.Forms.Button();
            this.btn_command = new System.Windows.Forms.Button();
            this.btn_behaviour = new System.Windows.Forms.Button();
            this.txt_status = new System.Windows.Forms.RichTextBox();
            this.test = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(564, 299);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(100, 23);
            this.btn_connect.TabIndex = 0;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // txt_serverIP
            // 
            this.txt_serverIP.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.txt_serverIP.Location = new System.Drawing.Point(564, 273);
            this.txt_serverIP.Name = "txt_serverIP";
            this.txt_serverIP.Size = new System.Drawing.Size(100, 20);
            this.txt_serverIP.TabIndex = 1;
            // 
            // btn_collect
            // 
            this.btn_collect.Location = new System.Drawing.Point(12, 12);
            this.btn_collect.Name = "btn_collect";
            this.btn_collect.Size = new System.Drawing.Size(93, 39);
            this.btn_collect.TabIndex = 2;
            this.btn_collect.Text = "Start Collecting";
            this.btn_collect.UseVisualStyleBackColor = true;
            this.btn_collect.Click += new System.EventHandler(this.btn_collect_Click);
            // 
            // btn_command
            // 
            this.btn_command.Location = new System.Drawing.Point(12, 57);
            this.btn_command.Name = "btn_command";
            this.btn_command.Size = new System.Drawing.Size(93, 39);
            this.btn_command.TabIndex = 3;
            this.btn_command.Text = "Activate Commad";
            this.btn_command.UseVisualStyleBackColor = true;
            this.btn_command.Click += new System.EventHandler(this.btn_command_Click);
            // 
            // btn_behaviour
            // 
            this.btn_behaviour.Location = new System.Drawing.Point(12, 103);
            this.btn_behaviour.Name = "btn_behaviour";
            this.btn_behaviour.Size = new System.Drawing.Size(93, 40);
            this.btn_behaviour.TabIndex = 5;
            this.btn_behaviour.Text = "Start Behavior";
            this.btn_behaviour.UseVisualStyleBackColor = true;
            this.btn_behaviour.Click += new System.EventHandler(this.btn_behaviour_Click);
            // 
            // txt_status
            // 
            this.txt_status.Location = new System.Drawing.Point(398, 13);
            this.txt_status.Name = "txt_status";
            this.txt_status.Size = new System.Drawing.Size(266, 254);
            this.txt_status.TabIndex = 7;
            this.txt_status.Text = "";
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(13, 299);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(75, 23);
            this.test.TabIndex = 8;
            this.test.Text = "Record";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // Panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 334);
            this.Controls.Add(this.test);
            this.Controls.Add(this.txt_status);
            this.Controls.Add(this.btn_behaviour);
            this.Controls.Add(this.btn_command);
            this.Controls.Add(this.btn_collect);
            this.Controls.Add(this.txt_serverIP);
            this.Controls.Add(this.btn_connect);
            this.Name = "Panel";
            this.Text = "Behavior Panel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btn_connect;
        public System.Windows.Forms.TextBox txt_serverIP;
        private System.Windows.Forms.Button btn_collect;
        private System.Windows.Forms.Button btn_command;
        private System.Windows.Forms.Button btn_behaviour;
        public System.Windows.Forms.RichTextBox txt_status;
        private System.Windows.Forms.Button test;
    }
}