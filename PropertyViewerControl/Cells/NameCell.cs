using System.Windows;

namespace PropertyViewerControl.Cells
{
    public class NameCell : CellBase
    {
        static NameCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NameCell), new FrameworkPropertyMetadata(typeof(NameCell)));
        }
    }
}