namespace EmguTrial
{
    partial class FaceDetection
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
            this.str_btn = new System.Windows.Forms.Button();
            this.cameraOut = new Emgu.CV.UI.ImageBox();
            this.snap_btn = new System.Windows.Forms.Button();
            this.snapFile = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.cameraOut)).BeginInit();
            this.SuspendLayout();
            // 
            // str_btn
            // 
            this.str_btn.Location = new System.Drawing.Point(659, 13);
            this.str_btn.Name = "str_btn";
            this.str_btn.Size = new System.Drawing.Size(75, 23);
            this.str_btn.TabIndex = 0;
            this.str_btn.Text = "start";
            this.str_btn.UseVisualStyleBackColor = true;
            this.str_btn.Click += new System.EventHandler(this.str_btn_Click);
            // 
            // cameraOut
            // 
            this.cameraOut.Location = new System.Drawing.Point(13, 13);
            this.cameraOut.Name = "cameraOut";
            this.cameraOut.Size = new System.Drawing.Size(640, 480);
            this.cameraOut.TabIndex = 2;
            this.cameraOut.TabStop = false;
            // 
            // snap_btn
            // 
            this.snap_btn.Location = new System.Drawing.Point(660, 43);
            this.snap_btn.Name = "snap_btn";
            this.snap_btn.Size = new System.Drawing.Size(75, 23);
            this.snap_btn.TabIndex = 3;
            this.snap_btn.Text = "snap";
            this.snap_btn.UseVisualStyleBackColor = true;
            this.snap_btn.Click += new System.EventHandler(this.snap_btn_Click);
            // 
            // snapFile
            // 
            this.snapFile.Location = new System.Drawing.Point(660, 73);
            this.snapFile.Name = "snapFile";
            this.snapFile.Size = new System.Drawing.Size(77, 20);
            this.snapFile.TabIndex = 4;
            // 
            // FaceDetection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 514);
            this.Controls.Add(this.snapFile);
            this.Controls.Add(this.snap_btn);
            this.Controls.Add(this.cameraOut);
            this.Controls.Add(this.str_btn);
            this.Name = "FaceDetection";
            this.Text = "FaceDetection";
            ((System.ComponentModel.ISupportInitialize)(this.cameraOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button str_btn;
        private Emgu.CV.UI.ImageBox cameraOut;
        private System.Windows.Forms.Button snap_btn;
        private System.Windows.Forms.TextBox snapFile;
    }
}