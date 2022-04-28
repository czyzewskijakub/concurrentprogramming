using System.Collections;
using System.Collections.ObjectModel;

namespace Logic;

public abstract class LogicAbstractApi
{
    public abstract void GenerateHandler(ICollection<MyPoint> coordinates, int ballsNumber, int minX, int maxX, int minY,
        int maxY);

    public abstract void MovingHandler(ObservableCollection<MyPoint> coordinates, System.Timers.Timer timer, int ballsNumber, int radius,
        int maxX, int maxY);

    public abstract void MoveBall(ObservableCollection<MyPoint> coordinates, int radius, int maxX, int maxY);

    public abstract void Stop(System.Timers.Timer timer);

    public abstract void ClearBalls(System.Timers.Timer timer, IList coordinates);

    public static LogicAbstractApi CreateApi()
    {
        return new LogicApi();
    }
}