using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    //Base for MC parts
    abstract class APartBase
    {
        public string   CurrentSelectedFile { get; protected set; }
        public string   path;
        protected string[] files;
        protected string[] dirs;
        protected bool Active;
        protected bool ShowDrives;
        protected Int32 CursorPos;
        protected Int32 ColumnStartIndex; 
        protected Int32 TotalContentLength;
        protected Int32 OverCameItems; 
        protected Int32 DirStartIndex, FileStartIndex;
        protected Int32 Pointer;


        public APartBase()
        {
            InitDrives();
            InitVars();
            Active = false;
        }

        public bool IsActive()
        {
            return Active;
        }
        public void UpdatePart()
        {
            InitContents();
            InitVars();
        }
        public void SetAvtivity(bool setter)
        {
            Active = setter;
        }
        protected void InitDrives()
        {
            path = @"\";
            dirs = Directory.GetLogicalDrives();
            files = new string[0];
            TotalContentLength = dirs.Length;
            
            ShowDrives = true;
        }
        protected void InitContents()
        {
            try
            {
                files = Directory.GetFiles(path);
                dirs = Directory.GetDirectories(path);
                TotalContentLength = files.Length + dirs.Length;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        protected void InitVars()
        {
                Pointer = -1;
                DirStartIndex = 0;
                FileStartIndex = 0;
                OverCameItems = 0;
                ColumnStartIndex = 2;
                CursorPos = 2;
        }
        public void ChangePart()
        {
            if (Active)
                Active = false;
            else Active = true;
        }
        public void ChangeHighlightUp()
        {
            if (!Active)
                return;
            if (CursorPos > 2)
            {
                CursorPos--;
                Pointer--;
                return;
            }

            if (CursorPos == 2)
            {
                if (DirStartIndex == 0 && FileStartIndex != 0)
                    --FileStartIndex;
                else if (DirStartIndex != 0) --DirStartIndex;
            }
            if (Pointer > -1)
                Pointer--;
        }
        public void ChangeHighlightDown() 
        {
            if (!Active)
                return;

            if (CursorPos < CoeficientsHolder.Height - 2 && Pointer < TotalContentLength - 1)
            {
                CursorPos++;
                Pointer++;
                return;
            }

            if (CursorPos == CoeficientsHolder.Height - 2 && Pointer < TotalContentLength - 1)
            {
                if (DirStartIndex == 0 && FileStartIndex == 0)
                    CursorPos -= 3;
                else CursorPos -= 2;
                if (DirStartIndex == dirs.Length)//проверка на максимум в директориях
                    FileStartIndex += 2;
                else DirStartIndex += 2;
            }

        }
        public void ChangeDirectory()
        {
            if (!Active)
                return;
            if (ShowDrives)
            {
                path = dirs[Pointer + 1];

                InitContents();
                InitVars();
                ShowDrives = false;
                return;
            }


            if (Pointer == -1)
            {

                if (Directory.GetParent(path)?.FullName != null)
                {
                    path = Directory.GetParent(path).FullName;
                }
                else
                {
                    InitDrives();
                    InitVars();
                    return;
                }
            }
            else if (Pointer < dirs.Length)
            {
                path = dirs[Pointer];
            }
            else return;
            InitContents();
            InitVars();


        }
        protected void SetCurrentField(bool Drives)
        {
            if (!Drives)
            {
                if (Pointer < dirs.Length)
                    CurrentSelectedFile = dirs[Pointer];
                else
                    CurrentSelectedFile = files[Pointer - dirs.Length];
            }
            else
            {
                    CurrentSelectedFile = dirs[Pointer+1];
            }

        }
        abstract protected void ShowLocalDrives(char[,] matrix);
        abstract public void FillThePart(char [,] matrix);
        abstract public void HightlightCursor(char[,] matrix);
        abstract protected void FillTheDirsAndFiles(char[,] matrix);
        abstract protected void FillSize(char[,] matrix);

    }
}
