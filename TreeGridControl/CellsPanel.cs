using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace TreeGridControl
{
    public class CellsPanel : Panel
    {

        public static readonly DependencyProperty CellsProperty = DependencyProperty.Register(
            "Cells", typeof(CellCollection), typeof(CellsPanel), new PropertyMetadata(default(CellCollection), OnCellsPropertyChanged));

        private Row? _row;

        private static void OnCellsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // TODO: listen when collection changes
            ((CellsPanel)d).InitFromCells();
        }

        public CellCollection? Cells
        {
            get => (CellCollection)GetValue(CellsProperty);
            set => SetValue(CellsProperty, value);
        }

        public TreeGrid? TreeGrid => Row?.TreeGrid;

        public Row? Row
        {
            get { return _row ??= FindRow(); }
        }

        private Row? FindRow()
        {
            var templatedParentTemp = TemplatedParent;
            while (templatedParentTemp != null)
            {
                if (templatedParentTemp is Row row)
                    return row;

                templatedParentTemp = (templatedParentTemp as FrameworkElement)?.TemplatedParent;
            }

            return null;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var cells = Cells;
            if (cells == null)
                return Size.Empty;

            TreeGrid?.InvalidateMeasure();

            var desiredWidth = 0.0;
            var desiredHeight = 0.0;

            foreach (var cell in cells)
            {
                var colActualWidth = cell.Column.ActualWidth;
                var availableWidth =/* (cell.Column.Width.GridUnitType == GridUnitType.Auto) ? double.PositiveInfinity :*/ colActualWidth;

                desiredWidth += colActualWidth;
                cell.Width = colActualWidth;
                cell.Measure(new Size(availableWidth, availableSize.Height));
                desiredHeight = Math.Max(desiredHeight, cell.DesiredSize.Height);
            }

            var desiredSize = new Size(desiredWidth, desiredHeight);

            Debug.WriteLine($"{this.GetType().Name}.{MethodBase.GetCurrentMethod().Name}={desiredSize}");

            return desiredSize;
        }


        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            base.OnChildDesiredSizeChanged(child);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = 0.0;
            var cells = Cells;
            if (cells == null)
                return finalSize;

            foreach (var cell in cells)
            {
                var columnActualWidth = cell.Column.ActualWidth;

                var finalRect = new Rect(x, 0, columnActualWidth, finalSize.Height);
                cell.Arrange(finalRect);

                x += columnActualWidth;
            }

            return finalSize;
        }

        private void InitFromCells()
        {
            this.Children.Clear();

            var cells = Cells;
            if (cells == null)
                return;

            foreach (var cell in cells)
            {
                cell.Column.ActualWidthChanged += OnColumnActualWidthChanged;
                
                this.Children.Add(cell);
            }
            this.TreeGrid?.InvalidateArrange();
        }

        /// <summary>
        /// This method is triggered when the <see cref="Column.ActualWidth"/> of one of the cell's column is changed.
        /// In this case, we invalidate the panel to rearrange cells accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnActualWidthChanged(object? sender, ActualWidthChangedEventArg e)
        {
            
            this.InvalidateMeasure();
        }
    }
}