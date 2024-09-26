using BCarAuctionManagementSystem.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCarAuctionManagementSystem.Models;

namespace BCarAuctionManagementSystem.Managers
{
    public class AuctionManager
    {
        public readonly List<Vehicle> inventory = [];

        public void AddVehicle(string vehicleType, string identifier, string manufacturer, string model, int year, decimal startingBid, int? numberOfDoors, int? numberOfSeats, decimal? loadCapacity) 
        {
            var (vehicle, creationErrors) = VehicleFactory.CreateVehicle(vehicleType, identifier, manufacturer, model, year, startingBid, numberOfDoors, numberOfSeats, loadCapacity);

            var errors = new List<string>();
            errors.AddRange(creationErrors);

            if (inventory.Any(v => v.Identifier == identifier))
                throw new ValidationException($"A vehicle with identifier {identifier} already exists.");

            if (vehicle != null)
            {
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(vehicle);

                if (!Validator.TryValidateObject(vehicle, validationContext, validationResults, true))
                    errors.AddRange(validationResults.Where(x => x.ErrorMessage != null).Select(x => x.ErrorMessage!));
            }

            if (errors.Count > 0)
            {
                var errorMessage = string.Join("; ", errors);
                throw new ValidationException($"Validation failed for the vehicle: {errorMessage}");
            }
            else if (vehicle != null)
            {
                inventory.Add(vehicle);
            }            
        }

        public List<Vehicle> SearchVehicles(string? vehicleType, string? manufacturer, string? model, int? year)
        {
            var query = inventory.AsQueryable();

            if (!string.IsNullOrWhiteSpace(vehicleType))
                query = query.Where(v => v.VehicleType.Equals(vehicleType, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(manufacturer))
                query = query.Where(v => v.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(model))
                query = query.Where(v => v.Model.Equals(model, StringComparison.OrdinalIgnoreCase));

            if (year.HasValue)
                query = query.Where(v => v.Year == year);

            return [.. query];
        }

        public void StartAuction(string identifier)
        {
            var vehicle = inventory.FirstOrDefault(v => v.Identifier == identifier) ?? throw new ArgumentException($"Vehicle with identifier {identifier} does not exist.");

            vehicle.AuctionState = vehicle.AuctionState switch
            {
                AuctionState.NotStarted => AuctionState.Active,
                AuctionState.Active => throw new InvalidOperationException($"Auction for vehicle {identifier} is already active."),
                AuctionState.Closed => throw new InvalidOperationException($"Auction for vehicle {identifier} has already concluded."),
                _ => throw new InvalidOperationException($"Unhandled auction state: {vehicle.AuctionState}"),
            };
        }

        public void CloseAuction(string identifier)
        {
            var vehicle = inventory.FirstOrDefault(v => v.Identifier == identifier) ?? throw new ArgumentException($"Vehicle with identifier {identifier} does not exist.");

            if (vehicle.AuctionState == AuctionState.Active)
                vehicle.AuctionState = AuctionState.Closed;
            else
                throw new InvalidOperationException($"Auction for vehicle {identifier} is not currently active.");
        }

        public void PlaceBid(string identifier, decimal newBid)
        {
            var vehicle = inventory.FirstOrDefault(v => v.Identifier == identifier) ?? throw new ArgumentException($"Vehicle with identifier {identifier} does not exist.");

            if (vehicle.AuctionState != AuctionState.Active)
                throw new InvalidOperationException($"Auction for vehicle {identifier} is not currently active.");

            var errors = new List<string>();
            if (newBid <= vehicle.HighestBid)
                errors.Add($"New bids for vehicle {identifier} must be higher than the current highest bid: {vehicle.HighestBid}€.");

            var decimalPlaces = BitConverter.GetBytes(decimal.GetBits(newBid)[3])[2];
            if (decimalPlaces > 2)
                errors.Add("The bid value must have no more than 2 decimal places.");

            if (errors.Count > 0)
                throw new InvalidOperationException(string.Join(" ", errors));

            vehicle.HighestBid = newBid;
        }

        public bool IdentifierExists(string identifier)
        {
            return inventory.Any(v => v.Identifier == identifier);
        }
    }
}
