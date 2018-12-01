using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lakehouse.Services
{
    public class ConsoleLogger
    {
        public static void Out(String s)
        {
            Console.WriteLine($"==== DEBUG ====> {s}");
        }
    }
}
