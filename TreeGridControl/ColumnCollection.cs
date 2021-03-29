using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace TreeGridControl
{
    public class ColumnCollection : ObservableCollection<Column>
    {
        public TreeGrid? TreeGrid { get; internal set; }

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

        public void UpdateWidth()
        {
            
        }
    }
}