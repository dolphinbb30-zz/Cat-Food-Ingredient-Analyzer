using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cat_Food_Ingredient_Analyzer.Models
{
    public class FoodVM
    {
        public string Brand { get; set; }

        public string Name { get; set; }

        [DisplayName("For Life Stage")]
        public LifeStage ForLifeStage { get; set; }

        public decimal? Mass { get; set; }

        [DisplayName("Unit of Mass")]
        [Required]
        public UnitOfMass UnitOfMass { get; set; }

        public decimal? Price { get; set; }

        [DisplayName("Currency of Price")]
        public string CurrencyOfPrice { get; set; }

        [Required]
        public string Ingredients { get; set; }

        public decimal GetMassInUnit(UnitOfMass UnitOfMass)
        {
            if (Mass.HasValue)
            {
                if (this.UnitOfMass != UnitOfMass)
                {
                    switch (UnitOfMass)
                    {
                        case UnitOfMass.Kilogram:
                            return (decimal)Mass * (decimal)0.453592;
                        case UnitOfMass.Pound:
                            return (decimal)Mass * (decimal)2.204620823516057;
                    }
                }

                return Mass.Value;
            }
            else
            {
                return 0;
            }
        }
    }
}