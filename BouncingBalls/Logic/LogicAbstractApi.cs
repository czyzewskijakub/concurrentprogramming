using System.Collections;
using System.Collections.ObjectModel;

namespace Logic;

public abstract class LogicAbstractApi
{
    public abstract ObservableCollection<MyPoint> Coordinates();
    public abstract void GenerateHandler(int ballsNumber, int minX, int maxX, int minY,
        int maxY);

    public abstract void MovingHandler(System.Timers.Timer timer, int ballsNumber, int radius,
        int maxX, int maxY);

    public abstract void MoveBalls(MyPoint point, double newX, double newY, double minX, double minY, double maxX, double maxY, double radius);

    public abstract void Stop(System.Timers.Timer timer);

    public abstract void ClearBalls(System.Timers.Timer timer, IList coordinates);

    public static LogicAbstractApi CreateApi()
    {
        return new LogicApi();
    }
}