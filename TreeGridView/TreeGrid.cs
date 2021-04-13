using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TreeGridView
{

    public class TreeGrid : ItemsControl
    {
        static TreeGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeGrid), new FrameworkPropertyMetadata(typeof(TreeGrid)));
        }

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(
            "Columns", typeof(ColumnCollection), typeof(TreeGrid), new PropertyMetadata(default(ColumnCollection)));

        public ColumnCollection Columns
        {
            get => (ColumnCollection) GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }


        public TreeGrid()
        {
            Columns = new ColumnCollection();
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var row = new Row();
            return row;
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

    public class ColumnCollection : ObservableCollection<Column>
    {
    }

    public class Column : DependencyObject
    {
        public static readonly DependencyProperty WidthProperty = DependencyProperty.Register(
            "Width", typeof(GridLength), typeof(Column), new PropertyMetadata(default(GridLength)));

        public GridLength Width
        {
            get => (GridLength) GetValue(WidthProperty);
            set => SetValue(WidthProperty, value);
        }

        public static readonly DependencyProperty CellTemplateProperty = DependencyProperty.Register(
            "CellTemplate", typeof(DataTemplate), typeof(Column), new PropertyMetadata(default(DataTemplate)));

        public DataTemplate CellTemplate
        {
            get => (DataTemplate) GetValue(CellTemplateProperty);
            set => SetValue(CellTemplateProperty, value);
        }
    }

    public class Row : Control
    {
        private TreeGrid? _treeGrid;

        static Row()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Row), new FrameworkPropertyMetadata(typeof(Row)));
        }

        public TreeGrid? TreeGrid
        {
            get { return _treeGrid ??= FindTreeGrid(); }
        }


        private TreeGrid? FindTreeGrid()
        {
            var templatedParentTemp = TemplatedParent;
            while (templatedParentTemp != null)
            {
                if (templatedParentTemp is TreeGrid treeGrid)
                    return treeGrid;

                templatedParentTemp = (templatedParentTemp as FrameworkElement)?.TemplatedParent;
            }

            return null;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        
    }

    public class CellsPanel : Panel
    {
        private Row? _row;

        public TreeGrid? TreeGrid => Row?.TreeGrid;

        public Row? Row
        {
            get { return _row ??= FindRow(); }
        }

        private Row? FindRow()
        {
            var templatedParentTemp = TemplatedParent;
            while (templatedParentTemp != null)
            {
                if (templatedParentTemp is Row row)
                    return row;

                templatedParentTemp = (templatedParentTemp as FrameworkElement)?.TemplatedParent;
            }

            return null;
        }




    }
}
