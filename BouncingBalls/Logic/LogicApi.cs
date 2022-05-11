using System.Collections;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;

namespace Logic;

public class LogicApi : LogicAbstractApi
{
        private readonly ObservableCollection<MyPoint> _coordinates = new();
        public override ObservableCollection<MyPoint> Coordinates() => _coordinates;

        public override void GenerateHandler(int ballsNumber, int minX, int maxX, int minY, int maxY)
        {
            var randomGenerator = new RandomGenerator();
            if (_coordinates.Count != 0) return;
            for (var i = 0; i < ballsNumber; i++)
            {
                var point = new MyPoint(randomGenerator.GenerateDouble(minX, maxX), 
                    randomGenerator.GenerateDouble(minY, maxY));
                _coordinates.Add(point);
            }
        }

        public override void MovingHandler(Timer timer, int ballsNumber, int radius,
            int maxX, int maxY)
        {
            if (ballsNumber == 0) return;
            
            var random = new RandomGenerator();
            for (var i = 0; i < _coordinates.Count; i++)
            {
                Thread.Sleep(6);
                var i1 = i;

                double x;
                double y;
                if (i % 2 == 0)
                {
                    x = random.GenerateDouble(radius, maxX - radius);
                    y = maxY - radius;
                }
                else
                {
                    x = maxX - radius;
                    y = random.GenerateDouble(radius, maxY - radius);
                }

                var x1 = x;
                var y1 = y;
                Task.Factory.StartNew(
                    () => MoveBalls(_coordinates[i1], x1, y1, radius, radius, maxX - radius, maxY - radius, radius),
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext()
                );

            }
        }

        public override async void MoveBalls(MyPoint point, double newX, double newY, double minX, double minY, double maxX, double maxY, double radius)
        {
            var random = new RandomGenerator();
            while (true)
            {
                var dx2 = newX - point.X;
                var dy2 = newY - point.Y;
                var dx = Math.Abs(point.X - newX);
                var dy = Math.Abs(point.Y - newY);
                var d = Math.Sqrt((dx * dx) + (dy * dy));
                var moveX = dx2 / d;
                var moveY = dy2 / d;

                int i;
                for (i = 0; i < d; i++)
                {
                    await Task.Delay(5);
                    point.X += moveX;
                    point.Y += moveY;
                }

                newX = random.GenerateDouble(minX, maxX) + random.GenerateDouble();
                newY = random.GenerateDouble(minY, maxY) + random.GenerateDouble();
            }
        }

        public override void Stop(Timer timer)
        {
            timer.Enabled = false;
        }

        public override void ClearBalls(Timer timer, IList coordinates)
        {
            Stop(timer);
            coordinates.Clear();
        }    

}