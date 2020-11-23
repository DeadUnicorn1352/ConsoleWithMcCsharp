using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    static class Counter
    {

        static Stack<string> commandsStack = new Stack<string>();

        public static void  GetAllCommands()
        {
            foreach(string i in commandsStack)
            {
                Console.WriteLine(i);
            }
        }
        public static void GetAllCommands(Int32 FirstNCommands)
        {
            for(int i = 0; i < FirstNCommands;++i)
            {
                Console.WriteLine(commandsStack.Pop());
            }

        }
        public static void AddCommand(string command = null)
        {
            commandsStack.Push(command);
        }
    }
}
