using System.Windows;

namespace PropertyViewerControl.Cells
{
    public class ValueCell : CellBase
    {
        static ValueCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ValueCell), new FrameworkPropertyMetadata(typeof(ValueCell)));
        }

        public ValueCell(PropertyViewer propertyViewer) : base(propertyViewer)
        {
        }
    }
}