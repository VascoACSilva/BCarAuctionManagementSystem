using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCarAuctionManagementSystem.Models
{
    public class SUV(string vehicleType, string identifier, string manufacturer, string model, int year, decimal startingBid, int? numberOfSeats) : Vehicle(vehicleType, identifier, manufacturer, model, year, startingBid)
    {
        [Required]
        [Range(2, 9)]
        public int? NumberOfSeats { get; set; } = numberOfSeats;
    }
}

