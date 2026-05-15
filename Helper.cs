using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Garage_Task
{
    internal class Helper
    {
        public static void WriteErrorMessage(string errorText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorText);
            Console.ResetColor();
        }

        public static void WriteWarningMessage(string warningText)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(warningText);
            Console.ResetColor();
        }

        public static void WriteMessage(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void WriteMessage(string text)
        {
            WriteMessage(text, ConsoleColor.White);
        }
    }
}
