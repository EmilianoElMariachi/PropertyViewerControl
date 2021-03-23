using System;
using System.Windows;
using System.Windows.Controls;

namespace PVC
{
    public class CellBase : Control
    {
        public static readonly DependencyProperty AttachedRowProperty = DependencyProperty.Register(
            "AttachedRow", typeof(Row), typeof(CellBase), new PropertyMetadata(default(Row)));

        public Row AttachedRow
        {
            get => (Row) GetValue(AttachedRowProperty);
            private init => SetValue(AttachedRowProperty, value);
        }

        public CellBase(Row row)
        {
            AttachedRow = row ?? throw new ArgumentNullException(nameof(row));
        }
    }
}