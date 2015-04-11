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
using System.Data.OleDb;
namespace EmguTrial
{
    public partial class FaceRecognition : Form
    {
        private Capture capture;
        private bool isCapturing = false;

        OleDbConnection connection = new OleDbConnection();
        OleDbDataAdapter dataAdapter;
        DataTable TSTabel = new DataTable();
        int totalRow = 0;
        int rowNumber = 0;

        public FaceRecognition()
        {
            InitializeComponent();
        }

        private void processFrame(object sender, EventArgs e)
        {
            Image<Bgr, byte> ImageCamera = capture.QueryFrame();
            if (ImageCamera != null)
            {

                Image<Gray, byte> grayFrame = ImageCamera.Convert<Gray, byte>();
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
            
        }
        private void connectToDatabase()
        {
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\Users\Ahmad Syarif\Documents\Visual Studio 2010\Projects\EmguTrial\EmguTrial\bin\Debug\database\face.mdb";
            connection.Open();
            dataAdapter = new OleDbDataAdapter("Select * From TrainingSet1", connection);
            OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dataAdapter);
            dataAdapter.Fill(TSTabel);
            if (TSTabel.Rows.Count != 0)
            {
                totalRow = TSTabel.Rows.Count;
                MessageBox.Show("load succesfull");
            }
            
        }

        private void load_btn_Click(object sender, EventArgs e)
        {
            if (load_btn.Text == "load")
            {
                connectToDatabase();
                load_btn.Text = "loaded";
            }

        }
    }
}
