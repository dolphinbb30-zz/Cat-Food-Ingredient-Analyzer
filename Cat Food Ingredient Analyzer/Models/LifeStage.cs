using System;

namespace Cat_Food_Ingredient_Analyzer.Models
{
    [Flags]
    public enum LifeStage
    {
        Kitten = 1,
        Adult = 2,
        Pregnancy = 4,
        Lactation = 8,
        Geriatric = 16
    }
}