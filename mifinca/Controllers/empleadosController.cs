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
    public class empleadosController : Controller
    {
        private mifincaEntities db = new mifincaEntities();

        // GET: empleados
        public ActionResult Index()
        {
            var empleado = db.empleado.Include(e => e.planilla).Include(e => e.tarea);
            return View(empleado.ToList());
        }

        // GET: empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: empleados/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.planilla, "id_empleado", "fecha_resolucion");
            ViewBag.id_tarea = new SelectList(db.tarea, "id_tarea", "nombre");
            return View();
        }

        // POST: empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_empleado,id_finca,id_tarea,nombre,apellido,puesto,direccion,telefono")] empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.planilla, "id_empleado", "fecha_resolucion", empleado.id_empleado);
            ViewBag.id_tarea = new SelectList(db.tarea, "id_tarea", "nombre", empleado.id_tarea);
            return View(empleado);
        }

        // GET: empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.planilla, "id_empleado", "fecha_resolucion", empleado.id_empleado);
            ViewBag.id_tarea = new SelectList(db.tarea, "id_tarea", "nombre", empleado.id_tarea);
            return View(empleado);
        }

        // POST: empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_empleado,id_finca,id_tarea,nombre,apellido,puesto,direccion,telefono")] empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.planilla, "id_empleado", "fecha_resolucion", empleado.id_empleado);
            ViewBag.id_tarea = new SelectList(db.tarea, "id_tarea", "nombre", empleado.id_tarea);
            return View(empleado);
        }

        // GET: empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            empleado empleado = db.empleado.Find(id);
            db.empleado.Remove(empleado);
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
