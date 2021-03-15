using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PropertyViewerControl.PropertyAnalysis;

namespace PropertyViewerControl
{
    public class PropertyViewer : Control
    {
        static PropertyViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyViewer), new FrameworkPropertyMetadata(typeof(PropertyViewer)));
        }

        #region Dependency Properties

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
            "Rows", typeof(ObservableCollection<Row>), typeof(PropertyViewer), new PropertyMetadata(new ObservableCollection<Row>()));

        public ObservableCollection<Row> Rows => (ObservableCollection<Row>)GetValue(RowsProperty);

        public static readonly DependencyProperty PropertyAnalyzerProperty = DependencyProperty.Register(
            "PropertyAnalyzer", typeof(IPropertyAnalyzer), typeof(PropertyViewer), new PropertyMetadata(new DefaultPropertyAnalyzer(), (_, args) =>
            {
                if (args.NewValue == null)
                    throw new ArgumentNullException(nameof(PropertyAnalyzer));
            }));

        public IPropertyAnalyzer PropertyAnalyzer
        {
            get => (IPropertyAnalyzer)GetValue(PropertyAnalyzerProperty);
            set => SetValue(PropertyAnalyzerProperty, value);
        }

        public static readonly DependencyProperty ObjectSourceProperty = DependencyProperty.Register(
            "ObjectSource", typeof(object), typeof(PropertyViewer), new PropertyMetadata(default, OnObjectSourceChanged));

        public object ObjectSource
        {
            get => GetValue(ObjectSourceProperty);
            set => SetValue(ObjectSourceProperty, value);
        }

        private static void OnObjectSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PropertyViewer)d).UpdateViewFromObjectSource();
        }

        #endregion

        protected GridSplitter? _gridSplitter;
        protected ColumnDefinition? _gridSplitterColumnDefinition;

        public PropertyViewer()
        {
            HeaderRow = new HeaderRow
            {
                PropertyViewer = this
            };

            Loaded += OnLoaded;

            NameColumn = new Column();
            ValueColumn = new Column();
        }

        public static readonly DependencyProperty HeaderRowProperty = DependencyProperty.Register(
            "HeaderRow", typeof(HeaderRow), typeof(PropertyViewer), new PropertyMetadata(default(HeaderRow)));

        public HeaderRow HeaderRow
        {
            get => (HeaderRow)GetValue(HeaderRowProperty);
            private init => SetValue(HeaderRowProperty, value);
        }

        public Column NameColumn { get; }

        public Column ValueColumn { get; }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            UpdateViewFromObjectSource();

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_gridSplitter != null)
                _gridSplitter.MouseDoubleClick -= OnGridSplitterMouseDoubleClick;

            _gridSplitter = this.GetTemplateChild("PART_GridSplitter") as GridSplitter;

            if (_gridSplitter != null)
                _gridSplitter.MouseDoubleClick += OnGridSplitterMouseDoubleClick;

            _gridSplitterColumnDefinition = this.GetTemplateChild("PART_GridSplitterColumn") as ColumnDefinition;

        }

        private void OnGridSplitterMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AutoSizeNameColumn();
        }

        private void AutoSizeNameColumn()
        {
            var width = 0d; //TODO: tenir compte de la cellule des headers

            var remainingRows = new List<Row>();
            remainingRows.AddRange(Rows);

            while (remainingRows.Count > 0)
            {
                var row = remainingRows[0];
                remainingRows.RemoveAt(0);

                if (row.IsExpanded)
                    remainingRows.AddRange(row.Children);

                var nameCell = row.NameCell;

                nameCell.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                width = Math.Max(nameCell.DesiredSize.Width, width);
            }

            NameColumn.Width = new GridLength(width);
        }

        public Row GetRowForProperty(IProperty property, int level)
        {
            return new(this, level)
            {
                BoundProperty = property
            };
        }

        private void UpdateViewFromObjectSource()
        {
            Rows.Clear();


            var srcObj = ObjectSource;
            var propertyAnalyzer = PropertyAnalyzer;

            var rootRows = propertyAnalyzer.GetProperties(srcObj)?.Select(property => GetRowForProperty(property, 0)).ToList();

            if (rootRows == null)
                return;

            foreach (var row in rootRows)
            {
                Rows.Add(row);
            }
        }

    }
}
