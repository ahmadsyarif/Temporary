using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using RabbitMQ.Util;
using Newtonsoft.Json;
using System.IO;

namespace Agent
{
    public class CommandHandler
    {
        IModel channelSend;
        public bool isHandling;
        NAudio.Wave.WaveIn sourceStream;
        NAudio.Wave.WaveFileWriter streamWriter;
        public bool isRecording = false;
        public CommandHandler()
        {
            isHandling = false;
        }
        public bool startHandling()
        {
            if (Program.connection.isConnected)
            {
                if (!isHandling)
                {
                    this.channelSend = Program.connection.channelSend;
                    isHandling = true;
                    return true;
                }
                else
                {
                    return false;
                   
                }
            }
            else
            {
                return false;
            }
        }
        public bool stopHandling()
        {
            if (isHandling)
            {
                isHandling = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        private void sendCommand(string command, string routingKey)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(command);
            this.channelSend.BasicPublish("amq.topic", routingKey, null, buffer);
        }
        //define command that will be send to NAO server with NS_ code
        public void NS_wakeUp()
        {
            if (isHandling)
            {
                Command com = new Command { type = "motion", method = "wakeUp" };
                string body = JsonConvert.SerializeObject(com);
                this.sendCommand(body, "avatar.NAO.command");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void NS_rest()
        {
            if (isHandling)
            {
                Command com = new Command { type = "motion", method = "rest" };
                string body = JsonConvert.SerializeObject(com);
                this.sendCommand(body, "avatar.NAO.command");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void NS_setAngles(List<string> jointName, List<float> angles, float speed)
        {
            if (isHandling)
            {
                Parameter par = new Parameter { jointName = jointName, angles = angles, speed = speed };
                Command com = new Command { type = "motion", method = "setAngles", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                this.sendCommand(body, "avatar.NAO.command");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void NS_changeAngles(List<string> jointName, List<float> angles, float speed)
        {
            if (isHandling)
            {
                Parameter par = new Parameter { jointName = jointName, angles = angles, speed = speed };
                Command com = new Command { type = "motion", method = "changeAngles", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                this.sendCommand(body, "avatar.NAO.command");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void NS_setStiffness(List<string> jointName, List<float> stiffnesses)
        {
            if (isHandling)
            {
                Parameter par = new Parameter { jointName = jointName, stiffnessess = stiffnesses };
                Command com = new Command { type = "motion", method = "setStiffnesses", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                this.sendCommand(body, "avatar.NAO.command");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void NS_moveInit()
        {
            if (isHandling)
            {
                Command com = new Command { type = "motion", method = "moveInit" };
                string body = JsonConvert.SerializeObject(com);
                this.sendCommand(body, "avatar.NAO.command");
            }
            else
            {
               // MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void NS_walkTo(float x, float y, float tetha)
        {
            if (isHandling)
            {
                Parameter par = new Parameter { x = x, y = y, tetha = tetha };
                Command com = new Command { type = "motion", method = "moveTo", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                this.sendCommand(body, "avatar.NAO.command");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void NS_tts(string toSay)
        {
            if (isHandling)
            {
                Parameter par = new Parameter { text = toSay };
                Command com = new Command { type = "texttospeech", method = "say", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                this.sendCommand(body, "avatar.NAO.command");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void NS_goToPosture(string Posture, float speed)
        {
            if (isHandling)
            {
                Parameter par = new Parameter { postureName = Posture, speed = speed };
                Command com = new Command { type = "Posture", method = "goToPosture", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                this.sendCommand(body, "avatar.NAO.command");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void NS_sendRemoteBufferToOutput(string filePath)
        {
            if (isHandling)
            {
                byte[] buffer = File.ReadAllBytes(filePath);
                string wav = Convert.ToBase64String(buffer);
                Parameter par = new Parameter { wavFile = wav };
                Command com = new Command { type = "audiodevice", method = "sendremotebuffertooutput", parameter = par };
                string body = JsonConvert.SerializeObject(com);
                this.sendCommand(body, "avatar.NAO.command");
            }
            else
            {
                
            }
        }

        //define command that will be send to Lumen Audio with LA_ code
        public void LA_speechRecognize(string filePath)
        {
            if (isHandling)
            {
                byte[] wavByte = File.ReadAllBytes(filePath);
                string wavString = Convert.ToBase64String(wavByte);
                sound wav = new sound { name = "speech", content = wavString, language = "id" };
                string body = JsonConvert.SerializeObject(wav);
                this.sendCommand(body, "lumen.audio.wav.stream");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void LA_textToSpeech(string toSay)
        {
            if (isHandling)
            {
                textData text = new textData { name = "textToSpeech", text = toSay, date = System.DateTime.Now.ToString() };
                string body = JsonConvert.SerializeObject(text);
                this.sendCommand(body, "lumen.audio.text.stream");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        public void LA_genderIdentify(string filePath)
        {
            if (isHandling)
            {
                byte[] wavByte = File.ReadAllBytes(filePath);
                string wavString = Convert.ToBase64String(wavByte);
                sound wav = new sound { name = "speech", content = wavString };
                string body = JsonConvert.SerializeObject(wav);
                this.sendCommand(body, "lumen.audio.get.wave");
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }


        //define command that will be send to Lumen Motion with LM_ code
        public void LM_startDancing()
        {
        }
        public void LM_basicExpression(string expression)
        {
        }

        //define command for recording
        public void startRecording(string filePath)
        {
            if (isHandling)
            {
                if (!isRecording)
                {
                    sourceStream = new NAudio.Wave.WaveIn();
                    sourceStream.DeviceNumber = 0;
                    sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, 1);
                    streamWriter = new NAudio.Wave.WaveFileWriter(filePath, sourceStream.WaveFormat);
                    sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
                    sourceStream.StartRecording();
                    isRecording = true;
                }
                else
                {
                   // MessageBox.Show("Command Handler is already recording", "CommandHandler");
                }
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }

        }
        public void stopRecording()
        {
            if (isHandling)
            {
                if (isRecording)
                {
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
                    isRecording = false;
                }
                else
                {
                   // MessageBox.Show("Command Handler is not recording", "CommandHandler");
                }
            }
            else
            {
                //MessageBox.Show("Command Handler is not started yet", "CommandHandler");
            }
        }
        private void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (streamWriter == null) return;
            streamWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            streamWriter.Flush();
        }
    }
}
