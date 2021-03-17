using System.Windows;

namespace PropertyViewerControl.Cells
{
    public class NameHeaderCell : HeaderCellBase
    {
        static NameHeaderCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NameHeaderCell), new FrameworkPropertyMetadata(typeof(NameHeaderCell)));
        }


        public NameHeaderCell(PropertyViewer propertyViewer) : base(propertyViewer)
        {
            this.Content = "Name";
        }

    }
}