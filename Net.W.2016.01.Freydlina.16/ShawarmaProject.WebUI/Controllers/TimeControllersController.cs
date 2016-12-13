using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShawarmaProject.DB;

namespace ShawarmaProject.WebUI.Controllers
{
    public class TimeControllersController : Controller
    {
        private ShawarmaDBEntities db = new ShawarmaDBEntities();

        // GET: TimeControllers
        public async Task<ActionResult> Index()
        {
            var timeControllers = db.TimeControllers.Include(t => t.Seller);
            return View(await timeControllers.ToListAsync());
        }

        // GET: TimeControllers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeController timeController = await db.TimeControllers.FindAsync(id);
            if (timeController == null)
            {
                return HttpNotFound();
            }
            return View(timeController);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
