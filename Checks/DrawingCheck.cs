using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CheckLibrary;
using System.Windows;
using Images = Checks.Properties.Resources;
using System.IO;
using System.Windows.Media.Imaging;

namespace Checks
{
    class DrawingCheck : DrawingFigure
    {
        public DrawingCheck(CheckLibrary.Color color, int x, int y)
        {
            XIndex = x;
            YIndex = y;

            if (color == CheckLibrary.Color.WhiteColor)
                ImageBit = Images.WhiteCheck;
            else
                ImageBit = Images.BlackCheck;
        }
        public override void DrawFigure(Grid grid)
        {
            Image uiImage = GetUIElementFrom(ImageBit);
            Grid.SetColumn(uiImage, XIndex);
            Grid.SetRow(uiImage, YIndex);
            grid.Children.Add(uiImage);
        }

        private Image GetUIElementFrom(System.Drawing.Bitmap bitmap)
        {
            Image rezult = new Image();
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.EndInit();
                rezult.Source = image;
            }

            return rezult;
        }
    }
}
