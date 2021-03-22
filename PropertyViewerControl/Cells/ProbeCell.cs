using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PropertyViewerControl.Rows;

namespace PropertyViewerControl.Cells
{
    public abstract class ProbeCell : Control
    {
        private readonly PropertyViewer _propertyViewer;

        public ProbeCell(PropertyViewer propertyViewer)
        {
            _propertyViewer = propertyViewer ?? throw new ArgumentNullException(nameof(propertyViewer));
        }

        protected override Size MeasureOverride(Size constraint)
        {

            static double GetBestCellWidth(UIElement cell)
            {
                cell.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                return cell.DesiredSize.Width;
            }

            var width = GetBestCellWidth(GetHeaderRowCell(_propertyViewer.HeaderRow));

            var remainingRows = new List<Row>();
            remainingRows.AddRange(_propertyViewer.Rows);

            while (remainingRows.Count > 0)
            {
                var row = remainingRows[0];
                remainingRows.RemoveAt(0);

                if (row.IsExpanded)
                    remainingRows.AddRange(row.Children);

                var cell = GetRowCell(row);

                width = Math.Max(GetBestCellWidth(cell), width);
            }

            return new Size(width, 0);

        }

        protected abstract CellBase GetRowCell(Row row);

        protected abstract CellBase GetHeaderRowCell(HeaderRow row);

    }

    public class NameProbeCell : ProbeCell
    {
        static NameProbeCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NameProbeCell), new FrameworkPropertyMetadata(typeof(NameProbeCell)));
        }

        public NameProbeCell(PropertyViewer propertyViewer) : base(propertyViewer)
        {
        }

        protected override CellBase GetRowCell(Row row)
        {
            return row.NameCell;
        }

        protected override CellBase GetHeaderRowCell(HeaderRow row)
        {
            return row.NameHeaderCell;
        }
    }

    public class ValueProbeCell : ProbeCell
    {
        public ValueProbeCell(PropertyViewer propertyViewer) : base(propertyViewer)
        {
        }

        protected override CellBase GetRowCell(Row row)
        {
            return row.ValueCell;
        }

        protected override CellBase GetHeaderRowCell(HeaderRow row)
        {
            return row.ValueHeaderCell;
        }
    }
}
