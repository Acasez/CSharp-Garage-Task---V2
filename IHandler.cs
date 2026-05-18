using CSharp_Garage_Task.VehicleClasses;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
