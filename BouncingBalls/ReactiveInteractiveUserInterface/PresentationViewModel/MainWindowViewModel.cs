using System.Collections;
using System.Windows.Input;
using PresentationModel;
using PresentationViewModel.MVVMLight;

namespace PresentationViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ModelAbstractApi ModelAbstractApi { get; }

        public MainWindowViewModel()
        {
            ModelAbstractApi = ModelAbstractApi.CreateApi();
            // Fields initialization
            CanvasHeight = ModelAbstractApi.CanvasHeight;
            CanvasWidth = ModelAbstractApi.CanvasWidth;
            Balls = ModelAbstractApi.GenerateHandler(BallsNumber);

            // Commands initialization
            StartCommand = new RelayCommand(() => ModelAbstractApi.GenerateHandler(BallsNumber));
            StopCommand = new RelayCommand(() => ModelAbstractApi.Stop());
        }

        private int _ballsNumber;
        private int _radius;
        private int _canvasWidth;
        private int _canvasHeight;

        public int BallsNumber
        {
            get => _ballsNumber;
            set
            {
                if (value == _ballsNumber) return;
                //Debug.WriteLine($"New value: {value}");
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
        
        public IList Balls { get; }

        public int CanvasWidth
        {
            get => _canvasWidth;
            set
            {
                _canvasWidth = value;
                RaisePropertyChanged();
            }
        }
        
        public int CanvasHeight
        {
            get => _canvasHeight;
            set
            {
                _canvasHeight = value;
                RaisePropertyChanged();
            }
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

    }
}