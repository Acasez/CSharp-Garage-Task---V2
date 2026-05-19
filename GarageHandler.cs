using CSharp_Garage_Task.VehicleClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using static CSharp_Garage_Task.Garage<CSharp_Garage_Task.VehicleClasses.Vehicle>;
using static CSharp_Garage_Task.VehicleClasses.Vehicle;

namespace CSharp_Garage_Task
{
    internal class GarageHandler : IHandler
    {
        const string vehicleFilter = "What should we filter for? \n";
        const string vehicleCreation = "Lets create a vehicle. What type do you want?";
        const string vehicleColorChoice = "What color should our vehicle be? \n";
        public Garage<Vehicle> Garage { get; private set; }

        public bool CreateGarage(int garageSpaces)
        {
            if (garageSpaces > 0)
            {
                Garage = new Garage<Vehicle>(garageSpaces, this);
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

        public void AddVehicle()
        {
            if (!CheckForGarageSpace())
            {
                Helper.WriteWarningMessage("Garage Full");
                return;
            }
            List<int> largestEmptyLot = GetLargestEmptyLot();

            Helper.WriteMessage("Largest empty lot is " + largestEmptyLot.Count + " long.");
            Helper.WriteMessage(vehicleCreation);
            VehicleTypes vehicleType = IHandler.GetVehicleType(largestEmptyLot.Count);
            if (!Enum.IsDefined(vehicleType))
            {
                return;
            }
            List<int> garageSpace = largestEmptyLot.GetRange(0, IHandler.GetSizeOfVehicle(vehicleType));

            Helper.WriteMessage("Creating " + vehicleType);

            Helper.WriteMessage("Write vehicle name: ");
            string? vehicleName = Helper.GetInput();
            if (vehicleName == null)
            {
                Helper.WriteWarningMessage("Cam't have null name");
                return;
            }

            string? vehicleID = IUI.InputVehicleID(this);
            if (vehicleID == null)
            {
                return;
            }

            Helper.WriteMessage(vehicleColorChoice);
            VehicleColors vehicleColor = IHandler.GetVehicleColor();
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
                    newVehicle = new Car(vehicleName, vehicleID, vehicleColor, vehicleType, garageSpace, carBrand);
                    break;
                case VehicleTypes.Motorcycle:
                    Helper.WriteMessage("What's the top speed");
                    int topSpeed = Helper.GetIntFromInput(0);
                    newVehicle = new Motorcycle(vehicleName, vehicleID, vehicleColor, vehicleType, garageSpace, topSpeed);
                    break;
                case VehicleTypes.Boat:
                    Helper.WriteMessage("Does the boat have sails? \n1: Yes \n2: No ");
                    int sailsInt = Helper.GetIntFromInput(1, 2);
                    if (sailsInt == 1)
                    {
                        newVehicle = new Boat(vehicleName, vehicleID, vehicleColor, vehicleType, garageSpace, true);
                    }
                    else if (sailsInt == 2)
                    {
                        newVehicle = new Boat(vehicleName, vehicleID, vehicleColor, vehicleType, garageSpace, false);
                    }
                    else
                    {
                        Helper.WriteErrorMessage("Invalid input");
                    }
                    break;
                case VehicleTypes.Airplane:
                    Helper.WriteMessage("How many flight hours do the plane have?");
                    int flightHours = Helper.GetIntFromInput(0);
                    newVehicle = new Airplane(vehicleName, vehicleID, vehicleColor, vehicleType, garageSpace, flightHours);
                    break;
                case VehicleTypes.Bus:
                    Helper.WriteMessage("How many people does the bus fit?");
                    int capacity = Helper.GetIntFromInput(0);
                    newVehicle = new Bus(vehicleName, vehicleID, vehicleColor, vehicleType, garageSpace, capacity);
                    break;
                default:
                    Helper.WriteErrorMessage("Invalid input, select a valid one.");
                    break;
            }
            if (newVehicle != null)
            {
                Garage.AddVehicle(newVehicle, garageSpace, true);
            }
        }

        public void DisplayGarageSpaces()
        {
            Vehicle? lastVehicle = null;
            Helper.WriteMessage("There are " + Garage.ParkedVehicles + " vehicles and " + Garage.Vehicles.Length + " spaces.");
            for (int i = 0; i < Garage.Vehicles.Length; i++)
            {
                if (Garage.Vehicles[i] == lastVehicle) { continue; }
                if (Garage.Vehicles[i] != null)
                {
                    Helper.WriteMessage("Spaces " + Garage.Vehicles[i].parkSpacesOccupied.ToCustomString() + " - " + Garage.Vehicles[i].ToString(false));
                }
                else
                {
                    Helper.WriteMessage("Space " + i + " - No vehicles parked");
                }
                lastVehicle = Garage.Vehicles[i];
            }
        }

        public Vehicle? GetVehicleByID(string? ID)
        {
            if (ID == null)
            {
                return null;
            }
            foreach (Vehicle vehicle in Garage)
            {
                if (vehicle != null && vehicle.RegisterID.Equals(ID, StringComparison.CurrentCultureIgnoreCase))
                {
                    return vehicle;
                }
            }
            return null;
            //Could do this but I think the foreach loop is easier to read
            //return (from vehicle in Garage where vehicle != null && vehicle.RegisterID.Equals(ID, StringComparison.CurrentCultureIgnoreCase) select vehicle).FirstOrDefault();
        }

        public void FindVehicleById()
        {
            DisplayGarageSpaces();
            Helper.WriteMessage("Enter the ID of the vehicle you wish to find");
            string? vehicleID = Helper.GetInput();
            Vehicle? vehicle = GetVehicleByID(vehicleID);
            if (vehicle != null)
            {
                Helper.WriteMessage("Found vehicle " + vehicle.ToString(true));
                Helper.WriteMessage("Do you wish to remove the vehicle? \n1: Yes \n2: No ");
                int yesNoInt = Helper.GetIntFromInput(1, 2);
                if (yesNoInt == 1)
                {
                    Helper.WriteMessage("Removed vehicle " + vehicle.ToString(false), ConsoleColor.Yellow);
                    List<int> newFreeSpaces = vehicle.parkSpacesOccupied;
                    foreach (int newFreeSpace in newFreeSpaces)
                    {
                        Garage.Vehicles[newFreeSpace] = null;
                    }
                    Garage.ParkedVehicles--;
                }
                else if (yesNoInt == 2)
                {
                    Helper.WriteMessage("Not removing vehicle");
                }
                return;
            }
            else
            {
                Helper.WriteWarningMessage("Couldn't find vehicle witht that ID");
            }
        }
        public void ListVehiclesTypesOld()
        {
            foreach (VehicleTypes type in Enum.GetValues<VehicleTypes>())
            {
                List<Vehicle> vehiclesOfType = [];

                foreach (Vehicle vehicle in Garage)
                {
                    if (vehicle != null && vehicle.VehicleType == type)
                    {
                        vehiclesOfType.Add(vehicle);
                    }
                }
                Helper.WriteMessage("There are " + vehiclesOfType.Count + " " + type.ToString() + "s");
                foreach(Vehicle vehicle in vehiclesOfType)
                {
                    Helper.WriteMessage(" - " + vehicle.ToString());
                }
            }
        }

        public void ListVehiclesTypes()
        {
            var vehiclesByType = Garage.Where(v => v != null).GroupBy(v => v.VehicleType).OrderBy(g => g.Key);

            foreach (var type in vehiclesByType)
            {
                Helper.WriteMessage("There are " + type.Count() + " " + type.Key + "s");
                foreach (var vehicle in type)
                {
                    Helper.WriteMessage(" - " + vehicle.ToString(true));
                }
            }
        }

        public void ListAllVehiclesFilterable()
        {
            VehicleTypes? typeFilter = null;
            VehicleColors? colorFilter = null;
            int? wheelCountFilter = null;
            bool looping = true;
            while (looping)
            {
                DisplayCurrentFilters(typeFilter, colorFilter, wheelCountFilter);
                List<Vehicle>? filteredVehicles = Garage.Where(v => v != null)
                    .Where(v => typeFilter == null || v.VehicleType == typeFilter)
                    .Where(v => colorFilter == null || v.Color == colorFilter)
                    .Where(v => wheelCountFilter == null || v.Wheels == wheelCountFilter)
                    .DistinctBy(v => v.RegisterID).ToList();

                filteredVehicles.ForEach(v => Helper.WriteMessage(v.ToString(true)));
                int fittingVehicles = filteredVehicles.Count;
                if (fittingVehicles == 0)
                {
                    Helper.WriteWarningMessage("No vehicles fitting filters");
                }
                Helper.WriteMessage(vehicleFilter);
                FilterOptions filter = IHandler.GetFilterOption();
                Helper.WriteMessage("Setup " + filter);
                switch (filter)
                {
                    case FilterOptions.Exit:
                        Helper.WriteMessage("Exiting view");
                        looping = false;
                        break;
                    case FilterOptions.Type:
                        typeFilter = IHandler.GetVehicleType();
                        break;
                    case FilterOptions.Color:
                        colorFilter = IHandler.GetVehicleColor();
                        break;
                    case FilterOptions.Wheels:
                        int wheelCount = Helper.GetIntFromInput(0);
                        wheelCountFilter = wheelCount;
                        break;
                    default:
                        Helper.WriteErrorMessage("Invalid input, select a valid one.");
                        break;
                }
            }
        }

        public bool CheckForGarageSpace()
        {
            if (Garage.GarageCapacity > Garage.ParkedVehicles)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetFirstEmptySpace()
        {
            for (int i = 0; i < Garage.Vehicles.Length; i++)
            {
                if (Garage.Vehicles[i] == null)
                {
                    return i;
                }
            }
            Helper.WriteErrorMessage("ERROR. No fitting spaces.");
            return -1; 
        }

        public List<int> GetLargestEmptyLot()
        {
            List<int> currentLot = [];
            List<int> largestLot = [];
            for (int i = 0; i < Garage.Vehicles.Length; i++)
            {
                if (Garage.Vehicles[i] == null)
                {
                    currentLot.Add(i);
                    if (currentLot.Count > largestLot.Count)
                    {
                        largestLot.Clear();
                        largestLot.AddRange(currentLot);
                    }
                }
                else
                {
                    currentLot.Clear();
                }
            }
            return largestLot;
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
    }
}
