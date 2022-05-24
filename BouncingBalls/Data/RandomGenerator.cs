namespace Data
{
    public class RandomGenerator
    {
        private readonly Random _random;
        
        public RandomGenerator()
        {
            _random = new Random();
        }
        
        public float NextFloat(double min, double max)
        {
            return (float) (_random.NextDouble() * (max - min) + min);
        }
    }
}