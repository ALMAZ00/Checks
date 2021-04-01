namespace CheckLibrary
{
    internal class DirectionDeterminant
    {
        public static (int, int) GetCoefficientsTo(Cell startCell, Cell endCell)
        {
            (int coefX, int coefY) coefficients;

            (XDirection xDirection, YDirection yDirection) = GetTableDirection(startCell, endCell);            
            coefficients.coefX = (int)xDirection;
            coefficients.coefY = (int)yDirection;

            return coefficients;
        }
        private static (XDirection, YDirection) GetTableDirection(Cell startCell, Cell endCell)
        {
            (XDirection xDirection, YDirection yDirection) direction;

            direction.xDirection = GetXDirection(startCell, endCell);
            direction.yDirection = GetYDirection(startCell, endCell);

            return direction;
        }
        private static XDirection GetXDirection(Cell startCell, Cell endCell)
        {
            XDirection direction = XDirection.None;

            if (startCell.Xindex > endCell.Xindex)
            {
                direction = XDirection.Left;
            }
            else if (startCell.Xindex < endCell.Xindex)
            {
                direction = XDirection.Right;
            }

            return direction;
        }
        private static YDirection GetYDirection(Cell startCell, Cell endCell)
        {
            YDirection direction = YDirection.None;

            if (startCell.Yindex > endCell.Yindex)
            {
                direction = YDirection.Top;
            }
            else if(startCell.Yindex < endCell.Yindex)
            {
                direction = YDirection.Bottom;
            }

            return direction;
        }
    }
}
