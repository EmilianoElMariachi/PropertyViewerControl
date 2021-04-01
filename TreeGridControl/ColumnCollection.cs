using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TreeGridControl
{
    public class ColumnCollection : ObservableCollection<Column>
    {
        public ColumnCollection()
        {
            ColumnWidthManager = new ColumnWidthManager(this);
        }

        public TreeGrid? TreeGrid { get; internal set; }

        public ColumnWidthManager ColumnWidthManager { get; }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            var newItems = e.NewItems;
            if (newItems != null)
            {
                foreach (var newItem in newItems)
                {
                    ((Column) newItem).TreeGrid = TreeGrid;
                }
            }
        }


    }
}