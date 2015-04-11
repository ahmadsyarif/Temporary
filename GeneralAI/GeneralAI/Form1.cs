using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System.IO;
using Newtonsoft.Json;
using System.Timers;
namespace GeneralAI
{
    public partial class Form1 : Form
    {
        static IModel channelSend;
        static IModel channelReceiveVisual;
        static IModel channelReceiveAudio;
        static IModel channelReceiveGender;
        static EventingBasicConsumer consumerAudio, consumerVisual,consumerGender;
        static string audioKeySendWav = "lumen.audio.wav.stream";
        static string audioKeyGetString = "lumen.audio.speech.recognition";
        static string visualKeyGet = "lumen.visual.face.detection";
        static string commandKey = "avatar.NAO.command";
        static string genderKey = "lumen.audio.get.wave";
        static string genderResultKey = "lumen.audio.gender.identification";
        static bool isStand = false;
        static IConnection connection;
        static bool isConnected = false;
        static System.Timers.Timer t = new System.Timers.Timer(5000);
        public Form1()
        {
            
            InitializeComponent();
            initialize();
            
        }
        private void initialize()
        {
            serverIP.Text = "localhost";
        }
        private void Connect_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                try
                {
                    ConnectionFactory factory = new ConnectionFactory();
                    factory.Uri = "amqp://lumen:lumen@" + serverIP.Text + "/%2F";
                    connection = factory.CreateConnection();
                    channelSend = connection.CreateModel();
                    channelReceiveVisual = connection.CreateModel();
                    channelReceiveAudio = connection.CreateModel();
                    channelReceiveGender = connection.CreateModel();

                    QueueDeclareOk audioQueue = channelReceiveAudio.QueueDeclare("", true, false, true, null);
                    channelReceiveAudio.QueueBind(audioQueue.QueueName, "amq.topic", audioKeyGetString);
                    consumerAudio = new EventingBasicConsumer(channelReceiveAudio);
                    channelReceiveAudio.BasicConsume(audioQueue.QueueName, true, consumerAudio);
                    consumerAudio.Received += new BasicDeliverEventHandler(consumerAudio_Received);
                    incomingCommand += new VoiceCommandCallback(voiceCommandHandling);

                    QueueDeclareOk visualQueue = channelReceiveVisual.QueueDeclare("", true, false, true, null);
                    channelReceiveVisual.QueueBind(visualQueue.QueueName, "amq.topic", visualKeyGet);
                    consumerVisual = new EventingBasicConsumer(channelReceiveVisual);
                    channelReceiveVisual.BasicConsume(visualQueue.QueueName, true, consumerVisual);
                    consumerVisual.Received+=new BasicDeliverEventHandler(consumerVisual_Received);

                    QueueDeclareOk genderQueue = channelReceiveGender.QueueDeclare("", true, false, true, null);
                    channelReceiveGender.QueueBind(genderQueue.QueueName, "amq.topic", genderResultKey);
                    consumerGender = new EventingBasicConsumer(channelReceiveGender);
                    channelReceiveGender.BasicConsume(genderQueue.QueueName, true, consumerGender);
                    consumerGender.Received += new BasicDeliverEventHandler(consumerGender_Received);
                    

                    MessageBox.Show("connected to server", "connection status");
                    isConnected = true;
                    Connect.Text = "disconnect";
                }
                catch (Exception error)
                {
                    MessageBox.Show("unable to connect to server : " + error.Message, "connection status");
                }
                
            }
            else if (isConnected)
            {
                if (connection != null)
                {
                    connection.Close();
                    Connect.Text = "connect";
                    MessageBox.Show("connection is disconnected", "connection status");
                    isConnected = false;
                }
            }
        }
        private void consumerGender_Received(object sender, BasicDeliverEventArgs ev)
        {
            Console.WriteLine("incoming speech recognition");
            string data = Encoding.UTF8.GetString(ev.Body);
            genderResult rec;
            JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects };
            rec = JsonConvert.DeserializeObject<genderResult>(data, setting);
            if (this.result.InvokeRequired)
            {
                setTextCallback d = new setTextCallback(setText);
                this.Invoke(d, new object[] { rec.gender });
            }
            else
            {
                this.result.Text = rec.gender;
            }
           
        }
        private void consumerAudio_Received(object sender, BasicDeliverEventArgs ev)
        {
            Console.WriteLine("incoming speech recognition");
            string data = Encoding.UTF8.GetString(ev.Body);
            recognizer rec;
            JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Objects };
            rec = JsonConvert.DeserializeObject<recognizer>(data, setting);
            if (incomingCommand != null)
            {
                incomingCommand(this, rec.result);
            }
            setText(rec.result);
        }
        private void consumerVisual_Received(object sender, BasicDeliverEventArgs ev)
        {
            string data = Encoding.UTF8.GetString(ev.Body);
            if (faceDetected != null)
            {
                faceDetected(this, data);
            }
        }
        private delegate void setTextCallback(string text);
        private delegate void VoiceCommandCallback(object sender, string command);
        private delegate void VisualCallback(object sender, string data);
        VisualCallback faceCallback = new VisualCallback(faceDetectedHandling);
        VisualCallback timerFace = new VisualCallback(timerHandling);
        private event VisualCallback faceDetected;
        private event VoiceCommandCallback incomingCommand;
        private void voiceCommandHandling(object sender, string command)
        {
            bool isContainName = command.Contains("name");
            bool isContainAge = command.Contains("old");
            bool isContainStand = command.Contains("play");
            bool isContainRest = command.Contains("sit");
            
            if (isContainName)
            {
                Parameter par = new Parameter { text = "my name is lumen" };
                Command com = new Command { type = "texttospeech", method = "say", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                byte[] buffer = Encoding.UTF8.GetBytes(body);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer);
            }
            else if (isContainAge)
            {
                Parameter par = new Parameter { text = "I am very young" };
                Command com = new Command { type = "texttospeech", method = "say", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                byte[] buffer = Encoding.UTF8.GetBytes(body);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer);

            }
            else if (isContainStand)
            {
                Parameter par = new Parameter {postureName= "Stand", speed = 0.8f };
                Command com = new Command { type = "posture", method = "gotoposture", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                byte[] buffer = Encoding.UTF8.GetBytes(body);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer);
                Parameter par1 = new Parameter { text = "ok, let's play together" };
                Command com1 = new Command { type = "texttospeech", method = "say", parameter = par1 };
                string body1 = JsonConvert.SerializeObject(com1);
                byte[] buffer1 = Encoding.UTF8.GetBytes(body1);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer1);
            }
            else if ((command.Contains("what") && command.Contains("doing"))||(command.Contains("ngapain")))
            {
                Parameter par = new Parameter { text = "I am talking with you" };
                Command com = new Command { type = "texttospeech", method = "say", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                byte[] buffer = Encoding.UTF8.GetBytes(body);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer);
            }
            else if ((command.Contains("how") && command.Contains("you"))||(command.Contains("kabar")))
            {
                Parameter par = new Parameter { text = "I am fine, what about you?" };
                Command com = new Command { type = "texttospeech", method = "say", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                byte[] buffer = Encoding.UTF8.GetBytes(body);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer);
            }
            else
            {
                Parameter par = new Parameter { text = "I don't understand" };
                Command com = new Command { type = "texttospeech", method = "say", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                byte[] buffer = Encoding.UTF8.GetBytes(body);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer);
            }
        }
        static private void timerHandling(object sender, string data)
        {
            if (!t.Enabled)
            {
                t.Enabled = true;
            }
            else if(t.Enabled)
            {
                t.Enabled = false;
                t.Enabled = true;
            }
        }
        static private void faceDetectedHandling(object sender, string data)
        {
            if (!isStand)
            {
                Parameter par = new Parameter { postureName = "Stand", speed = 0.8f };
                Command com = new Command { type = "posture", method = "gotoposture", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                byte[] buffer = Encoding.UTF8.GetBytes(body);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer);

                Parameter par1 = new Parameter { text = "Hello, what can I help you?" };
                Command com1 = new Command { type = "texttospeech", method = "say", parameter = par1 };
                string body1 = JsonConvert.SerializeObject(com1);
                byte[] buffer1 = Encoding.UTF8.GetBytes(body1);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer1);
                isStand = true;
            }
        }
        private void setText(string text)
        {
            if (this.result.InvokeRequired)
            {
                setTextCallback d = new setTextCallback(setText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.result.Text = text;
            }
        }
        private void sendWav_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                OpenFileDialog file = new OpenFileDialog();
                file.ShowDialog();
                if (file.FileName != null)
                {
                    byte[] wavByte = File.ReadAllBytes(file.FileName);
                    string wavString = Convert.ToBase64String(wavByte);
                    sound wav = new sound { name = "test", content = wavString };
                    string body = JsonConvert.SerializeObject(wav);
                    byte[] buffer = Encoding.UTF8.GetBytes(body);
                    channelSend.BasicPublish("amq.topic", audioKeySendWav, null, buffer);
                }
            }
            else
            {
                MessageBox.Show("not connected to server", "connection status");
            }
        }
        NAudio.Wave.WaveFileWriter waveWriter = null;
        NAudio.Wave.WaveIn sourceStream = null;
        bool isRecording = false;
        private void record_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (!isRecording)
                {
                    Console.WriteLine("start recording");
                    sourceStream = new NAudio.Wave.WaveIn();
                    sourceStream.DeviceNumber = 0;
                    sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, 1);
                    string fileName = Environment.CurrentDirectory + @"/recording.wav";
                    waveWriter = new NAudio.Wave.WaveFileWriter(fileName, sourceStream.WaveFormat);
                    sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
                    sourceStream.StartRecording();
                    record.Text = "stop";
                    isRecording = true;
                }
                else if (isRecording)
                {

                    Console.WriteLine("stop recording");
                    if (sourceStream != null)
                    {
                        sourceStream.StopRecording();
                        sourceStream.Dispose();
                        sourceStream = null;
                    }
                    if (waveWriter != null)
                    {
                        waveWriter.Dispose();
                        waveWriter = null;
                    }
                    record.Text = "record";
                    isRecording = false;
                    byte[] wavByte = File.ReadAllBytes(Environment.CurrentDirectory + @"\recording.wav");
                    string wavString = Convert.ToBase64String(wavByte);
                    sound wav = new sound { name = "test", content = wavString,language = "en-us" };
                    string body = JsonConvert.SerializeObject(wav);
                    byte[] buffer = Encoding.UTF8.GetBytes(body);
                    channelSend.BasicPublish("amq.topic", audioKeySendWav, null, buffer);
                }
            }
            else
            {
                MessageBox.Show("not connected to server", "connection status");
            }
        }
        private void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (waveWriter == null) return;
            Console.WriteLine("sesuatu");
            waveWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }
        private void rest_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                Command com1 = new Command { type = "motion", method = "rest" };
                string body1 = JsonConvert.SerializeObject(com1);
                byte[] buffer1 = Encoding.UTF8.GetBytes(body1);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer1);
                isStand = false;
            }
        }
        ElapsedEventHandler timeElapsed = new ElapsedEventHandler(timerFinished);
        static private void timerFinished(object sender,ElapsedEventArgs e)
        {
            Console.WriteLine("no face detected");
            if (isStand)
            {
                Command com1 = new Command { type = "motion", method = "rest" };
                string body1 = JsonConvert.SerializeObject(com1);
                byte[] buffer1 = Encoding.UTF8.GetBytes(body1);
                channelSend.BasicPublish("amq.topic", commandKey, null, buffer1);
                isStand = false;
            }
        }
        private void faceDec_Click(object sender, EventArgs e)
        {
            if (faceDec.Text == "Detect")
            {
                faceDetected += faceCallback;
                faceDetected += timerFace;
                t.Elapsed += timeElapsed;
                faceDec.Text = "Undetect";
            }
            else if (faceDec.Text == "Undetect")
            {
                faceDetected -= faceCallback;
                faceDetected -= timerFace;
                t.Elapsed -= timeElapsed;
                faceDec.Text = "Detect";
            }
        }

        private void identify_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                if (!isRecording)
                {
                    Console.WriteLine("start recording");
                    sourceStream = new NAudio.Wave.WaveIn();
                    sourceStream.DeviceNumber = 0;
                    sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, 1);
                    string fileName = Environment.CurrentDirectory + @"\recording.wav";
                    waveWriter = new NAudio.Wave.WaveFileWriter(fileName, sourceStream.WaveFormat);
                    sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
                    sourceStream.StartRecording();
                    identify.Text = "stop";
                    isRecording = true;
                }
                else if (isRecording)
                {

                    Console.WriteLine("stop recording");
                    if (sourceStream != null)
                    {
                        sourceStream.StopRecording();
                        sourceStream.Dispose();
                        sourceStream = null;
                    }
                    if (waveWriter != null)
                    {
                        waveWriter.Dispose();
                        waveWriter = null;
                    }
                    identify.Text = "identify";
                    isRecording = false;
                    byte[] wavByte = File.ReadAllBytes(Environment.CurrentDirectory + @"\recording.wav");
                    string wavString = Convert.ToBase64String(wavByte);
                    sound wav = new sound { name = "test", content = wavString };
                    string body = JsonConvert.SerializeObject(wav);
                    byte[] buffer = Encoding.UTF8.GetBytes(body);
                    channelSend.BasicPublish("amq.topic", genderKey, null, buffer);
                }
            }
            else
            {
                MessageBox.Show("not connected to server", "connection status");
            }
        }

        
        
    }
}
