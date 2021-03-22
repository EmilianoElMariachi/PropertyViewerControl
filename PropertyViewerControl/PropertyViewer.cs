using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PropertyViewerControl.Cells;
using PropertyViewerControl.PropertyAnalysis;
using PropertyViewerControl.Rows;

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

        public static readonly DependencyProperty NameProbeCellProperty = DependencyProperty.Register(
            "NameProbeCell", typeof(NameProbeCell), typeof(PropertyViewer), new PropertyMetadata(default(NameProbeCell)));

        public NameProbeCell NameProbeCell
        {
            get => (NameProbeCell) GetValue(NameProbeCellProperty);
            private init => SetValue(NameProbeCellProperty, value);
        }  
        
        public static readonly DependencyProperty ValueProbeCellProperty = DependencyProperty.Register(
            "ValueProbeCell", typeof(ValueProbeCell), typeof(PropertyViewer), new PropertyMetadata(default(ValueProbeCell)));

        public ValueProbeCell ValueProbeCell
        {
            get => (ValueProbeCell) GetValue(ValueProbeCellProperty);
            private init => SetValue(ValueProbeCellProperty, value);
        }

        #endregion

        public PropertyViewer()
        {
            HeaderRow = new HeaderRow(this);
            NameProbeCell = new NameProbeCell(this);
            ValueProbeCell = new ValueProbeCell(this);

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoaded;
            AutoSizeNameColumn();
        }

        public static readonly DependencyProperty HeaderRowProperty = DependencyProperty.Register(
            "HeaderRow", typeof(HeaderRow), typeof(PropertyViewer), new PropertyMetadata(default(HeaderRow)));

        public HeaderRow HeaderRow
        {
            get => (HeaderRow)GetValue(HeaderRowProperty);
            private init => SetValue(HeaderRowProperty, value);
        }

        public static readonly DependencyProperty NameColumnProperty = DependencyProperty.Register(
            "NameColumn", typeof(Column), typeof(PropertyViewer), new PropertyMetadata(default(Column?), (o, args) =>
            {

            }));

        public Column? NameColumn
        {
            get => (Column)GetValue(NameColumnProperty);
            set => SetValue(NameColumnProperty, value);
        }

        public static readonly DependencyProperty ValueColumnProperty = DependencyProperty.Register(
            "ValueColumn", typeof(Column), typeof(PropertyViewer), new PropertyMetadata(default(Column?)));

        public Column? ValueColumn
        {
            get => (Column)GetValue(ValueColumnProperty);
            set => SetValue(ValueColumnProperty, value);
        }

        public void AutoSizeNameColumn()
        {
            var nameColumn = NameColumn;
            if (nameColumn == null)
                return;

            static double GetBestCellWidth(UIElement cell)
            {
                cell.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                return cell.DesiredSize.Width;
            }

            var width = GetBestCellWidth(HeaderRow.NameHeaderCell);

            var remainingRows = new List<Row>();
            remainingRows.AddRange(Rows);

            while (remainingRows.Count > 0)
            {
                var row = remainingRows[0];
                remainingRows.RemoveAt(0);

                if (row.IsExpanded)
                    remainingRows.AddRange(row.Children);

                var nameCell = row.NameCell;

                width = Math.Max(GetBestCellWidth(nameCell), width);
            }

            nameColumn.Width = new GridLength(width);
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

            AutoSizeNameColumn();
        }

    }
}
