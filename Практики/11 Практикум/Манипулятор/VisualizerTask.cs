using System;
using System.Globalization;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media;

namespace Manipulation;

public static class VisualizerTask
{
    public static double X = 220;
    public static double Y = -100;
    public static double Alpha = 0.05;
    public static double Wrist = 2 * Math.PI / 3;
    public static double Elbow = 3 * Math.PI / 4;
    public static double Shoulder = Math.PI / 2;

    public static Brush UnreachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 255, 230, 230));
    public static Brush ReachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 230, 255, 230));
    public static Pen ManipulatorPen = new Pen(Brushes.Black, 3);
    public static Brush JointBrush = new SolidColorBrush(Colors.Gray);
    private const double AngleStep = 0.05;

    public static void KeyDown(Visual visual, KeyEventArgs key)
    {
        if (key.Key == Key.Q) Shoulder += AngleStep;
        else if (key.Key == Key.A) Shoulder -= AngleStep;
        else if (key.Key == Key.W) Elbow += AngleStep;
        else if (key.Key == Key.S) Elbow -= AngleStep;
        else return;

        Wrist = -Alpha - Shoulder - Elbow;
        visual.InvalidateVisual();
    }

    public static void MouseMove(Visual visual, PointerEventArgs e)
    {
        var shoulderPos = GetShoulderPos(visual);
        var windowPoint = e.GetPosition(visual);
        var mathPoint = ConvertWindowToMath(windowPoint, shoulderPos);

        X = mathPoint.X;
        Y = mathPoint.Y;

        UpdateManipulator();
        visual.InvalidateVisual();
    }

    public static void MouseWheel(Visual visual, PointerWheelEventArgs e)
    {
        Alpha += e.Delta.Y * 0.05;
        UpdateManipulator();
        visual.InvalidateVisual();
    }

    public static void UpdateManipulator()
    {
        var angles = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
        if (double.IsNaN(angles[0]) is false)
        {
            Shoulder = angles[0];
            Elbow = angles[1];
            Wrist = angles[2];
        }
    }

    public static void DrawManipulator(DrawingContext context, Point shoulderPos)
    {
        var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

        DrawReachableZone(context, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);

        var formattedText = new FormattedText(
          $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}",
          CultureInfo.InvariantCulture,
          FlowDirection.LeftToRight,
          Typeface.Default,
          18,
          Brushes.DarkRed
        ) { TextAlignment = TextAlignment.Center };

        context.DrawText(formattedText, new Point(10, 10));
        DrawManipulatorSegments(context, shoulderPos, joints);
    }

    private static void DrawManipulatorSegments(DrawingContext context, Point shoulderPos, Point[] joints)
    {
        var shoulderWindow = shoulderPos;
        var elbowWindow = ConvertMathToWindow(joints[0], shoulderPos);
        var wristWindow = ConvertMathToWindow(joints[1], shoulderPos);
        var palmEndWindow = ConvertMathToWindow(joints[2], shoulderPos);

        DrawSegment(context, shoulderWindow, elbowWindow);
        DrawSegment(context, elbowWindow, wristWindow);
        DrawSegment(context, wristWindow, palmEndWindow);

        DrawJoint(context, shoulderWindow);
        DrawJoint(context, elbowWindow);
        DrawJoint(context, wristWindow);
    }

    private static void DrawSegment(DrawingContext context, Point start, Point end)
    {
        context.DrawLine(ManipulatorPen, start, end);
    }

    private static void DrawJoint(DrawingContext context, Point position)
    {
        const double JointRadius = 10;
        context.DrawEllipse(JointBrush, null, position, JointRadius, JointRadius);
    }

    private static void DrawReachableZone(
        DrawingContext context,
        Brush reachableBrush,
        Brush unreachableBrush,
        Point shoulderPos,
        Point[] joints)
    {
        var rMin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
        var rMax = Manipulator.UpperArm + Manipulator.Forearm;
        var mathCenter = new Point(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
        var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
        context.DrawEllipse(reachableBrush, null, new Point(windowCenter.X, windowCenter.Y), rMax, rMax);
        context.DrawEllipse(unreachableBrush, null, new Point(windowCenter.X, windowCenter.Y), rMin, rMin);
    }

    public static Point GetShoulderPos(Visual visual)
    {
        return new Point(visual.Bounds.Width / 2, visual.Bounds.Height / 2);
    }

    public static Point ConvertMathToWindow(Point mathPoint, Point shoulderPos)
    {
        return new Point(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
    }

    public static Point ConvertWindowToMath(Point windowPoint, Point shoulderPos)
    {
        return new Point(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
    }
}

