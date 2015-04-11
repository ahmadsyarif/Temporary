using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GeneralAI
{
    public partial class Panel : Form
    {
        public Connection connection;
        public DataCollect dataCollect;
        public CommandHandler command;
        public Welcoming welcoming;
      
        public Panel()
        {
            InitializeComponent();
            connection = new Connection();
            dataCollect = new DataCollect();
            command = new CommandHandler();
            welcoming = new Welcoming();
            txt_serverIP.Text = "localhost";
        }

        private void connect_Click(object sender, EventArgs e)
        {
            if (!connection.isConnected)
            {
                connection.connect();
                txt_status.AppendText("connecting to server...\n");
            }
            else
            {
                connection.disconnect();
                
            }
        }

        private void btn_collect_Click(object sender, EventArgs e)
        {
            if (!dataCollect.isCollecting)
            {
                if (dataCollect.startCollecting())
                {
                    txt_status.AppendText("start collecting data...\n");
                    this.btn_collect.Text = "Stop Collecting";
                }
            }
            else if (dataCollect.isCollecting)
            {
                if (dataCollect.stopCollecting())
                {
                    txt_status.AppendText("stop collecting data...\n");
                    this.btn_collect.Text = "Start Collecting";
                }
            }
        }

        private void btn_command_Click(object sender, EventArgs e)
        {
            if (!command.isHandling)
            {
                if (command.startHandling())
                {
                    txt_status.AppendText("activating command...\n");
                    this.btn_command.Text = "Deactivate Command";
                }
            }
            else if (command.isHandling)
            {
                if (command.stopHandling())
                {
                    txt_status.AppendText("deactivating command...\n");
                    this.btn_command.Text = "Activate Command";
                }
            }
        }

        private void btn_behaviour_Click(object sender, EventArgs e)
        {
            if (btn_behaviour.Text == "Start Behavior")
            {
                welcoming.startWelcoming();
                txt_status.AppendText("start welcoming behavior...\n");
                btn_behaviour.Text = "Stop Behaviour";
            }
            else
            {
                btn_behaviour.Text = "Start Behavior";
                txt_status.AppendText("stop welcoming behavior...\n");
                welcoming.stopWelcoming();
            }
        }
        private void btn_test_Click(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            txt_status.Text = d.Hour.ToString();
        }

        NAudio.Wave.WaveIn sourceStream;
        NAudio.Wave.WaveFileWriter streamWriter;
        void test_Click(object sender, EventArgs e)
        {

            if (test.Text == "Record")
            {
                test.Text = "Stop";
                record();

            }
            else
            {
                test.Text = "Record";
                stopRecording();
                
            }
        }
        public void record()
        {
            Console.WriteLine("start recording");
            string filePath = Environment.CurrentDirectory + "/recordingToSend.wav";
            sourceStream = new NAudio.Wave.WaveIn();
            sourceStream.DeviceNumber = 0;
            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, 1);
            streamWriter = new NAudio.Wave.WaveFileWriter(filePath, sourceStream.WaveFormat);
            sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
            sourceStream.StartRecording();

        }
        public void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (streamWriter == null) return;
            streamWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            streamWriter.Flush();
        }
        public void stopRecording()
        {
            Console.WriteLine("stop recording");

            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;

            }
            if (streamWriter != null)
            {
                streamWriter.Dispose();
                streamWriter = null;
            }
            command.LA_speechRecognize(Environment.CurrentDirectory + "/recordingToSend.wav");

        }
        
    }
}
