using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio.Wave;
using NAudio;
using NAudio.WindowsMediaFormat;
using System.Numerics;
using System.IO;
namespace FFT
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            //double[] x = new double[4] { 0.5, 1, 3, 2 };
            //int nf = 4;
            string csvFile = Environment.CurrentDirectory + "/frequency.csv";
            StringBuilder sb = new StringBuilder();
            string newline = string.Format("female,");
            sb.Append(newline);
            Console.WriteLine("buka file wav");
            double[] x = getwaveready("female.wav");
            for (int i = 1; i < 4; i++)
            {
                
                Console.WriteLine(x.Length);
                int nf = 700;
                Console.WriteLine("hitung fft");
                Complex[] X = fft(x, nf, 300);
                Console.WriteLine("cari frekuensi maximum");
                int max = getFreqMax(X);
                Console.WriteLine(max + " " + X[max].Magnitude);
                newline = string.Format("{0},", max.ToString());
                sb.Append(newline);
            }
            sb.Append(string.Format("{0}", Environment.NewLine));
            sb.Append(string.Format("male,"));
            double[] y = getwaveready("male.wav");
            for (int i = 1; i < 4; i++)
            {

                Console.WriteLine(y.Length);
                int nf = 700;
                Console.WriteLine("hitung fft");
                Complex[] X = fft(y, nf, 300);
                Console.WriteLine("cari frekuensi maximum");
                int max = getFreqMax(X);
                Console.WriteLine(max + " " + X[max].Magnitude);
                newline = string.Format("{0},", max.ToString());
                sb.Append(newline);
            }
            sb.Append(string.Format("{0}", Environment.NewLine));
            File.WriteAllText(csvFile, sb.ToString());
        }

        static private Complex[] fft(double[] input, int nfUp,int nfDown)
        {
            int N = input.Length;

            Complex[] output = new Complex[nfUp];
            Console.WriteLine("lenght : " + output.Length);
            for (int k = nfDown; k < nfUp; k++)
            {
                Complex a = new Complex();
                Complex b = new Complex();
                Complex angle;
                for (int n = 0; n <= N / 2 - 1; n++)
                {
                    angle =  - Complex.ImaginaryOne* 2 * Math.PI * n * k * 2 / N;
                    a = a + input[2*n] * Complex.Exp(angle);
                    b = b + input[2 * n + 1] * Complex.Exp(angle);
                }
                Complex koef = -Complex.ImaginaryOne * 2 * Math.PI * k / N;
                output[k] = a + Complex.Exp(koef) * b;
            }
            return output;
        }

        static int getFreqMax(Complex[] spectrum)
        {
            int freq = 0;
            double max = 0.0;
            for (int i = 0; i < spectrum.Length; i++)
            {
                if (max < spectrum[i].Magnitude)
                {
                    freq = i;
                    max = spectrum[i].Magnitude;
                }
            }
            return freq;
        }
        static double[] getwaveready(string filename)
        {

            WaveFileReader reader = new WaveFileReader(Environment.CurrentDirectory + @"\"+filename);
            double a = Math.Log(reader.SampleCount, 2);
            double b = Math.Ceiling(a);
            //Console.WriteLine(reader.SampleCount);
            int n = (int)reader.SampleCount;
            byte[] buffer = new byte[n * 2];

            int byteread = reader.Read(buffer, 0, 2 * (int)reader.SampleCount);
            for (int i = 2 * (int)reader.SampleCount; i < n * 2; i++)
            {
                buffer[i] = 0;
            }

            short[] shortdata = new short[n];
            double [] x = new double[n];
            int sample = 0;
            for (int index = 0; index < n; index++)
            {
                shortdata[index] = BitConverter.ToInt16(buffer, sample);
                x[index] = (double)shortdata[index];
                sample += 2;
            }
            
            return x;

        }
        
    }

   
}
