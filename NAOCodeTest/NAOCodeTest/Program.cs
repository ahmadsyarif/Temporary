using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using NAudio;
using NAudio.FileFormats;
using NAudio.Wave.WaveFormats;
using NAudio.Utils;
using NAudio.Wave;
using System.IO;
using Aldebaran.Proxies;
namespace NAOCodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            testSpeaker();
        }
        static void testSpeaker()
        {
            int max = 16384;
            Console.WriteLine("mulai..");
            MemoryStream ms = new MemoryStream(File.ReadAllBytes(@"D:\Media-Convert_test6_PCM_Stereo_VBR_16SS_8000Hz.wav"));
            WaveFileReader wfr = new WaveFileReader(ms);
            Console.WriteLine("channel : " + wfr.WaveFormat.Channels);
            Console.WriteLine("bit rate : " + wfr.WaveFormat.BitsPerSample);
            Console.WriteLine("lenght : " + wfr.Length);
            byte[] buffer = new byte[max * 2];
            int flag = wfr.Read(buffer, 0, max*2);
            Console.WriteLine("byte read : " + flag);
            Int16[] buf = new Int16[max];
            int index = 0;

            ArrayList sent = new ArrayList();
            for (int i = 0; i < max; i++)
            {

                buf[i] = BitConverter.ToInt16(buffer, index);
                sent.Add(buf[i]);
                index += 2;
            }

            
            
            

            Console.WriteLine("selesai, sent.length :"+sent.Capacity);
            string ip = "167.205.66.61";
            int port = 9559;

            AudioDeviceProxy audio;
            try
            {
                audio = new AudioDeviceProxy(ip, port);
                audio.flushAudioOutputs();
                audio.setOutputVolume(60);
                audio.sendRemoteBufferToOutput(max, sent);
                Console.WriteLine("finish");
            }
            catch(Exception e)
            {
                Console.WriteLine("error : " + e.ToString());
            }
            
            Console.ReadKey();
            
        }
    }
}
