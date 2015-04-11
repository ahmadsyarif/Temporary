using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FFT
{
    class fftclass
    {
        public float getMaxFreq(string filePath)
        {
            float hasil = 0.0f;
            byte[] buffer = File.ReadAllBytes(filePath);
            float[] data = convertToData(buffer);
            return hasil;

        }

        private float[] convertToData(byte[] data)
        {
            float[] hasil = new float[100];
            return hasil;
        }

    }
}
