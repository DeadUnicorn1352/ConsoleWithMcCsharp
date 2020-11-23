using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    class CommandParser
    {
        private static readonly Dictionary<string, Tuple<string, CDelegate>> CommandList;
        static CommandParser()
        {
            CommandList = new Dictionary<string, Tuple<string, CDelegate>>();
        }
        public static void AddCommand(KeyValuePair<string, Tuple<string, CDelegate>> command)
        {
            CommandList.Add(command.Key, command.Value);

        }
        public static void AddCommand(KeyValuePair<string, Tuple<string, CDelegate>>[] commands)
        {
            foreach (var command in commands)
            {
                CommandList.Add(command.Key, command.Value);
            }
        }
        public static void ShowPutCommand(string[] CommandsToParse)
        {
            foreach (var i in CommandsToParse)
                Console.WriteLine(i);
        }
        public static void ShowAvailableCommands()
        {
            foreach (var command in CommandList)
            {
                Console.WriteLine($"{command.Key} - \t\t{command.Value.Item1}");
            }
        }
        internal static bool ParseCommands(string[] CommandsToParse)
        {
            string CurDir = Directory.GetCurrentDirectory();
            ShowPutCommand(CommandsToParse);
            try
            {
                CommandList[CommandsToParse[0].ToLower().Trim()].Item2(CommandsToParse[1]);
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                CommandList[CommandsToParse[0].ToLower().Trim()].Item2(null);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            
        }
    }
}
