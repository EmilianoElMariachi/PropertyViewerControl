using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PropertyViewerControl.Rows
{
    public abstract class RowBase : Control
    {

        public static readonly DependencyProperty PropertyViewerProperty = DependencyProperty.Register(
            "PropertyViewer", typeof(PropertyViewer), typeof(RowBase), new PropertyMetadata(default(PropertyViewer)));


        public PropertyViewer PropertyViewer
        {
            get => (PropertyViewer)GetValue(PropertyViewerProperty);
            init => SetValue(PropertyViewerProperty, value);
        }


        private GridSplitter? _gridSplitter;

        /// <summary>
        /// </summary>
        /// <param name="propertyViewer">The <see cref="PropertyViewer"/> this row is attached to</param>
        public RowBase(PropertyViewer propertyViewer)
        {
            PropertyViewer = propertyViewer ?? throw new ArgumentNullException(nameof(propertyViewer));
        }


        //public override void OnApplyTemplate()
        //{
        //    //TODO: a déplacer
        //    if (_gridSplitter != null)
        //        _gridSplitter.MouseDoubleClick -= OnGridSplitterMouseDoubleClick;

        //    _gridSplitter = this.GetTemplateChild("PART_GridSplitter") as GridSplitter;

        //    if (_gridSplitter != null)
        //        _gridSplitter.MouseDoubleClick += OnGridSplitterMouseDoubleClick;
        //}

        //private void OnGridSplitterMouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    PropertyViewer.AutoSizeNameColumn();
        //}
    }
}