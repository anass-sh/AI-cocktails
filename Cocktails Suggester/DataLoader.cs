using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cocktails_Suggester
{
    public class DataLoader
    {
        private string _dataPath;
        private string _userInput;
        private Tokenizer _tokenizer;
        private List<Cocktails> cocktails;
        private double[] _inputVector;
        public DataLoader(string dataPath,string userInput)
        {
            _dataPath = dataPath;
            _userInput= userInput;
            cocktails = new List<Cocktails>();
            _tokenizer = new Tokenizer();
        }
        public List<Cocktails> loadData()
        {
            try
            {
                string json = File.ReadAllText(_dataPath);
                cocktails = JsonConvert.DeserializeObject<List<Cocktails>>(json).ToList();   
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File not found: " + ex.Message);
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Invalid JSON format: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return cocktails;
        }
        public PreprocessedData PrepProcessData(List<Cocktails> cocktails)
        {
            // calculate the TF-IDF for each of the cocktail features
            foreach (var c in cocktails)
            {
                var cocktailVector = _tokenizer.CalculateTfIdf($"{c.Name} {c.Ingredients} {c.Glass} {c.Category} {c.Garnish} {c.Preparation} {c.Colors}", cocktails) ;
                c.CocktailVector = cocktailVector;
            }

            // Calculate for the input vector
            _inputVector = _tokenizer.CalculateTfIdf(_userInput,cocktails);

            return new PreprocessedData
            {
                InputVector = _inputVector,
                cocktailsVectors = cocktails
            };
        }
        public class PreprocessedData
        {
            public double[] InputVector { get; set; }
            public List<Cocktails> cocktailsVectors { get; set; }
        }
    }
}
