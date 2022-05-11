using System.Collections.ObjectModel;
using System.Windows.Input;
using Logic;
using PresentationModel;
using PresentationViewModel.MVVMLight;
using Timer = System.Timers.Timer;

namespace PresentationViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() : this(ModelAbstractApi.CreateApi()) { }

        private MainWindowViewModel(ModelAbstractApi modelLayer)
        {
            // Fields initialization
            var modelLayer1 = modelLayer;
            LogicAbstractApi logicLayer = new LogicApi();
            _radius = modelLayer1.Radius;
            _canvasWidth = modelLayer1.CanvasWidth;
            _canvasHeight = modelLayer1.CanvasHeight;
            var timer = new Timer();
            Coordinates = logicLayer.Coordinates();

            // Commands initialization
            GenerateCommand = new RelayCommand(() => logicLayer.GenerateHandler(BallsNumber, _radius, _canvasWidth - _radius, _radius, _canvasHeight - _radius));
            StartMoving = new RelayCommand(() => logicLayer.MovingHandler(timer, BallsNumber, _radius, modelLayer1.CanvasWidth, modelLayer1.CanvasHeight));
            StopMoving = new RelayCommand(() => logicLayer.Stop(timer));
            ClearBoard = new RelayCommand(() => logicLayer.ClearBalls(timer, Coordinates));
        }

        private int _ballsNumber;
        private ObservableCollection<MyPoint> _coordinates;
        private int _radius;
        private readonly int _canvasWidth;
        private readonly int _canvasHeight;

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

        public ObservableCollection<MyPoint> Coordinates
        {
            get => _coordinates;
            set
            {
                if (value.Equals(_coordinates)) 
                    return;
                _coordinates = value;
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

        public ICommand GenerateCommand { get; }
        public ICommand StartMoving { get; }
        public ICommand StopMoving { get; }
        public ICommand ClearBoard { get; }
        
    }
}