using System.Windows;

namespace PVC
{
    public class ValueCell : Cell
    {
        static ValueCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ValueCell), new FrameworkPropertyMetadata(typeof(ValueCell)));
        }

        public ValueCell(Row row, Column attachedColumn) : base(row, attachedColumn)
        {
        }
    }
}