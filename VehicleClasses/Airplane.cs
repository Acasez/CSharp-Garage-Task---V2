using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Garage_Task.VehicleClasses
{
    internal class Airplane : Vehicle
    {
        public int FlightHours { get; private set; }
        public Airplane(string name, string registerID, VehicleColors color, VehicleTypes vehicleType, int parkedNumber, int flightHours) : base(name, registerID, color, vehicleType, parkedNumber)
        {
            Wheels = 2;
            pluralName = "Airplanes";
            FlightHours = flightHours;
        }

        public override string ToString()
        {
            return base.ToString() + " with " + FlightHours + " flight hours";
        }
    }
}
