using CSharp_Garage_Task.VehicleClasses;
using System;
using System.Collections.Generic;
using System.Text;
using static CSharp_Garage_Task.Garage<CSharp_Garage_Task.VehicleClasses.Vehicle>;
using static CSharp_Garage_Task.VehicleClasses.Vehicle;

namespace CSharp_Garage_Task
{
    internal interface IHandler
    {
        bool CreateGarage(int garageSpaces);
        void DisplayGarageSpaces();
        Vehicle? GetVehicleByID(string? ID);
        void FindVehicleById();
        void ListVehiclesTypes();
        void ListAllVehiclesFilterable();
        bool CheckForGarageSpace();
        int? GetFirstEmptySpace();
        void AddVehicle();
        int GetLargestEmptyLot();

        #region Filters
        internal static VehicleColors GetVehicleColor()
        {
            while (true)
            {
                foreach (VehicleColors type in Enum.GetValues<VehicleColors>())
                {
                    Helper.WriteMessage((int)type + ": Color " + type.ToString());
                }
                if (!int.TryParse(Helper.GetInput(), out int vehicleColorInt))
                {
                    Helper.WriteErrorMessage("Error, not a interger");
                    continue;
                }
                if (!Enum.IsDefined(typeof(VehicleColors), vehicleColorInt))
                {
                    Helper.WriteErrorMessage("Invalid input, select a valid vehicle color.");
                    continue;
                }
                return (VehicleColors)vehicleColorInt;
            }
        }

        internal static VehicleTypes GetVehicleType()
        {
            while (true)
            {
                foreach (VehicleTypes type in Enum.GetValues<VehicleTypes>())
                {
                    Helper.WriteMessage((int)type + ": " + type.ToString());
                }
                if (!int.TryParse(Helper.GetInput(), out int vehicleTypeInt))
                {
                    Helper.WriteErrorMessage("Error, not an integer. Try again.");
                    continue;
                }
                if (!Enum.IsDefined(typeof(VehicleTypes), vehicleTypeInt))
                {
                    Helper.WriteErrorMessage("Invalid input, select a valid vehicle type. Try again.");
                    continue; 
                }

                return (VehicleTypes)vehicleTypeInt;
            }
        }

        internal static FilterOptions GetFilterOption()
        {
            while (true)
            {
                foreach (FilterOptions type in Enum.GetValues<FilterOptions>())
                {
                    Helper.WriteMessage((int)type + (type == 0 ? ": " : ": Vehicle ") + type.ToString());
                }
                if (!int.TryParse(Helper.GetInput(), out int vehicleFilterInt))
                {
                    Helper.WriteErrorMessage("Error, not a interger");
                    continue;
                }
                if (!Enum.IsDefined(typeof(FilterOptions), vehicleFilterInt))
                {
                    Helper.WriteErrorMessage("Invalid input, select a valid vehicle filter.");
                    continue;
                }
                return (FilterOptions)vehicleFilterInt;
            }
        }

        #endregion
    }
}
