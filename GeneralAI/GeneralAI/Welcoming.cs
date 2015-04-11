using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;
using System.Threading;
namespace GeneralAI
{
    //this is program to define behavior in welcoming visitor
    public class Welcoming
    {
        static private bool isStanding = false;
        static System.Timers.Timer t_Stand = new System.Timers.Timer(10000);
        static System.Timers.Timer t_Record = new System.Timers.Timer(3000);
        static DateTime d = new DateTime();
        DataCollect.FaceLocation_callback  faceLocationHandler = new DataCollect.FaceLocation_callback(dataCollect_faceLocReceive);
        DataCollect.FaceName_callback faceNameHandler = new DataCollect.FaceName_callback(dataCollect_faceNameReceive);
        DataCollect.SpeechRecognition_callback speechHandler = new DataCollect.SpeechRecognition_callback(dataCollect_SpeechRecognizedReceive);
        DataCollect.TactileData_callback tactileHandler = new DataCollect.TactileData_callback(dataCollect_TactileDataReceived);
        static private bool hasAsked = false;
        static private bool hasGreet = false;
        static private bool hasWrong = false;
        static string prevName;
        static string greeting;
        static private NAudio.Wave.WaveIn sourceStream;
        static private NAudio.Wave.WaveFileWriter streamWriter;
        static private bool isRecording = false;
        static private bool isFinishRecording = false;
        static private object eventLock = new object();
        public void startWelcoming()
        {
            t_Stand.Elapsed +=new ElapsedEventHandler(t_ElapsedStand);
            Program.panel.dataCollect.faceLocReceive += faceLocationHandler;
            Program.panel.dataCollect.faceNameReceive += faceNameHandler;
            Program.panel.dataCollect.SpeechRecognizedReceive += speechHandler;
            Program.panel.dataCollect.tactileDataReceive += tactileHandler;
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
            Program.panel.dataCollect.faceLocReceive -= faceLocationHandler;
            Program.panel.dataCollect.faceNameReceive -= faceNameHandler;
            Program.panel.dataCollect.SpeechRecognizedReceive -= speechHandler;
            Program.panel.dataCollect.tactileDataReceive -= tactileHandler;
        }
        
        //defenisi event handler
        static public void dataCollect_SpeechRecognizedReceive(object sender, recognizer speech)
        {
            
            string text = speech.result;
            status(text+"\n");
            if ((text.Contains("how") && text.Contains("you"))||(text.Contains("apa") && text.Contains("kabar")))// jika speech recognition mengandung kata "how"
            {
                Program.panel.command.NS_tts("I am fine, what about you?");
            }
            else if ((text.Contains("what") && text.Contains("name")) || (text.Contains("nama") && text.Contains("siapa")))
            {
                Program.panel.command.NS_tts("my name is lumen");
            }
            else if ((text.Contains("what") && text.Contains("doing"))||(text.Contains("ngapain")))
            {
                Program.panel.command.NS_tts("I am explaining this exhebition to you");
            }
            else if (text.Contains("where") && text.Contains("father"))
            {
                Program.panel.command.NS_tts("I don't know about that");
            }
            else if(text.Contains("f***"))
            {
                Program.panel.command.NS_tts("well, fuck you too");
            }
            else if (text.Contains("sit down"))
            {
                Program.panel.command.NS_rest();
            }
            else if (text.Contains("stand up"))
            {
                
                Program.panel.command.NS_goToPosture("Stand", 0.5f);
            }
            else
            {
                Program.panel.command.NS_tts("I don't understand");
            }


        }
        static public void dataCollect_faceLocReceive(object sender, FaceLocation loc)
        {
            status("face detected...\n");
            if (!isStanding)
            {
                Program.panel.command.NS_goToPosture("Stand", 0.5f);
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
        static public void dataCollect_faceNameReceive(object sender, FaceName name)
        {
            //status("incoming face name...\n");
            if (isStanding)
            {
                if (name.Name == "face unrecognize")
                {
                    
                    if (!hasAsked)
                    {
                        //status("face unrecognize...\n");
                        Program.panel.command.NS_tts(greeting + ", may I know your name");
                        prevName = name.Name;
                        hasAsked = true;
                        //hasGreet = true;
                        status("asking name");
                    }
                }
                else
                {
                    //status(name.Name + "\n");
                    if (!hasGreet && !hasWrong)
                    {
                        Program.panel.command.NS_tts(greeting + " " + name.Name + ", how are you?");
                        prevName = name.Name;
                        hasGreet = true;
                        status("greeting");
                        //hasAsked = true;
                    }
                    else if (hasGreet && !hasWrong)
                    {
                        if (name.Name != prevName)
                        {
                            hasWrong = true;
                            status("wrong previous result");
                            Program.panel.command.NS_tts("I am sorry, I mean how are you " + name.Name + "?");
                            prevName = name.Name;
                            //hasAsked = true;
                            hasGreet = false;
                        }
                    }
                }
            }
        }
        static public void dataCollect_TactileDataReceived(object sender, TactileData tactile)
        {
            //if (isFinishRecording)
            //{
            //    Console.WriteLine("stop recording...");
            //    isFinishRecording = false;
            //    stopRecording();
            //    Program.panel.command.LA_speechRecognize(Environment.CurrentDirectory + "/recordingToSend.wav");
            //    Console.WriteLine("sending file to Lumen Audio");
            //}
            //if (!isRecording)
            //{
            //    if (tactile.Values[3] > 0.5f)
            //    {
            //        Console.WriteLine("start recording");
            //        record();
            //        isRecording = true;
            //        if (t_Record.Enabled)
            //        {
            //            t_Record.Enabled = false;
            //            t_Record.Enabled = true;

            //        }
            //        else
            //        {
            //            t_Record.Enabled = true;
            //        }
            //    }
            //}
        }
        

        
        //event handler untuk timer 
        static public void t_ElapsedStand(object sender, EventArgs e)
        {
            if (isStanding)
            {
                status("no face detected...\n");
                Program.panel.command.NS_rest();
                isStanding = false;
                hasGreet = false;
                hasAsked = false;
                hasWrong = false;
            }
        }
        static public void t_Record_Elapsed(object sender, EventArgs e)
        {
            if (isRecording)
            {
                Console.WriteLine("timer finished");
                isFinishRecording = true;
                isRecording = false;
            }
        }


        //fungsi untuk menulis di text box
        static private void status(string text)
        {
            if (Program.panel.InvokeRequired)
            {
                setTextCallback d = new setTextCallback(status);
                Program.panel.Invoke(d, new object[] { text });
            }
            else
            {
                Program.panel.txt_status.AppendText(text);
            }
        }
        private delegate void setTextCallback(string text);


        //fungsi untuk recording
        static private void record()
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
        static private void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (streamWriter == null) return;
            streamWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            streamWriter.Flush();
        }
        static private void stopRecording()
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
