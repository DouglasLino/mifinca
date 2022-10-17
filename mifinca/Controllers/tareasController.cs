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
    public class tareasController : Controller
    {
        private mifincaEntities db = new mifincaEntities();

        // GET: tareas
        public ActionResult Index()
        {
            var tarea = db.tarea.Include(t => t.empleado).Include(t => t.tipo_tarea);
            return View(tarea.ToList());
        }

        // GET: tareas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarea tarea = db.tarea.Find(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }

        // GET: tareas/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre_empleado");
            ViewBag.id_tipo = new SelectList(db.tipo_tarea, "id_tipo", "tipo");
            return View();
        }

        // POST: tareas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tarea,id_empleado,id_tipo,fecha_inicio,fecha_fin,descripcion")] tarea tarea)
        {
            if (ModelState.IsValid)
            {
                db.tarea.Add(tarea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre_empleado", tarea.id_empleado);
            ViewBag.id_tipo = new SelectList(db.tipo_tarea, "id_tipo", "tipo", tarea.id_tipo);
            return View(tarea);
        }

        // GET: tareas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarea tarea = db.tarea.Find(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre_empleado", tarea.id_empleado);
            ViewBag.id_tipo = new SelectList(db.tipo_tarea, "id_tipo", "tipo", tarea.id_tipo);
            return View(tarea);
        }

        // POST: tareas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tarea,id_empleado,id_tipo,fecha_inicio,fecha_fin,descripcion")] tarea tarea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.empleado, "id_empleado", "nombre_empleado", tarea.id_empleado);
            ViewBag.id_tipo = new SelectList(db.tipo_tarea, "id_tipo", "tipo", tarea.id_tipo);
            return View(tarea);
        }

        // GET: tareas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tarea tarea = db.tarea.Find(id);
            if (tarea == null)
            {
                return HttpNotFound();
            }
            return View(tarea);
        }

        // POST: tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tarea tarea = db.tarea.Find(id);
            db.tarea.Remove(tarea);
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
