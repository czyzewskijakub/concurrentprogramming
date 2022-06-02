using System.Collections.ObjectModel;

namespace Data;

public abstract class DataAbstractApi
{
    public abstract void CreateBalls(int number);
    public abstract void StopBalls();
    public abstract ObservableCollection<BallData> GetBalls();
    public abstract int Width { get; }
    public abstract int Height { get; }
    public static DataAbstractApi CreateApi()
    {
        return new DataApi();
    }
    
    
}