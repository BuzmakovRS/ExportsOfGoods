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

namespace ExportsOfGoods.Controllers
{
    public class TypeOfInspectionsController : Controller
    {
        private ExportsContext db = new ExportsContext();

        [Authorize(Roles ="admin")]
        // GET: TypeOfInspecions
        public async Task<ActionResult> Index()
        {
            return View(await db.TypeOfInspection.ToListAsync());
        }

        [Authorize(Roles = "admin")]
        // GET: TypeOfInspecions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfInspecion typeOfInspecion = await db.TypeOfInspection.FindAsync(id);
            if (typeOfInspecion == null)
            {
                return HttpNotFound();
            }
            return View(typeOfInspecion);
        }

        [Authorize(Roles = "admin")]
        // GET: TypeOfInspecions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeOfInspecions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Type,Time")] TypeOfInspecion typeOfInspecion)
        {
            if (ModelState.IsValid)
            {
                db.TypeOfInspection.Add(typeOfInspecion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typeOfInspecion);
        }

        [Authorize(Roles = "admin")]
        // GET: TypeOfInspecions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfInspecion typeOfInspecion = await db.TypeOfInspection.FindAsync(id);
            if (typeOfInspecion == null)
            {
                return HttpNotFound();
            }
            return View(typeOfInspecion);
        }

        // POST: TypeOfInspecions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Type,Time")] TypeOfInspecion typeOfInspecion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeOfInspecion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typeOfInspecion);
        }

        // GET: TypeOfInspecions/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeOfInspecion typeOfInspecion = await db.TypeOfInspection.FindAsync(id);
            if (typeOfInspecion == null)
            {
                return HttpNotFound();
            }
            return View(typeOfInspecion);
        }

        // POST: TypeOfInspecions/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TypeOfInspecion typeOfInspecion = await db.TypeOfInspection.FindAsync(id);
            db.TypeOfInspection.Remove(typeOfInspecion);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize]
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
