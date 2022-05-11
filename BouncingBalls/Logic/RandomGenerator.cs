namespace Logic
{
    public class RandomGenerator
    {
        private readonly Random _random;
        
        public RandomGenerator()
        {
            _random = new Random();
        }
        
        public double GenerateDouble(double min, double max)
        {
            return _random.NextDouble() * (max - min) + min;
        }

        public double GenerateDouble()
        {
            return _random.NextDouble();
        }

    }
}