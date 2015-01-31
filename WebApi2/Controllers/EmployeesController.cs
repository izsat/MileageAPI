using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi2.Models;

namespace WebApi2.Controllers
{
    public class EmployeesController : ApiController
    {
        private QMileDbContext db = new QMileDbContext();

        // GET: api/Employees
        public IQueryable<EmployeeModel> GetEmployeeModel()
        {
            return db.EmployeeModel;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(EmployeeModel))]
        public IHttpActionResult GetEmployeeModel(int id)
        {
            EmployeeModel employeeModel = db.EmployeeModel.Find(id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return Ok(employeeModel);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeModel(int id, EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeModel.Id)
            {
                return BadRequest();
            }

            db.Entry(employeeModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeModelExists(id))
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

        // POST: api/Employees
        [ResponseType(typeof(EmployeeModel))]
        public IHttpActionResult PostEmployeeModel(EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeModel.Add(employeeModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeModel.Id }, employeeModel);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(EmployeeModel))]
        public IHttpActionResult DeleteEmployeeModel(int id)
        {
            EmployeeModel employeeModel = db.EmployeeModel.Find(id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            db.EmployeeModel.Remove(employeeModel);
            db.SaveChanges();

            return Ok(employeeModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeModelExists(int id)
        {
            return db.EmployeeModel.Count(e => e.Id == id) > 0;
        }
    }
}