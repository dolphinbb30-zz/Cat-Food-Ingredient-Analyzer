using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cat_Food_Ingredient_Analyzer.Models
{
    [MetadataType(typeof(FoodMetadata))]
    public partial class Food
    {
        RateService rateService = new RateService();

        public class FoodMetadata
        {
            [DisplayName("Brand Id")]
            public int BrandId { get; set; }

            [DisplayName("Food Name")]
            public string FoodName { get; set; }

            [DisplayName("For Life Stage")]
            public string ForLifeStage { get; set; }

            [DisplayName("Unit of Mass")]
            public UnitOfMass UnitOfMass { get; set; }

            [DisplayName("Currency of Price")]
            public string CurrencyOfPrice { get; set; }
        }

        public decimal? GetMassInUnit(UnitOfMass UnitOfMass)
        {
            if (Mass.HasValue)
            {
                if (this.UnitOfMass.Value != (int)UnitOfMass)
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
                return null;
            }
        }

        public decimal? GetPriceInCurrency(string currencyOfPrice)
        {
            if (Price.HasValue)
            {
                if (!CurrencyOfPrice.Equals(currencyOfPrice, StringComparison.OrdinalIgnoreCase))
                {
                    Rate from = rateService.GetRate("USD" + CurrencyOfPrice);
                    Rate to = rateService.GetRate("USD" + currencyOfPrice);
                    return (Price / from.ExchangeRate) * to.ExchangeRate;
                }
                else
                {
                    return Price;
                }
            }
            else
            {
                return null;
            }
        }
    }

    [MetadataType(typeof(BrandMetadata))]
    public partial class Brand
    {
        public class BrandMetadata
        {
            [DisplayName("Brand Id")]
            public int BrandId { get; set; }

            [DisplayName("Brand Name")]
            public string BrandName { get; set; }
        }
    }

    [MetadataType(typeof(RateMetadata))]
    public partial class Rate
    {
        public class RateMetadata
        {
            public string Quote { get; set; }

            [DisplayName("Exchange Rate")]
            public decimal ExchangeRate { get; set; }

            [DisplayName("Expiry Date Time")]
            public DateTime ExpiryDateTime { get; set; }
        }
    }
}