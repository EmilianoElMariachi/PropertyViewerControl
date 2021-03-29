using System.Collections.ObjectModel;

namespace TreeGridControl
{
    public class CellCollection : ObservableCollection<Cell>
    {
        public Cell? GetCell(Column column)
        {

            foreach (var cell in this)
            {
                if (cell.Column == column)
                    return cell;
            }

            return null;
        }
    }
}