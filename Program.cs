//#define MC
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FinalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferWidth = 91;
            Console.BufferHeight = 9001;

#if MC
            MCFramework mc = new MCFramework();
            mc.run();
#else
            Framework frame = new Framework();
            frame.run();
#endif
        }
    }
}
