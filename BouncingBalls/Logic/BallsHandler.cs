using System.Collections;
using System.Collections.ObjectModel;

namespace Logic;

public class BallsHandler : LogicAbstractApi
{
    private readonly RandomGenerator _randomGenerator = new();
    private readonly System.Timers.Timer _timer = new();
    
    public override IList Generate(int ballsNumber, int minPosRange, int maxPosRange)
    {
        IList balls = new ArrayList();
        for (var i = 0; i < ballsNumber; i++)
        {
            var point = new MyPoint(_randomGenerator.GenerateDouble(minPosRange, maxPosRange), 
                _randomGenerator.GenerateDouble(minPosRange, maxPosRange));
            balls.Add(point);
        }

        return balls;
    }

    public override void Move(IList coordinates, int minPosRange, int maxPosRange)
    {
        var context = SynchronizationContext.Current;
        _timer.Interval = 30;
        _timer.Elapsed += (_, _) => context.Send(_ => MoveBall(coordinates), null);
        _timer.AutoReset = true;
        _timer.Enabled = true;
    }
    
    private void MoveBall(IList coordinates)
    {
        var copy = Coordinates;
        for (var i = 0; i < Coordinates.Count; i++)
        {
            // Generate shifts
            var xShift = _randomGenerator.GenerateDouble(-1, 1);
            var yShift = _randomGenerator.GenerateDouble(-1, 1);
            var newPt = new MyPoint(copy[i].X + xShift, copy[i].Y + yShift);
            // Prevent exceeding canvas
            if (newPt.X - _radius < 0) newPt = new MyPoint(_radius, newPt.Y);
            if (newPt.X + _radius > _modelLayer.CanvasWidth) newPt = new MyPoint(_modelLayer.CanvasWidth - _radius, newPt.Y);
            if (newPt.Y - _radius < 0) newPt = new MyPoint(newPt.X, _radius);
            if (newPt.Y + _radius > _modelLayer.CanvasHeight) newPt = new MyPoint(newPt.X, _modelLayer.CanvasHeight - _radius);
            copy[i] = newPt;
        }
        // Refresh collection to subscribe PropertyChange event by setter
        Coordinates = new ObservableCollection<MyPoint>(copy);
    }

    public override void Stop()
    {
        throw new NotImplementedException();
    }
}