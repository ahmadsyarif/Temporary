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
    public partial class FaceDetection : Form
    {
        private Capture capture;
        private bool isCapturing = false;
        Image<Gray, byte> grayFrame;
        private HaarCascade haarFrontalFace, haarUpperBody, haarEye;
        public FaceDetection()
        {
            InitializeComponent();
        }
        private void processFrame(object sender, EventArgs e)
        {
            Image<Bgr, byte> ImageCamera = capture.QueryFrame();
            if (ImageCamera != null)
            {
                
                grayFrame = ImageCamera.Convert<Gray, byte>();
                var faces = haarFrontalFace.Detect(grayFrame, 1.4, 4, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(25, 25), Size.Empty);
                foreach(var face in faces)
                {
                    ImageCamera.Draw(face.rect, new Bgr(Color.Green), 3);
                }
                var eyes = haarEye.Detect(grayFrame, 1.4, 4, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(25, 25), Size.Empty);
                foreach (var eye in eyes)
                {
                    ImageCamera.Draw(eye.rect, new Bgr(Color.Blue), 3);
                }
                //var upperBodies = grayFrame.DetectHaarCascade(haarUpperBody, 1.4, 1, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(25, 25))[0];
                //foreach (var upperBody in upperBodies)
                //{
                //    ImageCamera.Draw(upperBody.rect, new Bgr(Color.Red), 3);
                //}
                cameraOut.Image = ImageCamera.Resize(640, 480, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            }
            
        }
        private void str_btn_Click(object sender, EventArgs e)
        {
            if (capture == null)
            {
                try
                {
                    capture = new Capture();
                    haarFrontalFace = new HaarCascade(Environment.CurrentDirectory + "/haarcascade/haarcascade_frontalface_default.xml");
                    haarUpperBody = new HaarCascade(Environment.CurrentDirectory + "/haarcascade/haarcascade_mcs_upperbody.xml");
                    haarEye = new HaarCascade(Environment.CurrentDirectory + "/haarcascade/haarcascade_eye.xml");
                }
                catch
                {

                }
            }
            if (capture != null) 
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

        private void snap_btn_Click(object sender, EventArgs e)
        {
            grayFrame.Save(Environment.CurrentDirectory + "/database/" + snapFile.Text + ".jpg");
        }
    }
}
