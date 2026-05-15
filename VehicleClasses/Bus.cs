using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Garage_Task.VehicleClasses
{
    internal class Bus : Vehicle
    {
        public int Capacity { get; private set; }
        public Bus(string name, string registerID, VehicleColors color, VehicleTypes vehicleType, int parkedNumber, int capacity) : base(name, registerID, color, vehicleType, parkedNumber)
        {
            pluralName = "Busses";
            Capacity = capacity;
        }

        public override string ToString()
        {
            return base.ToString() + " with a capacity of " + Capacity;
        }
    }
}
