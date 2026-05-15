using System;
using System.Collections.Generic;
using System.Text;
using static CSharp_Garage_Task.Garage;

namespace CSharp_Garage_Task.VehicleClasses
{
    internal class Car : Vehicle
    {
        public enum CarBrand
        {
            Volvo,
            Saab,
            Porshe,
            Toyota,
            Volkswagen,
            Ford,
            Nissan,
            Honda,
            Renualt
        }
        public CarBrand Brand { get; private set; }
        public Car(string name, string registerID, VehicleColors color, VehicleTypes vehicleType, int parkedNumber, CarBrand brand) : base(name, registerID, color, vehicleType, parkedNumber)
        {
            pluralName = "Cars";
            Brand = brand;
        }

        public override string ToString()
        {
            return base.ToString() + " of car brand " + Brand;
        }

        internal static CarBrand GetCarBrand()
        {
            foreach (CarBrand type in Enum.GetValues<VehicleTypes>())
            {
                Helper.WriteMessage((int)type + ": " + type.ToString());
            }
            if (!int.TryParse(Console.ReadLine(), out int carBrandInt))
            {
                Helper.WriteErrorMessage("Error, not a interger");
            }
            if (!Enum.IsDefined(typeof(CarBrand), carBrandInt))
            {
                Helper.WriteErrorMessage("Invalid input, select a valid car brand.");
            }
            return (CarBrand)carBrandInt;
        }
    }
}
