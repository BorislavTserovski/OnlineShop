using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using System.IO;

namespace OnlineShop.Controllers
{
    public class CarsController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        private bool isUserAuthorizedToEdit(Car car)
        {
            bool isAdmin = this.User.IsInRole("Admin");


            return isAdmin;
        }

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.Buyer);
            return View(cars.ToList());
        }


        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "FirstName");
            using (db)
            {
                var model = new Car();
                model.Categories = db.Categories
                    .OrderBy(c => c.Name)
                    .ToList();

                return View(model);
            }
          
        }

        // POST: Cars/Create


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Make,Model,Price,Year,DateAdded,Image,CategoryId")] Car car,
            HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                using (MemoryStream ms = new MemoryStream())
                    if (file != null)
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        car.Image = array;
                    }
               

                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuyerId = new SelectList(db.Users, "Id", "FirstName", car.BuyerId);
            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);

            if (!isUserAuthorizedToEdit(car))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (car == null)
            {
                return HttpNotFound();
            }
            car.Categories = db.Categories
                .OrderBy(c => c.Name)
                .ToList();
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "FirstName", car.BuyerId);
            return View(car);
        }

        // POST: Cars/Edit/5

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Make,Model,Price,Year,DateAdded,Image,CategoryId")] Car car,
            HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        car.Image = array;
                    }
                   
                }
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "FirstName", car.BuyerId);
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);

            if (!isUserAuthorizedToEdit(car))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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
