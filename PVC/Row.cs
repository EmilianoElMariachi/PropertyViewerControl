using System.Windows;
using System.Windows.Controls;

namespace PVC
{
    public class Row : Control
    {



        static Row()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Row), new FrameworkPropertyMetadata(typeof(Row)));
        }


        public static readonly DependencyProperty NameCellProperty = DependencyProperty.Register(
            "NameCell", typeof(UIElement), typeof(RowPanel), new PropertyMetadata(default(UIElement)));

        public UIElement NameCell
        {
            get => (UIElement)GetValue(NameCellProperty);
            set => SetValue(NameCellProperty, value);
        }

        public static readonly DependencyProperty ValueCellProperty = DependencyProperty.Register(
            "ValueCell", typeof(UIElement), typeof(RowPanel), new PropertyMetadata(default(UIElement)));

        public UIElement ValueCell
        {
            get => (UIElement)GetValue(ValueCellProperty);
            set => SetValue(ValueCellProperty, value);
        }

        private PropertyEditor? _propertyEditor;

        public PropertyEditor? PropertyEditor
        {
            get
            {
                if (_propertyEditor != null)
                    return _propertyEditor;

                var templatedParentTmp = TemplatedParent;
                while (templatedParentTmp != null)
                {
                    if (templatedParentTmp is PropertyEditor propertyEditor)
                    {
                        _propertyEditor = propertyEditor;
                        break;
                    }
                    if (templatedParentTmp is FrameworkElement frameworkElement)
                        templatedParentTmp = frameworkElement.TemplatedParent;
                    else
                        break;
                }

                return _propertyEditor;
            }

        }









        // Override the default Measure method of Panel
        protected override Size MeasureOverride(Size availableSize)
        {
            Size panelDesiredSize = new Size();

            // In our example, we just have one child.
            // Report that our panel requires just the size of its only child.
            foreach (UIElement child in InternalChildren)
            {

                child.Measure(availableSize);
                panelDesiredSize = child.DesiredSize;
            }


            return panelDesiredSize;
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                double x = 50;
                double y = 50;

                child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
            }
            return finalSize; // Returns the final Arranged size
        }
    }
}