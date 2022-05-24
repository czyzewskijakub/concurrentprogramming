using System.Collections.ObjectModel;
using System.Numerics;

namespace Data
{
    public class BallOperations 
    {
        private readonly RandomGenerator _randomGenerator = new();
        private CancellationTokenSource? _tokenSource;
        private readonly object _lock = new();

        public static int Height => 400;
        public static int Width => 800;
        private static int Radius => 30;

        private BallData GenerateBall()
        {
            var x = _randomGenerator.NextFloat(Radius, Width - Radius);
            var y = _randomGenerator.NextFloat(Radius, Height - Radius);
            var velocityX = _randomGenerator.NextFloat(-3, 3);
            var velocityY = _randomGenerator.NextFloat(-3, 3);
            var mass = _randomGenerator.NextFloat(0, 2);
            if(velocityX is > -1 and < 0)
            {
                velocityX = -1;
            }
            if (velocityX is > 0 and < 1)
            {
                velocityX = 1;
            }
            if (velocityY is > -1 and < 0)
            {
                velocityY = -1;
            }
            if (velocityY is > 0 and < 1)
            {
                velocityY = 1;
            }
            var velocity = new Vector2(velocityX, velocityY);
            return new BallData(x, y, Radius, mass, velocity);
        }

        public void CreateBalls(int number)
        {
            if (number > 0)
            {
                _tokenSource = new CancellationTokenSource();
                for (var i = 0; i < number; i++)
                {
                    Balls.Add(GenerateBall());
                }
            }
            MoveBalls();
        }

        public void StopBalls()
        {
            if (_tokenSource is not {IsCancellationRequested: false}) return;
            _tokenSource.Cancel();
            Balls.Clear();
        }

        /**
         * ================
         * CRITICAL SECTION
         * ================
         */
        private void MoveBalls()
        {
            foreach (var ball in Balls)
            {
                Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(5);
                        lock(_lock)
                        {
                            ball.Update();
                            while (ball.Movable == false) { }
                        }
                        ball.Update();
                        try { _tokenSource!.Token.ThrowIfCancellationRequested(); }
                        catch (OperationCanceledException) { break; }
                    }
                }, _tokenSource!.Token);
            }
        }

        public ObservableCollection<BallData> Balls { get; } = new();
    }
}