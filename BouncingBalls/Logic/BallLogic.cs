using System.ComponentModel;
using Data;

namespace Logic;

public class BallLogic : INotifyPropertyChanged
{
    public BallLogic(BallData ballData)
    {
        BallData = ballData;
        ballData.PropertyChanged += (_, _) => OnPropertyChanged("Vector");
    }

    public BallData BallData { get; }

    public float X
    {
        get => BallData.X;
        set
        {
            BallData.X = value;
            OnPropertyChanged(nameof(X));
        }
    }
    public float Y
    {
        get => BallData.Y;
        set
        {
            BallData.Y = value;
            OnPropertyChanged(nameof(Y));
        }
    }

    public float Radius => BallData.Radius;
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}