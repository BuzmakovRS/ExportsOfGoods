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
    public class CustomsQueuesController : Controller
    {
        private ExportsContext db = new ExportsContext();
        [Authorize]
        // GET: CustomsQueues
        public async Task<ActionResult> Index()
        {
            if (HttpContext.User.IsInRole("admin"))
            {
                ViewBag.isAdmin = true;
            }
            var customsQueues = db.CustomsQueues.Include(c => c.Customs).Include(c => c.Parti);
            return View(await customsQueues.ToListAsync());
        }

        // GET: CustomsQueues/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomsQueue customsQueue = await db.CustomsQueues.FindAsync(id);
            customsQueue.Customs = await db.Customs.FindAsync(customsQueue.CustomsId);
            customsQueue.Parti = await db.Parties.FindAsync(customsQueue.PartiId);
            if (customsQueue == null)
            {
                return HttpNotFound();
            }
            return View(customsQueue);
        }

        // GET: CustomsQueues/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CustomsId = new SelectList(db.Customs, "Id", "Name");
            ViewBag.PartiId = new SelectList(db.Parties, "Id", "GetName");

            return View();
        }

        // POST: CustomsQueues/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomsId,PartiId")] CustomsQueue customsQueue, string timeBegInsp, string timeEndInsp)
        {
            if (db.CustomsQueues.ToList().FirstOrDefault(c => c.PartiId == customsQueue.PartiId) != null)
            {
                ModelState.AddModelError("PartiId", "Данная партия уже зарегистрирована");
            }
            DateTime dt = new DateTime();
            if (!DateTime.TryParseExact(timeBegInsp, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
                ModelState.AddModelError("timeBegInsp", "Формат даты: dd.MM.yyyy HH:mm");
            else
            {
                customsQueue.TimeBegInsp = dt;
            }
            if (!DateTime.TryParseExact(timeEndInsp, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
                ModelState.AddModelError("timeEndInsp", "Формат даты: dd.MM.yyyy HH:mm");
            else
            {
                customsQueue.TimeEndInsp = dt;
            }
            if (!EntryInQueues(customsQueue))
            {
                ModelState.AddModelError("", "Ошибка записи: \nна это время уже назначена запись.\n Выберите другое время.");
            }
            ViewBag.ListQ = db.CustomsQueues.Where(c => c.TimeBegInsp.Year == customsQueue.TimeBegInsp.Year &&
            c.TimeBegInsp.Month == customsQueue.TimeBegInsp.Month &&
            c.TimeBegInsp.Day == customsQueue.TimeBegInsp.Day).ToList();
            if (ModelState.IsValid)
            {
                db.CustomsQueues.Add(customsQueue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomsId = new SelectList(db.Customs, "Id", "Name", customsQueue.CustomsId);
            ViewBag.PartiId = new SelectList(db.Parties, "Id", "GetName", customsQueue.PartiId);
            return View(customsQueue);
        }


        // GET: CustomsQueues/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomsQueue customsQueue = await db.CustomsQueues.FindAsync(id);
            customsQueue.Customs = await db.Customs.FindAsync(customsQueue.CustomsId);
            customsQueue.Parti = await db.Parties.FindAsync(customsQueue.PartiId);
            if (customsQueue == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomsId = new SelectList(db.Customs, "Id", "Name", customsQueue.CustomsId);
            return View(customsQueue);
        }

        // POST: CustomsQueues/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomsId,PartiId")] CustomsQueue customsQueue, string timeBegIns, string timeEndIns)
        {

            DateTime dt = new DateTime();
            if (!DateTime.TryParseExact(timeBegIns, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
                ModelState.AddModelError("", "Укажите дату в формате dd.MM.yyyy HH:mm");
            else
            {
                customsQueue.TimeBegInsp = dt;
            }
            if (!DateTime.TryParseExact(timeEndIns, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
                ModelState.AddModelError("timeEndInsp", "Укажите дату в формате dd.MM.yyyy HH:mm");
            else
            {
                customsQueue.TimeEndInsp = dt;
            }
            if (!EntryInQueues(customsQueue))
            {
                ModelState.AddModelError("", "Ошибка записи: \nна этот интервал времени уже произведена запись.\n Выберите другое время.");
            }
            //ViewBag.ListQ = db.CustomsQueues.Where(c => c.TimeBegInsp.Year == customsQueue.TimeBegInsp.Year &&
            //c.TimeBegInsp.Month == customsQueue.TimeBegInsp.Month &&
            //c.TimeBegInsp.Day == customsQueue.TimeBegInsp.Day).ToList();
            if (ModelState.IsValid)
            {
                db.Entry(customsQueue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomsId = new SelectList(db.Customs, "Id", "Name", customsQueue.CustomsId);
            return View(customsQueue);
        }

        // GET: CustomsQueues/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomsQueue customsQueue = await db.CustomsQueues.FindAsync(id);
            customsQueue.Customs = await db.Customs.FindAsync(customsQueue.CustomsId);
            customsQueue.Parti = await db.Parties.FindAsync(customsQueue.PartiId);
            if (customsQueue == null)
            {
                return HttpNotFound();
            }
            return View(customsQueue);
        }

        // POST: CustomsQueues/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CustomsQueue customsQueue = await db.CustomsQueues.FindAsync(id);
            db.CustomsQueues.Remove(customsQueue);
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
        [Authorize]
        public async Task<string> GetInspTimeEnd(string tb, string pId)
        {
            DateTime dt = new DateTime();
            if (!DateTime.TryParseExact(tb, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
            {
                return "Установите время начала досмотра";
            }
            else
            {
                Parti parti = await db.Parties.FindAsync(Convert.ToInt32(pId));
                dt = dt.AddHours(parti.InspectionTime.Value.Hour);
                dt = dt.AddMinutes(parti.InspectionTime.Value.Minute);
                return dt.ToString("dd.MM.yyyy HH:mm");
            }

        }

        private bool EntryInQueues(CustomsQueue customsQ)
        {
            int dayC = customsQ.TimeBegInsp.Day;
            var CQList = db.CustomsQueues.Where(c => c.CustomsId == customsQ.CustomsId && c.Id !=customsQ.Id)
                .Where(c => c.TimeBegInsp.Day == dayC).Include(c => c.Parti).ToList();

            bool freeDate = true;
            if (CQList != null)
            {
                for (int i = 0; i < CQList.Count; i++)
                {
                    if ((CQList[i].TimeBegInsp >= customsQ.TimeBegInsp && CQList[i].TimeBegInsp < customsQ.TimeEndInsp) ||
                        (CQList[i].TimeEndInsp > customsQ.TimeBegInsp && CQList[i].TimeEndInsp <= customsQ.TimeEndInsp) ||
                        (CQList[i].TimeBegInsp < customsQ.TimeBegInsp && CQList[i].TimeEndInsp > customsQ.TimeBegInsp) ||
                        (CQList[i].TimeBegInsp < customsQ.TimeEndInsp && CQList[i].TimeEndInsp >= customsQ.TimeEndInsp))
                    {
                        freeDate = false; break;
                    }
                    //if (dateTimeBeg.TimeOfDay < new TimeSpan(9, 0, 0) || dateTimeEnd.TimeOfDay > new TimeSpan(23, 0, 0) || dateTimeEnd.TimeOfDay < new TimeSpan(9,0,0))
                    //    freeDate = false;
                }
            }
            return freeDate;
        }

    }
}
