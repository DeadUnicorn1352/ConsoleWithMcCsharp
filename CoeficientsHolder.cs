using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    static public class CoeficientsHolder
    {
        public static Int32 LeftPart;
        public static Int32 RightPart;
        public static Int32 SizeHolderCoef;
        public static Int32 TimeHolderCoef;
        public static Int32 NameTextPos;        
        public static Int32 SizeTextPos;
        public static Int32 TimeTextPos;
        public static Int32 Height;
        static CoeficientsHolder()
        {

            LeftPart = Console.WindowWidth / 2;
            RightPart = Console.WindowWidth;
            SizeHolderCoef = Console.WindowWidth / 10;
            TimeHolderCoef = Console.WindowWidth / 6;
            NameTextPos = Console.WindowWidth / 3;
            SizeTextPos = Console.WindowWidth / 7;
            TimeTextPos = Console.WindowWidth / 13;
            Height = Console.WindowHeight-3;

        }
        public static void UpdateCoeficients()
        {
            LeftPart = Console.WindowWidth / 2;
            RightPart = Console.WindowWidth;
            SizeHolderCoef = Console.WindowWidth / 10;
            TimeHolderCoef = Console.WindowWidth / 6;
            NameTextPos = Console.WindowWidth / 3;
            SizeTextPos = Console.WindowWidth / 7;
            TimeTextPos = Console.WindowWidth / 13;
            Height = Console.WindowHeight-3;
        }
    }
}
