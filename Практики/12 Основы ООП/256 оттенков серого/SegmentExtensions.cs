using System.Collections.Generic;
using Avalonia.Media;
using Geometry;

namespace GeometryPainting;

    //Напишите здесь код, который заставит работать методы segment.GetColor и segment.SetColor

    public class SegmentColorDict
    {
        private static Dictionary<Segment, Color> segmentColors = new Dictionary<Segment, Color>();

        public static Color GetColor(Segment segment)
        {
            if (segmentColors.ContainsKey(segment))
                return segmentColors[segment];
            return Color.FromRgb(0, 0, 0); // Цвет по умолчанию
        }

        public static void SetColor(Segment segment, Color color)
        {
            segmentColors[segment] = color;
        }
    }

    public static class SegmentExtensions
    {
        public static Color GetColor(this Segment segment)
        {
            return SegmentColorDict.GetColor(segment);
        }

        public static void SetColor(this Segment segment, Color color)
        {
            SegmentColorDict.SetColor(segment, color);
        }
    }