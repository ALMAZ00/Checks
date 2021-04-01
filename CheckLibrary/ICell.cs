namespace CheckLibrary
{
    public interface ICell
    {
        IFigure Figure { get; set; }
        int Xindex { get; }
        int Yindex { get; }
        bool IsNotEmpty();
    }
}