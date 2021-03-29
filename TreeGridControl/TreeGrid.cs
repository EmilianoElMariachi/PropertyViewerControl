using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TreeGridControl
{

    public class TreeGrid : ItemsControl
    {
        static TreeGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeGrid), new FrameworkPropertyMetadata(typeof(TreeGrid)));
        }

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(
            "Columns", typeof(ColumnCollection), typeof(TreeGrid), new PropertyMetadata(default(ColumnCollection), OnColumnsPropertyChanged));

        private static void OnColumnsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ColumnCollection)e.NewValue).TreeGrid = (TreeGrid)d;
        }

        public ColumnCollection Columns
        {
            get => (ColumnCollection)GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new Row(this, null);
        }

        public TreeGrid()
        {
            Columns = new ColumnCollection();
        }

        public IEnumerable<Row> Rows
        {
            get
            {
                var generator = this.ItemContainerGenerator;

                if (generator.Status != GeneratorStatus.ContainersGenerated)
                    yield break;

                foreach (var item in generator.Items)
                {
                    var containerFromItem = generator.ContainerFromItem(item);
                    var row = containerFromItem as Row;
                    if (row == null)
                        continue;
                    yield return row;
                }
            }
        }


    }
}
