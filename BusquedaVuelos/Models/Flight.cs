using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusquedaVuelos.Models
{
    /// <summary>
    /// Clase que representa los vuelos disponibles en el sistema
    /// </summary>
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public virtual Transport Transport { get; set; }
        public int TransportId { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}