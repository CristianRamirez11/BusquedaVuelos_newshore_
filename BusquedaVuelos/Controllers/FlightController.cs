using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusquedaVuelos.Context;
using BusquedaVuelos.Models;
using BusquedaVuelos.Repository.IRepository;
using Newtonsoft.Json;

namespace BusquedaVuelos.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightRepositorio _flightRepositorio;
        private AplicationDBContext db = new AplicationDBContext();

        private const string BaseUrl = "http://testapi.vivaair.com/otatest/api/values";

        public FlightController()
        {
            this._flightRepositorio = new FlightRepositorio(new AplicationDBContext());
        }

        // GET: Flight
        public async Task<ActionResult> Index(string OrigenString, string DestinoString)
        {
            try
            {
                if (!String.IsNullOrEmpty(OrigenString) || !String.IsNullOrEmpty(DestinoString))
                {
                    List<Flight> flights = await _flightRepositorio.GetFlightByOrigenAndDestino(OrigenString, DestinoString);
                    return View(flights.ToList().OrderBy(f => f.DepartureStation));
                }
                else
                {
                    var flights = db.Flights.Include(f => f.Transport);
                    return View(flights.ToList().OrderBy(f => f.DepartureStation));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // GET: Flight/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flight/Create
        public ActionResult Create()
        {
            ViewBag.TransportId = new SelectList(db.Transports, "Id", "FlightNumber");
            return View();
        }

        // POST: Flight/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepartureStation,ArrivalStation,DepartureDate,TransportId,Price,Currency")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TransportId = new SelectList(db.Transports, "Id", "FlightNumber", flight.TransportId);
            return View(flight);
        }

        // GET: Flight/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.TransportId = new SelectList(db.Transports, "Id", "FlightNumber", flight.TransportId);
            return View(flight);
        }

        // POST: Flight/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepartureStation,ArrivalStation,DepartureDate,TransportId,Price,Currency")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TransportId = new SelectList(db.Transports, "Id", "FlightNumber", flight.TransportId);
            return View(flight);
        }

        // GET: Flight/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Servicio para consumir el API y consultar los vuelos
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> IndexApi()
        {
            try
            {
                List<Flight> infoFlights = new List<Flight>();
                Response response = new Response()
                {
                    Origin = "BOG",
                    Destination = "CTG",
                    From = "2020-08-20"
                };

                StringContent JSONResult = new StringContent(JsonConvert.SerializeObject(response), Encoding.UTF8, "application/json");
                string JSONGenerado = await JSONResult.ReadAsStringAsync();

                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(BaseUrl);
                    cliente.DefaultRequestHeaders.Clear();
                    cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage res = await cliente.PostAsJsonAsync(BaseUrl,JSONGenerado);
                    if (res.IsSuccessStatusCode)
                    {
                        var flightResponse = res.Content.ReadAsStringAsync().Result;
                        infoFlights = JsonConvert.DeserializeObject<List<Flight>>(flightResponse);
                    }

                    return View(infoFlights);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
