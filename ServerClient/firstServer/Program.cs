using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;

namespace firstServer
{
    class Program
    {
        static byte[] buffer { get; set; }
        static Socket sck;
        static void Main(string[] args)
        {
            Bitmap image;
            BinaryWriter bw;
            BinaryReader br;
            MemoryStream msR;
            MemoryStream ms = new MemoryStream();
            byte[] data;
            int size;
            try
            {
                while (true)
                {
                    image = new Bitmap(@"D:\test.jpg");
                    ms = new MemoryStream();
                    bw = new BinaryWriter(ms);
                    bw.Write(1000);
                    Console.WriteLine("size after add image size    : " + ms.Length.ToString());
                    image.Save(ms, ImageFormat.Jpeg);
                    data = ms.ToArray();
                    Console.WriteLine("size after save image        : " + ms.Length.ToString());

                    msR = new MemoryStream(data);
                    br = new BinaryReader(msR);
                    Console.WriteLine("after read buffer            : " + msR.Length.ToString());
                    size = br.ReadInt32();
                    Console.WriteLine("after read size              : " + size);
                    Console.WriteLine("size                         : " + msR.Length.ToString());
                    
                    Thread.Sleep(500);


                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error : "+e.ToString());
            }
            Console.WriteLine("Starting Server Program...!");
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.Bind(new IPEndPoint(0, 2222));
            sck.Listen(2);
            
            Socket accepted = sck.Accept();
            //buffer = new byte[accepted.SendBufferSize];
            //int byteRead = accepted.Receive(buffer);
            //byte[] formatted = new byte[byteRead];
            //for (int i = 0; i < byteRead; i++)
            //{
            //    formatted[i] = buffer[i];
            //}

            //string strData = Encoding.ASCII.GetString(formatted);
            //Console.WriteLine(strData);
            //Console.Read();


            sck.Close();
            accepted.Close();
        }

        private static BinaryWriter BinaryWriter(MemoryStream ms)
        {
            throw new NotImplementedException();
        }
    }
}
