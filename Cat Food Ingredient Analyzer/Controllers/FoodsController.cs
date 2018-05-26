using Cat_Food_Ingredient_Analyzer.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Cat_Food_Ingredient_Analyzer.Controllers
{
    public class FoodsController : Controller
    {
        private mySampleDatabaseEntities db = new mySampleDatabaseEntities();

        // GET: Foods
        public ActionResult Index(UnitOfMass unitOfMass = UnitOfMass.Kilogram, string currencyOfPrice = "MYR")
        {
            ViewBag.CurrencyOrderByFrequencies = db.CurrencyOrderByFrequencies.OrderByDescending(v => v.Frequency).ThenBy(e => e.CurrencyCode).Select(i => new SelectListItem
            {
                Text = i.CurrencyCode + " - " + i.FullCurrencyName,
                Value = i.CurrencyCode
            });

            DbSet<Food> foods = db.Foods;

            foreach (Food f in foods)
            {
                if (f.Mass.HasValue)
                {
                    f.Mass = Convert.ToDecimal(f.GetMassInUnit(unitOfMass).Value.ToString("N2"));
                    f.UnitOfMass = (int)unitOfMass;
                }

                if (f.Price.HasValue)
                {
                    f.Price = Convert.ToDecimal(f.GetPriceInCurrency(currencyOfPrice).Value.ToString("N2"));
                    f.CurrencyOfPrice = currencyOfPrice;
                }
            }

            return View(foods.ToList());
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", food.BrandId);
            food.CurrencyOfPrice = "MYR";
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrandId,FoodName,ForLifeStage,Ingredients,Mass,UnitOfMass,Price,CurrencyOfPrice")] Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "BrandName", food.BrandId);
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
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
