using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Garage_Task
{
    internal class UIWriter
    {
        const string garageMenu = 
        "Welcome to the garage. \n" +
        "Select an option. \n" +
        "0 = Exit \n" +
        "1 = Add vehicle \n" +
        "2 = Find/Remove vehicle \n" +
        "3 = List garage spaces \n" +
        "4 = List vehicles types \n" +
        "5 = List all vehicles (filterable)";

        const string garageStart = 
        "Lets create a garage. How many spaces do you want in the garage? \n" +
        "Type -1 for predefined luxury garage. \n" +
        "Type -2 for predefined huge garage. \n" +
        "Type -3 for predefined spaced garage";

        public static void StartDisplay()
        {
            Helper.WriteMessage(garageStart);
            if (!int.TryParse(Console.ReadLine(), out int garageSpaces))
            {
                Helper.WriteErrorMessage("Error, not a interger");
            }
            GarageHandler handler = new();

            bool looping = handler.CreateGarage(garageSpaces);

            //handlerRef = handler;

            while (looping)
            {
                looping = LoopDisplay(looping, handler);
            }
        }

        public static bool LoopDisplay(bool looping, GarageHandler handler)
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
                    handler.AddVehicle();
                    break;
                case "2":
                    handler.FindVehicleById();
                    break;
                case "3":
                    handler.DisplayGarageSpaces();
                    break;
                case "4":
                    handler.ListVehiclesTypes();
                    break;
                case "5":
                    handler.ListAllVehiclesFilterable();
                    break;
                default:
                    Helper.WriteErrorMessage("Invalid input, select a valid one.");
                    break;
            }

            Console.WriteLine();
            return looping;
        }

        internal static string? InputVehicleName(Garage garage, GarageHandler handler)
        {
            Helper.WriteMessage("Write register ID (6 Chars)");
            string? vehicleID = Console.ReadLine();
            if (vehicleID == null)
            {
                Helper.WriteWarningMessage("Cam't have null ID");
                return null;
            }
            else if (handler.GetVehicleByID(vehicleID) != null)
            {
                Helper.WriteWarningMessage("Another vehicle with same ID already parked here");
                return null;
            }
            else if (vehicleID.Length != 6)
            {
                Helper.WriteWarningMessage("Register ID should be 6 characthers long");
                return null;
            }
            return vehicleID;
        }
    }
}
