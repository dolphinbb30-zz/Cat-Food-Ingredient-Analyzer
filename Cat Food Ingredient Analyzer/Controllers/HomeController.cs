using Cat_Food_Ingredient_Analyzer.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Cat_Food_Ingredient_Analyzer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //1. http://www.thesprucepets.com/whats-wrong-with-by-products-554368 - DONE
        //2. http://www.holisticforpets.com/pdf/UnderstandingPetFoodLabels.pdf - DONE
        //3. http://www.homewardpet.org/wp-content/uploads/2013/04/What-to-Look-For-in-Your-Cats-Food1.pdf - DONE
        [HttpPost]
        public ActionResult Index(ResultVM resultVM)
        {
            string[] ingredients = resultVM.Food.Ingredients.Trim().Split(new string[] { ",", "(", ")", ";", "." }, StringSplitOptions.RemoveEmptyEntries).Where(s => !s.CaseInsensitiveContains("%"))
                .Where(s => !s.CaseInsensitiveContains("mg") && !s.Any(char.IsDigit)).ToArray();
            Dictionary<string, decimal> ingredientScore = new Dictionary<string, decimal>();

            for (int index = 0; index < ingredients.Length; ++index)
            {
                string i = ingredients[index].Trim();

                if (!string.IsNullOrEmpty(i) && !string.IsNullOrWhiteSpace(i))
                {
                    decimal worth = ((decimal)ingredients.Length - (decimal)index) / (decimal)ingredients.Length * (decimal)100;
                    decimal negative = 0 - worth;
                    decimal neutral = 0;
                    decimal positive = 0 + worth;

                    if (i.CaseInsensitiveContains("boneless") ||
                        i.CaseInsensitiveContains("deboned"))
                    {
                        ingredientScore.Add(i, positive);
                    }
                    else if (i.CaseInsensitiveContains("digest") ||
                        (i.CaseInsensitiveContains("by") && i.CaseInsensitiveContains("product")) ||
                        ((i.CaseInsensitiveContains("meat") || i.CaseInsensitiveContains("animal")) && i.CaseInsensitiveContains("meal")) ||
                        i.CaseInsensitiveContains("bone") ||
                        (i.CaseInsensitiveContains("BHA") || i.CaseInsensitiveContains("BHT") || i.CaseInsensitiveContains("ethoxyquin")) ||
                        (i.CaseInsensitiveContains("propylene") & i.CaseInsensitiveContains("Glycol")) ||
                        ((i.CaseInsensitiveContains("lactic") || i.CaseInsensitiveContains("acetic")) && i.CaseInsensitiveContains("acid")) ||
                        (i.CaseInsensitiveContains("sodium") && i.CaseInsensitiveContains("nitrate")) ||
                        (i.CaseInsensitiveContains("potassium") && i.CaseInsensitiveContains("salt")) ||
                        (i.CaseInsensitiveContains("potassium") && i.CaseInsensitiveContains("sorbate")) ||
                        (i.CaseInsensitiveContains("calcium") && i.CaseInsensitiveContains("proprionate")) ||
                        (i.CaseInsensitiveContains("sodium") && i.CaseInsensitiveContains("diacetate") && i.CaseInsensitiveContains("ethoxyquin")) ||
                        i.CaseInsensitiveContains("MSG") ||
                        (i.CaseInsensitiveContains("sodium") && i.CaseInsensitiveContains("meta") && i.CaseInsensitiveContains("bisulfate")) ||
                        i.CaseInsensitiveContains("brewer"))
                    {
                        ingredientScore.Add(i, negative);
                    }
                    else if ((i.CaseInsensitiveContains("mill") && i.CaseInsensitiveContains("run")) ||
                        (index < 5 && (i.CaseInsensitiveContains("rice") || (!i.CaseInsensitiveContains("sweet") && i.CaseInsensitiveContains("potato")) || i.CaseInsensitiveContains("pasta"))))
                    {
                        ingredientScore.Add(i, negative);
                    }
                    else if ((i.CaseInsensitiveContains("corn") || i.CaseInsensitiveContains("soy")) && i.CaseInsensitiveContains("oil"))
                    {
                        ingredientScore.Add(i, positive);
                    }
                    else if (i.CaseInsensitiveContains("wheat") ||
                        i.CaseInsensitiveContains("soy") ||
                        i.CaseInsensitiveContains("beet") ||
                        i.CaseInsensitiveContains("gluten") ||
                        i.CaseInsensitiveContains("middling") ||
                        i.CaseInsensitiveContains("bran") ||
                        (i.CaseInsensitiveContains("peanut") && i.CaseInsensitiveContains("hull")) ||
                        (i.CaseInsensitiveContains("vegetable") && i.CaseInsensitiveContains("oil")) ||
                        ((i.CaseInsensitiveContains("animal") || i.CaseInsensitiveContains("poultry")) && i.CaseInsensitiveContains("fat")))
                    {
                        ingredientScore.Add(i, negative);
                    }
                    else if (i.CaseInsensitiveContains("chicken") ||
                        i.CaseInsensitiveContains("turkey") ||
                        i.CaseInsensitiveContains("fish") ||
                        i.CaseInsensitiveContains("venison") ||
                        i.CaseInsensitiveContains("rabbit") ||
                        i.CaseInsensitiveContains("duck") ||
                        ((i.CaseInsensitiveContains("chicken") || i.CaseInsensitiveContains("beef")) && i.CaseInsensitiveContains("fat")) ||
                        (i.CaseInsensitiveContains("vitamin") && (i.CaseInsensitiveContains("C") || i.CaseInsensitiveContains("E"))) ||
                        (i.CaseInsensitiveContains("mix") && i.CaseInsensitiveContains("tocopherol")) ||
                        (i.CaseInsensitiveContains("DL") && i.CaseInsensitiveContains("alphatocopheral")) ||
                        (i.CaseInsensitiveContains("ascorbic") && i.CaseInsensitiveContains("acid")) ||
                        (i.CaseInsensitiveContains("chelated") && i.CaseInsensitiveContains("vitamin") && i.CaseInsensitiveContains("mineral")) ||
                        (i.CaseInsensitiveContains("Digestive") && i.CaseInsensitiveContains("enzyme") && i.CaseInsensitiveContains("Protease")) ||
                        i.CaseInsensitiveContains("amylase") ||
                        i.CaseInsensitiveContains("Lipase") ||
                        i.CaseInsensitiveContains("sucrase") ||
                        (i.CaseInsensitiveContains("inulin") && i.CaseInsensitiveContains("Micro") && i.CaseInsensitiveContains("organism")) ||
                        i.CaseInsensitiveContains("Lactobacillus") ||
                        i.CaseInsensitiveContains("acidophilus") ||
                        i.CaseInsensitiveContains("Probiotic") ||
                        i.CaseInsensitiveContains("Vegetable") ||
                        i.CaseInsensitiveContains("Fruit") ||
                        (i.CaseInsensitiveContains("Seaweed") && i.CaseInsensitiveContains("Product")) ||
                        i.CaseInsensitiveContains("Chondroitin") ||
                        ((i.CaseInsensitiveContains("Shark") || i.CaseInsensitiveContains("Bovine")) && i.CaseInsensitiveContains("Cartilage")) ||
                        (i.CaseInsensitiveContains("Perna") && i.CaseInsensitiveContains("Mussel")) ||
                        i.CaseInsensitiveContains("Herring") ||
                        i.CaseInsensitiveContains("salmon"))
                    {
                        ingredientScore.Add(i, positive);
                    }
                    else if (i.CaseInsensitiveContains("corn") ||
                        (index < 3 && (!i.CaseInsensitiveContains("meat") || !i.CaseInsensitiveContains("meal"))) ||
                        (i.CaseInsensitiveContains("artificial") && i.CaseInsensitiveContains("colo")) ||
                        i.CaseInsensitiveContains("flavo") ||
                        i.CaseInsensitiveContains("sweetener") ||
                        i.CaseInsensitiveContains("meal"))
                    {
                        ingredientScore.Add(i, negative);
                    }
                    else if (i.CaseInsensitiveContains("barley") ||
                        i.CaseInsensitiveContains("millet") ||
                        i.CaseInsensitiveContains("oat") ||
                        i.CaseInsensitiveContains("rye") ||
                        i.CaseInsensitiveContains("quinoa") ||
                        i.CaseInsensitiveContains("amaranth") ||
                        (i.CaseInsensitiveContains("brown") && i.CaseInsensitiveContains("rice")) ||
                        (i.CaseInsensitiveContains("sweet") && i.CaseInsensitiveContains("potato")) ||
                        ((i.CaseInsensitiveContains("sun") || i.CaseInsensitiveContains("saf")) && i.CaseInsensitiveContains("flower")))
                    {
                        ingredientScore.Add(i, positive);
                    }
                    else if (i.CaseInsensitiveContains("beef") ||
                        i.CaseInsensitiveContains("chemical") ||
                        i.CaseInsensitiveContains("lamb") ||
                        i.CaseInsensitiveContains("salt") ||
                        i.CaseInsensitiveContains("NaCl") ||
                        i.CaseInsensitiveContains("extrude"))
                    {
                        ingredientScore.Add(i, negative);
                    }
                    else if (((i.CaseInsensitiveContains("flax") || i.CaseInsensitiveContains("fish")) && i.CaseInsensitiveContains("oil")) ||
                        i.CaseInsensitiveContains("canola") ||
                        i.CaseInsensitiveContains("olive") ||
                        i.CaseInsensitiveContains("bake") ||
                        i.CaseInsensitiveContains("glucosamine"))
                    {
                        ingredientScore.Add(i, positive);
                    }
                    else
                    {
                        if (!ingredientScore.ContainsKey(i))
                        {
                            ingredientScore.Add(i, neutral);
                        }
                    }
                }
            }

            resultVM.IngredientScore = ingredientScore;
            return View(resultVM);
        }

        public ActionResult Result(ResultVM resultVM)
        {
            return View(resultVM);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}