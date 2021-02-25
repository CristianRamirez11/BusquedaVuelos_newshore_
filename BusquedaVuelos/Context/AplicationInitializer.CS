using BusquedaVuelos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusquedaVuelos.Context
{
    public class AplicationInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AplicationDBContext>
    {
        protected override void Seed(AplicationDBContext context)
        {
            List<Transport> transports = new List<Transport>
            {
                new Transport{ FlightNumber = "811" },
                new Transport{ FlightNumber = "8020" }
            };

            transports.ForEach(t => context.Transports.Add(t));
            context.SaveChanges();

            List<Flight> flights = new List<Flight>
            {
                new Flight{ DepartureStation = "MDE", ArrivalStation = "BOG", DepartureDate = DateTime.Parse("2019-08-01"), TransportId = 2, Price = 23094, Currency = "COP" },
                new Flight{ DepartureStation = "MDE", ArrivalStation = "BOG", DepartureDate = DateTime.Parse("2019-08-01"), TransportId = 1, Price = 23094, Currency = "COP" }
            };
            flights.ForEach(f => context.Flights.Add(f));
            context.SaveChanges();
        }
    }
}