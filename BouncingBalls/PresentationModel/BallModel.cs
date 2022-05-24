using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;

namespace PresentationModel;

public sealed class BallModel : INotifyPropertyChanged
{
    private float _xPosition;
    private float _yPosition;
    private float _radius;

    public BallModel(BallLogic ballLogic)
    {
        ballLogic.PropertyChanged += BallPropertyChanged;
        _xPosition = ballLogic.X;
        _yPosition = ballLogic.Y;
        _radius = ballLogic.Radius;
    }
    
    public float Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            RaisePropertyChanged(nameof(Radius));
        }
    }
    public float XPosition
    {
        get => _xPosition;
        set
        {
            _xPosition = value;
            RaisePropertyChanged(nameof(XPosition));
        }
    }
    public float YPosition
    {
        get => _yPosition;
        set
        {
            _yPosition = value;
            RaisePropertyChanged(nameof(YPosition));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    private void BallPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var ballLogic = (BallLogic)sender;

        XPosition = ballLogic.X;
        YPosition = ballLogic.Y;
        RaisePropertyChanged(nameof(XPosition));
        RaisePropertyChanged(nameof(YPosition));
    }
}