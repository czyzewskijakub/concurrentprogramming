using System.Collections;
using System.Collections.ObjectModel;
using Data;
using Logic;

namespace PresentationModel
{
    public abstract class ModelAbstractApi
    {
        public abstract int CanvasHeight { get; }
        public abstract int CanvasWidth { get; }
        public abstract int ActualBallsNumber { get; }

        public abstract ObservableCollection<BallModel> GenerateHandler(int ballsNumber);
        
        public abstract void Stop();

        public static ModelAbstractApi CreateApi(LogicAbstractApi logicApi = default!)
        {
            return new ModelApi(new LogicApi(new DataApi()));
        }
    }

}