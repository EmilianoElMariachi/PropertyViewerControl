using System;
using System.Windows;

namespace TreeGridControl
{
    public class Column : DependencyObject
    {

        public static readonly DependencyProperty WidthProperty;
        public static readonly DependencyProperty CellTemplateProperty;
        private double _actualWidth;

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
            ((Column)d).TreeGrid?.InvalidateMeasure();
        }

        private static object CoerceWidthProperty(DependencyObject d, object baseValue)
        {
            var baseGridLength = (GridLength)baseValue;

            if (baseGridLength.Value >= 0)
                return baseGridLength;

            return new GridLength(0, baseGridLength.GridUnitType);
        }

        public double ActualWidth => _actualWidth;

        public event EventHandler<ActualWidthChangedEventArg>? ActualWidthChanged;

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

        protected virtual void NotifyActualWidthChanged(double oldWidth, double newWidth)
        {
            ActualWidthChanged?.Invoke(this, new ActualWidthChangedEventArg(oldWidth, newWidth));
        }

        public void SetActualWidth(double actualWidth)
        {
            if (_actualWidth == actualWidth)
                return;

            var oldWidth = _actualWidth;
            _actualWidth = actualWidth;
            NotifyActualWidthChanged(oldWidth, _actualWidth);
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