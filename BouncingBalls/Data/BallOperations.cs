using System.Collections.ObjectModel;
using System.Numerics;
using System.Text.Json;

namespace Data
{
    public class BallOperations 
    {
        private readonly RandomGenerator _randomGenerator = new();
        private CancellationTokenSource? _tokenSource;
        private readonly List<Task> _tasks = new();
        private readonly object _lock = new();
        private const string JsonFilename = "log.json";

        public ObservableCollection<BallData> Balls { get; } = new();

        public static int Height => 400;
        public static int Width => 800;
        private static int Radius => 30;

        private BallData GenerateBall()
        {
            var x = _randomGenerator.NextFloat(Radius, Width - Radius);
            var y = _randomGenerator.NextFloat(Radius, Height - Radius);
            var velocityX = _randomGenerator.NextFloat(-3, 3);
            var velocityY = _randomGenerator.NextFloat(-3, 3);
            var mass = _randomGenerator.NextFloat(0, 2);
            if(velocityX is > -1 and < 0)
            {
                velocityX = -1;
            }
            if (velocityX is > 0 and < 1)
            {
                velocityX = 1;
            }
            if (velocityY is > -1 and < 0)
            {
                velocityY = -1;
            }
            if (velocityY is > 0 and < 1)
            {
                velocityY = 1;
            }
            var velocity = new Vector2(velocityX, velocityY);
            return new BallData(x, y, Radius, mass, velocity);
        }

        public void CreateBalls(int number)
        {
            if (number > 0)
            {
                _tokenSource = new CancellationTokenSource();
                for (var i = 0; i < number; i++)
                {
                    Balls.Add(GenerateBall());
                }
            }
            MoveBalls();
        }

        public void StopBalls()
        {
            if (_tokenSource is not {IsCancellationRequested: false}) return;
            _tokenSource.Cancel();
            Balls.Clear();
            _tasks.Clear();
        }

        /**
         * ================
         * CRITICAL SECTION
         * ================
         */
        private void MoveBalls()
        {
            // Update balls positions
            foreach (var ball in Balls)
            {
                _tasks.Add(Task.Run(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(5);
                        ball.Update();
                        try { _tokenSource!.Token.ThrowIfCancellationRequested(); }
                        catch (OperationCanceledException) { break; }
                    }
                }, _tokenSource!.Token));
            }
            
            // Write diagnostic data
            _tasks.Add(Task.Run(async () =>
            {
                lock (_lock)
                {
                    File.WriteAllText(JsonFilename, string.Empty);
                }

                while (true)
                {
                    await Task.Delay(5000);
                    var opt = new JsonSerializerOptions {WriteIndented = true};
                    var ballsSerialized =  JsonSerializer.Serialize(Balls, opt);
                    var jsonString = "[ \"Date/Time\": \"" + DateTime.Now + "\",\n  \"Balls\": " + ballsSerialized + " ]\n";
                    lock (_lock)
                    {
                        File.AppendAllText(JsonFilename, jsonString);
                    }
                    try { _tokenSource!.Token.ThrowIfCancellationRequested(); }
                    catch (OperationCanceledException) { break; }
                }
            }));
        }
    }
}