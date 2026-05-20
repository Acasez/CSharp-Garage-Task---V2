using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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

        public static string? GetInput()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">");
            Console.ResetColor();
            string? input = Console.ReadLine();
            return input;
        }
        public static int GetIntFromInput(int min, int max = 999999999)
        {
            while (true)
            {
                string? input = GetInput();
                if (input != null)
                {
                    if (int.TryParse(input, out int outputInt))
                    {
                        if (outputInt >= min && outputInt <= max)
                        {
                            return outputInt;
                        }
                        else
                        {
                            WriteErrorMessage("Needs to fit within the specified options");
                        }
                    }
                    else
                    {
                        WriteErrorMessage("Not an int, try again");
                    }
                }
                else
                {
                    WriteErrorMessage("Invalid input, try again");
                }
            }
        }

        internal static void WriteHorizontalLine()
        {
            WriteMessage("-----------------------------------", ConsoleColor.White);
        }
    }
}
