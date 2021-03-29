using System;
using System.Windows;
using System.Windows.Controls;

namespace TreeGridControl
{
    public class Row : Control
    {
        static Row()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Row), new FrameworkPropertyMetadata(typeof(Row)));
        }


        public TreeGrid TreeGrid { get; }

        public Row? ParentRow { get; }

        public Row(TreeGrid treeGrid, Row? parentRow)
        {
            Cells = new CellCollection();

            TreeGrid = treeGrid ?? throw new ArgumentNullException(nameof(treeGrid));
            ParentRow = parentRow;

            Initialize();
        }

        private void Initialize()
        {
            Cells.Clear();
            foreach (var column in TreeGrid.Columns)
            {
                var cell = new Cell(this, column)
                {
                    Content = column.CellTemplate?.LoadContent()
                };
                Cells.Add(cell);
            }
        }


        public static readonly DependencyProperty CellsProperty = DependencyProperty.Register(
            "Cells", typeof(CellCollection), typeof(Row), new PropertyMetadata(default(CellCollection)));

        public CellCollection Cells
        {
            get => (CellCollection)GetValue(CellsProperty);
            private init => SetValue(CellsProperty, value);
        }

    }
}