using System.Windows;

namespace PVC
{
    public class ValueCell : CellBase
    {
        static ValueCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ValueCell), new FrameworkPropertyMetadata(typeof(ValueCell)));
        }

        public ValueCell(RowPanel rowPanel) : base(rowPanel)
        {
        }
    }
}