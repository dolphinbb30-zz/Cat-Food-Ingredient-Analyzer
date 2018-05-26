using Cat_Food_Ingredient_Analyzer.Models;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class String
    {
        public static bool CaseInsensitiveContains(this string text, string value, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }

        //MAKE SURE THE AUTHORS ARE CREDITABLE
        static string FrannySYUFY = "http://www.thesprucepets.com/whats-wrong-with-by-products-554368";
        static string DrBradEvergreen = "http://www.homewardpet.org/wp-content/uploads/2013/04/What-to-Look-For-in-Your-Cats-Food1.pdf";
        static string DrBecker_Prebiotic = "https://healthypets.mercola.com/sites/healthypets/archive/2017/01/06/prebiotics-probiotics-pet-food.aspx";
        static string NelLiquorman = "http://www.thedogpress.com/dogfood/Rosemary-Neurotoxin-10032_Liquorman.asp";
        static string DrCallumTurner = "https://wagwalking.com/cat/condition/brewers-yeast-allergy";
        static string DrBecker_Antifreeze = "https://healthypets.mercola.com/sites/healthypets/archive/2016/01/11/propylene-glycol-pet-food.aspx";
        static string DrGregALDRICH = "https://www.petfoodindustry.com/articles/1704-citric-acid-suffers-from-misperceptions-and-misplaced-blame?v=preview";
        static string DrFosterSmith = "https://www.petcoach.co/article/the-use-of-seaweed-algae-and-kelp-in-dogs-cats/";
        static string DrJenniferKvamme = "https://www.petmd.com/cat/nutrition/evr_ct_can_carrots_improve_cat_eyesight#";
        static string DrArtCraigmill = "https://www.avodermnatural.com/about/why-avocados";
        static string ValClows = "http://www.holisticforpets.com/pdf/UnderstandingPetFoodLabels.pdf";
        //https://www.petcurean.com/blog/why-are-peas-so-popular-in-pet-food/ - BY Dr. Jennifer Adolphe

        public static List<Info> ListInfoAnalyze(this string Ingredients)
        {
            string[] ingredients = Ingredients.Replace('.', ' ').Replace("&", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ").Replace(" and ", ",").Trim().Split(new string[] { ",",
                ";" }, StringSplitOptions.RemoveEmptyEntries).Where(q => (Char.IsNumber(q.Trim()[0]) && (q.Trim().EndsWith("%", StringComparison.OrdinalIgnoreCase) || Char.IsNumber(q.Trim()[q.Trim().
                Length - 1]))) == false).Where(s => !s.CaseInsensitiveContains("removed"))
                //.Where(s => !s.CaseInsensitiveContains("%")).Where(s => !s.CaseInsensitiveContains("mg") && !s.Any(char.IsDigit))
                .ToArray();
            List<Info> infos = new List<Info>();

            for (int index = 0; index < ingredients.Length; ++index)
            {
                string i = ingredients[index].Trim();

                if (infos.Any(n => n.Keyword.Equals(i, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                else if (!string.IsNullOrEmpty(i) && !string.IsNullOrWhiteSpace(i))
                {
                    decimal worth = ((decimal)ingredients.Length - (decimal)index) / (decimal)ingredients.Length * (decimal)100;
                    decimal negative = 0 - worth;
                    decimal neutral = 0;
                    decimal positive = 0 + worth;

                    if (i.CaseInsensitiveContains("digest") && !i.CaseInsensitiveContains("digestible"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = ValClows,
                            Reason = "Chemically or enzymatically digested animal tissues including the non-specific unidentified parts, including beaks, feathers, feces hair and roadkill"
                        });
                    }

                    else if (i.CaseInsensitiveContains("folate") ||
                        i.CaseInsensitiveContains("niacin") ||
                        i.CaseInsensitiveContains("fiber"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = positive,
                            Link = DrArtCraigmill,
                            Reason = "folate, potassium, niacin, essential fatty acids, fiber, and many other nutrients essential to good skin and coat health as well as good overall health"
                        });
                    }

                    else if (i.CaseInsensitiveContains("avocado") ||
                        (i.CaseInsensitiveContains("alligator") && i.CaseInsensitiveContains("pear")))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = positive,
                            Link = DrArtCraigmill,
                            Reason = "the avocado meat of the fruit and oils have not been shown to be toxic"
                        });
                    }

                    else if ((i.CaseInsensitiveContains("Citric") && i.CaseInsensitiveContains("acid")))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = positive,
                            Link = DrGregALDRICH,
                            Reason = "citric acid is a common additive used mostly in the fat preservative (antioxidant) system"
                        });
                    }

                    else if ((i.CaseInsensitiveContains("vitamin") && i.EndsWith("A", StringComparison.OrdinalIgnoreCase)) ||
                        i.CaseInsensitiveContains("retinal"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = positive,
                            Link = DrJenniferKvamme,
                            Reason = "cats must be fed a form of vitamin A that is available for the body to use immediately"
                        });
                    }

                    else if (i.CaseInsensitiveContains("Seaweed") ||
                        i.CaseInsensitiveContains("algae") ||
                        i.CaseInsensitiveContains("kelp") ||
                        i.CaseInsensitiveContains("iron") ||
                        i.CaseInsensitiveContains("iodine") ||
                       (i.CaseInsensitiveContains("Sea") && i.CaseInsensitiveContains("vegetable")) ||
                       i.CaseInsensitiveContains("fucus") ||
                       i.CaseInsensitiveContains("gigartina") ||
                       i.CaseInsensitiveContains("nori") ||
                       i.CaseInsensitiveContains("dulce") ||
                       (i.CaseInsensitiveContains("Iris") && i.CaseInsensitiveContains("Moss")) ||
                       i.CaseInsensitiveContains("cargeena") ||
                       (i.CaseInsensitiveContains("sea") && i.CaseInsensitiveContains("lettuce")) ||
                       i.CaseInsensitiveContains("cesium") ||
                       i.CaseInsensitiveContains("chlorophyll") ||
                        i.CaseInsensitiveContains("glycogen"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = positive,
                            Link = DrFosterSmith,
                            Reason = "Seaweed, algae, and kelp can serve as excellent sources of minerals such as iron, iodine, potassium, and trace minerals"
                        });
                    }

                    else if (i.CaseInsensitiveContains("corn") ||
                        i.CaseInsensitiveContains("maize"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = FrannySYUFY,
                            Reason = "commonly known to be difficult for cats to digest and implicated in food allergies"
                        });
                    }

                    else if (i.CaseInsensitiveContains("chicken") ||
                       i.CaseInsensitiveContains("turkey") ||
                       i.CaseInsensitiveContains("venison") ||
                       i.CaseInsensitiveContains("rabbit") ||
                       i.CaseInsensitiveContains("duck") ||
                       ((i.CaseInsensitiveContains("chicken") || i.CaseInsensitiveContains("beef")) && i.CaseInsensitiveContains("fat")) ||
                       (i.CaseInsensitiveContains("vitamin") && (i.CaseInsensitiveContains("C") || i.CaseInsensitiveContains("B") || i.CaseInsensitiveContains("F") || i.CaseInsensitiveContains("D") || i.CaseInsensitiveContains("E"))) ||
                       i.CaseInsensitiveContains("riboflavin") ||
                        i.CaseInsensitiveContains("tocopherol") ||
                       (i.CaseInsensitiveContains("DL") && i.CaseInsensitiveContains("alphatocopheral")) ||
                       (i.CaseInsensitiveContains("ascorbic") && i.CaseInsensitiveContains("acid")) ||
                       (i.CaseInsensitiveContains("chelated") && i.CaseInsensitiveContains("vitamin") && i.CaseInsensitiveContains("mineral")) ||
                       (i.CaseInsensitiveContains("Digestive") && i.CaseInsensitiveContains("enzyme") && i.CaseInsensitiveContains("Protease")) ||
                       i.CaseInsensitiveContains("amylase") ||
                       i.CaseInsensitiveContains("Lipase") ||
                       i.CaseInsensitiveContains("sucrase") ||
                       i.CaseInsensitiveContains("Lactobacillus") ||
                       i.CaseInsensitiveContains("acidophilus") ||
                       i.CaseInsensitiveContains("Fruit") ||
                       i.CaseInsensitiveContains("Chondroitin") ||
                       ((i.CaseInsensitiveContains("Shark") || i.CaseInsensitiveContains("Bovine")) && i.CaseInsensitiveContains("Cartilage")) ||
                       (i.CaseInsensitiveContains("Perna") && i.CaseInsensitiveContains("Mussel")) ||
                       i.CaseInsensitiveContains("Herring") ||
                       i.CaseInsensitiveContains("salmon") ||
                       (i.CaseInsensitiveContains("rock") && i.CaseInsensitiveContains("sole")) ||
                         i.CaseInsensitiveContains("fish") ||
                        i.CaseInsensitiveContains("menhaden") ||
                        i.CaseInsensitiveContains("tuna") ||
                        i.CaseInsensitiveContains("squid") ||
                        i.CaseInsensitiveContains("trout"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = positive
                        });
                    }

                    else if (i.CaseInsensitiveContains("oxide") ||
                       i.CaseInsensitiveContains("sulfate"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBecker_Antifreeze,
                            Reason = "avoid all foods that use cheap options (avoid \"oxides\" and \"sulfates\" on the label"
                        });
                    }

                    else if ((!i.CaseInsensitiveContains("natural") && i.CaseInsensitiveContains("flavo")) ||
                        i.CaseInsensitiveContains("colo") ||
                        i.CaseInsensitiveContains("sweetener") ||
                        i.CaseInsensitiveContains("preserv") ||
                        i.CaseInsensitiveContains("BHT") ||
                        i.CaseInsensitiveContains("BHA") ||
                        i.CaseInsensitiveContains("ethoxyquin") ||
                        (i.CaseInsensitiveContains("propyl") && i.CaseInsensitiveContains("gallate")) ||
                        (i.CaseInsensitiveContains("red") && i.CaseInsensitiveContains("40")) ||
                        (i.CaseInsensitiveContains("yellow") && (i.CaseInsensitiveContains("5") || i.CaseInsensitiveContains("6"))) ||
                        (i.CaseInsensitiveContains("blue") && (i.CaseInsensitiveContains("2") || i.CaseInsensitiveContains("1"))) ||
                        i.CaseInsensitiveContains("caramel"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBradEvergreen,
                            Reason = "Avoid Artificial colours, flavours, sweeteners or preservatives, especially BHT, BHA, Ethoxyquin, Propyl Gallate"
                        });
                    }

                    else if ((i.CaseInsensitiveContains("mill") && i.CaseInsensitiveContains("run")) ||
                       (index < 5 && (i.CaseInsensitiveContains("rice") || i.CaseInsensitiveContains("potato") || i.CaseInsensitiveContains("pasta") || i.CaseInsensitiveContains("grain"))))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBradEvergreen,
                            Reason = "grain ingredient is used in the first five ingredients"
                        });
                    }

                    else if ((i.CaseInsensitiveContains("propylene") && i.CaseInsensitiveContains("Glycol")) ||
                        (i.CaseInsensitiveContains("ethylene") && i.CaseInsensitiveContains("glycol")) ||
                        i.CaseInsensitiveContains("onion") ||
                        i.CaseInsensitiveContains("garlic") ||
                        i.CaseInsensitiveContains("kale") ||
                        i.CaseInsensitiveContains("turnip") ||
                        i.CaseInsensitiveContains("zinc") ||
                        i.CaseInsensitiveContains("acetaminophen") ||
                        (i.CaseInsensitiveContains("vitamin") && i.CaseInsensitiveContains("K")) ||
                        i.CaseInsensitiveContains("benzocaine") ||
                        i.CaseInsensitiveContains("menadione") ||
                        (i.CaseInsensitiveContains("sodium") && i.CaseInsensitiveContains("selenite")) ||
                        (i.CaseInsensitiveContains("synthetic") && i.CaseInsensitiveContains("selenium")))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBecker_Antifreeze,
                            Reason = "can cause Heinz body hemolytic anemia"
                        });
                    }

                    else if (i.CaseInsensitiveContains("starch") ||
                        i.CaseInsensitiveContains("flour"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBecker_Antifreeze,
                            Reason = "Avoid formulas that fragment, or split ingredients"
                        });
                    }

                    else if (i.CaseInsensitiveContains("barley") ||
                        i.CaseInsensitiveContains("millet") ||
                        i.StartsWith("oat", StringComparison.OrdinalIgnoreCase) ||
                        i.CaseInsensitiveContains("rye") ||
                        i.CaseInsensitiveContains("quinoa") ||
                        i.CaseInsensitiveContains("amaranth") ||
                        (i.CaseInsensitiveContains("whole") && i.CaseInsensitiveContains("brown") && i.CaseInsensitiveContains("rice")) ||
                        ((i.CaseInsensitiveContains("sun") || i.CaseInsensitiveContains("saf")) && i.CaseInsensitiveContains("flower")) ||
                        i.CaseInsensitiveContains("Pollock") ||
                        i.CaseInsensitiveContains("crab"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = positive
                        });
                    }

                    else if (i.CaseInsensitiveContains("wheat") ||
                        (i.CaseInsensitiveContains("rice") && !i.CaseInsensitiveContains("lico")))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBecker_Antifreeze,
                            Reason = "foods containing corn, wheat or rice could be contaminated with mycotoxins that are hazardous to all living beings in a variety of ways"
                        });
                    }

                    else if (i.CaseInsensitiveContains("soy"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBecker_Antifreeze,
                            Reason = "These ingredients have been genetically modified, and feeding GMOs to pets causes GI problems (at best) and systemic immune crises at worst"
                        });
                    }

                    else if (i.CaseInsensitiveContains("lectin"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBecker_Antifreeze,
                            Reason = "sticky proteins that contribute to gut permeability issues (dysbiosis)"
                        });
                    }

                    else if (i.CaseInsensitiveContains("polysaccharide") ||
                        i.CaseInsensitiveContains("proteinate") ||
                        i.CaseInsensitiveContains("krill") ||
                        i.CaseInsensitiveContains("anchovy") ||
                        i.CaseInsensitiveContains("sardine"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = positive
                        });
                    }

                    else if (i.CaseInsensitiveContains("boneless") ||
                       i.CaseInsensitiveContains("deboned") ||
                       i.CaseInsensitiveContains("flounder") ||
                       i.CaseInsensitiveContains("Thiamine") ||
                       i.CaseInsensitiveContains("pheasant") ||
                       i.CaseInsensitiveContains("pilchard") ||
                       i.CaseInsensitiveContains("cod") ||
                       i.CaseInsensitiveContains("whiting") ||
                       i.CaseInsensitiveContains("lamb") ||
                       (i.CaseInsensitiveContains("egg") && !i.CaseInsensitiveContains("plant")) ||
                       i.CaseInsensitiveContains("beef") ||
                       i.CaseInsensitiveContains("pork") ||
                       i.CaseInsensitiveContains("hake") ||
                       i.CaseInsensitiveContains("mackerel") ||
                       i.CaseInsensitiveContains("boar") ||
                       i.CaseInsensitiveContains("bison") ||
                       i.CaseInsensitiveContains("goat"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = positive
                        });
                    }

                    else if ((i.CaseInsensitiveContains("by") && i.CaseInsensitiveContains("product")))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = FrannySYUFY,
                            Reason = "While it's true that cats in the wild eat the whole bodies of their catch (including the heads in some cases), the term \"meat by - products\" has become a \"dirty word\" to many cat experts, because of its misuse by some members of the cat food industry"
                        });
                    }

                    else if ((i.CaseInsensitiveContains("ROSE") && i.CaseInsensitiveContains("MARY")) ||
                        i.CaseInsensitiveContains("sage") ||
                        i.CaseInsensitiveContains("thyme") ||
                        (i.CaseInsensitiveContains("worm") && i.CaseInsensitiveContains("wood")) ||
                        i.CaseInsensitiveContains("dill") ||
                        i.CaseInsensitiveContains("mint"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = NelLiquorman,
                            Reason = "Seizures, allergies, and brain function problems are on the rise since pet food companies began adding rosemary extracts and other ingredients not meant for carnivores"
                        });
                    }

                    else if (i.CaseInsensitiveContains("Brewer") && i.CaseInsensitiveContains("Yeast"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrCallumTurner,
                            Reason = "Brewer’s yeast is a by-product of the beer brewing process which can be toxic to dogs and cats"
                        });
                    }

                    else if ((i.CaseInsensitiveContains("beet") && i.CaseInsensitiveContains("pulp")) ||
                        i.CaseInsensitiveContains("Prebiotic") ||
                        i.CaseInsensitiveContains("fructooligosaccharide") ||
                        i.CaseInsensitiveContains("FOS") ||
                        i.CaseInsensitiveContains("inulin") ||
                        i.CaseInsensitiveContains("oligofructose") ||
                        i.CaseInsensitiveContains("sugar") ||
                        i.CaseInsensitiveContains("cane") ||
                        i.CaseInsensitiveContains("chicory") ||
                        i.CaseInsensitiveContains("mannanoligosaccharide"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBecker_Prebiotic,
                            Reason = "prebiotics nourish unhealthy bacteria as well"
                        });
                    }

                    else if (i.CaseInsensitiveContains("Probiotic"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBecker_Prebiotic,
                            Reason = "Probiotics are sensitive to moisture and heat, so if they're added to a pet food formula — especially kibble — they’ll be long dead and virtually useless by the time they make it into your dog's or cat's digestive tract"
                        });
                    }

                    else if (i.CaseInsensitiveContains("middling") ||
                        i.CaseInsensitiveContains("bran") ||
                        (i.CaseInsensitiveContains("peanut") && i.CaseInsensitiveContains("hull")))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative
                        });
                    }

                    else if (i.CaseInsensitiveContains("chemical") ||
                        i.CaseInsensitiveContains("NaCl") ||
                        i.CaseInsensitiveContains("extrude"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative
                        });
                    }

                    else if ((i.CaseInsensitiveContains("flax") && i.CaseInsensitiveContains("oil")) ||
                        i.CaseInsensitiveContains("canola") ||
                        i.CaseInsensitiveContains("olive") ||
                        i.CaseInsensitiveContains("bake") ||
                        i.CaseInsensitiveContains("glucosamine"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = positive
                        });
                    }

                    else if (i.CaseInsensitiveContains("meat") ||
                        i.CaseInsensitiveContains("animal") ||
                        (i.CaseInsensitiveContains("meal") && !i.CaseInsensitiveContains("alfalfa")) ||
                        i.CaseInsensitiveContains("poultry") ||
                        i.CaseInsensitiveContains("fat"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBecker_Antifreeze,
                            Reason = "Avoid any formula that uses unidentified sources, described non-specifically as \"meat\", \"animal\" or \"poultry\""
                        });
                    }

                    else if (i.CaseInsensitiveContains("bone") ||
                        (i.CaseInsensitiveContains("acetic") && i.CaseInsensitiveContains("acid")) ||
                        (i.CaseInsensitiveContains("sodium") && i.CaseInsensitiveContains("nitrate")) ||
                        (i.CaseInsensitiveContains("potassium") && i.CaseInsensitiveContains("sorbate")) ||
                        (i.CaseInsensitiveContains("calcium") && i.CaseInsensitiveContains("proprionate")) ||
                        i.CaseInsensitiveContains("MSG") ||
                        i.CaseInsensitiveContains("brewer") ||
                        i.CaseInsensitiveContains("melamine") ||
                        i.CaseInsensitiveContains("fluoride"))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative
                        });
                    }

                    else if (index < 3 && !(i.CaseInsensitiveContains("meat") || i.CaseInsensitiveContains("meal")))
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = negative,
                            Link = DrBradEvergreen,
                            Reason = "Protein sources are not meat in the top 3 ingredients"
                        });
                    }

                    else
                    {
                        infos.Add(new Info
                        {
                            Keyword = i,
                            Score = neutral
                        });
                    }
                }
            }

            return infos;
        }

        public static decimal? ScoreAnalyze(this string Ingredients)
        {
            List<Info> infos = Ingredients.ListInfoAnalyze();
            decimal? s = 0;

            foreach (Info i in infos)
            {
                s = s + i.Score;
            }

            if (infos.Count > 0)
            {
                return Convert.ToDecimal((s / infos.Count).Value.ToString("N2"));
            }
            else
            {
                return null;
            }
        }
    }
}