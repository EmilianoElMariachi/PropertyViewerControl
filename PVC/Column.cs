using System;
using System.Windows;

namespace PVC
{

    
    public class Column : FrameworkElement
    {
        public PropertyEditor PropertyEditor { get; }

        public static readonly DependencyProperty WidthDefProperty = DependencyProperty.Register(
            "WidthDef", typeof(GridLength), typeof(Column), new PropertyMetadata(default(GridLength)));

        public GridLength WidthDef
        {
            get => (GridLength) GetValue(WidthDefProperty);
            set => SetValue(WidthDefProperty, value);
        }

        public Column(PropertyEditor propertyEditor)
        {
            PropertyEditor = propertyEditor;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var desiredWidth = 0.0;

            foreach (var row in PropertyEditor.Rows)
            {
                var cell = row.GetCell(this);
                if (cell == null)
                    continue;

                cell.Measure(constraint);

                desiredWidth = Math.Max(desiredWidth, cell.DesiredSize.Width);
            }
            return new Size(desiredWidth, 1);
        }

    }

}