using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace TreeGridControl
{
    public class CellsPanel : Panel
    {

        public static readonly DependencyProperty CellsProperty = DependencyProperty.Register(
            "Cells", typeof(CellCollection), typeof(CellsPanel), new PropertyMetadata(default(CellCollection), OnCellsPropertyChanged));

        private TreeGrid? _treeGrid;
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

            var width = 0.0;
            var height = 0.0;

            foreach (var cell in cells)
            {
                var actualWidth = cell.Column.ActualWidth;
                width += actualWidth;
                cell.Measure(new Size(availableSize.Height, actualWidth));
                height = Math.Max(height, cell.DesiredSize.Height);
            }

            Trace.WriteLine("Measuring cells of Row");

            return new Size(width, height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = 0.0;
            var cells = Cells;
            if (cells == null)
                return finalSize;

            var i = 0;
            foreach (var cell in cells)
            {
                var columnActualWidth = cell.Column.ActualWidth;

                var finalRect = new Rect(x, 0, columnActualWidth, finalSize.Height);

                Trace.WriteLine($"C{i++}={finalRect}");

                cell.Arrange(finalRect);

                var cellRenderSize = cell.RenderSize;
                if (finalRect.Size != cellRenderSize)
                {

                }
                x += columnActualWidth;
            }

            Trace.WriteLine("Arranging cells of Row");

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
                cell.Column.InvalidateActualWidth();
                this.Children.Add(cell);
            }
        }

        private void OnColumnActualWidthChanged(object? sender, ActualWidthChangedEventArg e)
        {
            this.InvalidateArrange();
        }
    }
}