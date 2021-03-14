using System;
using System.Collections.Generic;
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


        public static readonly DependencyProperty HeaderPropertyNameProperty = DependencyProperty.Register(
            "HeaderPropertyName", typeof(object), typeof(PropertyViewer), new PropertyMetadata("Property"));

        public object HeaderPropertyName
        {
            get => GetValue(HeaderPropertyNameProperty);
            set => SetValue(HeaderPropertyNameProperty, value);
        }

        public static readonly DependencyProperty HeaderPropertyValueProperty = DependencyProperty.Register(
            "HeaderPropertyValue", typeof(object), typeof(PropertyViewer), new PropertyMetadata("Value"));

        public object HeaderPropertyValue
        {
            get => GetValue(HeaderPropertyValueProperty);
            set => SetValue(HeaderPropertyValueProperty, value);
        }

        #endregion

        private Panel? _rowsContainer;

        protected GridSplitter? _gridSplitter;
        protected ColumnDefinition? _nameColumnDefinition;
        protected ColumnDefinition? _valueColumnDefinition;
        protected ColumnDefinition? _gridSplitterColumnDefinition;

        public IEnumerable<PropertyViewerRow> Rows
        {
            get
            {
                if (_rowsContainer == null)
                    yield break;

                foreach (var child in _rowsContainer.Children)
                {
                    if (child is PropertyViewerRow row)
                        yield return row;
                }
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _rowsContainer = this.GetTemplateChild("PART_RowsContainer") as Panel;

            if (_gridSplitter != null)
                _gridSplitter.MouseDoubleClick -= OnGridSplitterMouseDoubleClick;

            _gridSplitter = this.GetTemplateChild("PART_GridSplitter") as GridSplitter;

            if (_gridSplitter != null)
                _gridSplitter.MouseDoubleClick += OnGridSplitterMouseDoubleClick;

            _nameColumnDefinition = this.GetTemplateChild("PART_PropertyNameColumn") as ColumnDefinition;
            _gridSplitterColumnDefinition = this.GetTemplateChild("PART_GridSplitterColumn") as ColumnDefinition;
            _valueColumnDefinition = this.GetTemplateChild("PART_PropertyValueColumn") as ColumnDefinition;

            UpdateViewFromObjectSource();
        }

        private void OnGridSplitterMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AutoSizeNameColumn();
        }

        private void AutoSizeNameColumn()
        {
            var nameColumnDefinition = _nameColumnDefinition;
            if (nameColumnDefinition == null)
                return;
            var width = 0d; //TODO: tenir compte de la cellule des headers

            var remainingRows = new List<PropertyViewerRow>();
            remainingRows.AddRange(Rows);

            while (remainingRows.Count > 0)
            {
                var row = remainingRows[0];
                remainingRows.RemoveAt(0);

                if (row.IsExpanded)
                    remainingRows.AddRange(row.Children);

                var nameCell = row.PropertyViewerNameCell;
                if (nameCell == null)
                    continue;
                nameCell.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                width = Math.Max(nameCell.DesiredSize.Width, width);
            }

            nameColumnDefinition.Width = new GridLength(width);
        }

        public PropertyViewerRow GetRowForProperty(IProperty property, int level)
        {
            return new(this, property, level);
        }

        private void UpdateViewFromObjectSource()
        {
            var rowsContainer = _rowsContainer;
            if (rowsContainer == null)
                return;

            rowsContainer.Children.Clear();

            var srcObj = ObjectSource;
            var propertyAnalyzer = PropertyAnalyzer;

            var rootRows = propertyAnalyzer.GetProperties(srcObj)?.Select(property => GetRowForProperty(property, 0)).ToList();

            if (rootRows == null)
                return;

            foreach (var row in rootRows)
            {
                rowsContainer.Children.Add(row);
            }
        }

    }
}
