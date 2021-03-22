using System;
using System.Windows;
using System.Windows.Controls;

namespace PVC
{
    public class RowPanel : Panel
    {

        private Row? _row;

        public NameCell NameCell { get; }

        public ValueCell ValueCell { get; }


        public Row? Row
        {
            get
            {
                if (_row != null)
                    return _row;

                var templatedParentTmp = TemplatedParent;
                while (templatedParentTmp != null)
                {
                    if (templatedParentTmp is Row row)
                    {
                        _row = row;
                        break;
                    }
                    if (templatedParentTmp is FrameworkElement frameworkElement)
                        templatedParentTmp = frameworkElement.TemplatedParent;
                    else
                        break;
                }

                return _row;
            }
        }

        public PropertyEditor? PropertyEditor => Row?.PropertyEditor;

        public RowPanel()
        {
            NameCell = new NameCell(this);
            ValueCell = new ValueCell(this);
            this.Children.Add(NameCell);
            this.Children.Add(ValueCell);
        }


        // Override the default Measure method of Panel
        protected override Size MeasureOverride(Size availableSize)
        {
            var propertyEditor = PropertyEditor;
            if (propertyEditor == null)
                return Size.Empty;

            var minHeight = 0.0;
            var minWidth = 0.0;


            //propertyEditor.NameColumn.


            // In our example, we just have one child.
            // Report that our panel requires just the size of its only child.
            foreach (UIElement child in InternalChildren)
            {

                child.Measure(availableSize);
                var desiredSize = child.DesiredSize;

                minHeight = Math.Max(minHeight, desiredSize.Height);
                minWidth += desiredSize.Height;
            }


            return new Size(minWidth, minHeight);
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            var x = 0.0;
            foreach (UIElement child in InternalChildren)
            {
                double y = 0;

                child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
                x += child.DesiredSize.Width;
            }
            return finalSize; // Returns the final Arranged size
        }

    }
}