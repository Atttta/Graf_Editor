﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using AvaloniaPaint.Models;
using ReactiveUI;

namespace AvaloniaPaint.ViewModels.Pages
{
    public class AvaloniaPaintPolylineViewModel : PaintShape
    {
        public override string ViewName => "Ломаная линия";

        private string name;
        public string Name
        {
            get => name;
            set
            {
                this.RaiseAndSetIfChanged(ref name, value);
            }
        }
        private int strokeThickness;
        public int StrokeThickness
        {
            get => strokeThickness;
            set
            {
                this.RaiseAndSetIfChanged(ref strokeThickness, value);
            }
        }
        private ISolidColorBrush selectedColor;
        public ISolidColorBrush SelectedColor
        {
            get => selectedColor;
            set
            {
                this.RaiseAndSetIfChanged(ref selectedColor, value);
            }
        }
        private ObservableCollection<ISolidColorBrush> fillColors;

        public ObservableCollection<ISolidColorBrush> FillColors
        {
            get => fillColors;
            set
            {
                this.RaiseAndSetIfChanged(ref fillColors, value);
            }
        }
        private List<Point> points;
        public List<Point> Points
        {
            get => points;
            set
            {
                this.RaiseAndSetIfChanged(ref points, value);
            }
        }
        public AvaloniaPaintPolylineViewModel()
        {
            Name = "";

            StrokeThickness = 1;
            selectedColor = new SolidColorBrush(Colors.White);

            var brushes = typeof(Brushes).GetProperties().Select(brush => (ISolidColorBrush)brush.GetValue(brush));
            fillColors = new ObservableCollection<ISolidColorBrush>(brushes);
        }
        public override Unit ClearShape()
        {
            Name = "";
            List<Point> tempPoints = new List<Point>{ new Point(0, 0) };
            Points = new List<Point> { new Point(0, 0) };
            SelectedColor = new SolidColorBrush(Colors.White) ;
            StrokeThickness = 1;
            return Unit.Default;
        }

        public override PaintBaseFigure? GetShape()
        {
            if(Name!="")
                if (Points is not null)
                    return new PaintPolyline()
                    {
                        Name = Name,
                        Points = Points,
                        Stroke = SelectedColor.Color,
                        StrokeThickness = StrokeThickness
                    };
            return null;
        }
    }
}
