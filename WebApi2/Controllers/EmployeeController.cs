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
    public class EmployeeController : ApiController
    {
        private QMileDbContext db = new QMileDbContext();

        // GET: api/Employee
        public IQueryable<EmployeeModel> GetEmployees()
        {
            return db.EmployeeModel;
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