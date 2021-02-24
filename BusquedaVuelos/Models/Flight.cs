using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusquedaVuelos.Models
{
    public class Flight
    {
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public virtual Transport Transport { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}