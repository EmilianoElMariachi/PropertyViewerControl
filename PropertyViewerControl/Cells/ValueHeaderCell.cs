using System.Windows;

namespace PropertyViewerControl.Cells
{
    public class ValueHeaderCell : HeaderCellBase
    {
        static ValueHeaderCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ValueHeaderCell), new FrameworkPropertyMetadata(typeof(ValueHeaderCell)));
        }
    }
}