using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using RabbitMQ.Util;

namespace Agent
{
    public class Connection
    {
        public bool isConnected = false;
        public IModel channelSend;
        private IModel channelVisual1, channelVisual2, channelVisual3, channelAudio1, channelAudio2, channelAudio3, channelAvatar1;
        public EventingBasicConsumer consumerVisual1, consumerVisual2, consumerVisual3, consumerAudio1, consumerAudio2, consumerAudio3, consumerAvatar1;
        IConnection connection;
        public Connection()
        {
            Console.WriteLine("creating new Connection");
            isConnected = false;
        }
        public void connect()
        {
            if (!isConnected)
            {
                try
                {
                    string routingKey;
                    ConnectionFactory factory = new ConnectionFactory();
                    factory.Uri = "amqp://lumen:lumen@localhost/%2F";
                    connection = factory.CreateConnection();
                    channelSend = connection.CreateModel(); // untuk mengirim
                    channelVisual1 = connection.CreateModel(); // untuk menerima face.detection
                    channelVisual2 = connection.CreateModel(); // untuk menerima face.recognition
                    channelVisual3 = connection.CreateModel(); // untuk menerima human.detection
                    channelAudio1 = connection.CreateModel(); // untuk menerima text.to.speech
                    channelAudio2 = connection.CreateModel(); // untuk menerima speech.recognition
                    channelAudio3 = connection.CreateModel(); // untuk menerima gender.identification
                    channelAvatar1 = connection.CreateModel(); // untuk menerima data dari NAO, untuk kasus ini tactile sensor

                    //menyiapkan consumer untuk visual 1
                    QueueDeclareOk queueVisual1 = channelVisual1.QueueDeclare("", true, false, true, null);
                    routingKey = "lumen.visual.face.detection";
                    channelVisual1.QueueBind(queueVisual1.QueueName, "amq.topic", routingKey);
                    consumerVisual1 = new EventingBasicConsumer(channelVisual1);
                    channelVisual1.BasicConsume(queueVisual1.QueueName, true, consumerVisual1);

                    //menyiapkan consumer untuk visual 2
                    QueueDeclareOk queueVisual2 = channelVisual2.QueueDeclare("", true, false, true, null);
                    routingKey = "lumen.visual.face.recognition";
                    channelVisual2.QueueBind(queueVisual2.QueueName, "amq.topic", routingKey);
                    consumerVisual2 = new EventingBasicConsumer(channelVisual2);
                    channelVisual2.BasicConsume(queueVisual2.QueueName, true, consumerVisual2);

                    //menyiapkan consumer untuk visual 3
                    QueueDeclareOk queueVisual3 = channelVisual2.QueueDeclare("", true, false, true, null);
                    routingKey = "lumen.visual.human.detection";
                    channelVisual3.QueueBind(queueVisual3.QueueName, "amq.topic", routingKey);
                    consumerVisual3 = new EventingBasicConsumer(channelVisual3);
                    channelVisual3.BasicConsume(queueVisual3.QueueName, true, consumerVisual3);

                    //menyiapkan consumer untuk audio 1
                    QueueDeclareOk queueAudio1 = channelAudio1.QueueDeclare("", true, false, true, null);
                    routingKey = "lumen.audio.text.to.speech";
                    channelAudio1.QueueBind(queueAudio1.QueueName, "amq.topic", routingKey);
                    consumerAudio1 = new EventingBasicConsumer(channelAudio1);
                    channelAudio1.BasicConsume(queueAudio1.QueueName, true, consumerAudio1);

                    //menyiapkan consumer untuk audio 2
                    QueueDeclareOk queueAudio2 = channelAudio1.QueueDeclare("", true, false, true, null);
                    routingKey = "lumen.audio.speech.recognition";
                    channelAudio2.QueueBind(queueAudio2.QueueName, "amq.topic", routingKey);
                    consumerAudio2 = new EventingBasicConsumer(channelAudio2);
                    channelAudio2.BasicConsume(queueAudio2.QueueName, true, consumerAudio2);

                    //menyiapkan consumer untuk audio 1
                    QueueDeclareOk queueAudio3 = channelAudio1.QueueDeclare("", true, false, true, null);
                    routingKey = "lumen.audio.gender.identification";
                    channelAudio3.QueueBind(queueAudio3.QueueName, "amq.topic", routingKey);
                    consumerAudio3 = new EventingBasicConsumer(channelAudio3);
                    channelAudio3.BasicConsume(queueAudio3.QueueName, true, consumerAudio3);

                    QueueDeclareOk queueAvatar1 = channelAvatar1.QueueDeclare("", true, false, true, null);
                    routingKey = "avatar.NAO.data.tactile";
                    channelAvatar1.QueueBind(queueAvatar1.QueueName, "amq.topic", routingKey);
                    consumerAvatar1 = new EventingBasicConsumer(channelAvatar1);
                    channelAvatar1.BasicConsume(queueAvatar1.QueueName, true, consumerAvatar1);

                    isConnected = true;
                    Console.WriteLine("program is connected to server");
                    //Program.panel.btn_connect.Text = "Disconnect";
                }
                catch
                {
                    //MessageBox.Show("unable to connect to server", "connection");
                }
            }
            else
            {
                //MessageBox.Show("already connected to server!", "connection");
            }
        }

        public void disconnect()
        {
            if (isConnected)
            {
                if (!this.isProcessRunning())
                {
                    isConnected = false;
                    //Program.panel.btn_connect.Text = "Connect";
                    this.connection.Close();
                }
                else
                {
                    //MessageBox.Show("Stop Process Before Disconnecting", "Connection");
                }
            }
        }

        public bool isProcessRunning()
        {
            return true;
            //if ((Program.panel.dataCollect.isCollecting) || (Program.panel.command.isHandling))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }

}
