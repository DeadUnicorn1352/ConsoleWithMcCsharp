using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    static class Manual
    {

        static Dictionary<string, string> CommandManualPair = new Dictionary<string, string>();
        static Manual()
        {

            while (!File.Exists("Man.txt"))
            {
                Directory.SetCurrentDirectory(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
            }

            string[] text = File.ReadAllLines("Man.txt", Encoding.UTF8);
            for (Int32 i = 0; i < text.Length; ++i)
            {
                if (text[i].Length < 10)
                {
                    CommandManualPair.Add(text[i].Trim(), text[++i]);
                }
            }
        }
        public static void GetManual(string s)
        {
            
            try
            {
                Console.WriteLine(CommandManualPair[s]);
            }
            catch(Exception)
            {
                Console.WriteLine("No manual for this command"); 
            }
        }
    }
}
