using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class LeftPart : APartBase
    {
        protected override void ShowLocalDrives(char[,] matrix)
        {
            ColumnStartIndex = 2;
            for (int i = 0; i < CoeficientsHolder.LeftPart && i < path.Length; ++i) // вывод пути сверху рамки 
            {
                matrix[0, i + 1] = path[i];
            }
            for (Int32 dirIndex = DirStartIndex; dirIndex < dirs.Length && ColumnStartIndex < CoeficientsHolder.Height - 1; ++dirIndex)
            {
                matrix[ColumnStartIndex, 1] = '/';
                for (int i = 1, j = 0; i < CoeficientsHolder.LeftPart - CoeficientsHolder.TimeHolderCoef - 2 && j < dirs[dirIndex].Length; ++i, ++j)
                {
                    matrix[ColumnStartIndex, i + 1] = dirs[dirIndex][j];

                }
                ++ColumnStartIndex;

            }
        }
        public override void FillThePart(char[,] matrix)
        {
            if (ShowDrives)
                ShowLocalDrives(matrix);
            else
            { 
                FillTheDirsAndFiles(matrix);
                FillSize(matrix);
            }


        }
        protected override void FillTheDirsAndFiles(char[,] matrix)
        {
            ColumnStartIndex = 2;
            if (DirStartIndex == 0 && FileStartIndex == 0)
            {
                matrix[2, 1] = '/';
                matrix[2, 2] = '.';
                matrix[2, 3] = '.';
                ColumnStartIndex = 3;

            }

            for (int i = 0; i < CoeficientsHolder.LeftPart && i < path.Length; ++i) // вывод пути сверху рамки 
            {
                matrix[0, i + 1] = path[i];
            }

            for (Int32 dirIndex = DirStartIndex; dirIndex < dirs.Length && ColumnStartIndex < CoeficientsHolder.Height - 1; ++dirIndex)
            {
                matrix[ColumnStartIndex, 1] = '/';
                for (int i = 1, j = 0; i < CoeficientsHolder.LeftPart - CoeficientsHolder.TimeHolderCoef - 2 && j < Path.GetFileName(dirs[dirIndex]).Length; ++i, ++j)
                {
                    matrix[ColumnStartIndex, i + 1] = Path.GetFileName(dirs[dirIndex])[j];
                    
                }
                ++ColumnStartIndex;

            }
            for (Int32 fileIndex = FileStartIndex; fileIndex < files.Length && ColumnStartIndex < CoeficientsHolder.Height - 1; ++fileIndex)
            {
                for (int i = 0; i < (CoeficientsHolder.LeftPart - CoeficientsHolder.TimeHolderCoef - 2) && i < Path.GetFileName(files[fileIndex]).Length ; ++i)
                {
                    matrix[ColumnStartIndex, i + 2] = Path.GetFileName(files[fileIndex])[i];
                }
                ++ColumnStartIndex;
            }


        }
        //заполняем консоль размер и время
        protected override void FillSize(char [,] matrix) 
        {
            
                ColumnStartIndex = 2;
            if (DirStartIndex == 0 && FileStartIndex == 0)
                ColumnStartIndex = 3;
            
                DirectoryInfo inf = new DirectoryInfo(path);
                DirectoryInfo[] dirsInfo = inf.GetDirectories();
                FileInfo[] filesInfo = inf.GetFiles();

                for (Int32 dirIndex = DirStartIndex; dirIndex < dirs.Length && ColumnStartIndex < CoeficientsHolder.Height - 1; ++dirIndex)
                {
                    for (int i = CoeficientsHolder.LeftPart - CoeficientsHolder.SizeHolderCoef, j = 1; i < CoeficientsHolder.LeftPart - 1 && j < Convert.ToString(dirsInfo[dirIndex].LastWriteTimeUtc.ToShortDateString()).Length + 1; ++i, j++)
                    {
                        matrix[ColumnStartIndex, i + 1] = (dirsInfo[dirIndex].LastWriteTimeUtc.ToShortDateString()).ToString()[j - 1];
                    }
                    ++ColumnStartIndex;
                }
                for (Int32 fileIndex = FileStartIndex; fileIndex < files.Length && ColumnStartIndex < CoeficientsHolder.Height - 1; ++fileIndex)
                {//размер файла
                    for (int i = CoeficientsHolder.LeftPart - CoeficientsHolder.TimeHolderCoef, j = 1; i < CoeficientsHolder.LeftPart - CoeficientsHolder.TimeTextPos && j < Convert.ToString(filesInfo[fileIndex].Length/1024).Length + 1; ++i, j++)
                    {
                    /*if((filesInfo[fileIndex].Length).ToString().Length % 10 < 7)
                    matrix[ColumnStartIndex, i + 1] = (filesInfo[fileIndex].Length).ToString()[j - 1];
                    else
                        matrix[ColumnStartIndex, i + 1] = (filesInfo[fileIndex].Length/1024).ToString()[j - 1];*/

                    matrix[ColumnStartIndex, i + 1] = (filesInfo[fileIndex].Length/1024).ToString()[j - 1];

                }
                    //последняя модификация файла
                    for (int i = CoeficientsHolder.LeftPart - CoeficientsHolder.SizeHolderCoef, j = 1; i < CoeficientsHolder.LeftPart - 1 && j < Convert.ToString(filesInfo[fileIndex].LastWriteTimeUtc.ToShortDateString()).Length + 1; ++i, j++)
                    {
                        matrix[ColumnStartIndex, i + 1] = (filesInfo[fileIndex].LastWriteTimeUtc.ToShortDateString()).ToString()[j - 1];
                    }
                    ++ColumnStartIndex;
                }
       
        }
        public override void HightlightCursor(char[,] matrix)
        {
            if (!Active)
                return;

            try
            {
                
                Console.SetCursorPosition(1, CursorPos);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                for (var j = 1; j < CoeficientsHolder.LeftPart - 1; j++)
                {
                    Console.Write(matrix[CursorPos, j]);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Blue;
                SetCurrentField(ShowDrives);
                
            }
            catch (Exception)
            {

            }

        }
    }
}
