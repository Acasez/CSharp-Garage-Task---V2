using CSharp_Garage_Task.VehicleClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static CSharp_Garage_Task.VehicleClasses.Vehicle;

namespace CSharp_Garage_Task
{
    internal class Garage//<T>: IEnumerator<T> where T : Vehicle 
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
        public readonly Vehicle[] vehicles;
        public GarageHandler GarageHandler { get; private set; }
        public int GarageCapacity { get; private set; }
        public int ParkedVehicles { get; set; }
        public Garage(int size, GarageHandler handler)
        {
            GarageHandler = handler;
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
            VehicleTypes vehicleType = GarageHandler.GetVehicleType();
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

            string? vehicleID = UIWriter.InputVehicleName(this, GarageHandler);
            if (vehicleID == null)
            {
                return;
            }

            Helper.WriteMessage(vehicleColorChoice);
            VehicleColors vehicleColor = GarageHandler.GetVehicleColor();
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

        internal void AddPredefinedVehicle(Vehicle vehicle, int space)
        {
            vehicles[space] = vehicle;
            ParkedVehicles ++;
        }

        internal void ListAllVehiclesOfType()
        {
            VehicleTypes vehicleType = GarageHandler.GetVehicleType();

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
                GarageHandler.DisplayCurrentFilters(typeFilter, colorFilter, wheelCountFilter);
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
                FilterOptions filter = GarageHandler.GetFilterOption();
                Helper.WriteMessage("Setup " + filter);
                switch (filter)
                {
                    case FilterOptions.Exit:
                        Helper.WriteMessage("Exiting view");
                        looping = false;
                        break;
                    case FilterOptions.Type:
                        typeFilter = GarageHandler.GetVehicleType();
                        break;
                    case FilterOptions.Color:
                        colorFilter = GarageHandler.GetVehicleColor();
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
    }
}
