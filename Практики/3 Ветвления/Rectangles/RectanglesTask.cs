using Avalonia.Controls;
using System;

namespace Rectangles;

public static class RectanglesTask
{
    // Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
    public static bool AreIntersected(Rectangle r1, Rectangle r2)
    {
        // так можно обратиться к координатам левого верхнего угла первого прямоугольника: r1.Left, r1.Top
        bool conditionXSides1 = (r1.Left <= r2.Right && r1.Right >= r2.Left);
        bool conditionYSides1 = (r1.Top >= r2.Bottom && r1.Bottom <= r2.Top);

        bool conditionXSides2 = (r2.Left <= r1.Right && r2.Right >= r1.Left);
        bool conditionYSides2 = (-r2.Top >= -r1.Bottom && -r2.Bottom <= -r1.Top);
        
        return (conditionXSides1 && conditionYSides1) || (conditionXSides2 && conditionYSides2);
    }

    // Площадь пересечения прямоугольников
    public static int IntersectionSquare(Rectangle r1, Rectangle r2)
    { 
        if (AreIntersected(r1, r2))
        {
            var IntersectedWidth = Math.Min(r1.Right, r2.Right) - Math.Max(r1.Left, r2.Left);
            var IntersectedHight = Math.Min(-r1.Top, -r2.Top) - Math.Max(-r1.Bottom, -r2.Bottom);

            return IntersectedWidth * IntersectedHight;
        }

        else return 0;
    }

    // Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
    // Иначе вернуть -1
    // Если прямоугольники совпадают, можно вернуть номер любого из них.
    public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
    {
        bool secondInFirstCond1 = ((r2.Left >= r1.Left) && (r2.Right <= r1.Right));
        bool secondInFirstCond2 = ((-r1.Top >= -r2.Top) && (-r1.Bottom <= -r2.Bottom))
            ;
        bool firstInSecondCond1 = ((r1.Left >= r2.Left) && (r1.Right <= r2.Right));
        bool firstInSecondCond2 = ((-r2.Top >= -r1.Top) && (-r2.Bottom <= -r1.Bottom));

        if (firstInSecondCond1 && firstInSecondCond2) { return 0; } 
        else if (secondInFirstCond1 && secondInFirstCond2) { return 1; }
        else return -1;
    }
}