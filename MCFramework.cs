using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinalProject
{
    class MCFramework
    {
        private LeftPart  LeftPart;
        private RightPart RightPart;
        private APartBase APartBase;
        private char[,] ConsoleMatrix;
        // [ столбец,строка]

        public MCFramework()
        {
            LeftPart = new LeftPart();
            RightPart = new RightPart();

            LeftPart.SetAvtivity(true);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
        }
        public void FillTheBorders()
        {
            ConsoleMatrix = new char[CoeficientsHolder.Height,CoeficientsHolder.RightPart];
            string Name = "Name";
            string Size = "Size";
            string ModTime = "ModTime";
            Console.BackgroundColor = ConsoleColor.Blue;
            //рисуем грацины
            for (var i = 1; i < CoeficientsHolder.RightPart - 1; i++)
            {
                ConsoleMatrix[0, i] = '═';
                ConsoleMatrix[CoeficientsHolder.Height - 1, i] = '═';
            }
            for (var i = 1; i < CoeficientsHolder.Height - 1; i++)
            {
                ConsoleMatrix[i, 0] = '║';
                ConsoleMatrix[i, CoeficientsHolder.RightPart - 1] = '║';
                ConsoleMatrix[i, CoeficientsHolder.LeftPart - 1] = '║';
                ConsoleMatrix[i, CoeficientsHolder.LeftPart ] = '║';


                ConsoleMatrix[i, CoeficientsHolder.LeftPart  - CoeficientsHolder.SizeHolderCoef] = '│'; // Size
                ConsoleMatrix[i, CoeficientsHolder.LeftPart  - CoeficientsHolder.TimeHolderCoef] = '│'; // Time
                ConsoleMatrix[i, CoeficientsHolder.RightPart    - CoeficientsHolder.SizeHolderCoef] = '│';
                ConsoleMatrix[i, CoeficientsHolder.RightPart    - CoeficientsHolder.TimeHolderCoef] = '│';
            }
            //Настройка левого окна
            ConsoleMatrix[0, 0] = '╔';
            ConsoleMatrix[0, CoeficientsHolder.LeftPart - 1] = '╗';
            ConsoleMatrix[CoeficientsHolder.Height - 1, 0] = '╚';
            ConsoleMatrix[CoeficientsHolder.Height - 1, CoeficientsHolder.LeftPart - 1] = '╝';

            //Настройка правого окна
            ConsoleMatrix[0, CoeficientsHolder.LeftPart] = '╔';
            ConsoleMatrix[0, CoeficientsHolder.RightPart - 1] = '╗';
            ConsoleMatrix[CoeficientsHolder.Height - 1, CoeficientsHolder.LeftPart] = '╚';
            ConsoleMatrix[CoeficientsHolder.Height - 1, CoeficientsHolder.RightPart - 1] = '╝';
            //Вставка Name,Size,ModTime
            for(int i = 0; i < Name.Length; ++i)
            {
                ConsoleMatrix[1, (CoeficientsHolder.LeftPart - CoeficientsHolder.NameTextPos)  +i] = Name[i];
                ConsoleMatrix[1, (CoeficientsHolder.RightPart   - CoeficientsHolder.NameTextPos)     +i] = Name[i];
            }
            for (int i = 0; i < Size.Length; ++i)
            {
                ConsoleMatrix[1, (CoeficientsHolder.LeftPart - CoeficientsHolder.SizeTextPos + i)] = Size[i];
                ConsoleMatrix[1, (CoeficientsHolder.RightPart     - CoeficientsHolder.SizeTextPos + i)] = Size[i];
            }
            for (int i = 0; i < ModTime.Length; ++i)
            {
                ConsoleMatrix[1, CoeficientsHolder.LeftPart - CoeficientsHolder.TimeTextPos + i] = ModTime[i];
                ConsoleMatrix[1, CoeficientsHolder.RightPart     - CoeficientsHolder.TimeTextPos + i] = ModTime[i];

            }
        }
        void FillTheConsole()
        {
            APartBase = LeftPart;
            APartBase.FillThePart(ConsoleMatrix);
            APartBase = RightPart;
            APartBase.FillThePart(ConsoleMatrix);
        }
        void HiLightLine()
        {
            APartBase = LeftPart;
            APartBase.HightlightCursor(ConsoleMatrix);
            APartBase = RightPart;
            APartBase.HightlightCursor(ConsoleMatrix);
        }
        public APartBase ActivePart()
        {
            return LeftPart.IsActive() ? (APartBase)LeftPart : (APartBase)RightPart;
        }
        public APartBase OppositePart()
        {
            return LeftPart.IsActive() ? (APartBase)RightPart : (APartBase)LeftPart;
        }
        public void ParseKeys(ConsoleKey key)
        {
            
            switch (key)
            {
                case ConsoleKey.Tab:
                    APartBase = LeftPart;
                    APartBase.ChangePart();
                    APartBase = RightPart;
                    APartBase.ChangePart();
                    break;
                case ConsoleKey.Enter:
                    APartBase = ActivePart();
                    APartBase.ChangeDirectory();
                    break;
                case ConsoleKey.UpArrow:
                    APartBase = ActivePart();
                    APartBase.ChangeHighlightUp();
                    break;
                case ConsoleKey.DownArrow:
                    APartBase = ActivePart();
                    APartBase.ChangeHighlightDown();
                    break;
                case ConsoleKey.F6:
                    APartBase = ActivePart();
                    Commands.CopyTo(APartBase.path,APartBase.CurrentSelectedFile, OppositePart().path);
                    OppositePart().UpdatePart();
                    break;
                case ConsoleKey.F5:
                    APartBase = ActivePart();
                    Commands.MoveTo(APartBase.path, APartBase.CurrentSelectedFile, OppositePart().path);
                    OppositePart().UpdatePart();
                    break;
                case ConsoleKey.Delete:
                    APartBase = ActivePart();
                    Commands.Del(APartBase.CurrentSelectedFile);
                    ActivePart().UpdatePart();
                    break;
                case ConsoleKey.F3:
                    Console.Clear();
                    if(File.GetAttributes(ActivePart().CurrentSelectedFile) != FileAttributes.Directory)
                    {
                        FileReader FR = new FileReader(ActivePart().CurrentSelectedFile);
                        FR.run();
                    }
                    
                    break;
                default:
                        break;
                }
            
        }
        void DrawConsole()
        {
            try
            {
                Console.Clear();
                for (var i = 0; i < CoeficientsHolder.Height; i++)
                {
                    for (var j = 0; j < CoeficientsHolder.RightPart; j++)
                    {
                        Console.Write(ConsoleMatrix[i, j]);
                    }
                }
                Console.SetCursorPosition(0, 0);
            }
            catch(Exception)
            {

            }
        }
        void ShowAllPath()
        {
            APartBase = ActivePart();
            Console.WriteLine(APartBase.CurrentSelectedFile);
        }
        void UpdateConsole()
        {
            FillTheBorders();
            FillTheConsole();
            DrawConsole();
        }
        public void run()
        {
            ConsoleKey key = 0;
            Console.CursorVisible = false;
            while (key != ConsoleKey.Escape)
            {

                UpdateConsole();
                HiLightLine();
                Console.SetCursorPosition(0, CoeficientsHolder.Height);
                ShowAllPath();
                /*Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("║1 ReadFile║");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Blue;*/




                key = Console.ReadKey().Key;
                ParseKeys(key);
                //Console.ReadKey(false);
                CoeficientsHolder.UpdateCoeficients();
                
            }
            Console.Clear();
            Console.CursorVisible = true;
        }
                    /*
         ╔
        ═
        ╗
        ║
        ╚
        ╝ 
         │
                */
    }
}
