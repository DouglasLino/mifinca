using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mifinca.Models;

namespace mifinca.Controllers
{
    public class planillasController : Controller
    {
        private mifincaEntities db = new mifincaEntities();

        // GET: planillas
        public ActionResult Index()
        {
            var planilla = db.planilla.Include(p => p.empleado).Include(p => p.finca);
            return View(planilla.ToList());
        }

        // GET: planillas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planilla planilla = db.planilla.Find(id);
            if (planilla == null)
            {
                return HttpNotFound();
            }
            return View(planilla);
        }

        // GET: planillas/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre_empleado");
            //ViewBag.id_finca = new SelectList(db.finca, "id_finca", "foto_finca");
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "nombre_finca");
            return View();
        }

        // POST: planillas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_planilla,id_empleado,id_finca,fecha_resolucion,csv_planilla")] planilla planilla)
        {
            if (ModelState.IsValid)
            {
                db.planilla.Add(planilla);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre_empleado", planilla.id_empleado);
            //ViewBag.id_finca = new SelectList(db.finca, "id_finca", "foto_finca", planilla.id_finca);
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "nombre_finca", planilla.id_finca);
            return View(planilla);
        }

        // GET: planillas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planilla planilla = db.planilla.Find(id);
            if (planilla == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre_empleado", planilla.id_empleado);
            //ViewBag.id_finca = new SelectList(db.finca, "id_finca", "foto_finca", planilla.id_finca);
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "nombre_finca", planilla.id_finca);
            return View(planilla);
        }

        // POST: planillas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_planilla,id_empleado,id_finca,fecha_resolucion,csv_planilla")] planilla planilla)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planilla).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre_empleado", planilla.id_empleado);
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "foto_finca", planilla.id_finca);
            return View(planilla);
        }

        // GET: planillas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            planilla planilla = db.planilla.Find(id);
            if (planilla == null)
            {
                return HttpNotFound();
            }
            return View(planilla);
        }

        // POST: planillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            planilla planilla = db.planilla.Find(id);
            db.planilla.Remove(planilla);
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
    }
}
