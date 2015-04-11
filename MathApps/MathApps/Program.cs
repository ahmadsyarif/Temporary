using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testMathLibrary;
namespace MathApps
{
    class Program
    {
        static void Main(string[] args)
        {
            add a = new add(1, 2);
            substract b = new substract(4, 3);
            multiply c = new multiply(3, 3);
            division d = new division(4, 3);
            Console.WriteLine("hasil penjumlahan : {0}", a.tryAdd());
            Console.WriteLine("hasil pengurangan : {0}", b.trySubstract());
            Console.WriteLine("hasil perkalian : {0}", c.tryMultiply());
            Console.WriteLine("hasil pembagian : {0}", d.tryDivision());
            Console.Read();
        }
    }
}
