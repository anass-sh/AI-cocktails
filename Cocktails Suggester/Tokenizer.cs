using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cocktails_Suggester
{
    public class Tokenizer
    {
        // Method to calculate the calculate the TF-IDF vector for a given input sentence
        public double[] CalculateTfIdf(string input, List<Cocktails> _cocktails)
        {
            // Tokenize the input sentence into individual words
            var tokens = TokenizeWords(input);

            // Calculate the word counts for each word in the input string
            var termFreq = new Dictionary<string, double>();
            foreach (var token in tokens)
            {
                if (!termFreq.ContainsKey(token))
                {
                    termFreq[token] = 0;
                }

                termFreq[token]++;
            }
            var numTokens = tokens.Length;

            // Calculate the term frequency (TF) for each word in the input string
            foreach (var token in termFreq.Keys.ToList())
            {
                termFreq[token] /= numTokens;
            }

            // Calculate the inverse document frequency (IDF) for each word in the input string 
            var inverseDocFreq = new Dictionary<string, double>();
            foreach (var token in termFreq.Keys)
            {
                var numCocktailsContainingToken = _cocktails.Count(c =>
                 (c.Name != null && c.Name.Contains(token)) ||
                 (c.Ingredients != null && c.Ingredients.Any(i => i.IngredientName != null && i.IngredientName.Contains(token))) ||
                 (c.Glass != null && c.Glass.Contains(token)) ||
                 (c.Category != null && c.Category.Contains(token)) ||
                 (c.Garnish != null && c.Garnish.Contains(token)) ||
                 (c.Preparation != null && c.Preparation.Contains(token)) ||
                 (c.Colors != null && c.Colors.Contains(token))
             );
                inverseDocFreq[token] = Math.Log((double)_cocktails.Count / numCocktailsContainingToken);
            }

            var tfidf = new double[termFreq.Count];
            var index = 0;

            foreach (var token in termFreq.Keys)
            {
                tfidf[index] = termFreq[token] * inverseDocFreq[token];
                index++;
            }
            return tfidf;
        }

        private string[] TokenizeWords(string input)
        {
            return Regex.Split(input, @"\W+")
                .Where(w => !string.IsNullOrEmpty(w))
                .Select(w => w.ToLowerInvariant())
                .ToArray();
        }
    }
}
