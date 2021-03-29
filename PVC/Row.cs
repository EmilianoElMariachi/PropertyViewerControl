using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PVC
{
    public class Row : Panel
    {

        public static readonly DependencyProperty NameCellProperty = DependencyProperty.Register(
            "NameCell", typeof(NameCell), typeof(Row), new PropertyMetadata(default(NameCell)));

        public NameCell NameCell
        {
            get => (NameCell)GetValue(NameCellProperty);
            private init => SetValue(NameCellProperty, value);
        }

        public static readonly DependencyProperty ValueCellProperty = DependencyProperty.Register(
            "ValueCell", typeof(ValueCell), typeof(Row), new PropertyMetadata(default(ValueCell)));

        public ValueCell ValueCell
        {
            get => (ValueCell)GetValue(ValueCellProperty);
            private init => SetValue(ValueCellProperty, value);
        }

        public static readonly DependencyProperty CellProperty = DependencyProperty.Register(
            "Cell", typeof(Cell), typeof(Row), new PropertyMetadata(default(Cell)));

        public Cell SplitterCell
        {
            get => (Cell)GetValue(CellProperty);
            private init => SetValue(CellProperty, value);
        }

        public Row(PropertyEditor propertyEditor)
        {
            PropertyEditor = propertyEditor ?? throw new ArgumentNullException(nameof(propertyEditor));
            NameCell = new NameCell(this, propertyEditor.NameColumn);
            SplitterCell = new Cell(this, propertyEditor.SplitterColumn);
            ValueCell = new ValueCell(this, propertyEditor.ValueColumn);
            this.Children.Add(NameCell);
            this.Children.Add(SplitterCell);
            this.Children.Add(ValueCell);


            propertyEditor.NameColumn.SizeChanged += OnSizeChanged;
            propertyEditor.ValueColumn.SizeChanged += OnSizeChanged;
            propertyEditor.SplitterColumn.SizeChanged += OnSizeChanged;

        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //this.InvalidateArrange();
        }

        public PropertyEditor PropertyEditor { get; }

        public IEnumerable<Cell> Cells => this.InternalChildren.OfType<Cell>();

        public Cell? GetCell(Column column)
        {
            foreach (var cell in Cells)
            {
                if (cell.AttachedColumn == column)
                    return cell;
            }

            return null;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var measureOverride = base.MeasureOverride(availableSize);

            var width = 0.0;
            var height = 0.0;
            foreach (var cell in Cells)
            {
                width += cell.AttachedColumn.ActualWidth;
                height = Math.Max(height, cell.DesiredSize.Height);
            }

            return new Size(width, height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = 0.0;
            foreach (var cell in Cells)
            {
                cell.Arrange(new Rect(x, 0, cell.AttachedColumn.ActualWidth, cell.DesiredSize.Height));
                x += cell.AttachedColumn.ActualWidth;
            }

            return finalSize;//Rect.Union(nameRect, valueRect).Size; // Returns the final Arranged size
        }


    }
}