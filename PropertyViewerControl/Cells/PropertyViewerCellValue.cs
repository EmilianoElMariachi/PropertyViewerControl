using System.Windows;

namespace PropertyViewerControl.Cells
{
    public class PropertyViewerCellValue : PropertyViewerCell
    {
        static PropertyViewerCellValue()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyViewerCellValue), new FrameworkPropertyMetadata(typeof(PropertyViewerCellValue)));
        }
    }
}