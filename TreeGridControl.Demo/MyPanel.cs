using System.Windows;
using System.Windows.Controls;

namespace TreeGridControl.Demo
{
    class MyPanel : Panel
    {

        protected override Size MeasureOverride(Size availableSize)
        {
            var internalChild = this.InternalChildren[0];

            internalChild.Measure(availableSize);

            return internalChild.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var internalChild = this.InternalChildren[0];

            internalChild.Arrange(new Rect(0,0, internalChild.DesiredSize.Width, internalChild.DesiredSize.Height));

            return finalSize;
        }
    }
}
