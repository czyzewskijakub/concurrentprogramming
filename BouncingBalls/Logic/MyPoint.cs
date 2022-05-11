using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic;

public class MyPoint : INotifyPropertyChanged
{
    private double x { get; set; }
    private double y { get; set; }

    public MyPoint(double x, double y)
    {
        X = x;
        Y = y;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public double X
    {
        get => x;
        set
        {
            if (value.Equals(x)) return;
            x = value;
            OnPropertyChanged(nameof(X));
        }
    }
    public double Y
    {
        get => y;
        set
        {
            if (value.Equals(y)) return;
            y = value;
            OnPropertyChanged(nameof(Y));
        }
    }
}