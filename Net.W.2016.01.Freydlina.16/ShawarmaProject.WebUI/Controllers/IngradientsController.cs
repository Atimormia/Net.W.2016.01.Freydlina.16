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
    public class IngradientsController : Controller
    {
        private ShawarmaDBEntities db = new ShawarmaDBEntities();

        // GET: Ingradients
        public async Task<ActionResult> Index()
        {
            var ingradients = db.Ingradients.Include(i => i.IngradientCategory);
            return View(await ingradients.ToListAsync());
        }

        // GET: Ingradients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingradient ingradient = await db.Ingradients.FindAsync(id);
            if (ingradient == null)
            {
                return HttpNotFound();
            }
            return View(ingradient);
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
