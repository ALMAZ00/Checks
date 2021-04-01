namespace CheckLibrary
{
    public interface IFigure
    {
        Color Color { get; set; }
        bool IsWhiteColor();
        bool IsBlackColor();
        IDrawingFigure GetDrawingFigure(IDrawingFigureFactory factory);
    }
}