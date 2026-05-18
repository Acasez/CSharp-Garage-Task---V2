using CSharp_Garage_Task.VehicleClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static CSharp_Garage_Task.VehicleClasses.Vehicle;

namespace CSharp_Garage_Task
{
    internal class Garage <T>: IEnumerable<T> where T : Vehicle 
    {
        public enum FilterOptions
        {
            Exit,
            Type,
            Color,
            Wheels
        }
        public Vehicle[] Vehicles { get; private set; }
        public GarageHandler GarageHandler { get; private set; }
        public int GarageCapacity { get; private set; }
        public int ParkedVehicles { get; set; }
        public Garage(int size, GarageHandler handler)
        {
            GarageHandler = handler;
            if (size > 0)
            {
                GarageCapacity = size;
                Vehicles = new Vehicle[size];
            }
            else
            {
                throw new ArgumentException("Garage cannot be smaller than 0");
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (Vehicle v in this.Vehicles)
            {
                yield return (T)v;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal void AddPredefinedVehicle(Vehicle vehicle, int space)
        {
            Vehicles[space] = vehicle;
            ParkedVehicles ++;
        }

        internal void ListAllVehiclesOfType()
        {
            VehicleTypes vehicleType = GarageHandler.GetVehicleType();

            for (int i = 0; i < Vehicles.Length; i++)
            {
                if (Vehicles[i] != null && Vehicles[i].VehicleType == vehicleType)
                {
                    Helper.WriteMessage(Vehicles[i].ToString());
                }
            }
        }
    }

    /*internal class GarageNew<T> : IEnumerable<T> where T: Vehicle
{

  private Vehicle[] vehicles;

  public Vehicle[] Vehicles
  {
    get {return vehicles;}
    set
    {
      vehicles = value;
    }
  }

  internal GarageNew(int sizeOfGarage)
  {
    Vehicles = new Vehicle[sizeOfGarage];
  }

  public void AddNewVehicle(Vehicle newVehicle)
  {
    for (int i = 0; i <= Vehicles.Length; i++)
    {
      if (Vehicles[i] == null)
      {
        Vehicles[i] = newVehicle;
        Console.WriteLine("Vehicle was added successfully!");
        return;
      }
      else
      {
        continue;
      }
    }
  }

  public IEnumerator<T> GetEnumerator()
  {
    foreach (Vehicle v in this.Vehicles)
    {
      yield return (T)v;
    }
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}*/
}
