using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class FileReader
    {

        Int32 Pointer;
        Int32 CurrentLine;
        Int32 Offset;
        string[] Text;


        public FileReader(string path)
        {
            Text = File.ReadAllLines(path);
        }

        private void Down()
        {
            if (Offset + 2 < Text.Length - CurrentLine)
            {
                Offset += 2;
            }
                
        }
        private void Up()
        {
            if (Offset - 2 >= 0)
            {
                Offset -= 2;
            }
        }


        private void ParseKeys(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.DownArrow:
                    Down();
                    break;
                case ConsoleKey.UpArrow:
                    Up();
                    break;


            }

        }

        private void ShowText()
        {
            Pointer = Offset;
            CurrentLine = 0;
            while(CurrentLine != CoeficientsHolder.Height  && Pointer + 1 < Text.Length)
            {
                Console.WriteLine(Text[Pointer]);
                Pointer++;
                CurrentLine++;
            }
        }

        public void run()
        {
            ConsoleKey key = 0;
            while (key != ConsoleKey.Escape)
            {
                ShowText();
                key = Console.ReadKey().Key;
                ParseKeys(key);
                Console.Clear();

            }
        }
    }
}
