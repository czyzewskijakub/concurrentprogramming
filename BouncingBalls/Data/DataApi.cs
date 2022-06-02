using System.Collections.ObjectModel;

namespace Data;

public class DataApi : DataAbstractApi
{
    private BallOperations _ballOperations = new();

    public override void CreateBalls(int number)
    {
        _ballOperations = new BallOperations();
        _ballOperations.CreateBalls(number);
    }

    public override void StopBalls()
    {
        _ballOperations.StopBalls();
    }

    public override ObservableCollection<BallData> GetBalls() => _ballOperations.Balls;

    public override int Width => BallOperations.Width;
    public override int Height => BallOperations.Height;
}