using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace EmguTrial
{
    public partial class camera : Form
    {
        private Capture capture;
        private bool isCapturing;
        public camera()
        {
            InitializeComponent();
        }
        private void processFrame(object sender, EventArgs e)
        {
            Image<Bgr, byte> image = capture.QueryFrame();
            cameraOut.Image = image.Resize(320,240,Emgu.CV.CvEnum.INTER.CV_INTER_AREA);
        }
        private void str_btn_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                try
                {
                    capture = new Capture();
                }
                catch
                {
                }
            }
            if (capture != null) ;
            {
                if (isCapturing)
                {
                    str_btn.Text = "start";
                    Application.Idle -= processFrame;
                }
                else
                {
                    str_btn.Text = "stop";
                    Application.Idle += processFrame;
                }
                isCapturing = !isCapturing;
            }
        }
        private void release()
        {
            if (capture != null)
            {
                capture.Dispose();
            }
        }
    }
}
