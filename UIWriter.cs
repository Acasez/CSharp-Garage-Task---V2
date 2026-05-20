using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Garage_Task
{
    internal class UIWriter : IUI
    {
        const string garageMenu =
        "Welcome to the garage. \n" +
        "Select an option. \n" +
        "0 = Exit \n" +
        "1 = Add vehicle \n" +
        "2 = Find/Remove vehicle \n" +
        "3 = List garage spaces \n" +
        "4 = List vehicles types \n" +
        "5 = List all vehicles (filterable) \n" +
        "6 = Get Largest empty lot";

        const string garageStart =
        "Let's create a garage. How many spots do you want?\n" +
        "Or pick a preset:\n" +
        "  -1: Luxury (5 spots — Porsche, airplane, yacht)\n" +
        "  -2: Huge (25 spots, 8 vehicles)\n" +
        "  -3: Sparse (12 spots, 6 vehicles in non-contiguous slots)";

        public static void StartDisplay()
        {
            Helper.WriteMessage(garageStart);
            if (!int.TryParse(Helper.GetInput(), out int garageSpaces))
            {
                Helper.WriteErrorMessage("Error, not an integer");
            }
            IHandler handler = new GarageHandler();

            bool looping = handler.CreateGarage(garageSpaces);

            while (looping)
            {
                looping = LoopDisplay(looping, handler);
            }
        }

        public static bool LoopDisplay(bool looping, IHandler handler)
        {
            Helper.WriteMessage(garageMenu);

            string? input = Helper.GetInput();

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
                case "6":
                    List<int> lot = handler.GetLargestEmptyLot();
                    if (lot.Count > 0)
                    {
                        Helper.WriteMessage("Largest empty lot is " + (lot.Count == 1 ? "space " : "spaces ") + lot.ToCustomString());
                    }
                    else
                    {
                        Helper.WriteWarningMessage("No avaible empty lot");
                    }
                    
                    break;
                default:
                    Helper.WriteErrorMessage("Invalid input, select a valid one.");
                    break;
            }

            Console.WriteLine();
            return looping;
        }
    }
}
