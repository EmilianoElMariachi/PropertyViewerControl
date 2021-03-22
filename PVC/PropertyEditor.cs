using System;
using System.Windows;
using System.Windows.Controls;

namespace PVC
{

    public class PropertyEditor : ItemsControl
    {
        static PropertyEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyEditor), new FrameworkPropertyMetadata(typeof(PropertyEditor)));
        }

        public static readonly DependencyProperty NameColumnProperty = DependencyProperty.Register(
            "NameColumn", typeof(Column), typeof(PropertyEditor), new PropertyMetadata(default(Column), OnColumnChanged));

        private static void OnColumnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                throw new ArgumentNullException(e.Property.Name);
        }

        public Column NameColumn
        {
            get => (Column) GetValue(NameColumnProperty);
            set => SetValue(NameColumnProperty, value);
        }


        public static readonly DependencyProperty SplitterColumnProperty = DependencyProperty.Register(
            "SplitterColumn", typeof(Column), typeof(PropertyEditor), new PropertyMetadata(default(Column), OnColumnChanged));

        public Column SplitterColumn
        {
            get => (Column) GetValue(SplitterColumnProperty);
            set => SetValue(SplitterColumnProperty, value);
        }

        public static readonly DependencyProperty ValueColumnProperty = DependencyProperty.Register(
            "ValueColumn", typeof(Column), typeof(PropertyEditor), new PropertyMetadata(default(Column), OnColumnChanged));

        public Column ValueColumn
        {
            get => (Column) GetValue(ValueColumnProperty);
            set => SetValue(ValueColumnProperty, value);
        }

        public PropertyEditor()
        {
            NameColumn = new Column();
            SplitterColumn = new Column();
            ValueColumn = new Column();

            //new DataGrid()
        }

        /// <summary>
        ///     Determines if an item is its own container.
        /// </summary>
        /// <param name="item">The item to test.</param>
        /// <returns>true if the item is a DataGridRow, false otherwise.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is Row;
        }

        /// <summary>
        ///     Instantiates an instance of a container.
        /// </summary>
        /// <returns>A new DataGridRow.</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new Row();
        }

        /// <summary>
        ///     Prepares a new container for a given item.
        /// </summary>
        /// <param name="element">The new container.</param>
        /// <param name="item">The item that the container represents.</param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            //DataGridRow row = (DataGridRow)element;
            //if (row.DataGridOwner != this)
            //{
            //    row.Tracker.StartTracking(ref _rowTrackingRoot);
            //    if (item == CollectionView.NewItemPlaceholder ||
            //        (IsAddingNewItem && item == EditableItems.CurrentAddItem))
            //    {
            //        row.IsNewItem = true;
            //    }
            //    else
            //    {
            //        row.ClearValue(DataGridRow.IsNewItemPropertyKey);
            //    }
            //    EnsureInternalScrollControls();
            //    EnqueueNewItemMarginComputation();
            //}

            //row.PrepareRow(item, this);
            //OnLoadingRow(new DataGridRowEventArgs(row));
        }

        /// <summary>
        ///     Clears a container of references.
        /// </summary>
        /// <param name="element">The container being cleared.</param>
        /// <param name="item">The data item that the container represented.</param>
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);

            //DataGridRow row = (DataGridRow)element;
            //if (row.DataGridOwner == this)
            //{
            //    row.Tracker.StopTracking(ref _rowTrackingRoot);
            //    row.ClearValue(DataGridRow.IsNewItemPropertyKey);
            //    EnqueueNewItemMarginComputation();
            //}

            //OnUnloadingRow(new DataGridRowEventArgs(row));
            //row.ClearRow(this);
        }
    }
}
