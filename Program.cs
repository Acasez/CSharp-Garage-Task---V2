using CSharp_Garage_Task;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
internal class Program
{

    public static void Main()
    {
        try
        {
            UIWriter.StartDisplay();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            Console.ResetColor();
        }
    }
}