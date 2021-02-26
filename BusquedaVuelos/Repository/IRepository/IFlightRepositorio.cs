using BusquedaVuelos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusquedaVuelos.Repository.IRepository
{
    public interface IFlightRepositorio : IDisposable
    {
        Task<List<Flight>> GetAllFlights();
        Task<List<Flight>> GetFlightByOrigenAndDestino(string origen, string destino);
    }
}
