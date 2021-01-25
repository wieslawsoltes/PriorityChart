using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace PriorityChartDemo
{
    public class PriorityChart : Control
    {
        public List<string> Labels = new List<string>()
        {
            "6 days",
            "4 days",
            "3 days",
            "1 day",
            "22 hours",
            "20 hours",
            "18 hours",
            "10 hours",
            "6 hours",
            "4 hours",
            "2 hours",
            "1 hour",
            "50 min",
            "30 min",
            "20 min"
        };

        public List<double> Values = new List<double>()
        {
            15,
            22,
            44,
            50,
            64,
            68,
            92,
            114,
            118,
            142,
            182,
            222,
            446,
            548,
            600
        };

        public static readonly StyledProperty<bool> IsFilledProperty = 
            AvaloniaProperty.Register<PriorityChart, bool>(nameof(IsFilled), true);

        public static readonly StyledProperty<bool> IsStrokedProperty = 
            AvaloniaProperty.Register<PriorityChart, bool>(nameof(IsStroked), true);

        public bool IsFilled
        {
            get => GetValue(IsFilledProperty);
            set => SetValue(IsFilledProperty, value);
        }

        public bool IsStroked
        {
            get => GetValue(IsStrokedProperty);
            set => SetValue(IsStrokedProperty, value);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            var width = this.Bounds.Width;
            var height = this.Bounds.Height;

            var topMargin = 20.0;
            var bottomMargin = 60.0;
            var leftMargin = 60.0;
            var rightMargin = 20.0;

            var valuesWidth = width - leftMargin - rightMargin;
            var valuesHeight = height - topMargin - bottomMargin;
            var max = Values.Max();
            var scaledValues = Values.Select(y => valuesHeight - y / max * valuesHeight).ToList();
            var step = valuesWidth / (Values.Count - 1);
            var points = new Point[Values.Count];
            for (var i = 0; i < Values.Count; i++)
            {
                points[i] = new Point(i * step, scaledValues[i]);
            }

            if (IsFilled)
            {
                DrawDataFill(context, points, valuesWidth, valuesHeight, leftMargin, topMargin);
            }

            if (IsStroked)
            {
                DrawDataStroke(context, points, leftMargin, topMargin);
            }

            DrawLabels(context, step, height, rightMargin, topMargin, bottomMargin);

            DrawBorder(context);
        }

        private void DrawDataFill(DrawingContext context, Point[] points, double width, double height, double leftMargin, double topMargin)
        {
            var geometry = new StreamGeometry();
            using var geometryContext = geometry.Open();
            geometryContext.BeginFigure(points[0], true);
            for (var i = 1; i < Values.Count; i++)
            {
                geometryContext.LineTo(points[i]);
            }
            geometryContext.LineTo(new Point(width, height));
            geometryContext.LineTo(new Point(0, height));
            geometryContext.EndFigure(true);
            var brush = new ImmutableSolidColorBrush(Color.FromArgb(255, 198, 218, 252));
            var transform = context.PushPreTransform(Matrix.CreateTranslation(leftMargin, topMargin));
            context.DrawGeometry(brush, null, geometry);
            transform.Dispose();
        }

        private void DrawDataStroke(DrawingContext context, Point[] points, double leftMargin, double topMargin)
        {
            var geometry = new StreamGeometry();
            using var geometryContext = geometry.Open();
            geometryContext.BeginFigure(points[0], false);
            for (var i = 1; i < Values.Count; i++)
            {
                geometryContext.LineTo(points[i]);
            }
            geometryContext.EndFigure(false);
            var brush = new ImmutableSolidColorBrush(Color.FromArgb(255, 66, 133, 244));
            var pen = new ImmutablePen(brush, 2);
            var transform = context.PushPreTransform(Matrix.CreateTranslation(leftMargin, topMargin));
            context.DrawGeometry(null, pen, geometry);
            transform.Dispose();
        }

        private void DrawLabels(DrawingContext context, double step, double height, double rightMargin, double topMargin, double bottomMargin)
        {
            var brush = new ImmutableSolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            var typeface = new Typeface("system", FontStyle.Normal, FontWeight.Normal);
            for (var i = 0; i < Labels.Count; i++)
            {
                var origin = new Point(rightMargin + i * step, height - topMargin);
                var formattedText = new FormattedText()
                {
                    Typeface = typeface,
                    Text = Labels[i],
                    TextAlignment = TextAlignment.Right,
                    TextWrapping = TextWrapping.NoWrap,
                    FontSize = 12,
                    Constraint = new Size(step, bottomMargin)
                };
                var matrix = Matrix.CreateTranslation(-origin.X, -origin.Y)
                             * Matrix.CreateRotation(-Math.PI / 4)
                             * Matrix.CreateTranslation(origin.X, origin.Y);
                var transform = context.PushPreTransform(matrix);
                context.DrawText(brush, origin, formattedText);
                transform.Dispose();
            }
        }

        private void DrawBorder(DrawingContext context)
        {
            var brush = new ImmutableSolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            var thickness = 1.0;
            var pen = new ImmutablePen(brush, thickness);
            var rect = new Rect(0, 0, this.Bounds.Width, this.Bounds.Height);
            var rectDeflate = rect.Deflate(thickness * 0.5);
            context.DrawRectangle(null, pen, rectDeflate, 0, 0);
        }
    }
}