using System.Windows;

namespace PropertyViewerControl.Cells
{
    public abstract class HeaderCellBase : CellBase
    {

        static HeaderCellBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderCellBase), new FrameworkPropertyMetadata(typeof(HeaderCellBase)));
        }

        protected HeaderCellBase(PropertyViewer propertyViewer) : base(propertyViewer)
        {
        }
    }
}