using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace PropertyViewerControl.Cells
{

    [ContentProperty(nameof(Content))]
    public abstract class CellBase : Control
    {
        static CellBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CellBase), new FrameworkPropertyMetadata(typeof(CellBase)));
        }


        public static readonly DependencyProperty PropertyViewerProperty = DependencyProperty.Register(
            "PropertyViewer", typeof(PropertyViewer ), typeof(CellBase), new PropertyMetadata(default(PropertyViewer )));

        public PropertyViewer PropertyViewer
        {
            get => (PropertyViewer ) GetValue(PropertyViewerProperty);
            private init => SetValue(PropertyViewerProperty, value);
        }

        public CellBase(PropertyViewer propertyViewer)
        {
            PropertyViewer = propertyViewer ?? throw new ArgumentNullException(nameof(propertyViewer));
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(object), typeof(CellBase), new PropertyMetadata(default(object)));

        /// <summary>
        /// Get or set the cell content
        /// </summary>
        public object? Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }


    }

}