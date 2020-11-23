using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    static class Commands
    {

        static readonly string[] commands = { "dir", "help", "cd", "mc","del","add","cls","copy","move","read","man","last" };
        static readonly string[] descriptions = { "show available directories and files", "show all commands with descriptions", "change directory [name of directory] or .. to go back", "open sort of Midnight Commander" ,"deletes selected file or directory", "add a file or a directory","clear the screen","copies file to another directory","moves file to another directory","reads the file","gives you the manual for the command","get last N commands"};
        public static string[] GetCommands()
        {
            return commands;
        }
        public static string[] GetDescriptions()
        {
            return descriptions;
        }
        public static void ChangeDir(string param = null)               // CD
        {
            string CurDir = Directory.GetCurrentDirectory();
            if (param == null || param.Trim() == "")
            {
                Directory.SetCurrentDirectory(CurDir.Substring(0,3));
                return;
            }
            if (param.Trim() == "..")
            {
                Directory.SetCurrentDirectory(Directory.GetParent(CurDir).FullName);
                return;
            }
            if(Directory.Exists(param))
                Directory.SetCurrentDirectory(param);
            if (Directory.Exists(CurDir + "\\" + param))
                Directory.SetCurrentDirectory(param);
        }
        public static void ShowDir(string s)
        {
            string CurDir = Directory.GetCurrentDirectory();
            foreach (var dir in Directory.GetDirectories(CurDir))
            {
                Console.WriteLine("<DIR>\t" + Path.GetFileName(dir));
            }
            foreach (var dir in Directory.GetFiles(CurDir))
            {
                Console.WriteLine("<FILE>\t" + Path.GetFileName(dir));
            }
        }
        public static void ShowHelp(string s = null)
        {
            if(s == null)
            CommandParser.ShowAvailableCommands();
        }
        public static void StartMC(string s = null)
        {
            MCFramework mc = new MCFramework();
            mc.run();
        }
        public static void Del(string s = null)
        {
            if (s == null)
                return;
            if(File.Exists(s))
            {
                File.Delete(s);
                return;
            }
            
            char command = 's';
            Console.WriteLine("Подтвердите что хотите удалить каталог рекурсивно ( 'r' )");
            command = Convert.ToChar(Console.Read());
            if(Char.ToLower(command) == 'r')
                
            {
               Directory.Delete(s, true);
            }
           
            return;
        }
        public static void Add(string s = null)
        {
            if (s == null)
                return;
            if(Path.HasExtension(s))
            {
                File.Create(s);
                return;
            }
            Console.WriteLine("Выбирете что хотите создать (1 - файл, 2 - директорию)");
            char type = (char)Console.Read();
            if (type == '1')
                File.Create(s);
            else if (type == '2')
                Directory.CreateDirectory(s);
        }
        public static void CLS(string s = null)
        {
            Console.Clear();
        }
        public static void CopyTo(string fileName = null)
        {
            if (fileName == null)
                return;
            Console.WriteLine("Where to copy to?");
            string path = Console.ReadLine();
            string sourceFile = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            string destFile = Path.Combine(path, fileName);

            if (Directory.Exists(path))
                if (File.GetAttributes(fileName) == FileAttributes.Directory)
                {
                    string[] files = Directory.GetFiles(fileName);
                    Directory.CreateDirectory(destFile);
                    // Copy the files and overwrite destination files if they already exist.
                    foreach (string s in files)
                    {
                        // Use static Path methods to extract only the file name from the path.
                        destFile = Path.Combine(path, s);
                        File.Copy(s, destFile, true);
                    }
                }
                else
                {
                    File.Copy(sourceFile, destFile, true);
                }
            
            
        }
        public static void CopyTo(string path,string fileName, string where)
        {
            if (fileName == null)
                return;
            string sourceFile;
            string destFile;

            if (Directory.Exists(where))
                if (File.GetAttributes(fileName) == FileAttributes.Directory)
                {
                    sourceFile = Path.Combine(path, Path.GetDirectoryName(fileName));
                    destFile = Path.Combine(where, Path.GetFileName(fileName));
                    string[] files = Directory.GetFiles(fileName);
                    where = Path.Combine(where, Path.GetFileName(fileName));

                    Directory.CreateDirectory(destFile);
                    foreach (string s in files)
                    {
                        sourceFile = Path.Combine(fileName, Path.GetFileName(s));
                        destFile = Path.Combine(where, Path.GetFileName(s));
                        File.Copy(sourceFile, destFile, true);
                    }
                }
                else
                {
                    sourceFile = Path.Combine(path, Path.GetFileName(fileName));
                    destFile = Path.Combine(where, Path.GetFileName(fileName));
                    File.Copy(sourceFile, destFile, true);
                }


        }
        public static void MoveTo(string fileName = null)
        {
            if (fileName == null)
                return;
            Console.WriteLine("Where to move to?");
            string path = Console.ReadLine();
            string sourceFile = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            string destFile = Path.Combine(path, fileName);
            Directory.CreateDirectory(destFile);
            if (Directory.Exists(path))
                if (File.GetAttributes(fileName) == FileAttributes.Directory)
                {
                    string[] files = Directory.GetFiles(fileName);
                    
                    // Copy the files and overwrite destination files if they already exist.
                    foreach (string s in files)
                    {
                        // Use static Path methods to extract only the file name from the path.
                        destFile = Path.Combine(path, s);
                        Directory.Move(s, destFile);
                    }
                }
                else
                {
                    Directory.Move(sourceFile, destFile);
                }


        }
        public static void MoveTo(string path,string fileName,string where)
        {
            if (fileName == null)
                return;
            string sourceFile ;
            string destFile;   

            if (Directory.Exists(where))
                if (File.GetAttributes(fileName) == FileAttributes.Directory)
                {
                    sourceFile = Path.Combine(path, Path.GetDirectoryName(fileName));
                    destFile = Path.Combine(where, Path.GetFileName(fileName));
                    string[] files = Directory.GetFiles(fileName);
                    where = Path.Combine(where, Path.GetFileName(fileName));
                    Directory.CreateDirectory(destFile);
                    foreach (string s in files)
                    {
                        sourceFile = Path.Combine(fileName, Path.GetFileName(s));
                        destFile = Path.Combine(where, Path.GetFileName(s));
                        Directory.Move(s, destFile);
                    }
                }
                else
                {
                    sourceFile = Path.Combine(path, Path.GetFileName(fileName));
                    destFile = Path.Combine(where, Path.GetFileName(fileName));
                    File.Move(sourceFile, destFile);
                }


        }
        public static void ReadFile(string filename = null)
        {
            Console.Clear();
            if (File.GetAttributes(filename) != FileAttributes.Directory)
            {
                FileReader FR = new FileReader(filename);
                FR.run();
            }
            Console.Clear();
        }
        public static void ShowMan(string command)
        {
            if (command == null)
                return;
            Manual.GetManual(command);
        }
        public static void GetLastCommands(string num = null)
        {
            if (num == null)
                return;
            else if (Convert.ToInt32(num) == -1)
                Counter.GetAllCommands();
            else Counter.GetAllCommands(Convert.ToInt32(num));
        }
        public static KeyValuePair<string, Tuple<string, CDelegate>>[] GetKeyValuePairs()
        {
            KeyValuePair<string, Tuple<string, CDelegate>>[] valuePair = new KeyValuePair<string, Tuple<string, CDelegate>>[commands.Length];
            CDelegate[] DelegateArr = new CDelegate[commands.Length];
            DelegateArr[0] = ShowDir;
            DelegateArr[1] = ShowHelp;
            DelegateArr[2] = ChangeDir;
            DelegateArr[3] = StartMC;
            DelegateArr[4] = Del;
            DelegateArr[5] = Add;
            DelegateArr[6] = CLS;
            DelegateArr[7] = CopyTo;
            DelegateArr[8] = MoveTo;
            DelegateArr[9] = ReadFile;
            DelegateArr[10] = ShowMan;
            DelegateArr[11] = GetLastCommands;
            for (int i = 0; i < commands.Length; ++i)
            {
                try
                {
                    valuePair[i] = new KeyValuePair<string, Tuple<string, CDelegate>>(commands[i], new Tuple<string, CDelegate>(descriptions[i], DelegateArr[i]));
                }
                catch (IndexOutOfRangeException)
                {
                    valuePair[i] = new KeyValuePair<string, Tuple<string, CDelegate>>(commands[i], new Tuple<string, CDelegate>("No comments added", DelegateArr[i]));
                }
            }
            return valuePair;
        }

    }
}
