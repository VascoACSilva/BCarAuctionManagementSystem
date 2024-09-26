using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCarAuctionManagementSystem.Models;

namespace BCarAuctionManagementSystem.Factories
{
    public static class VehicleFactory
    {
        public static (Vehicle? vehicle, List<string> errors) CreateVehicle(string vehicleType, string identifier, string manufacturer, string model, int year, decimal startingBid, int? numberOfDoors = null, int? numberOfSeats = null, decimal? loadCapacity = null)
        {
            var errors = new List<string>();

            if (!Enum.TryParse(vehicleType, out VehicleType parsedVehicleType))
                errors.Add("Invalid vehicle type.");
            if (errors.Count != 0)
                return (null, errors);
            

            Vehicle? vehicle = parsedVehicleType switch
            {
                VehicleType.Hatchback => new Hatchback(vehicleType, identifier, manufacturer, model, year, startingBid, numberOfDoors),
                VehicleType.Sedan => new Sedan(vehicleType, identifier, manufacturer, model, year, startingBid, numberOfDoors),
                VehicleType.SUV => new SUV(vehicleType, identifier, manufacturer, model, year, startingBid, numberOfSeats),
                VehicleType.Truck => new Truck(vehicleType, identifier, manufacturer, model, year, startingBid, loadCapacity),
                _ => null
            };

            return (vehicle, errors);
        }
    }
}
