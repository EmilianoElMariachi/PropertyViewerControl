using System;
using System.Diagnostics;
using System.Windows;

namespace TreeGridControl
{
    public class Column : DependencyObject
    {

        public static readonly DependencyProperty WidthProperty;
        public static readonly DependencyProperty CellTemplateProperty;
        private bool _actualWidthInvalidated = true;
        private double _actualWidth;
        private bool _isUpdatingActualWidth;

        static Column()
        {
            CellTemplateProperty = DependencyProperty.Register("CellTemplate", typeof(DataTemplate), typeof(Column), new PropertyMetadata(default(DataTemplate)));

            WidthProperty = DependencyProperty.Register("Width", typeof(GridLength), typeof(Column), new PropertyMetadata(default(GridLength), OnWidthPropertyChanged)
            {
                CoerceValueCallback = CoerceWidthProperty,
            });
        }

        private static void OnWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Column)d).UpdateActualWidth();
        }

        private static object CoerceWidthProperty(DependencyObject d, object baseValue)
        {
            var baseGridLength = (GridLength)baseValue;

            if (baseGridLength.Value >= 0)
                return baseGridLength;

            return new GridLength(0, baseGridLength.GridUnitType);
        }

        public double ActualWidth
        {
            get
            {
                if (_actualWidthInvalidated)
                {
                    UpdateActualWidth();
                }

                return _actualWidth;
            }
        }

        public event EventHandler<ActualWidthChangedEventArg>? ActualWidthChanged;


        private void UpdateActualWidth()
        {
            try
            {
                _isUpdatingActualWidth = true;
                _actualWidthInvalidated = false;

                var oldWidth = _actualWidth;
                double newWidth;
                var width = Width;
                if (width.GridUnitType == GridUnitType.Auto)
                {
                    newWidth = ComputeBestColumnWidth();
                }
                else if (width.GridUnitType == GridUnitType.Pixel)
                {
                    newWidth = width.Value;
                }
                else
                {
                    return; //TODO: à implémenter
                }

                _actualWidth = newWidth;
                Trace.WriteLine($"New Column Width {TreeGrid?.Columns.IndexOf(this)}: {_actualWidth}");
                NotifyActualWidthChanged(oldWidth, newWidth);
            }
            finally
            {
                _isUpdatingActualWidth = false;
            }
        }

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

        public GridLength Width
        {
            get => (GridLength)GetValue(WidthProperty);
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
            this.Width = GridLength.Auto;
        }

        public void InvalidateActualWidth()
        {
            if (!_isUpdatingActualWidth)
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