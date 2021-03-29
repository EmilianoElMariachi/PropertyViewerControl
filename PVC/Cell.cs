using System;
using System.Windows;
using System.Windows.Controls;

namespace PVC
{
    public class Cell : Control
    {
        public static readonly DependencyProperty AttachedRowProperty = DependencyProperty.Register(
            "AttachedRow", typeof(Row), typeof(Cell), new PropertyMetadata(default(Row)));

        public Row AttachedRow
        {
            get => (Row)GetValue(AttachedRowProperty);
            private init => SetValue(AttachedRowProperty, value);
        }

        public Column AttachedColumn { get; }

        public Cell(Row row, Column attachedColumn)
        {
            AttachedRow = row ?? throw new ArgumentNullException(nameof(row));
            AttachedColumn = attachedColumn ?? throw new ArgumentNullException(nameof(attachedColumn));
        }


        protected override Size MeasureOverride(Size constraint)
        {
            AttachedColumn.InvalidateMeasure();
            return base.MeasureOverride(constraint);
        }


    }
}