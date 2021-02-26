using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusquedaVuelos.Models
{
    /// <summary>
    /// Clase para obtener los parametros con los cuales se va ser la consulta 
    /// a la API
    /// </summary>
    public class Response
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string From { get; set; }
    }
}