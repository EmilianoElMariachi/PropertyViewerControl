using System.Windows;
using PropertyViewerControl.Cells;

namespace PropertyViewerControl.Rows
{
    public class HeaderRow : RowBase
    {
        static HeaderRow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderRow), new FrameworkPropertyMetadata(typeof(HeaderRow)));
        }

        public static readonly DependencyProperty NameHeaderCellProperty = DependencyProperty.Register(
            "NameHeaderCell", typeof(NameHeaderCell), typeof(HeaderRow), new PropertyMetadata(default(NameHeaderCell)));

        public NameHeaderCell NameHeaderCell
        {
            get => (NameHeaderCell)GetValue(NameHeaderCellProperty);
            private init => SetValue(NameHeaderCellProperty, value);
        }

        public static readonly DependencyProperty ValueHeaderCellProperty = DependencyProperty.Register(
            "ValueHeaderCell", typeof(ValueHeaderCell), typeof(HeaderRow), new PropertyMetadata(default(ValueHeaderCell)));

        public ValueHeaderCell ValueHeaderCell
        {
            get => (ValueHeaderCell)GetValue(ValueHeaderCellProperty);
            private init => SetValue(ValueHeaderCellProperty, value);
        }

        public HeaderRow(PropertyViewer propertyViewer) : base(propertyViewer)
        {
            NameHeaderCell = new NameHeaderCell(propertyViewer);
            ValueHeaderCell = new ValueHeaderCell(propertyViewer);
        }

    }
}