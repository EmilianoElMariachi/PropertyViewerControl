using System.Windows;
using System.Windows.Controls;
using PropertyViewerControl.Cells;

namespace PropertyViewerControl
{
    public class HeaderRow : Control
    {
        static HeaderRow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderRow), new FrameworkPropertyMetadata(typeof(HeaderRow)));
        }

        public static readonly DependencyProperty NameHeaderCellProperty = DependencyProperty.Register(
            "NameHeaderCell", typeof(NameHeaderCell), typeof(HeaderRow), new PropertyMetadata(default(NameHeaderCell)));

        public NameHeaderCell NameHeaderCell
        {
            get => (NameHeaderCell) GetValue(NameHeaderCellProperty);
            private init => SetValue(NameHeaderCellProperty, value);
        }


        public static readonly DependencyProperty ValueHeaderCellProperty = DependencyProperty.Register(
            "ValueHeaderCell", typeof(ValueHeaderCell), typeof(HeaderRow), new PropertyMetadata(default(ValueHeaderCell)));

        public ValueHeaderCell ValueHeaderCell
        {
            get => (ValueHeaderCell) GetValue(ValueHeaderCellProperty);
            private init => SetValue(ValueHeaderCellProperty, value);
        }

        public static readonly DependencyProperty PropertyViewerProperty = DependencyProperty.Register(
            "PropertyViewer", typeof(PropertyViewer), typeof(HeaderRow), new PropertyMetadata(default(PropertyViewer)));

        public PropertyViewer PropertyViewer
        {
            get => (PropertyViewer) GetValue(PropertyViewerProperty);
            set => SetValue(PropertyViewerProperty, value);
        }

        public HeaderRow()
        {
            NameHeaderCell = new NameHeaderCell();
            ValueHeaderCell = new ValueHeaderCell();
        }

    }
}