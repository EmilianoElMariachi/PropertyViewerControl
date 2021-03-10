using System.Windows;

namespace PropertyViewerControl.Cells
{
    public class PropertyViewerCellName : PropertyViewerCell
    {
        static PropertyViewerCellName()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyViewerCellName), new FrameworkPropertyMetadata(typeof(PropertyViewerCellName)));
        }
    }
}