using Cat_Food_Ingredient_Analyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Cat_Food_Ingredient_Analyzer.Controllers
{
    public class HomeController : Controller
    {
        //TODO: SEO
        mySampleDatabaseEntities db = new mySampleDatabaseEntities();

        public ActionResult Index(int? id = null)
        {
            ViewBag.CurrencyOrderByFrequencies = db.CurrencyOrderByFrequencies.OrderByDescending(v => v.Frequency).ThenBy(e => e.CurrencyCode).Select(i => new SelectListItem
            {
                Text = i.CurrencyCode + " - " + i.FullCurrencyName,
                Value = i.CurrencyCode
            });

            if (id.HasValue)
            {
                Food food = db.Foods.Find(id);

                if (food != null)
                {
                    return Index(new ResultVM
                    {
                        Food = new FoodVM
                        {
                            Brand = food.Brand.BrandName,
                            FoodName = food.FoodName,
                            Ingredients = food.Ingredients,
                            CurrencyOfPrice = food.CurrencyOfPrice,
                            ForLifeStage = (LifeStage?)food.ForLifeStage,
                            Mass = food.Mass,
                            Price = food.Price,
                            UnitOfMass = (UnitOfMass?)food.UnitOfMass
                        }
                    });
                }
            }

            return View(new ResultVM
            {
                Food = new FoodVM
                {
                    UnitOfMass = UnitOfMass.Kilogram
                }
            });
        }

        [HttpPost]
        public ActionResult Index(ResultVM resultVM)
        {
            ViewBag.CurrencyOrderByFrequencies = db.CurrencyOrderByFrequencies.OrderByDescending(v => v.Frequency).ThenBy(e => e.CurrencyCode).Select(i => new SelectListItem
            {
                Text = i.CurrencyCode + " - " + i.FullCurrencyName,
                Value = i.CurrencyCode
            });

            Brand brand = db.Brands.FirstOrDefault(b => b.BrandName.Equals(resultVM.Food.Brand.Trim(), StringComparison.OrdinalIgnoreCase));

            //if brand exist then add food only
            if (brand != null)
            {
                //if food exist, dont add
                if (brand.Foods.Any(i => i.FoodName.Equals(resultVM.Food.FoodName.Trim(), StringComparison.OrdinalIgnoreCase))) { }
                //if food doesnt exist, add
                else
                {
                    db.Foods.Add(new Food
                    {
                        BrandId = brand.BrandId,
                        FoodName = resultVM.Food.FoodName.Trim(),
                        Ingredients = resultVM.Food.Ingredients.Trim(),
                        CurrencyOfPrice = resultVM.Food.CurrencyOfPrice.Trim(),
                        Mass = resultVM.Food.Mass,
                        Price = resultVM.Food.Price,
                        UnitOfMass = (int)resultVM.Food.UnitOfMass
                    });

                    db.SaveChanges();
                }
            }
            //if brand not exist then insert brand
            else
            {
                if (string.IsNullOrEmpty(resultVM.Food.Brand) ||
                    string.IsNullOrWhiteSpace(resultVM.Food.Brand) ||
                    string.IsNullOrEmpty(resultVM.Food.FoodName) ||
                    string.IsNullOrWhiteSpace(resultVM.Food.FoodName)) { }
                else
                {
                    db.Brands.Add(new Brand
                    {
                        BrandName = resultVM.Food.Brand.Trim(),

                        Foods = new List<Food>()
                        {
                            new Food
                            {
                                FoodName =resultVM.Food.FoodName.Trim(),
                                Ingredients =resultVM.Food.Ingredients.Trim(),
                                CurrencyOfPrice=resultVM.Food.CurrencyOfPrice.Trim(),
                                Mass=resultVM.Food.Mass,
                                Price=resultVM.Food.Price,
                                UnitOfMass=(int)resultVM.Food.UnitOfMass
                            }
                        }
                    });

                    db.SaveChanges();
                }
            }

            resultVM.Infos = resultVM.Food.Ingredients.ListInfoAnalyze();
            return View(resultVM);
        }

        public JsonResult Brands(string s)
        {
            return Json(db.Brands.Where(e => e.BrandName.ToLower().Contains(s.ToLower())).Select(r => new
            {
                r.BrandName
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FoodNames(string BrandName = null, string FoodName = null)
        {
            var d = db.Brands.FirstOrDefault(r => r.BrandName.Equals(BrandName.Trim(), StringComparison.OrdinalIgnoreCase));

            if (d == null)
            {
                return null;
            }
            else
            {
                return Json(d.Foods.Where(r => FoodName == null || r.FoodName.CaseInsensitiveContains(FoodName)).OrderBy(s => s.FoodName).Select(e => new
                {
                    e.FoodName,
                    e.Ingredients,
                    e.Mass,
                    e.UnitOfMass,
                    e.Price,
                    e.CurrencyOfPrice
                }), JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}