using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    public class TripController : ApiController
    {
        private QMileDbContext db = new QMileDbContext();
         
        

        // GET: api/Trip
        public IQueryable<TripModel> GetTrips()
        {
            List<TripModel> trips = new List<TripModel>();

            IQueryable<TripModel> tripList = db.Trips.Include(b => b.EndLocation);

            return tripList;
        }

        // GET: api/Trip/5
        [ResponseType(typeof(TripModel))]
        public IHttpActionResult GetTripModel(int id)
        {
            TripModel tripModel = db.Trips.Find(id);
            if (tripModel == null)
            {
                return NotFound();
            }

            return Ok(tripModel);
        }

        // PUT: api/Trip/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTripModel(int id, TripModel tripModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tripModel.Id)
            {
                return BadRequest();
            }

            db.Entry(tripModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Trip
        [ResponseType(typeof(TripModel))]
        public IHttpActionResult PostTripModel(TripModel tripModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trips.Add(tripModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tripModel.Id }, tripModel);
        }

        // DELETE: api/Trip/5
        [ResponseType(typeof(TripModel))]
        public IHttpActionResult DeleteTripModel(int id)
        {
            TripModel tripModel = db.Trips.Find(id);
            if (tripModel == null)
            {
                return NotFound();
            }

            db.Trips.Remove(tripModel);
            db.SaveChanges();

            return Ok(tripModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TripModelExists(int id)
        {
            return db.Trips.Count(e => e.Id == id) > 0;
        }
    }
}