using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World nevo");

            HashSet<string> hash = new HashSet<string>();

            string a = "a";
            string b = "a";
            string c = "a";

            hash.Add(a);
            hash.Add(b);
            hash.Add(c);

            int aa = a.GetHashCode();

            Console.WriteLine(aa);
            string p = Console.ReadLine();




        }
    }
}
