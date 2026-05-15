using CSharp_Garage_Task.VehicleClasses;
using System;
using System.Collections.Generic;
using System.Text;
using static CSharp_Garage_Task.Garage;
using static CSharp_Garage_Task.VehicleClasses.Vehicle;

namespace CSharp_Garage_Task
{
    internal class PredefinedGarages
    {
        internal static Garage LuxuryGarage()
        {
            Garage luxuryGarage = new(5);
            Vehicle fastCar = new Car("Wroom", "24", VehicleColors.Red, VehicleTypes.Car, 0, Car.CarBrand.Porshe);
            luxuryGarage.AddPredefinedVehicle(fastCar, 0);

            Vehicle coolPlane = new Airplane("Zoom", "87", VehicleColors.Silver, VehicleTypes.Airplane, 1, 32);
            luxuryGarage.AddPredefinedVehicle(coolPlane, 1);

            Vehicle yacht = new Boat("Yacht", "420", VehicleColors.Blue, VehicleTypes.Boat, 2, true);
            luxuryGarage.AddPredefinedVehicle(yacht, 2);
            return luxuryGarage;
        }

        internal static Garage HugeGarage()
        {
            Garage hugeGarage = new (25);
            Vehicle fastCar = new Car("Wroom", "24", VehicleColors.Red, VehicleTypes.Car, 0, Car.CarBrand.Porshe);
            hugeGarage.AddPredefinedVehicle(fastCar, 0);

            Vehicle mcBike = new Motorcycle("mcBike", "994MCC", VehicleColors.Black, VehicleTypes.Motorcycle, 1, 120);
            hugeGarage.AddPredefinedVehicle(mcBike, 1);

            Vehicle yesINeedASUV = new Car("yesINeedASUV", "221SUV", VehicleColors.White, VehicleTypes.Car, 2, Car.CarBrand.Ford);
            hugeGarage.AddPredefinedVehicle(yesINeedASUV, 2);

            Vehicle fancyElectric = new Car("fancyElectric", "410POW", VehicleColors.Silver, VehicleTypes.Car, 3, Car.CarBrand.Renualt);
            hugeGarage.AddPredefinedVehicle(fancyElectric, 3);

            Vehicle rustyButWorking = new Car("rustyButWorking", "822YER", VehicleColors.Green, VehicleTypes.Car, 4, Car.CarBrand.Toyota);
            hugeGarage.AddPredefinedVehicle(rustyButWorking, 4);

            Vehicle schoolBus = new Bus("schoolBus", "123BUS", VehicleColors.Yellow, VehicleTypes.Bus, 5, 30);
            hugeGarage.AddPredefinedVehicle(schoolBus, 5);

            Vehicle ordinaryBus = new Bus("ordinaryBus", "321BUS", VehicleColors.Red, VehicleTypes.Bus, 6, 50);
            hugeGarage.AddPredefinedVehicle(ordinaryBus, 6);

            Vehicle coolerBike = new Motorcycle("coolerBike", "995WIN", VehicleColors.Silver, VehicleTypes.Motorcycle, 7, 150);
            hugeGarage.AddPredefinedVehicle(coolerBike, 7);
            return hugeGarage;
        }

        internal static Garage SpacedGarage()
        {
            Garage spacedGarage = new (12);
            Vehicle fastCar = new Car("Wroom", "24", VehicleColors.Red, VehicleTypes.Car, 0, Car.CarBrand.Porshe);
            spacedGarage.AddPredefinedVehicle(fastCar, 0);

            Vehicle mcBike = new Motorcycle("mcBike", "994MCC", VehicleColors.Black, VehicleTypes.Motorcycle, 1, 120);
            spacedGarage.AddPredefinedVehicle(mcBike, 1);

            Vehicle fancyElectric = new Car("fancyElectric", "410POW", VehicleColors.Silver, VehicleTypes.Car, 3, Car.CarBrand.Renualt);
            spacedGarage.AddPredefinedVehicle(fancyElectric, 3);

            Vehicle coolerBike = new Motorcycle("coolerBike", "995WIN", VehicleColors.Silver, VehicleTypes.Motorcycle, 6, 150);
            spacedGarage.AddPredefinedVehicle(coolerBike, 6);

            Vehicle daPlane = new Airplane("daPlane", "000FLY", VehicleColors.Blue, VehicleTypes.Airplane, 10, 500);
            spacedGarage.AddPredefinedVehicle(daPlane, 10);

            Vehicle ohSeven = new Car("ohSeven", "007BON", VehicleColors.Silver, VehicleTypes.Car, 7, Car.CarBrand.Saab);
            spacedGarage.AddPredefinedVehicle(ohSeven, 7);
            return spacedGarage;
        }
    }
}
