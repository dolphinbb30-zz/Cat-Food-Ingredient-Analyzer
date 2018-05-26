using System.Collections.Generic;

namespace Cat_Food_Ingredient_Analyzer.Models
{
    public class ResultVM
    {
        public FoodVM Food { get; set; }

        public List<Info> Infos { get; set; }
    }
}