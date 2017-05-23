using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExportsOfGoods.Models;
using System.Globalization;

namespace ExportsOfGoods.Controllers
{
    public class PartiesController : Controller
    {
        private ExportsContext db = new ExportsContext();

        // GET: Parties
        public async Task<ActionResult> Index()
        {
            var parties = db.Parties.Include(p => p.Product);
            return View(await parties.ToListAsync());
        }

        // GET: Parties/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parti parti = await db.Parties.FindAsync(id);
            parti.Product = await db.Products.FindAsync(parti.ProductId);
            if (parti == null)
            {
                return HttpNotFound();
            }
            return View(parti);
        }

        // GET: Parties/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: Parties/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProductId,PartiSize")] Parti parti, string inspDate)
        {
            DateTime dt = new DateTime();
            if (!DateTime.TryParseExact(inspDate, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
                ModelState.AddModelError("", "Формат даты dd.MM.yyyy HH:mm");
            else
            {
                int min = dt.Minute;
                if (min < 30)
                    dt = dt.AddMinutes(30 - min);
                else
                    dt = dt.AddMinutes(60 - min);
                parti.InspectionDate = dt;
                dt = dt.AddMinutes(30);
                parti.InspectionTime = dt;
            }

            if (ModelState.IsValid)
            {
                db.Parties.Add(parti);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", parti.ProductId);
            return View(parti);
        }

        // GET: Parties/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parti parti = await db.Parties.FindAsync(id);
            parti.Product = await db.Products.FindAsync(parti.ProductId);
            if (parti == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", parti.ProductId);
            return View(parti);
        }

        // POST: Parties/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProductId,PartiSize")] Parti parti, string inspDate)
        {
            DateTime dt = new DateTime();
            if (!DateTime.TryParseExact(inspDate, "dd.MM.yyyy HH:mm", new CultureInfo("fr-FR"), DateTimeStyles.None, out dt))
                ModelState.AddModelError("", "Формат даты: dd.MM.yyyy HH:mm");
            else
            {
                parti.InspectionDate = dt;
            }
            if (ModelState.IsValid)
            {
                db.Entry(parti).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", parti.ProductId);
            return View(parti);
        }

        // GET: Parties/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parti parti = await db.Parties.FindAsync(id);
            parti.Product = await db.Products.FindAsync(parti.ProductId);
            if (parti == null)
            {
                return HttpNotFound();
            }
            return View(parti);
        }

        // POST: Parties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Parti parti = await db.Parties.FindAsync(id);
            db.Parties.Remove(parti);
            await db.SaveChangesAsync();
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
    }
}
