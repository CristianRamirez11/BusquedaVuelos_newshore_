using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BusquedaVuelos.Context;
using BusquedaVuelos.Models;

namespace BusquedaVuelos.Repository.IRepository
{
    public class FlightRepositorio : IFlightRepositorio
    {
        private readonly AplicationDBContext _dBContext;

        public FlightRepositorio(AplicationDBContext dBContext)
        {
            this._dBContext = dBContext;
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            return _dBContext.Flights.ToList();
        }

        public async Task<List<Flight>> GetFlightByOrigenAndDestino(string origen, string destino)
        {
            List<Flight> vuelosEncontrados = await GetAllFlights();

            vuelosEncontrados = vuelosEncontrados.Where(f => f.DepartureStation.Trim().Equals(origen.Trim())
            || f.ArrivalStation.Trim().Equals(destino.Trim())).ToList();

            return vuelosEncontrados;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dBContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}