using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;
using System.Threading;
namespace Agent
{
    //this is program to define behavior in welcoming visitor
    public class Welcoming
    {
        private bool isStanding = false;
        System.Timers.Timer t_Stand = new System.Timers.Timer(10000);
        System.Timers.Timer t_Record = new System.Timers.Timer(5000);
        DateTime d = new DateTime();
        private bool hasAsked = false;
        private bool hasGreet = false;
        string prevName;
        string greeting;
        private NAudio.Wave.WaveIn sourceStream;
        private NAudio.Wave.WaveFileWriter streamWriter;
        private bool isRecording = false;
        private bool isFinishRecording = false;
        private object eventLock = new object();
        public void startWelcoming()
        {
            t_Stand.Elapsed += new ElapsedEventHandler(t_ElapsedStand);
            t_Record.Elapsed+=new ElapsedEventHandler(t_Record_Elapsed);
            Program.dataCollect.faceLocReceive +=new DataCollect.FaceLocation_callback(dataCollect_faceLocReceive);
            Program.dataCollect.faceNameReceive +=new DataCollect.FaceName_callback(dataCollect_faceNameReceive);
            Program.dataCollect.SpeechRecognizedReceive +=new DataCollect.SpeechRecognition_callback(dataCollect_SpeechRecognizedReceive);
            Program.dataCollect.tactileDataReceive +=new DataCollect.TactileData_callback(dataCollect_tactileDataReceive);
            if (DateTime.Now.Hour < 11)
            {
                greeting = "good morning";
            }
            else if (DateTime.Now.Hour >= 11 && DateTime.Now.Hour < 18)
            {
                greeting = "good afternoon";
            }
            else if (DateTime.Now.Hour >= 18)
            {
                greeting = "good night";
            }
            Console.WriteLine("starting behavior...");
        }
        public void stopWelcoming()
        {
            Program.dataCollect.faceLocReceive -= new DataCollect.FaceLocation_callback(dataCollect_faceLocReceive);
            Program.dataCollect.faceNameReceive -= new DataCollect.FaceName_callback(dataCollect_faceNameReceive);
            Program.dataCollect.SpeechRecognizedReceive-= new DataCollect.SpeechRecognition_callback(dataCollect_SpeechRecognizedReceive);
            Program.dataCollect.tactileDataReceive -= new DataCollect.TactileData_callback(dataCollect_tactileDataReceive);
        }

        //defenisi event handler
        public void dataCollect_SpeechRecognizedReceive(object sender, recognizer speech)
        {

            string text = speech.result;
            Console.WriteLine("speech : " + text);
           if ((text.Contains("how") && text.Contains("you")) || (text.Contains("apa") && text.Contains("kabar")))// jika speech recognition mengandung kata "how"
            {
                Program.command.NS_tts("I am fine, what about you?");
            }
            else if ((text.Contains("what") && text.Contains("name")) || (text.Contains("nama") && text.Contains("siapa")))
            {
                Program.command.NS_tts("my name is lumen");
            }
            else if ((text.Contains("what") && text.Contains("doing")) || (text.Contains("ngapain")))
            {
                Program.command.NS_tts("I am explaining this exhebition to you");
            }
            else if (text.Contains("where") && text.Contains("father"))
            {
                Program.command.NS_tts("I don't know about that");
            }
            else if (text.Contains("f***"))
            {
                Program.command.NS_tts("well, fuck you too");
            }
            else if (text.Contains("sit down"))
            {
                Program.command.NS_rest();
            }
            else if (text.Contains("stand up"))
            {

                Program.command.NS_goToPosture("Stand", 0.5f);
            }
            else
            {
                Program.command.NS_tts("I don't understand");
            }


        }
        public void dataCollect_faceLocReceive(object sender, FaceLocation loc)
        {
            
            if (!isStanding)
            {
                Program.command.NS_goToPosture("Stand", 0.5f);
                isStanding = true;
            }
            if (!t_Stand.Enabled)
            {
                t_Stand.Enabled = true;
            }
            else
            {
                t_Stand.Enabled = false;
                t_Stand.Enabled = true;
            }
        }
        public void dataCollect_faceNameReceive(object sender, FaceName name)
        {
            
            if (isStanding)
            {
                if (name.Name == "face unrecognize")
                {
                    if (!hasAsked)
                    {
                        Program.command.NS_tts(greeting + ", may I know your name");
                        prevName = name.Name;
                        hasAsked = true;
                        hasGreet = true;
                    }
                }
                else
                {
                    if (!hasGreet)
                    {
                        Program.command.NS_tts(greeting + " " + name.Name + ", how are you?");
                        prevName = name.Name;
                        hasGreet = true;
                        hasAsked = true;
                    }
                    else
                    {
                        if (name.Name != prevName)
                        {
                            Program.command.NS_tts("I am sorry, I mean how are you " + name.Name + "?");
                            prevName = name.Name;
                        }
                    }
                }
            }
        }
        public void dataCollect_tactileDataReceive(object sender, TactileData tactile)
        {
            if (isFinishRecording)
            {
                Console.WriteLine("stop recording...");
                isFinishRecording = false;
                Program.stopRecording();
                Program.command.LA_speechRecognize(Environment.CurrentDirectory + "/recordingToSend.wav");
                Console.WriteLine("sending file to Lumen Audio");
            }
            if (!isRecording)
            {
                if (tactile.Values[3] > 0.5f)
                {
                    Console.WriteLine("start recording");
                    Program.record();
                    isRecording = true;
                    if (t_Record.Enabled)
                    {
                        t_Record.Enabled = false;
                        t_Record.Enabled = true;

                    }
                    else
                    {
                        t_Record.Enabled = true;
                    }
                }
            }
            
            
        }
        //event handler untuk timer 
        public void t_ElapsedStand(object sender, EventArgs e)
        {
            if (isStanding)
            {
               
                Program.command.NS_rest();
                isStanding = false;
                hasGreet = false;
                hasAsked = false;
            }
        }
        public void t_Record_Elapsed(object sender, EventArgs e)
        {
            if (isRecording)
            {
                Console.WriteLine("timer finished");
                isFinishRecording = true;
                isRecording = false;
            }
        }

        //fungsi untuk recording
        private void record()
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
        private void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            Console.WriteLine("sesuatu");
            if (streamWriter == null) 
                return;
            streamWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            streamWriter.Flush();
        }
        private void stopRecording()
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
