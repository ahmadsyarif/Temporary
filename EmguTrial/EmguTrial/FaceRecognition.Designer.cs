namespace EmguTrial
{
    partial class FaceRecognition
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
            this.cameraOut = new Emgu.CV.UI.ImageBox();
            this.str_btn = new System.Windows.Forms.Button();
            this.load_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cameraOut)).BeginInit();
            this.SuspendLayout();
            // 
            // cameraOut
            // 
            this.cameraOut.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.cameraOut.Location = new System.Drawing.Point(13, 13);
            this.cameraOut.Name = "cameraOut";
            this.cameraOut.Size = new System.Drawing.Size(640, 480);
            this.cameraOut.TabIndex = 2;
            this.cameraOut.TabStop = false;
            // 
            // str_btn
            // 
            this.str_btn.Location = new System.Drawing.Point(663, 13);
            this.str_btn.Name = "str_btn";
            this.str_btn.Size = new System.Drawing.Size(75, 23);
            this.str_btn.TabIndex = 3;
            this.str_btn.Text = "start";
            this.str_btn.UseVisualStyleBackColor = true;
            this.str_btn.Click += new System.EventHandler(this.str_btn_Click);
            // 
            // load_btn
            // 
            this.load_btn.Location = new System.Drawing.Point(663, 43);
            this.load_btn.Name = "load_btn";
            this.load_btn.Size = new System.Drawing.Size(75, 23);
            this.load_btn.TabIndex = 4;
            this.load_btn.Text = "load";
            this.load_btn.UseVisualStyleBackColor = true;
            this.load_btn.Click += new System.EventHandler(this.load_btn_Click);
            // 
            // FaceRecognition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 566);
            this.Controls.Add(this.load_btn);
            this.Controls.Add(this.str_btn);
            this.Controls.Add(this.cameraOut);
            this.Name = "FaceRecognition";
            this.Text = "FaceRecognition";
            ((System.ComponentModel.ISupportInitialize)(this.cameraOut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox cameraOut;
        private System.Windows.Forms.Button str_btn;
        private System.Windows.Forms.Button load_btn;
    }
}