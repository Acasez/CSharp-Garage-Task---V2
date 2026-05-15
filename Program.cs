using CSharp_Garage_Task;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
internal class Program
{
    const string garageMenu = "Welcome to the garage. \n" +
        "Select an option. \n" +
        "0 = Exit \n" +
        "1 = Add vehicle \n" +
        "2 = Find/Remove vehicle \n" +
        "3 = List garage spaces \n" +
        "4 = List vehicles types \n" +
        "5 = List all vehicles (filterable)";
    const string garageStart = "Lets create a garage. How many spaces do you want in the garage? \n"+
        "Type -1 for predefined luxury garage. \n" +
        "Type -2 for predefined huge garage. \n" +
        "Type -3 for predefined spaced garage";

    public static void Main()
    {
        try
        {
            bool looping = true;
            Helper.WriteMessage(garageStart);
            if (!int.TryParse(Console.ReadLine(), out int garageSpaces))
            {
                Helper.WriteErrorMessage("Error, not a interger");
            }
            Garage? ourGarage = null;

            if (garageSpaces > 0)
            {
                ourGarage = new Garage(garageSpaces);
            }
            else
            {
                switch (garageSpaces)
                {
                    case -1:
                        ourGarage = PredefinedGarages.LuxuryGarage();
                        break;
                    case -2:
                        ourGarage = PredefinedGarages.HugeGarage();
                        break;
                    case -3:
                        ourGarage = PredefinedGarages.SpacedGarage();
                        break;
                    default:
                        Helper.WriteErrorMessage("Invalid input, select a valid one.");
                        looping = false;
                        break;
                }
            }

            while (looping)
            {
                looping = LoopDisplay(looping, ourGarage);
            }
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

    public static bool LoopDisplay(bool looping, Garage garage)
    {
        Helper.WriteMessage(garageMenu);
        Console.Write("Your choice: ");

        string? input = Console.ReadLine();

        switch (input)
        {
            case "0":
                looping = false;
                Helper.WriteMessage("Leaving the garage");
                break;
            case "1":
                garage.AddVehicle();
                break;
            case "2":
                garage.FindVehicleById();
                break;
            case "3":
                garage.DisplayGarageSpaces();
                break;
            case "4":
                garage.ListVehiclesTypes();
                break;
            case "5":
                garage.ListAllVehiclesFilterable();
                break;
            default:
                Helper.WriteErrorMessage("Invalid input, select a valid one.");
                break;
        }

        Console.WriteLine();
        return looping;
    }
}