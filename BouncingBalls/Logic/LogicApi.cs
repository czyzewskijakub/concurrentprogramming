using System.ComponentModel;
using System.Numerics;
using System.Text.Json;
using Data;

namespace Logic;

public sealed class LogicApi : LogicAbstractApi
{
    private readonly DataAbstractApi _dataAbstractApi;
    private readonly object _lockBalls = new();
    private readonly object _lockLogger = new();

    public LogicApi(DataAbstractApi dataAbstractApi)
    {
        _dataAbstractApi = dataAbstractApi;
    }

    public override void GenerateBalls(int ballsNumber)
    {
        BallsLogics = new List<BallLogic>();
        _dataAbstractApi.CreateBalls(ballsNumber);
        foreach (var ball in _dataAbstractApi.GetBalls())
        {
            BallsLogics.Add(new BallLogic(ball));
            ball.PropertyChanged += DetectCollision;
        }
    }

        public override void StopBalls() =>_dataAbstractApi.StopBalls();
        public override List<BallLogic> GetBalls() => BallsLogics;
        public override int CanvasHeight => _dataAbstractApi.Height;
        public override int CanvasWidth => _dataAbstractApi.Width;

        private void DetectCollision(object sender, PropertyChangedEventArgs e)
        {
            var b = (BallData)sender;
            if (e.PropertyName != "Vector") return;
            Interactions(CanvasHeight, b.Radius, b);
        }

        private void Interactions(int height, int radius, BallData ballData)
        {
            var opt = new JsonSerializerOptions {WriteIndented = true};
            foreach (var ball in BallsLogics.Where(ball => ball.BallData != ballData))
            {
                // Lock on balls data
                lock (_lockBalls)
                {
                    var distance = Vector2.Distance(ballData.Vector, ball.BallData.Vector);
                    // Collision with another ball
                    if (!(distance <= ballData.Radius + ball.BallData.Radius)) continue;
                    if (!(Vector2.Distance(ballData.Vector, ball.BallData.Vector) >
                          Vector2.Distance(ballData.Vector + ballData.Velocity,
                              ball.BallData.Vector + ball.BallData.Velocity))) continue;
                    
                    var newV1 = (ballData.Velocity * (ballData.Mass - ball.BallData.Mass) + ball.BallData.Velocity * 2 * ball.BallData.Mass) / (ballData.Mass + ball.BallData.Mass);
                    var newV2 = (ball.BallData.Velocity * (ball.BallData.Mass - ballData.Mass) + ballData.Velocity * 2 * ballData.Mass) / (ballData.Mass + ball.BallData.Mass);
                    
                    
                    var logStr = "[ \"Time\": \"" + DateTime.Now + "\",\n  \"Collision\": " 
                                 + "\n\"Ball 1\": " + JsonSerializer.Serialize(ball.BallData, opt) + " \"New VelocityX\": " + JsonSerializer.Serialize(newV1.X, opt)
                                 + "\n  \"New VelocityY\": " + JsonSerializer.Serialize(newV1.Y, opt)
                                 + "\n\"Ball 2\": " + JsonSerializer.Serialize(ballData, opt) + " \"New VelocityX\": " + JsonSerializer.Serialize(newV2.X, opt)
                                 + "\n  \"New VelocityY\": " + JsonSerializer.Serialize(newV2.Y, opt) + " ]\n";
                    
                    ballData.Velocity = newV1;
                    ball.BallData.Velocity = newV2;

                    lock (_lockLogger)
                    {
                        File.AppendAllText("../../../../../log.json", logStr);
                    }

                    JsonSerializer.Serialize(ballData);
                }

            }

            // Border strike
            if (ballData.X + ballData.VelocityX > CanvasWidth - radius || ballData.X + ballData.VelocityX < 0)
            {
                ballData.VelocityX *= -1;
            }
            else if (ballData.Y + ballData.VelocityY > height - radius || ballData.Y + ballData.VelocityY < 0)
            {
                ballData.VelocityY *= -1;
            }
        }
}