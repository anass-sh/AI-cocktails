using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cocktails_Suggester
{
    public class KNN
    {
        public List<Cocktails> _cocktails;
        public KNN(List<Cocktails> cocktails)
        {
            _cocktails = cocktails;
        }
        private double CalculateDistance(double[] p, double[] q)
        {
            double distance = 0.0;
            for (int i = 0; i < p.Length; i++)
            {
                distance += Math.Pow(p[i] - q[i], 2);
            }
            return Math.Sqrt(distance);
        }

        public List<CocktailRecommender> Classify(double[] array, int K)
        {
            var distances=new List<CocktailRecommender>();

            foreach (var c in _cocktails)
            {
                double distance = CalculateDistance(array, c.CocktailVector);
                distances.Add(new CocktailRecommender
                {
                    Cocktail = c,
                    Similarity = distance
                }
             ); ;
            }
            distances.OrderBy(x=>x.Similarity).ToList();
            return distances.Take(K).ToList();
        }
    }
}
