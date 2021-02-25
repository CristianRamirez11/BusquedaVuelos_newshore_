using BusquedaVuelos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BusquedaVuelos.Context
{
    public class AplicationDBContext : DbContext
    {
        public AplicationDBContext() : base("AplicationDBContext")
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Transport> Transports { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}