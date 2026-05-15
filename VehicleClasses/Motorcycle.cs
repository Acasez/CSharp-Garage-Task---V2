using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Garage_Task.VehicleClasses
{
    internal class Motorcycle : Vehicle
    {
        public int TopSpeed { get; private set; }
        public Motorcycle(string name, string registerID, VehicleColors color, VehicleTypes vehicleType, int parkedNumber, int topSpeed) : base(name, registerID, color, vehicleType, parkedNumber)
        {
            Wheels = 2;
            pluralName = "Motorcycles";
            TopSpeed = topSpeed;
        }

        public override string ToString()
        {
            return base.ToString() + " with a top speed of " + TopSpeed;
        }
    }
}
