using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace PVC
{
    public class Column : DependencyObject
    {
        public PropertyEditor PropertyEditor { get; }

        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            "Width", typeof(double), typeof(Column), new PropertyMetadata(default(double)));

        public double Width
        {
            get => (double)GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }


        public Column(PropertyEditor propertyEditor)
        {
            PropertyEditor = propertyEditor;
        }

    }

    public abstract class CellBaseColumn : Column
    {
        public CellBaseColumn(PropertyEditor propertyEditor) : base(propertyEditor)
        {
        }



        public void UpdateWidth()
        {
            Width = ComputeWidth();
        }

        private double ComputeWidth()
        {
            var generator = PropertyEditor.ItemContainerGenerator;

            if (generator.Status != GeneratorStatus.ContainersGenerated)
                return 0;

            var width = 0.0;

            foreach (var item in generator.Items)
            {
                var containerFromItem = generator.ContainerFromItem(item);
                if (!(containerFromItem is Row row))
                    continue;

                //row.InvalidateArrange();

                var cell = GetCell(row);

                //var cell = row.NameCell;

                //var childrenCount = VisualTreeHelper.GetChildrenCount(cell);
                //if (childrenCount > 0)
                //{
                //    var dependencyObject = VisualTreeHelper.GetChild(cell, 0);
                //}

                cell.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                width = Math.Max(width, cell.DesiredSize.Width);
            }

            return width;
        }

        protected abstract CellBase GetCell(Row row);
    }


    public class NameColumn : CellBaseColumn
    {
        public NameColumn(PropertyEditor propertyEditor) : base(propertyEditor)
        {
        }


        protected override CellBase GetCell(Row row)
        {
            return row.NameCell;
        }
    }

    public class ValueColumn : CellBaseColumn
    {
        public ValueColumn(PropertyEditor propertyEditor) : base(propertyEditor)
        {
        }

        protected override CellBase GetCell(Row row)
        {
            return row.ValueCell;
        }
    }

}