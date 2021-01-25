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

            
            
            var geometryValuesLine = new StreamGeometry();
            using var geometryContextLine =  geometryValuesLine.Open();
            geometryContextLine.BeginFigure(points[0], false);
            for (var i = 1; i < Values.Count; i++)
            {
                geometryContextLine.LineTo(points[i]);
            }
            geometryContextLine.EndFigure(false);
            var brushValuesLine = new ImmutableSolidColorBrush(Color.FromArgb(255, 66, 133, 244));
            var penValuesLine = new ImmutablePen(brushValuesLine, 2);
            var valuesLineTransform = context.PushPreTransform(Matrix.CreateTranslation(leftMargin, topMargin));
            context.DrawGeometry(null, penValuesLine, geometryValuesLine);
            valuesLineTransform.Dispose();

            

            
            var geometryValuesFill = new StreamGeometry();
            using var geometryContextFill =  geometryValuesFill.Open();
            geometryContextFill.BeginFigure(points[0], true);
            for (var i = 1; i < Values.Count; i++)
            {
                geometryContextFill.LineTo(points[i]);
            }
            geometryContextFill.LineTo(new Point(valuesWidth, valuesHeight));
            geometryContextFill.LineTo(new Point(0, valuesHeight));
            geometryContextFill.EndFigure(true);
            var brushValuesFill = new ImmutableSolidColorBrush(Color.FromArgb(255, 198, 218, 252));
            var valuesFillTransform = context.PushPreTransform(Matrix.CreateTranslation(leftMargin, topMargin));
            context.DrawGeometry(brushValuesFill, null, geometryValuesFill);
            valuesFillTransform.Dispose();   

            
            
            var brushBorder = new ImmutableSolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            var penBorderThickness = 1.0;
            var penBorder = new ImmutablePen(brushBorder, penBorderThickness);
            var borderRect = new Rect(0, 0, this.Bounds.Width, this.Bounds.Height);
            var borderRectDeflate = borderRect.Deflate(penBorderThickness * 0.5);
            context.DrawRectangle(null, penBorder, borderRectDeflate, 0, 0);



            var brushText = new ImmutableSolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            var typeface = new Typeface("Arial");
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
                var textTransform = context.PushPreTransform(matrix);
                context.DrawText(brushText, origin, formattedText);
                textTransform.Dispose();
            }

            
            
            
        }
    }
}