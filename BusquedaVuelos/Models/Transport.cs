using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusquedaVuelos.Models
{
    public class Transport
    {
        [Key]
        public int Id { get; set; }
        public string FlightNumber { get; set; }

        public virtual List<Flight> Flights { get; set; }
    }
}