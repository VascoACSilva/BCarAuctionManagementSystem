using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCarAuctionManagementSystem.Models
{
    public class Hatchback (string vehicleType, string identifier, string manufacturer, string model, int year, decimal startingBid, int? numberOfDoors) : Vehicle (vehicleType, identifier, manufacturer, model, year, startingBid)
    {
        [Required]
        [Range(2, 5)]
        public int? NumberOfDoors { get; set; } = numberOfDoors;
    }
}
