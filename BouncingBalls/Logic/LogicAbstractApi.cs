using System.Collections;

namespace Logic;

public abstract class LogicAbstractApi
{
    public static LogicAbstractApi CreateBallApi() => new BallsHandler();
    public abstract IList Generate(int ballsNumber, int minPosRange, int maxPosRange);
    public abstract void Move(IList coordinates, int minPosRange, int maxPosRange);
    public abstract void Stop();
}