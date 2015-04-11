using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System.Threading;
using Newtonsoft.Json;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            withJson();
        }

        static void firstTutorial()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("hello", false, consumer);

                    Console.WriteLine("waiting...");
                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(message);
                    }
                }
            }
        }
        static void secondTutorial()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("hello", false, consumer);

                    Console.WriteLine("waiting...");
                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine(message);

                        int dots = message.Split('.').Length - 1;
                        Thread.Sleep(dots * 1000);
                        Console.WriteLine("done");
                    }
                }
            }
        }
        static void thirdTutorial()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("logs", "fanout");
                    var queueName = channel.QueueDeclare().QueueName;
                    channel.QueueBind(queueName, "logs", "");
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName, true, consumer);
                    Console.WriteLine("waiting...");
                    while (true)
                    {
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("message : " + message);

                    }
                }
            }
        }
        static void withJson()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.Uri = "amqp://guest:guest@localhost/%2F";
                IConnection connection = factory.CreateConnection();
                IModel channel = connection.CreateModel();
                connection.AutoClose = true;

                QueueDeclareOk queue = channel.QueueDeclare("", false, true, true, null);
                string streamKey = "log.data";
                channel.QueueBind(queue.QueueName, "amq.topic", streamKey);
                Subscription sub = new Subscription(channel, queue.QueueName);
                BasicDeliverEventArgs ev;
                Console.WriteLine("waiting..");
                while (true)
                {
                    if (sub.Next(0, out ev))
                    {
                        string body = Encoding.UTF8.GetString(ev.Body);
                        JsonSerializerSettings setting = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects };
                        Basic basic = JsonConvert.DeserializeObject<Basic>(body,setting);
                        Console.WriteLine("message : "+basic.ToString());
                    }
                }
            }
            catch(Exception error)
            {
                Console.Write("error : "+error.ToString());
            }
        }
    }
}
