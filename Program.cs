using CSharp_Garage_Task;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CSharp Garage Tests")]
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