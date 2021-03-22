using System.Windows;

namespace PVC
{
    public class NameCell : CellBase
    {
        static NameCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NameCell), new FrameworkPropertyMetadata(typeof(NameCell)));
        }

        public NameCell(RowPanel rowPanel) : base(rowPanel)
        {
        }
    }
}