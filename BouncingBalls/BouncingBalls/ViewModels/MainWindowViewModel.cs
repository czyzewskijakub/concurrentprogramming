using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using BouncingBalls.Models;

namespace BouncingBalls.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() : this(ModelAbstractApi.CreateApi()) { }

        private MainWindowViewModel(ModelAbstractApi modelLayer)
        {
            // Fields initialization
            _modelLayer = modelLayer;
            _coordinates = new ObservableCollection<Point>();
            _randomGenerator = new RandomGenerator();
            _radius = _modelLayer.Radius;
            _canvasWidth = _modelLayer.CanvasWidth;
            _canvasHeight = _modelLayer.CanvasHeight;
            _dispatcherTimer = new DispatcherTimer();

            // Commands initialization
            GenerateCommand = new RelayCommand(GenerateHandler);
            StartMoving = new RelayCommand(MovingHandler);
            StopMoving = new RelayCommand(Stop);
            ClearBoard = new RelayCommand(ClearBalls);
        }

        private readonly ModelAbstractApi _modelLayer;
        private int _ballsNumber;
        private readonly ObservableCollection<Point> _coordinates;
        private readonly RandomGenerator _randomGenerator;
        private int _radius;
        private readonly int _canvasWidth;
        private readonly int _canvasHeight;
        private readonly DispatcherTimer _dispatcherTimer;

        public int BallsNumber
        {
            get => _ballsNumber;
            set
            {
                if (value == _ballsNumber) return;
                _ballsNumber = value;
                RaisePropertyChanged();
            }
        }

        public int Radius
        {
            get => _radius;
            set
            {
                if (value == _radius) return;
                _radius = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Point> Coordinates
        {
            get => _coordinates;
            set
            {
                if (value == _coordinates) return;
                RaisePropertyChanged();
            }
        }

        public int CanvasWidth
        {
            get => _canvasWidth;
            set
            {
                if (value.Equals(_canvasWidth)) return;
                RaisePropertyChanged();
            }
        }
        
        public int CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                if (value.Equals(_canvasHeight)) return;
                RaisePropertyChanged();
            }
        }

        private void GenerateHandler()
        {
            if (Coordinates.Count != 0) return;
            for (var i = 0; i < BallsNumber; i++)
            {
                var point = new Point(_randomGenerator.GenerateDouble(Radius, _modelLayer.CanvasWidth - Radius), 
                    _randomGenerator.GenerateDouble(Radius, _modelLayer.CanvasHeight - Radius));
                Coordinates.Add(point);
            }
        }

        private void MovingHandler()
        {
            if (BallsNumber == 0) return;
            _dispatcherTimer.Tick += (sender, args) => MoveBall();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 30);
            _dispatcherTimer.Start();
        }

        private void MoveBall()
        {
            var copy = Coordinates;
            for (var i = 0; i < Coordinates.Count; i++)
            {
                // Generate shifts
                var xShift = _randomGenerator.GenerateDouble(-1, 1);
                var yShift = _randomGenerator.GenerateDouble(-1, 1);
                var newPt = Point.Add(copy[i], new Vector(xShift, yShift));
                // Prevent exceeding canvas
                if (newPt.X - _radius < 0) newPt = new Point(_radius, newPt.Y);
                if (newPt.X + _radius > _modelLayer.CanvasWidth) newPt = new Point(_modelLayer.CanvasWidth - _radius, newPt.Y);
                if (newPt.Y - _radius < 0) newPt = new Point(newPt.X, _radius);
                if (newPt.Y + _radius > _modelLayer.CanvasHeight) newPt = new Point(newPt.X, _modelLayer.CanvasHeight - _radius);
                copy[i] = newPt;
            }
            // Refresh collection to subscribe PropertyChange event by setter
            Coordinates = new ObservableCollection<Point>(copy);
        }

        private void Stop()
        {
            _dispatcherTimer.Stop();
        }

        private void ClearBalls()
        {
            Stop();
            Coordinates.Clear();
        }

        public ICommand GenerateCommand { get; }
        public ICommand StartMoving { get; }
        public ICommand StopMoving { get; }
        public ICommand ClearBoard { get; }
        
    }
}