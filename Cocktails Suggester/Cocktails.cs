using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cocktails_Suggester
{
    public class Cocktails
    {
        /*https://github.com/mikeyhogarth/cocktails/blob/master/src/data/cocktails.json */
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public string Glass { get; set; }
        public string Category { get; set; }
        public string Garnish { get; set; }
        public string Preparation { get; set; }
        public string[] Colors { get; set; }
        public double[] CocktailVector { get; set; }

    }
    public class Ingredient
    {
        public string IngredientName { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}
