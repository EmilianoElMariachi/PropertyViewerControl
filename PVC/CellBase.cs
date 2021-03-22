using System;
using System.Windows;
using System.Windows.Controls;

namespace PVC
{
    public class CellBase : Control
    {
        public static readonly DependencyProperty AttachedPanelProperty = DependencyProperty.Register(
            "AttachedPanel", typeof(RowPanel), typeof(CellBase), new PropertyMetadata(default(RowPanel)));

        public RowPanel AttachedPanel
        {
            get => (RowPanel) GetValue(AttachedPanelProperty);
            private init => SetValue(AttachedPanelProperty, value);
        }

        public CellBase(RowPanel rowPanel)
        {
            AttachedPanel = rowPanel ?? throw new ArgumentNullException(nameof(rowPanel));
        }
    }
}