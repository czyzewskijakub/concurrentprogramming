using System.ComponentModel;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Data;

public sealed class BallData : INotifyPropertyChanged
{
    private Vector2 _vector;
    private Vector2 _velocity;

    public BallData(float xPos, float yPos, int radius, float mass, Vector2 velocity)
    {
        _vector.X = xPos;
        _vector.Y = yPos;
        Radius = radius;
        Mass = mass;
        _velocity = velocity;
    }
    
    public void Update()
    {
        _vector += _velocity;
        OnPropertyChanged(nameof(Vector));
    }

    [JsonIgnore]
    public Vector2 Vector => _vector;

    [JsonIgnore]
    public Vector2 Velocity
    {
        get => _velocity;
        set => _velocity = value;
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
        set => _vector.Y = value;
    }

    public int Radius { get; }

    public float Mass { get; }

    public float VelocityX
    {
        get => _velocity.X;
        set
        {
            value = value switch
            {
                > 3 => 3,
                < -3 => -3,
                _ => value
            };
            _velocity.X = value;
        }
    }

    public float VelocityY
    {
        get => _velocity.Y;
        set
        {
            value = value switch
            {
                > 3 => 3,
                < -3 => -3,
                _ => value
            };
            _velocity.Y = value;
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}