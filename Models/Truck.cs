using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCarAuctionManagementSystem.Models
{
    public class Truck(string vehicleType, string identifier, string manufacturer, string model, int year, decimal startingBid, decimal? loadCapacity) : Vehicle(vehicleType, identifier, manufacturer, model, year, startingBid)
    {
        [Required]
        [Range(1, double.MaxValue)]
        public decimal? LoadCapacity { get; set; } = loadCapacity;
    }
}
