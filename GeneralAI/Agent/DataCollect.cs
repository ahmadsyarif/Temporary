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
namespace Agent
{
    public class DataCollect
    {
        public FaceLocation faceLoc;
        public FaceName faceName;
        public UpperBodyLocation upperBodyLocation;
        public soundResult textToSpeech;
        public recognizer speechRecognized;
        public genderResult gender;
        public bool isCollecting = false;
        public DataCollect()
        {
            Console.WriteLine("creating new DataCollect");
        }
        public bool startCollecting()
        {
            
            if (Program.connection.isConnected)
            {
                if (!isCollecting)
                {
                    Console.WriteLine("start Collecting data");
                    Program.connection.consumerVisual1.Received += new BasicDeliverEventHandler(consumerVisual1_Received);
                    Program.connection.consumerVisual2.Received += new BasicDeliverEventHandler(consumerVisual2_Received);
                    Program.connection.consumerVisual3.Received += new BasicDeliverEventHandler(consumerVisual3_Received);
                    Program.connection.consumerAudio1.Received += new BasicDeliverEventHandler(consumerAudio1_Received);
                    Program.connection.consumerAudio2.Received += new BasicDeliverEventHandler(consumerAudio2_Received);
                    Program.connection.consumerAudio3.Received += new BasicDeliverEventHandler(consumerAudio3_Received);
                    Program.connection.consumerAvatar1.Received += new BasicDeliverEventHandler(consumerAvatar1_Received);
                    isCollecting = true;
                    return true;
                }
                else
                {
                    //MessageBox.Show("Data Collecting is already running!", "DataCollect");
                    return false;
                }
            }
            else
            {
                //MessageBox.Show("Not Connected to Server!", "DataCollect");
                return false;
            }
        }

        public bool stopCollecting()
        {
            if (isCollecting)
            {
                Program.connection.consumerVisual1.Received -= new BasicDeliverEventHandler(consumerVisual1_Received);
                Program.connection.consumerVisual2.Received -= new BasicDeliverEventHandler(consumerVisual2_Received);
                Program.connection.consumerVisual3.Received -= new BasicDeliverEventHandler(consumerVisual3_Received);
                Program.connection.consumerAudio1.Received -= new BasicDeliverEventHandler(consumerAudio1_Received);
                Program.connection.consumerAudio2.Received -= new BasicDeliverEventHandler(consumerAudio2_Received);
                Program.connection.consumerAudio3.Received -= new BasicDeliverEventHandler(consumerAudio3_Received);
                isCollecting = false;
                return true;
            }
            else
            {
                //MessageBox.Show("Data Collecting is already not running!", "DataCollect");
                return false;
            }
        }
        //defenisi semua event handler untuk consumer
        public delegate void FaceLocation_callback(object sender, FaceLocation faceLoc);
        public event FaceLocation_callback faceLocReceive;
        public void consumerVisual1_Received(object sender, BasicDeliverEventArgs ev)
        {
            //melakukan query terhadap face location
            string body = Encoding.UTF8.GetString(ev.Body);
            JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            faceLoc = JsonConvert.DeserializeObject<FaceLocation>(body, setting);
            if (faceLocReceive != null)
            {
                faceLocReceive(this, faceLoc);
            }
        }

        public delegate void FaceName_callback(object sender, FaceName name);
        public event FaceName_callback faceNameReceive;
        public void consumerVisual2_Received(object sender, BasicDeliverEventArgs ev)
        {
            //melakukan query terhadap face recognition
            string body = Encoding.UTF8.GetString(ev.Body);
            JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            faceName = JsonConvert.DeserializeObject<FaceName>(body, setting);
            if (this.faceNameReceive != null)
            {
                this.faceNameReceive(this, faceName);
            }
        }

        //belum ada event handler nya
        public void consumerVisual3_Received(object sender, BasicDeliverEventArgs ev)
        {
            //melakukan query terhadap human detection
            string body = Encoding.UTF8.GetString(ev.Body);
            JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            upperBodyLocation = JsonConvert.DeserializeObject<UpperBodyLocation>(body, setting);
        }

        //belum ada event handler nya
        public void consumerAudio1_Received(object sender, BasicDeliverEventArgs ev)
        {
            //melakukan query terhadap text to speech
            string body = Encoding.UTF8.GetString(ev.Body);
            JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            textToSpeech = JsonConvert.DeserializeObject<soundResult>(body, setting);
        }

        public delegate void SpeechRecognition_callback(object sender, recognizer r);
        public event SpeechRecognition_callback SpeechRecognizedReceive;
        public void consumerAudio2_Received(object sender, BasicDeliverEventArgs ev)
        {
            //melakukan query terhadap speech recognition
            string body = Encoding.UTF8.GetString(ev.Body);
            JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            speechRecognized = JsonConvert.DeserializeObject<recognizer>(body, setting);
            if (this.SpeechRecognizedReceive != null)
            {
                this.SpeechRecognizedReceive(this, speechRecognized);
            }
        }

        //belum ada event handler nya
        public void consumerAudio3_Received(object sender, BasicDeliverEventArgs ev)
        {
            //melakukan query terhadap gender identification
            string body = Encoding.UTF8.GetString(ev.Body);
            JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            gender = JsonConvert.DeserializeObject<genderResult>(body, setting);
        }

        public delegate void TactileData_callback(object sender, TactileData t);
        public event TactileData_callback tactileDataReceive;
        public void consumerAvatar1_Received(object sender, BasicDeliverEventArgs ev)
        {
            string body = Encoding.UTF8.GetString(ev.Body);
            JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
            TactileData t = JsonConvert.DeserializeObject<TactileData>(body, setting);
            //Console.WriteLine("incoming...");
            if (this.tactileDataReceive != null)
            {
                this.tactileDataReceive(this, t);
            }
        }



    }
}
