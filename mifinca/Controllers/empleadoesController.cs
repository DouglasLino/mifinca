using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mifinca.Models;
using System.Drawing;


namespace mifinca.Controllers
{
    public class empleadoesController : Controller
    {
        private mifincaEntities db = new mifincaEntities();

        // GET: empleadoes
        

        public ActionResult Index()
        {
            var empleado = db.empleado.Include(e => e.finca);
            return View(empleado.ToList());
        }

        public ActionResult Look(string nombre)
        {
            if (nombre == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            empleado empleado = db.empleado.Find(nombre);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: empleadoes/Details/5
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

        // GET: empleadoes/Create
        public ActionResult Create()
        {
            //ViewBag.id_finca = new SelectList(db.finca, "id_finca", "foto_finca");
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "nombre_finca");
            return View();
        }

        // POST: empleadoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_empleado,id_finca,nombre_empleado,apellido_empleado,direccion_empleado,telefono_empleado,telefonoEmergencia_empleado,qr_empleado")] empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.id_finca = new SelectList(db.finca, "id_finca", "foto_finca", empleado.id_finca);
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "nombre_finca", empleado.id_finca);
            return View(empleado);
        }

        // GET: empleadoes/Edit/5
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
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "nombre_finca", empleado.id_finca);
            return View(empleado);
        }

        // POST: empleadoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_empleado,id_finca,nombre_empleado,apellido_empleado,direccion_empleado,telefono_empleado,telefonoEmergencia_empleado,qr_empleado")] empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                System.Console.WriteLine(db.Entry(empleado).State);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "nombre_finca", empleado.id_finca);
            return View(empleado);
        }

        // GET: empleadoes/Delete/5
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

        // POST: empleadoes/Delete/5
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
