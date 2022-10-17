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
    public class tipo_tareaController : Controller
    {
        private mifincaEntities db = new mifincaEntities();

        // GET: tipo_tarea
        public ActionResult Index()
        {
            return View(db.tipo_tarea.ToList());
        }

        // GET: tipo_tarea/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_tarea tipo_tarea = db.tipo_tarea.Find(id);
            if (tipo_tarea == null)
            {
                return HttpNotFound();
            }
            return View(tipo_tarea);
        }

        // GET: tipo_tarea/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipo_tarea/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo,tipo")] tipo_tarea tipo_tarea)
        {
            if (ModelState.IsValid)
            {
                db.tipo_tarea.Add(tipo_tarea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_tarea);
        }

        // GET: tipo_tarea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_tarea tipo_tarea = db.tipo_tarea.Find(id);
            if (tipo_tarea == null)
            {
                return HttpNotFound();
            }
            return View(tipo_tarea);
        }

        // POST: tipo_tarea/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo,tipo")] tipo_tarea tipo_tarea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_tarea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_tarea);
        }

        // GET: tipo_tarea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tipo_tarea tipo_tarea = db.tipo_tarea.Find(id);
            if (tipo_tarea == null)
            {
                return HttpNotFound();
            }
            return View(tipo_tarea);
        }

        // POST: tipo_tarea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tipo_tarea tipo_tarea = db.tipo_tarea.Find(id);
            db.tipo_tarea.Remove(tipo_tarea);
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
