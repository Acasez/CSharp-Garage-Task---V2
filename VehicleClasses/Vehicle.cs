using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Garage_Task.VehicleClasses
{
    abstract class Vehicle
    {
        public enum VehicleTypes
        {
            Car,
            Motorcycle,
            Boat,
            Airplane,
            Bus
        }
        public enum VehicleColors
        {
            White,
            Black,
            Red,
            Blue,
            Green,
            Silver,
            Yellow
        }
        public string Name { get; private set; }
        public int Wheels { get; protected set; } = 4;
        public string RegisterID { get; private set; }
        public VehicleColors Color { get; private set; }
        public VehicleTypes VehicleType { get; private set; }

        public readonly int parkedNumber;

        public static string pluralName = "Vehicles";
        public Vehicle (string name, string registerID, VehicleColors color, VehicleTypes vehicleType, int parkedNumber)
        {
            Name = name;
            RegisterID = registerID;
            Color = color;
            VehicleType = vehicleType;
            this.parkedNumber = parkedNumber;
        }

        public override string ToString()
        {
            return VehicleType.ToString() + ": " + Name + " with ID " + RegisterID + " of color " + Color.ToString();
        }
    }
}
