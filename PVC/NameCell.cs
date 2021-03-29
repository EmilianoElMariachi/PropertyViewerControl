using System.Windows;

namespace PVC
{
    public class NameCell : Cell
    {
        static NameCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NameCell), new FrameworkPropertyMetadata(typeof(NameCell)));
        }

        public NameCell(Row row, Column attachedColumn) : base(row, attachedColumn)
        {
        }
    }
}