using CSharp_Garage_Task.VehicleClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static CSharp_Garage_Task.VehicleClasses.Vehicle;

namespace CSharp_Garage_Task
{
    internal class Garage
    {
        const string vehicleCreation = "Lets create a vehicle. What type do you want?";
        const string vehicleColorChoice = "What color should our vehicle be? \n";
        const string vehicleFilter = "What should we filter for? \n";

        public enum FilterOptions
        {
            Exit,
            Type,
            Color,
            Wheels
        }
        private readonly Vehicle[] vehicles;
        public int GarageCapacity { get; private set; }
        public int ParkedVehicles { get; private set; }
        public Garage(int size)
        {
            if (size > 0)
            {
                GarageCapacity = size;
                vehicles = new Vehicle[size];
            }
            else
            {
                throw new ArgumentException("Garage cannot be smaller than 0");
            }
        }

        public void DisplayGarageSpaces()
        {
            Helper.WriteMessage("There are " + ParkedVehicles + " vehicles and " + vehicles.Length + " spaces.");
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] != null)
                {
                    Helper.WriteMessage("Space " + i + " - " + vehicles[i].ToString());
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
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] != null && vehicles[i].RegisterID.ToLower() == ID.ToLower())
                {
                    return vehicles[i];
                }
            }
            return null;
        }

        public bool CheckForGarageSpace()
        {
            if (GarageCapacity > ParkedVehicles)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int? GetFirstEmptySpace()
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] == null)
                {
                    return i;
                }
            }
            return null;
        }

        public void AddVehicle()
        {
            if (!CheckForGarageSpace())
            {
                Helper.WriteWarningMessage("Garage Full");
                return;
            }
            int? garageSpace = GetFirstEmptySpace();
            if (garageSpace == null)
            {
                Helper.WriteWarningMessage("No fitting space");
                return;
            }

            Helper.WriteMessage(vehicleCreation);
            VehicleTypes vehicleType = GetVehicleType();
            if (!Enum.IsDefined(vehicleType))
            {
                return;
            }
            Helper.WriteMessage("Creating " + vehicleType);

            Helper.WriteMessage("Write vehicle name: ");
            string? vehicleName = Console.ReadLine();
            if (vehicleName == null)
            {
                Helper.WriteWarningMessage("Cam't have null name");
                return;
            }

            Helper.WriteMessage("Write register ID");
            string? vehicleID = Console.ReadLine();
            if (vehicleID == null)
            {
                Helper.WriteWarningMessage("Cam't have null ID");
                return;
            }
            if (GetVehicleByID(vehicleID) != null) {
                Helper.WriteWarningMessage("Another vehicle with same ID already parked here");
                return;
            }

            Helper.WriteMessage(vehicleColorChoice);
            VehicleColors vehicleColor = GetVehicleColor();
            if (!Enum.IsDefined(vehicleColor))
            {
                return;
            }

            Vehicle? newVehicle = null;
            switch (vehicleType)
            {
                case VehicleTypes.Car:
                    Helper.WriteMessage("What's the car brand?");
                    Car.CarBrand carBrand = Car.GetCarBrand();
                    newVehicle = new Car(vehicleName, vehicleID, vehicleColor, vehicleType, ParkedVehicles, carBrand);
                    break;
                case VehicleTypes.Motorcycle:
                    Helper.WriteMessage("What's the top speed");
                    if (!int.TryParse(Console.ReadLine(), out int topSpeed))
                    {
                        Helper.WriteErrorMessage("Invalid input");
                    }
                    newVehicle = new Motorcycle(vehicleName, vehicleID, vehicleColor, vehicleType, ParkedVehicles, topSpeed);
                    break;
                case VehicleTypes.Boat:
                    Helper.WriteMessage("Does the boat have sails? \n1: Yes \n2: No ");
                    string? sailInput = Console.ReadLine();
                    int.TryParse(sailInput, out int sailsInt);
                    if (sailsInt == 1)
                    {
                        newVehicle = new Boat(vehicleName, vehicleID, vehicleColor, vehicleType, ParkedVehicles, true);
                    }
                    else if (sailsInt == 2)
                    {
                        newVehicle = new Boat(vehicleName, vehicleID, vehicleColor, vehicleType, ParkedVehicles, false);
                    }
                    else
                    {
                        Helper.WriteErrorMessage("Invalid input");
                    }
                    break;
                case VehicleTypes.Airplane:
                    Helper.WriteMessage("How many flight hours do the plane have?");
                    if (!int.TryParse(Console.ReadLine(), out int flightHours)) {
                        Helper.WriteErrorMessage("Invalid input");
                    }
                    newVehicle = new Airplane(vehicleName, vehicleID, vehicleColor, vehicleType, ParkedVehicles, flightHours);
                    break;
                case VehicleTypes.Bus:
                    Helper.WriteMessage("How many people does the bus fit?");
                    if (!int.TryParse(Console.ReadLine(), out int capacity))
                    {
                        Helper.WriteErrorMessage("Invalid input");
                    }
                    newVehicle = new Bus(vehicleName, vehicleID, vehicleColor, vehicleType, ParkedVehicles, capacity);
                    break;
                default:
                    Helper.WriteErrorMessage("Invalid input, select a valid one.");
                    break;
            }
            if (newVehicle != null)
            {
                Helper.WriteMessage("Added vehicle " + newVehicle.ToString() + " to garage space " + garageSpace);
                vehicles[(int)garageSpace] = newVehicle;
                ParkedVehicles++;
            }
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
                string ? yesNoInput = Console.ReadLine();
                int.TryParse(yesNoInput, out int yesNoInt);
                if (yesNoInt == 1)
                {
                    Helper.WriteMessage("Removed vehicle " + vehicle.ToString(), ConsoleColor.Yellow);
                    vehicles[vehicle.parkedNumber] = null;
                    ParkedVehicles--;
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

        internal void AddPredefinedVehicle(Vehicle vehicle, int space)
        {
            vehicles[space] = vehicle;
            ParkedVehicles ++;
        }

        internal void ListAllVehiclesOfType()
        {
            VehicleTypes vehicleType = GetVehicleType();

            for (int i = 0; i < vehicles.Length; i++)
            {
                if (vehicles[i] != null && vehicles[i].VehicleType == vehicleType)
                {
                    Helper.WriteMessage(vehicles[i].ToString());
                }
            }
        }
        internal void ListVehiclesTypes()
        {
            foreach (VehicleTypes type in Enum.GetValues<VehicleTypes>())
            {
                int vehiclesOfType = 0;
                for (int i = 0; i < vehicles.Length; i++)
                {
                    if (vehicles[i] != null && vehicles[i].VehicleType == type)
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
                for (int i = 0; i < vehicles.Length; i++)
                {
                    if (vehicles[i] != null)
                    {
                        if (typeFilter != null)
                        {
                            if (vehicles[i].VehicleType != typeFilter)
                            {
                                continue;
                            }
                        }
                        if (colorFilter != null)
                        {
                            if (vehicles[i].Color != colorFilter)
                            {
                                continue;
                            }
                        }
                        if (wheelCountFilter != null)
                        {
                            if (vehicles[i].Wheels != wheelCountFilter)
                            {
                                continue;
                            }
                        }
                        fittingVehicles++;
                        Helper.WriteMessage(vehicles[i].ToString());
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

        private static void DisplayCurrentFilters(VehicleTypes? typeFilter, VehicleColors? colorFilter, int? wheelCountFilter)
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
        private static VehicleColors GetVehicleColor()
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

        private static VehicleTypes GetVehicleType()
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

        private static FilterOptions GetFilterOption()
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
