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
    }
}
