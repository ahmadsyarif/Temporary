using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            withJson();
        }

        static void firstTutorial()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.HostName = "localhost";
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("hello", false, false, false, null);
                        
                        string message = "hello world";
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish("", "hello", null, body);
                        Console.WriteLine("sent " + message);
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error : " + e.ToString());
                Console.ReadKey();
            }
        }
        static void secondTutorial()
        {
            try
            {
                ConnectionFactory factory = new ConnectionFactory();
                factory.HostName = "localhost";
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("hello", false, false, false, null);
                        var property = channel.CreateBasicProperties();
                        property.DeliveryMode = 2;

                        string message = Console.ReadLine();
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish("", "hello", null, body);
                        Console.WriteLine("sent " + message);
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error : " + e.ToString());
                Console.ReadKey();
            }
        }
        static void thirdTutorial()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("logs", "fanout");
                string message = "start";
                while (message != "stop")
                {
                    message = Console.ReadLine();
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("logs", "", null, body);
                    Console.WriteLine("sent : " + message);

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
                //channel.QueueBind(queue.QueueName, "amq.topic", streamKey);
                //Subscription sub = new Subscription(channel, queue.QueueName);

                string message = "start";
                while (message != "stop")
                {
                    Console.Write("name :");
                    string name = Console.ReadLine();
                    message = name;
                    int value;
                    Console.Write("value : ");
                    Int32.TryParse(Console.ReadLine(), out value);
                    Basic basic = new Basic(name, value);
                    string body = JsonConvert.SerializeObject(basic);
                    byte[] buffer = Encoding.UTF8.GetBytes(body);
                    channel.BasicPublish("amq.topic", streamKey, null, buffer);
                    Console.WriteLine("sent : " + basic.ToString());
                }
            }
            catch
            {
                Console.Write("error");
            }
        }
    }
}
