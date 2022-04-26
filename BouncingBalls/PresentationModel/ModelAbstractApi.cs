namespace PresentationModel
{
    public abstract class ModelAbstractApi
    {
        public abstract int CanvasHeight { get; }
        public abstract int CanvasWidth { get; }
        public abstract int Radius { get; }

        public static ModelAbstractApi CreateApi()
        {
            return new ModelApi();
        }
    }

}