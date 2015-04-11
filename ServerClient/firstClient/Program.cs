using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
namespace firstClient
{
    class Program
    {
        static Socket sck;
        static void Main(string[] args)
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2222);
            try
            {
                sck.Connect(localEndPoint);
                Console.WriteLine("connected to the server");
            }
            catch
            {
                Console.WriteLine("Unable to connect to the server!");
                Main(args);
            }

            Bitmap image;
            MemoryStream ms = new MemoryStream() ;
            byte[] data;
            try
            {
                while (true)
                {
                    image = new Bitmap(@"D\test.jpg");
                    image.Save (ms,ImageFormat.Jpeg);
                    data = ms.ToArray();
                    Console.WriteLine("size : " + ms.Length.ToString());
                    Thread.Sleep(500);
                }
            }
            catch
            {
            }
            

            sck.Close();
        }
    }
}
