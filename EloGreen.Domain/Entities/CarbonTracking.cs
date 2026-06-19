using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EloGreen.Domain.Entities
{
    public class CarbonTracking
    {
        public Guid Id { get; set; }
        public string ActivityDescription { get; set; } = string.Empty;
        public decimal CarbonEmitted { get; set; }
        public DateTime TrackingDate { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
