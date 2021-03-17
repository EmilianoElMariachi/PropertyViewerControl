using System.Windows;

namespace PropertyViewerControl.Cells
{
    public class ValueHeaderCell : HeaderCellBase
    {
        static ValueHeaderCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ValueHeaderCell), new FrameworkPropertyMetadata(typeof(ValueHeaderCell)));
        }

        public ValueHeaderCell(PropertyViewer propertyViewer) : base(propertyViewer)
        {
            this.Content = "Value";
        }
    }
}