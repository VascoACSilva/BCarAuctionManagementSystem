using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BCarAuctionManagementSystem.Validations;

namespace BCarAuctionManagementSystem.Models
{
    public abstract class Vehicle (string vehicleType, string identifier, string manufacturer, string model, int year, decimal startingBid)
    {
        [Required(AllowEmptyStrings = false)]
        public string VehicleType { get; set; } = vehicleType;

        [Required(AllowEmptyStrings = false)]
        [StringLength(6, MinimumLength = 6)]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Only capital letters and numbers allowed.")]
        public string Identifier { get; set; } = identifier;

        [Required(AllowEmptyStrings = false)]
        [StringLength(20)]
        public string Manufacturer { get; set; } = manufacturer;

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string Model {  get; set; } = model;

        [Required]
        [CurrentYearRange]
        public int Year { get; set; } = year;

        [Required]
        [Range(1, double.MaxValue)]
        [TwoDecimalPlaces]
        public decimal StartingBid { get; set; } = startingBid;

        [Required]
        [Range(1, double.MaxValue)]
        [TwoDecimalPlaces]
        public decimal HighestBid { get; set; } = startingBid;

        [Required]
        public AuctionState AuctionState { get; set; } = AuctionState.NotStarted;
    }
}
