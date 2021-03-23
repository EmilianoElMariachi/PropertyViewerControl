using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PVC
{
    public class Row : Panel
    {

        public NameCell NameCell { get; }

        public ValueCell ValueCell { get; }

        public Row(PropertyEditor propertyEditor)
        {
            PropertyEditor = propertyEditor ?? throw new ArgumentNullException(nameof(propertyEditor));
            NameCell = new NameCell(this);
            ValueCell = new ValueCell(this);

            //NameCell.SetBinding(NameCell.WidthProperty, new Binding(nameof(NameColumn.Width))
            //{
            //    Mode = BindingMode.OneWay,
            //    Source = PropertyEditor.NameColumn
            //});

            //ValueCell.SetBinding(ValueCell.WidthProperty, new Binding(nameof(ValueColumn.Width))
            //{
            //    Mode = BindingMode.OneWay,
            //    Source = PropertyEditor.ValueColumn
            //});

            this.Children.Add(NameCell);
            this.Children.Add(ValueCell);

        }



        public PropertyEditor PropertyEditor { get; }


        // Override the default Measure method of Panel
        protected override Size MeasureOverride(Size availableSize)
        {
            var propertyEditor = PropertyEditor;


            propertyEditor.NameColumn.UpdateWidth();
            propertyEditor.ValueColumn.UpdateWidth();


            var width = propertyEditor.NameColumn.Width + propertyEditor.SplitterColumn.Width + propertyEditor.ValueColumn.Width;

            var desiredRowHeight = GetDesiredRowHeight();
            return new Size(width, desiredRowHeight);
        }
        protected override Size ArrangeOverride(Size finalSize)
        {

            var propertyEditor = PropertyEditor;

            NameCell.Arrange(new Rect(new Point(0, 0), new Size(propertyEditor.NameColumn.Width, NameCell.DesiredSize.Height)));

            ValueCell.Arrange(new Rect(new Point(propertyEditor.NameColumn.Width + propertyEditor.SplitterColumn.Width, 0), new Size(propertyEditor.ValueColumn.Width, ValueCell.DesiredSize.Height)));

            return finalSize; // Returns the final Arranged size
        }

        private double GetDesiredRowHeight()
        {
            //child.Measure(availableSize);
            //var desiredSize = child.DesiredSize;
            return Math.Max(NameCell.DesiredSize.Height, ValueCell.DesiredSize.Height);
        }
    }
}