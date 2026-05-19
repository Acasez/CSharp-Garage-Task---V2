using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Garage_Task.VehicleClasses
{
    internal class Boat : Vehicle
    {
        public bool Sail { get; private set; }
        public static readonly int vehicleSize = 2;
        public Boat(string name, string registerID, VehicleColors color, VehicleTypes vehicleType, int parkedNumber, bool sail) : base(name, registerID, color, vehicleType, parkedNumber)
        {
            Wheels = 0;
            pluralName = "Boats";
            Sail = sail;
            Size = 2;
        }

        public override string ToString()
        {
            return base.ToString() + " with" + (Sail ? " sails" : " no sails");
        }
    }
}
