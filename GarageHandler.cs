using CSharp_Garage_Task.VehicleClasses;
using System;
using System.Collections.Generic;
using System.Text;
using static CSharp_Garage_Task.Garage;
using static CSharp_Garage_Task.VehicleClasses.Vehicle;

namespace CSharp_Garage_Task
{
    internal class GarageHandler
    {
        const string vehicleFilter = "What should we filter for? \n";
        public Garage Garage { get; private set; }

        public bool CreateGarage(int garageSpaces)
        {
            if (garageSpaces > 0)
            {
                Garage = new Garage(garageSpaces, this);
            }
            else
            {
                switch (garageSpaces)
                {
                    case -1:
                        Garage = PredefinedGarages.LuxuryGarage(this);
                        break;
                    case -2:
                        Garage = PredefinedGarages.HugeGarage(this);
                        break;
                    case -3:
                        Garage = PredefinedGarages.SpacedGarage(this);
                        break;
                    default:
                        Helper.WriteErrorMessage("Invalid input, select a valid one.");
                        return false;
                }
            }

            return true;
        }

        public void DisplayGarageSpaces()
        {
            Helper.WriteMessage("There are " + Garage.ParkedVehicles + " vehicles and " + Garage.vehicles.Length + " spaces.");
            for (int i = 0; i < Garage.vehicles.Length; i++)
            {
                if (Garage.vehicles[i] != null)
                {
                    Helper.WriteMessage("Space " + i + " - " + Garage.vehicles[i].ToString());
                }
                else
                {
                    Helper.WriteMessage("Space " + i + " - No vehicles parked");
                }
            }
        }

        public Vehicle? GetVehicleByID(string? ID)
        {
            if (ID == null)
            {
                return null;
            }
            for (int i = 0; i < Garage.vehicles.Length; i++)
            {
                if (Garage.vehicles[i] != null && Garage.vehicles[i].RegisterID.Equals(ID, StringComparison.CurrentCultureIgnoreCase))
                {
                    return Garage.vehicles[i];
                }
            }
            return null;
        }

        internal void FindVehicleById()
        {
            DisplayGarageSpaces();
            Helper.WriteMessage("Enter the ID of the vehicle you wish to find");
            string? vehicleID = Console.ReadLine();
            Vehicle? vehicle = GetVehicleByID(vehicleID);
            if (vehicle != null)
            {
                Helper.WriteMessage("Found vehicle " + vehicle.ToString());
                Helper.WriteMessage("Do you wish to remove the vehicle? \n1: Yes \n2: No ");
                string? yesNoInput = Console.ReadLine();
                int.TryParse(yesNoInput, out int yesNoInt);
                if (yesNoInt == 1)
                {
                    Helper.WriteMessage("Removed vehicle " + vehicle.ToString(), ConsoleColor.Yellow);
                    Garage.vehicles[vehicle.parkedNumber] = null;
                    Garage.ParkedVehicles--;
                }
                else if (yesNoInt == 2)
                {
                    Helper.WriteMessage("Not removing vehicle");
                }
                else
                {
                    Helper.WriteErrorMessage("Invalid input");
                }
                return;
            }
            else
            {
                Helper.WriteWarningMessage("Couldn't find vehicle witht that ID");
            }
        }
        internal void ListVehiclesTypes()
        {
            foreach (VehicleTypes type in Enum.GetValues<VehicleTypes>())
            {
                int vehiclesOfType = 0;
                for (int i = 0; i < Garage.vehicles.Length; i++)
                {
                    if (Garage.vehicles[i] != null && Garage.vehicles[i].VehicleType == type)
                    {
                        vehiclesOfType++;
                    }
                }
                Helper.WriteMessage("There are " + vehiclesOfType + " " + type.ToString() + "s");
            }
        }

