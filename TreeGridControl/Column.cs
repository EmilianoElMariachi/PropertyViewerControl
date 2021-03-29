using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace TreeGridControl
{
    public class Column : DependencyObject
    {

        public static readonly DependencyProperty WidthProperty;
        public static readonly DependencyProperty CellTemplateProperty;
        private bool _actualWidthInvalidated = true;
        private double _actualWidth;

        static Column()
        {
            CellTemplateProperty = DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(Column), new PropertyMetadata(default(DataTemplate)));

            WidthProperty = DependencyProperty.Register("Width", typeof(double), typeof(Column), new PropertyMetadata(default(double), OnWidthPropertyChanged)
            {
                CoerceValueCallback = CoerceWidthProperty,
            });
        }

        private static void OnWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Column) d).InvalidateActualWidth();
        }

        private static object CoerceWidthProperty(DependencyObject d, object baseValue)
        {
            return Math.Max((double)baseValue, 0d);
        }

        public double ActualWidth
        {
            get
            {
                if (_actualWidthInvalidated)
                {
                    var oldWidth = _actualWidth;
                    _actualWidth = ComputeBestColumnWidth();
                    NotifyActualWidthChanged(oldWidth, _actualWidth);
                    _actualWidthInvalidated = false;
                }

                return _actualWidth;
            }
        }

        public event EventHandler<ActualWidthChangedEventArg>? ActualWidthChanged;

        private double ComputeBestColumnWidth()
        {
            var bestWidth = 0d;
            var treeGrid = TreeGrid;
            if (treeGrid == null)
                return bestWidth;

            foreach (var row in treeGrid.Rows)
            {
                var cell = row.Cells.GetCell(this);
                if (cell == null)
                    continue;

                //cell.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                bestWidth = Math.Max(bestWidth, cell.DesiredSize.Width);
            }

            Trace.WriteLine("ComputeBestColumnWidth");

            return bestWidth;
        }

        public double Width
        {
            get => (double)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        public DataTemplate? CellTemplate
        {
            get => (DataTemplate)GetValue(CellTemplateProperty);
            set => SetValue(CellTemplateProperty, value);
        }

        public TreeGrid? TreeGrid { get; internal set; }


        public void AutoSize()
        {

            InvalidateActualWidth();
            //Width = width;

        }


        public void UpdateWidth()
        {
            AutoSize();
        }

        public void InvalidateActualWidth()
        {
            _actualWidthInvalidated = true;
        }

        protected virtual void NotifyActualWidthChanged(double oldWidth, double newWidth)
        {
            ActualWidthChanged?.Invoke(this, new ActualWidthChangedEventArg(oldWidth, newWidth));    
        }
    }

    public class ActualWidthChangedEventArg
    {
        public double OldWidth { get; }
        public double NewWidth { get; }

        public ActualWidthChangedEventArg(double oldWidth, double newWidth)
        {
            OldWidth = oldWidth;
            NewWidth = newWidth;
        }
    }
}