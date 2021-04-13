using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace TreeGridControl
{
    public class ColumnWidthManager
    {
        private bool _isUpdating;
        public ColumnCollection Columns { get; }

        public ColumnWidthManager(ColumnCollection columnCollection)
        {
            Columns = columnCollection ?? throw new ArgumentNullException(nameof(columnCollection));
        }

        public void Update(Size constraint)
        {
            try
            {
                if(_isUpdating)
                    return;

                _isUpdating = true;

                Debug.WriteLine($"{this.GetType().Name}.{MethodBase.GetCurrentMethod().Name}");

                var treeGrid = Columns.TreeGrid;
                if (treeGrid == null)
                    return;

                var availableWidth = constraint.Width; //TODO: prendre le conteneur des rows et non le TreeGrid lui même

                var rows = treeGrid.Rows.ToArray();


                UpdateAutoColumns(Columns, rows, out var remainingColumns, out var totalAutoWidth);

                UpdateAbsoluteColumns(remainingColumns, rows, out remainingColumns, out var totalFixedWidth);

                var remainingAvailableWidth = availableWidth - (totalAutoWidth + totalFixedWidth);

                UpdateStarColumns(remainingColumns, rows, out remainingColumns, remainingAvailableWidth);

                if (remainingColumns.Count > 0)
                {
                    //TODO: faire le message d'erreur
                }
            }
            finally
            {
                _isUpdating = false;
            }


        }

        private void UpdateAutoColumns(IEnumerable<Column> columns, Row[] rows, out List<Column> remainingColumns, out double totalAutoWidth)
        {
            remainingColumns = new List<Column>();
            totalAutoWidth = 0;
            foreach (var column in columns)
            {
                if (!column.Width.IsAuto)
                {
                    remainingColumns.Add(column);
                    continue;
                }

                var actualWidth = ComputeColumnAutoWidth(column, rows);
                totalAutoWidth += actualWidth;
                SetColumnActualWidth(column, actualWidth);
            }
        }


        private void UpdateAbsoluteColumns(IEnumerable<Column> columns, Row[] rows, out List<Column> remainingColumns, out double totalFixedWidth)
        {
            remainingColumns = new List<Column>();
            totalFixedWidth = 0;
            foreach (var column in columns)
            {
                var columnWidth = column.Width;
                if (!columnWidth.IsAbsolute)
                {
                    remainingColumns.Add(column);
                    continue;
                }

                var actualWidth = columnWidth.Value;
                totalFixedWidth += actualWidth;
                SetColumnActualWidth(column, actualWidth);
            }
        }

        private void UpdateStarColumns(List<Column> columns, Row[] rows, out List<Column> remainingColumns, double remainingAvailableWidth)
        {
            remainingColumns = new List<Column>();
            var starColumns = new List<Column>();
            var totalWeights = 0.0;

            foreach (var column in columns)
            {
                if (!column.Width.IsStar)
                {
                    remainingColumns.Add(column);
                    continue;
                }
                starColumns.Add(column);
                totalWeights += column.Width.Value;
            }

            if (remainingAvailableWidth <= 0 || totalWeights <= 0)
            {
                foreach (var starColumn in starColumns)
                {
                    SetColumnActualWidth(starColumn, 0);
                }
                return;
            }



            var weightWidth = remainingAvailableWidth / totalWeights;

            foreach (var starColumn in starColumns)
            {
                var weight = starColumn.Width.Value;
                var actualWidth = weight * weightWidth;


                SetColumnActualWidth(starColumn, actualWidth);
            }
        }


        private void SetColumnActualWidth(Column column, double actualWidth)
        {
            Debug.WriteLine($"{this.GetType().Name}.{MethodBase.GetCurrentMethod().Name}={actualWidth}");
            column.SetActualWidth(actualWidth);
        }

        private double ComputeColumnAutoWidth(Column column, IEnumerable<Row> rows)
        {
            var bestWidth = 0d;

            foreach (var row in rows)
            {
                var cell = row.Cells.GetCell(column);
                if (cell == null)
                    continue;

                cell.Measure(new Size(double.PositiveInfinity, cell.DesiredSize.Height));
                bestWidth = Math.Max(bestWidth, cell.DesiredSize.Width);
            }

            return bestWidth;
        }

    }
}
