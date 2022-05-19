using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic;

public class Ball : INotifyPropertyChanged
{
    private Vector2 _vector;
    private Vector2 _velocity;
    private float _mass;

    public Ball(float x, float y, float mass, Vector2 velocity)
    {
        _vector.X = x;
        _vector.Y = y;
        _mass = mass;
        _velocity = velocity;
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    public float X
    {
        get => _vector.X;
        set
        {
            _vector.X = value;
            OnPropertyChanged(nameof(X));
        }
    }
    public float Y
    {
        get => _vector.Y;
        set
        {
            _vector.Y = value;
            OnPropertyChanged(nameof(Y));
        }
    }
}