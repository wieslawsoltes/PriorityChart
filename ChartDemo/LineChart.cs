using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace ChartDemo
{
    public class LineChart : Control
    {
        #region Properties

        public static readonly StyledProperty<List<double>> ValuesProperty = 
            AvaloniaProperty.Register<LineChart, List<double>>(nameof(Values));

        public static readonly StyledProperty<List<string>> LabelsProperty = 
            AvaloniaProperty.Register<LineChart, List<string>>(nameof(Labels));

        public static readonly StyledProperty<double> MinValueProperty = 
            AvaloniaProperty.Register<LineChart, double>(nameof(MinValue));

        public static readonly StyledProperty<double> MaxValueProperty = 
            AvaloniaProperty.Register<LineChart, double>(nameof(MaxValue));

        public static readonly StyledProperty<bool> LogarithmicScaleProperty = 
            AvaloniaProperty.Register<LineChart, bool>(nameof(LogarithmicScale));

        public static readonly StyledProperty<IBrush?> FillProperty = 
            AvaloniaProperty.Register<LineChart, IBrush?>(nameof(Fill));

        public static readonly StyledProperty<IBrush?> StrokeProperty = 
            AvaloniaProperty.Register<LineChart, IBrush?>(nameof(Stroke));

        public static readonly StyledProperty<double> StrokeThicknessProperty = 
            AvaloniaProperty.Register<LineChart, double>(nameof(StrokeThickness));

        public static readonly StyledProperty<IBrush?> LabelForegroundProperty = 
            AvaloniaProperty.Register<LineChart, IBrush?>(nameof(LabelForeground));

        public static readonly StyledProperty<double> LabelOffsetProperty = 
            AvaloniaProperty.Register<LineChart, double>(nameof(LabelOffset));

        public static readonly StyledProperty<double> LabelHeightProperty = 
            AvaloniaProperty.Register<LineChart, double>(nameof(LabelHeight));

        public static readonly StyledProperty<TextAlignment> LabelAlignmentProperty = 
            AvaloniaProperty.Register<LineChart, TextAlignment>(nameof(LabelAlignment));

        public static readonly StyledProperty<double> LabelAngleProperty = 
            AvaloniaProperty.Register<LineChart, double>(nameof(LabelAngle));

        public static readonly StyledProperty<Thickness> LineMarginProperty = 
            AvaloniaProperty.Register<LineChart, Thickness>(nameof(LineMargin));

        public static readonly StyledProperty<IBrush?> CursorStrokeProperty = 
            AvaloniaProperty.Register<LineChart, IBrush?>(nameof(CursorStroke));
        
        public static readonly StyledProperty<double> CursorThicknessProperty = 
            AvaloniaProperty.Register<LineChart, double>(nameof(CursorThickness));
        
        public static readonly StyledProperty<double> CursorValueProperty = 
            AvaloniaProperty.Register<LineChart, double>(nameof(CursorValue));

        public static readonly StyledProperty<IBrush?> BorderBrushProperty = 
            AvaloniaProperty.Register<LineChart, IBrush?>(nameof(BorderBrush));

        public static readonly StyledProperty<double> BorderThicknessProperty = 
            AvaloniaProperty.Register<LineChart, double>(nameof(BorderThickness));

        static LineChart()
        {
            AffectsMeasure<LineChart>(StrokeThicknessProperty);
            AffectsRender<LineChart>(
                ValuesProperty,
                LabelsProperty,
                MinValueProperty,
                MaxValueProperty,
                FillProperty, 
                StrokeProperty, 
                StrokeThicknessProperty,
                LabelForegroundProperty,
                LabelAngleProperty,
                CursorStrokeProperty,
                CursorThicknessProperty,
                CursorValueProperty);
        }

        public List<double> Values
        {
            get => GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }

        public List<string> Labels
        {
            get => GetValue(LabelsProperty);
            set => SetValue(LabelsProperty, value);
        }

        public double MinValue
        {
            get => GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public double MaxValue
        {
            get => GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public bool LogarithmicScale
        {
            get => GetValue(LogarithmicScaleProperty);
            set => SetValue(LogarithmicScaleProperty, value);
        }

        public IBrush? Fill
        {
            get => GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        public IBrush? Stroke
        {
            get => GetValue(StrokeProperty);
            set => SetValue(StrokeProperty, value);
        }

        public double StrokeThickness
        {
            get => GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }

        public IBrush? LabelForeground
        {
            get => GetValue(LabelForegroundProperty);
            set => SetValue(LabelForegroundProperty, value);
        }

        public double LabelAngle
        {
            get => GetValue(LabelAngleProperty);
            set => SetValue(LabelAngleProperty, value);
        }

        public double LabelOffset
        {
            get => GetValue(LabelOffsetProperty);
            set => SetValue(LabelOffsetProperty, value);
        }

        public double LabelHeight
        {
            get => GetValue(LabelHeightProperty);
            set => SetValue(LabelHeightProperty, value);
        }

        public TextAlignment LabelAlignment
        {
            get => GetValue(LabelAlignmentProperty);
            set => SetValue(LabelAlignmentProperty, value);
        }

        public Thickness LineMargin
        {
            get => GetValue(LineMarginProperty);
            set => SetValue(LineMarginProperty, value);
        }

        public IBrush? CursorStroke
        {
            get => GetValue(CursorStrokeProperty);
            set => SetValue(CursorStrokeProperty, value);
        }

        public double CursorThickness
        {
            get => GetValue(CursorThicknessProperty);
            set => SetValue(CursorThicknessProperty, value);
        }

        public double CursorValue
        {
            get => GetValue(CursorValueProperty);
            set => SetValue(CursorValueProperty, value);
        }

        public IBrush? BorderBrush
        {
            get => GetValue(BorderBrushProperty);
            set => SetValue(BorderBrushProperty, value);
        }

        public double BorderThickness
        {
            get => GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        #endregion

        private static double ScaleHorizontal(double value, double max, double range)
        {
            return value / max * range;
        }

        private static double ScaleVertical(double value, double max, double range)
        {
            return range - value / max * range;
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            var width = this.Bounds.Width;
            var height = this.Bounds.Height;

            var lineMargin = LineMargin;
            var valuesWidth = width - lineMargin.Left - lineMargin.Right;
            var valuesHeight = height - lineMargin.Top - lineMargin.Bottom;
            var logarithmicScale = LogarithmicScale;
            var values = Values;
            var labels = Labels;
            var valuesList = logarithmicScale ? values.Select(y => Math.Log(y)).ToList() : values.ToList();
            var valuesMax = valuesList.Max();
            var scaledValues = valuesList.Select(y => ScaleVertical(y, valuesMax, valuesHeight)).ToList();
            var step = valuesWidth / (valuesList.Count - 1);
            var points = new Point[valuesList.Count];
            for (var i = 0; i < valuesList.Count; i++)
            {
                points[i] = new Point(i * step, scaledValues[i]);
            }

            var minValue = MinValue;
            var maxValue = MaxValue;
            var cursorValue = CursorValue;
            var cursorPosition = ScaleHorizontal(maxValue - cursorValue, maxValue, valuesWidth);

            if (Fill is not null)
            {
                DrawFill(context, points, valuesWidth, valuesHeight, lineMargin);
            }

            if (Stroke is not null)
            {
                DrawStroke(context, points, lineMargin);
            }

            if (CursorStroke is not null)
            {
                DrawCursor(context, cursorPosition, valuesHeight, lineMargin);
            }

            if (LabelForeground is not null)
            {
                DrawLabels(context, labels, step, valuesHeight, lineMargin);
            }

            if (BorderBrush is not null)
            {
                DrawBorder(context, 0, 0, width, height);
            }
        }

        private void DrawFill(DrawingContext context, Point[] points, double width, double height, Thickness margin)
        {
            var fill = Fill;
            var geometry = new StreamGeometry();
            using var geometryContext = geometry.Open();
            geometryContext.BeginFigure(points[0], true);
            for (var i = 1; i < points.Length; i++)
            {
                geometryContext.LineTo(points[i]);
            }
            geometryContext.LineTo(new Point(width, height));
            geometryContext.LineTo(new Point(0, height));
            geometryContext.EndFigure(true);
            var transform = context.PushPreTransform(Matrix.CreateTranslation(margin.Left, margin.Top));
            context.DrawGeometry(fill, null, geometry);
            transform.Dispose();
        }

        private void DrawStroke(DrawingContext context, Point[] points, Thickness margin)
        {
            var stroke = Stroke;
            var strokeThickness = StrokeThickness;
            var geometry = new StreamGeometry();
            using var geometryContext = geometry.Open();
            geometryContext.BeginFigure(points[0], false);
            for (var i = 1; i < points.Length; i++)
            {
                geometryContext.LineTo(points[i]);
            }
            geometryContext.EndFigure(false);
            var pen = new Pen(stroke, strokeThickness);
            var transform = context.PushPreTransform(Matrix.CreateTranslation(margin.Left, margin.Top));
            context.DrawGeometry(null, pen, geometry);
            transform.Dispose();
        }

        private void DrawCursor(DrawingContext context, double position, double height, Thickness margin)
        {
            var brush = CursorStroke;
            var thickness = CursorThickness;
            var pen = new Pen(brush, thickness);
            var p1 = new Point(position, 0);
            var p2 = new Point(position, height);
            var transform = context.PushPreTransform(Matrix.CreateTranslation(margin.Left, margin.Top));
            context.DrawLine(pen, p1, p2);
            transform.Dispose();
        }

        private void DrawLabels(DrawingContext context, List<string> labels, double step, double height, Thickness margin)
        {
            var typeface = new Typeface("system", FontStyle.Normal, FontWeight.Normal);
            var labelFontSize = 12;
            var labelOffset = LabelOffset;
            var labelHeight = LabelHeight;
            var labelForeground = LabelForeground;
            var labelAngle = LabelAngle;
            var labelAlignment = LabelAlignment;
            for (var i = 0; i < labels.Count; i++)
            {
                var origin = new Point(i * step - step / 2 + margin.Left, height + margin.Top + labelOffset);
                var constraint = new Size(step, labelHeight);
                var formattedText = new FormattedText()
                {
                    Typeface = typeface,
                    Text = labels[i],
                    TextAlignment = labelAlignment,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = labelFontSize,
                    Constraint = constraint
                };
                var matrix = Matrix.CreateTranslation(-(origin.X + constraint.Width / 2), -(origin.Y + constraint.Height / 2))
                             * Matrix.CreateRotation(Math.PI / 180.0 * labelAngle)
                             * Matrix.CreateTranslation(origin.X + constraint.Width / 2, origin.Y + constraint.Height / 2);
                var transform = context.PushPreTransform(matrix);
                context.DrawText(labelForeground, origin, formattedText);
#if true
                context.DrawRectangle(null, new Pen(new SolidColorBrush(Colors.Magenta)), new Rect(origin, constraint));
#endif
                transform.Dispose();
#if true
                context.DrawRectangle(null, new Pen(new SolidColorBrush(Colors.Cyan)), new Rect(origin, constraint));
#endif
            }
        }

        private void DrawBorder(DrawingContext context, double x, double y, double width, double height)
        {
            var brush = BorderBrush;
            var thickness = BorderThickness;
            var pen = new Pen(brush, thickness);
            var rect = new Rect(x, y, width, height);
            var rectDeflate = rect.Deflate(thickness * 0.5);
            context.DrawRectangle(null, pen, rectDeflate, 0, 0);
        }
    }
}