        internal void ListAllVehiclesFilterable()
        {
            VehicleTypes? typeFilter = null;
            VehicleColors? colorFilter = null;
            int? wheelCountFilter = null;
            bool looping = true;
            while (looping)
            {
                int fittingVehicles = 0;
                DisplayCurrentFilters(typeFilter, colorFilter, wheelCountFilter);
                for (int i = 0; i < Garage.vehicles.Length; i++)
                {
                    if (Garage.vehicles[i] != null)
                    {
                        if (typeFilter != null)
                        {
                            if (Garage.vehicles[i].VehicleType != typeFilter)
                            {
                                continue;
                            }
                        }
                        if (colorFilter != null)
                        {
                            if (Garage.vehicles[i].Color != colorFilter)
                            {
                                continue;
                            }
                        }
                        if (wheelCountFilter != null)
                        {
                            if (Garage.vehicles[i].Wheels != wheelCountFilter)
                            {
                                continue;
                            }
                        }
                        fittingVehicles++;
                        Helper.WriteMessage(Garage.vehicles[i].ToString());
                    }
                }
                if (fittingVehicles == 0)
                {
                    Helper.WriteWarningMessage("No vehicles fitting filters");
                }
                Helper.WriteMessage(vehicleFilter);
                FilterOptions filter = GetFilterOption();
                Helper.WriteMessage("Setup " + filter);
                switch (filter)
                {
                    case FilterOptions.Exit:
                        Helper.WriteMessage("Exiting view");
                        looping = false;
                        break;
                    case FilterOptions.Type:
                        typeFilter = GetVehicleType();
                        break;
                    case FilterOptions.Color:
                        colorFilter = GetVehicleColor();
                        break;
                    case FilterOptions.Wheels:
                        if (!int.TryParse(Console.ReadLine(), out int wheelCount))
                        {
                            Helper.WriteErrorMessage("Error, not a interger");
                        }
                        wheelCountFilter = wheelCount;
                        break;
                    default:
                        Helper.WriteErrorMessage("Invalid input, select a valid one.");
                        break;
                }
            }
        }


        internal static void DisplayCurrentFilters(VehicleTypes? typeFilter, VehicleColors? colorFilter, int? wheelCountFilter)
        {
            if (typeFilter == null && colorFilter == null && wheelCountFilter == null)
            {
                Helper.WriteMessage("No filters currently", ConsoleColor.Green);
            }
            if (typeFilter != null)
            {
                Helper.WriteMessage("Type filter: " + typeFilter, ConsoleColor.Green);
            }
            if (colorFilter != null)
            {
                Helper.WriteMessage("Color filter: " + colorFilter, ConsoleColor.Green);
            }
            if (wheelCountFilter != null)
            {
                Helper.WriteMessage("Wheel count filter: " + wheelCountFilter, ConsoleColor.Green);
            }
        }

        #region Filters
        internal static VehicleColors GetVehicleColor()
        {
            foreach (VehicleColors type in Enum.GetValues<VehicleColors>())
            {
                Helper.WriteMessage((int)type + ": Color " + type.ToString());
            }
            if (!int.TryParse(Console.ReadLine(), out int vehicleColorInt))
            {
                Helper.WriteErrorMessage("Error, not a interger");
            }
            if (!Enum.IsDefined(typeof(VehicleColors), vehicleColorInt))
            {
                Helper.WriteErrorMessage("Invalid input, select a valid vehicle color.");
            }
            return (VehicleColors)vehicleColorInt;
        }

        internal static VehicleTypes GetVehicleType()
        {
            foreach (VehicleTypes type in Enum.GetValues<VehicleTypes>())
            {
                Helper.WriteMessage((int)type + ": " + type.ToString());
            }
            if (!int.TryParse(Console.ReadLine(), out int vehicleTypeInt))
            {
                Helper.WriteErrorMessage("Error, not a interger");
            }
            if (!Enum.IsDefined(typeof(VehicleTypes), vehicleTypeInt))
            {
                Helper.WriteErrorMessage("Invalid input, select a valid vehicle type.");
            }
            return (VehicleTypes)vehicleTypeInt;
        }

        internal static FilterOptions GetFilterOption()
        {
            foreach (FilterOptions type in Enum.GetValues<FilterOptions>())
            {
                Helper.WriteMessage((int)type + (type == 0 ? ": " : ": Vehicle ") + type.ToString());
            }
            if (!int.TryParse(Console.ReadLine(), out int vehicleFilterInt))
            {
                Helper.WriteErrorMessage("Error, not a interger");
            }
            if (!Enum.IsDefined(typeof(FilterOptions), vehicleFilterInt))
            {
                Helper.WriteErrorMessage("Invalid input, select a valid vehicle filter.");
            }
            return (FilterOptions)vehicleFilterInt;
        }
        #endregion
    }
}
