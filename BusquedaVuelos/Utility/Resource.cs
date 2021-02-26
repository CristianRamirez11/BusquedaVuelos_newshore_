using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusquedaVuelos.Utility
{
    public class Resource
    {
        public const string APIBaseUrl = "http://localhost:62006/";
        public const string FlightAPIUrl = APIBaseUrl + "api/flight/";
        public const string ContentType = "application/json";
    }
}