using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FinalProject
{
    public delegate void CDelegate(string param);   
    class Framework
    {
        public Framework() 
        {
            CommandParser.AddCommand(Commands.GetKeyValuePairs());
        }

        public void run()
        {

            string EnteredCommand = null;
            string[] CommandsToParse = null;
            CommandParser.ShowAvailableCommands();
            while (true)
            {
                Counter.AddCommand(EnteredCommand);
                Console.Write($"{Directory.GetCurrentDirectory()} ");
                EnteredCommand  = Console.ReadLine();
                
                CommandsToParse = EnteredCommand.Split(' ');
                if (CommandsToParse[0] == "")
                    continue;
                if (CommandParser.ParseCommands(CommandsToParse))
                    continue;
                Console.WriteLine("Не удалось найти комманду, вот список доступных комманд (help):");
                CommandParser.ShowAvailableCommands();
                continue;
            }
        }
    }

}
