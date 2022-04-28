using System.Collections;
using System.Collections.ObjectModel;
using Timer = System.Timers.Timer;

namespace Logic;

public class LogicApi : LogicAbstractApi
{
        public override void GenerateHandler(ICollection<MyPoint> coordinates, int ballsNumber, int minX, int maxX, int minY, int maxY)
        {
            var randomGenerator = new RandomGenerator();
            if (coordinates.Count != 0) return;
            for (var i = 0; i < ballsNumber; i++)
            {
                var point = new MyPoint(randomGenerator.GenerateDouble(minX, maxX), 
                    randomGenerator.GenerateDouble(minY, maxY));
                coordinates.Add(point);
            }
        }

        public override void MovingHandler(ObservableCollection<MyPoint> coordinates, Timer timer, int ballsNumber, int radius,
            int maxX, int maxY)
        {
            if (ballsNumber == 0) return;

            var context = SynchronizationContext.Current;
            timer.Interval = 30;
            timer.Elapsed += (_, _) => context.Send(_ => MoveBall(coordinates, radius, maxX, maxY), null);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public override void MoveBall(ObservableCollection<MyPoint> coordinates, int radius, int maxX, int maxY)
        {
            var randomGenerator = new RandomGenerator();
            var copy = coordinates;
            for (var i = 0; i < coordinates.Count; i++)
            {
                // Generate shifts
                var xShift = randomGenerator.GenerateDouble(-1, 1);
                var yShift = randomGenerator.GenerateDouble(-1, 1);
                var newPt = new MyPoint(copy[i].X + xShift, copy[i].Y + yShift);
                // Prevent exceeding canvas
                if (newPt.X - radius < 0) newPt = new MyPoint(radius, newPt.Y);
                if (newPt.X + radius > maxX) newPt = new MyPoint(maxX - radius, newPt.Y);
                if (newPt.Y - radius < 0) newPt = new MyPoint(newPt.X, radius);
                if (newPt.Y + radius > maxY) newPt = new MyPoint(newPt.X, maxY - radius);
                copy[i] = newPt;
            }
            // Refresh collection to subscribe PropertyChange event by setter
            coordinates = new ObservableCollection<MyPoint>(copy);
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