namespace Cocktails_Suggester
{
    public class Program
    {
        static void Main(string[] args)
        {
            // reading input from user
            var input = Console.ReadLine();

            //loading the data
            DataLoader dataLoader = new DataLoader("cocktails.json",input);
            var loadedData = dataLoader.loadData();
            var processedData = dataLoader.PrepProcessData(loadedData);
          
            //perform KNN
            KNN knn=new KNN(processedData.cocktailsVectors);
            var nearestNeighbour = knn.Classify(processedData.InputVector, 5);

            string result = "";
           
            //Console.WriteLine($"{result} ");
            foreach (var cocktail in nearestNeighbour)
            {
                var c = cocktail.Cocktail;
                result += $"\n{c.Name} \n{c.Ingredients} \n{c.Glass} \n{c.Category} \n{c.Garnish} \n{c.Preparation} \n{c.Colors}\n";
            }
            Console.WriteLine(result);
        }
    }
}