using System.ComponentModel;
using System.Numerics;
using Data;

namespace Logic;

public sealed class LogicApi : LogicAbstractApi
{
    private readonly DataAbstractApi _dataAbstractApi;

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
            b.Movable = true;
        }

        private void Interactions(int height, int radius, BallData ballData)
        {
            foreach (var ball in BallsLogics.Where(ball => ball.BallData != ballData))
            {
                ball.BallData.Movable = false;
                var distance = Vector2.Distance(ballData.Vector, ball.BallData.Vector);
                // Collision with another ball
                if (distance <= ballData.Radius + ball.BallData.Radius)
                {
                    if (Vector2.Distance(ballData.Vector, ball.BallData.Vector) > Vector2.Distance(ballData.Vector + ballData.Velocity, ball.BallData.Vector + ball.BallData.Velocity))
                    {
                        var newV1 = (ballData.Velocity * (ballData.Mass - ball.BallData.Mass) + ball.BallData.Velocity * 2 * ball.BallData.Mass) / (ballData.Mass + ball.BallData.Mass);
                        var newV2 = (ball.BallData.Velocity * (ball.BallData.Mass - ballData.Mass) + ballData.Velocity * 2 * ballData.Mass) / (ballData.Mass + ball.BallData.Mass);
                        if (newV1.X > 5) newV1.X = 5;
                        if (newV1.Y > 5) newV1.Y = 5;
                        if (newV1.Y < -5) newV1.Y = -5;
                        if (newV1.X < -5) newV1.X = -5;
                        ballData.Velocity = newV1;
                        ball.BallData.Velocity = newV2;
                    }
                }

                ball.BallData.Movable = true;
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