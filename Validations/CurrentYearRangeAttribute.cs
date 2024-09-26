using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BCarAuctionManagementSystem.Validations
{
    public class CurrentYearRangeAttribute : RangeAttribute
    {
        public CurrentYearRangeAttribute() : base(1950, DateTime.Now.Year) { }
    }
}
