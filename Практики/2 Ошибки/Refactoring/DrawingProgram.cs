using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    class Picture
    {
        static float x, y;
        static IGraphics graphics;

        public static void Initialize(IGraphics newGraphics)
        {
            graphics = newGraphics;
            graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void DrawLineTo(Pen pen, double length, double angle)
        {
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void MoveTo(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle));
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        static float firstSize;
        static float secondSize;
        static float x0;
        static float y0;

        public static void SetParameters(int width, int height)
        {
            var size = Math.Min(width, height);
            firstSize = size * 0.375f;
            secondSize = size * 0.04f;

            var diagonalLength = Math.Sqrt(2) * (firstSize + secondSize) / 2;
            x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;
        }

        public static void DrawSide(double firstSize, double secondSize, double pi)
        {
            Picture.DrawLineTo(new Pen(Brushes.Yellow), firstSize, pi);
            Picture.DrawLineTo(new Pen(Brushes.Yellow), secondSize * Math.Sqrt(2), pi + Math.PI / 4);
            Picture.DrawLineTo(new Pen(Brushes.Yellow), firstSize, pi + Math.PI);
            Picture.DrawLineTo(new Pen(Brushes.Yellow), firstSize - secondSize, pi + Math.PI / 2);

            Picture.MoveTo(secondSize, pi - Math.PI);
            Picture.MoveTo(secondSize * Math.Sqrt(2), pi + 3 * Math.PI / 4);
        }
        public static void Draw(int width, int hight, double rotationAngle, IGraphics graphics)
        {
            SetParameters(width, hight);
            Picture.Initialize(graphics);
            Picture.SetPosition(x0, y0);
            
            //1-ую сторону
            DrawSide(firstSize, secondSize, rotationAngle + 0);

            //2-ую сторону
            DrawSide(firstSize, secondSize, rotationAngle - Math.PI / 2);

            //3-ю сторону
            DrawSide(firstSize, secondSize, rotationAngle + Math.PI);

            //4-ую сторону
            DrawSide(firstSize, secondSize, rotationAngle + Math.PI / 2);
        }
    }
}