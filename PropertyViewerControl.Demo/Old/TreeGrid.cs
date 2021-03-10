using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PropertyViewerControl.Demo.Old
{
    public class TreeGrid : DataGrid
    {
        private readonly ObservableCollection<object> _rows = new();

        public static readonly DependencyProperty TreeItemsSourceProperty = DependencyProperty.Register(
            "TreeItemsSource", typeof(object), typeof(TreeGrid), new PropertyMetadata(default, OnTreeItemsSourceChanged));

        private static void OnTreeItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TreeGrid)d).UpdateRows();
        }

        public TreeGrid()
        {
            AutoGenerateColumns = false;
            ItemsSource = _rows;
        }

        private void UpdateRows()
        {
            _rows.Clear();

            var enumerable = TreeItemsSource as IEnumerable;
            if (enumerable == null)
                return;

            var remainingRows = enumerable.Cast<object>().Select(o => new TreeGridRow(o,0)).ToList();

            while (remainingRows.Count > 0)
            {
                var remainingRow = remainingRows[0];
                _rows.Add(remainingRow);
                remainingRows.RemoveAt(0);

                var children = GetChildren(remainingRow.DataContext);
                if (children == null)
                    continue;

                var childrenRows = children.Cast<object>().Select(o => new TreeGridRow(o,remainingRow.Level + 1)).ToList();

                remainingRow.Children.AddRange(childrenRows);

                remainingRows.InsertRange(0, childrenRows);
            }

        }

        private IEnumerable? GetChildren(object? item)
        {
            if (item is IItem i2)
                return i2.Children;
            return null;
        }


        public object TreeItemsSource
        {
            get => GetValue(TreeItemsSourceProperty);
            set => SetValue(TreeItemsSourceProperty, value);
        }

    }
}

