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

        public static readonly StyledProperty<FontFamily> LabelFontFamilyProperty = 
            AvaloniaProperty.Register<LineChart, FontFamily>(nameof(LabelFontFamily));
        
        public static readonly StyledProperty<FontStyle> LabelFontStyleProperty = 
            AvaloniaProperty.Register<LineChart, FontStyle>(nameof(LabelFontStyle));
        
        public static readonly StyledProperty<FontWeight> LabelFontWeightProperty = 
            AvaloniaProperty.Register<LineChart, FontWeight>(nameof(LabelFontWeight));

        public static readonly StyledProperty<double> LabelFontSizeProperty = 
            AvaloniaProperty.Register<LineChart, double>(nameof(LabelFontSize));

        public static readonly StyledProperty<Thickness> ValuesMarginProperty = 
            AvaloniaProperty.Register<LineChart, Thickness>(nameof(ValuesMargin));

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

        public FontFamily LabelFontFamily
        {
            get => GetValue(LabelFontFamilyProperty);
            set => SetValue(LabelFontFamilyProperty, value);
        }

        public FontStyle LabelFontStyle
        {
            get => GetValue(LabelFontStyleProperty);
            set => SetValue(LabelFontStyleProperty, value);
        }

        public FontWeight LabelFontWeight
        {
            get => GetValue(LabelFontWeightProperty);
            set => SetValue(LabelFontWeightProperty, value);
        }

        public double LabelFontSize
        {
            get => GetValue(LabelFontSizeProperty);
            set => SetValue(LabelFontSizeProperty, value);
        }

        public Thickness ValuesMargin
        {
            get => GetValue(ValuesMarginProperty);
            set => SetValue(ValuesMarginProperty, value);
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

            var valuesMargin = ValuesMargin;
            var valuesWidth = width - valuesMargin.Left - valuesMargin.Right;
            var valuesHeight = height - valuesMargin.Top - valuesMargin.Bottom;
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
                DrawFill(context, points, valuesWidth, valuesHeight, valuesMargin);
            }

            if (Stroke is not null)
            {
                DrawStroke(context, points, valuesMargin);
            }

            if (CursorStroke is not null)
            {
                DrawCursor(context, cursorPosition, valuesHeight, valuesMargin);
            }

            if (false)
            {
                DrawHorizontalAxis(context, valuesWidth, valuesHeight, valuesMargin); 
            }

            if (true)
            {
                DrawVerticalAxis(context, valuesWidth, valuesHeight, valuesMargin);
            }

            if (LabelForeground is not null)
            {
                DrawLabels(context, labels, step, valuesHeight, valuesMargin);
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
            var transform = context.PushPreTransform(Matrix.CreateTranslation(margin.Left + 0.5, margin.Top + 0.5));
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
            var transform = context.PushPreTransform(Matrix.CreateTranslation(margin.Left + 0.5, margin.Top + 0.5));
            context.DrawGeometry(null, pen, geometry);
            transform.Dispose();
        }

        private void DrawCursor(DrawingContext context, double position, double height, Thickness margin)
        {
            var brush = CursorStroke;
            var thickness = CursorThickness;
            var pen = new Pen(brush, thickness);
            var deflate = thickness / 0.5;
            var p1 = new Point(position + deflate, 0);
            var p2 = new Point(position + deflate, height);
            var transform = context.PushPreTransform(Matrix.CreateTranslation(margin.Left, margin.Top));
            context.DrawLine(pen, p1, p2);
            transform.Dispose();
        }

        private void DrawHorizontalAxis(DrawingContext context, double width, double height, Thickness margin)
        {
            var size = 3.5;
            var brush = Brushes.Black;
            var thickness = 1;
            var pen = new Pen(brush, thickness);
            var deflate = thickness / 0.5;
            var p1 = new Point(margin.Left + 0.0, margin.Top + height + deflate);
            var p2 = new Point(margin.Left + width, margin.Top + height + deflate);
            context.DrawLine(pen, p1, p2);
            var p3 = new Point(p2.X, p2.Y);
            var p4 = new Point(p2.X - size, p2.Y - size);
            context.DrawLine(pen, p3, p4);
            var p5 = new Point(p2.X, p2.Y);
            var p6 = new Point(p2.X - size, p2.Y + size);
            context.DrawLine(pen, p5, p6);
        }

        private void DrawVerticalAxis(DrawingContext context, double width, double height, Thickness margin)
        {
            var size = 3.5;
            var brush = Brushes.Black;
            var thickness = 1;
            var pen = new Pen(brush, thickness);
            var deflate = thickness / 0.5;
            var p1 = new Point(margin.Left / 2 + deflate, margin.Top + 0.0);
            var p2 = new Point(margin.Left / 2 + deflate, margin.Top + height);
            context.DrawLine(pen, p1, p2);
            var p3 = new Point(p1.X, p1.Y);
            var p4 = new Point(p1.X - size, p1.Y + size);
            context.DrawLine(pen, p3, p4);
            var p5 = new Point(p1.X, p1.Y);
            var p6 = new Point(p1.X + size, p1.Y + size);
            context.DrawLine(pen, p5, p6);
        }

        private void DrawLabels(DrawingContext context, List<string> labels, double step, double height, Thickness margin)
        {
            var fontFamily = LabelFontFamily;
            var fontStyle = LabelFontStyle;
            var fontWeight = LabelFontWeight;
            var typeface = new Typeface(fontFamily, fontStyle, fontWeight);
            var fontSize = LabelFontSize;
            var labelOffset = LabelOffset;
            var labelHeight = LabelHeight;
            var labelForeground = LabelForeground;
            var labelAngleRadians = Math.PI / 180.0 * LabelAngle;
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
                    FontSize = fontSize,
                    Constraint = constraint
                };
                var xPosition = origin.X + constraint.Width / 2;
                var yPosition = origin.Y + constraint.Height / 2;
                var matrix = Matrix.CreateTranslation(-xPosition, -yPosition)
                             * Matrix.CreateRotation(labelAngleRadians)
                             * Matrix.CreateTranslation(xPosition, yPosition);
                var transform = context.PushPreTransform(matrix);
                context.DrawText(labelForeground, origin, formattedText);
#if false
                context.DrawRectangle(null, new Pen(new SolidColorBrush(Colors.Magenta)), new Rect(origin, constraint));
#endif
                transform.Dispose();
#if false
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