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

    internal class ModelApi : ModelAbstractApi
    {
        public override int CanvasHeight => 300;
        public override int CanvasWidth => 600;
        public override int Radius => 20;
    }

}