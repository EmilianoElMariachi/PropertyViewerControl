using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PropertyViewerControl.Cells;
using PropertyViewerControl.PropertyAnalysis;

namespace PropertyViewerControl
{
    public class Row : Control
    {
        static Row()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Row), new FrameworkPropertyMetadata(typeof(Row)));
        }

        public static readonly DependencyProperty LevelProperty = DependencyProperty.Register(
            "Level", typeof(int), typeof(Row), new PropertyMetadata(default(int)));

        public int Level
        {
            get => (int)GetValue(LevelProperty);
            private set => SetValue(LevelProperty, value);
        }

        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
            "IsExpanded", typeof(bool), typeof(Row), new PropertyMetadata(false));

        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public static readonly DependencyProperty HasChildrenProperty = DependencyProperty.Register(
            "HasChildren", typeof(bool), typeof(Row), new PropertyMetadata(default(bool)));

        public bool HasChildren
        {
            get => (bool)GetValue(HasChildrenProperty);
            private set => SetValue(HasChildrenProperty, value);
        }

        public static readonly DependencyProperty PropertyViewerProperty = DependencyProperty.Register(
            "PropertyViewer", typeof(PropertyViewer), typeof(Row), new PropertyMetadata(default(PropertyViewer)));

        public PropertyViewer PropertyViewer
        {
            get => (PropertyViewer)GetValue(PropertyViewerProperty);
            private set => SetValue(PropertyViewerProperty, value);
        }


        public static readonly DependencyProperty NameCellProperty = DependencyProperty.Register(
            "NameCell", typeof(NameCell), typeof(Row), new PropertyMetadata(default(NameCell)));

        public NameCell NameCell
        {
            get => (NameCell) GetValue(NameCellProperty);
            private init => SetValue(NameCellProperty, value);
        }


        public static readonly DependencyProperty ValueCellProperty = DependencyProperty.Register(
            "ValueCell", typeof(ValueCell), typeof(Row), new PropertyMetadata(default(ValueCell)));

        public ValueCell ValueCell
        {
            get => (ValueCell) GetValue(ValueCellProperty);
            private init => SetValue(ValueCellProperty, value);
        }


        public static readonly DependencyProperty BoundPropertyProperty = DependencyProperty.Register(
            "BoundProperty", typeof(IProperty), typeof(Row), new PropertyMetadata(default(IProperty), OnBoundPropertyChanged));

        private static void OnBoundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Row)d).UpdateFromBoundProperty();
        }

        public IProperty? BoundProperty
        {
            get => (IProperty)GetValue(BoundPropertyProperty);
            set => SetValue(BoundPropertyProperty, value);
        }


        public Row(PropertyViewer propertyViewer, int level)
        {
            PropertyViewer = propertyViewer ?? throw new ArgumentNullException(nameof(propertyViewer));
            NameCell = new NameCell();
            ValueCell = new ValueCell();

            Level = level;
        }

        public static readonly DependencyProperty ChildrenProperty = DependencyProperty.Register(
            "Children", typeof(ObservableCollection<Row>), typeof(Row), new PropertyMetadata(new ObservableCollection<Row>()));

        public ObservableCollection<Row> Children => (ObservableCollection<Row>)GetValue(ChildrenProperty);


        private void UpdateFromBoundProperty()
        {
            var boundProperty = BoundProperty;

            NameCell.DataContext = boundProperty?.Name;
            ValueCell.DataContext = boundProperty?.Value;

            Children.Clear();

            var children = boundProperty?.Children;
            if (children != null)
            {
                var childRows = children.Select(property => PropertyViewer.GetRowForProperty(property, this.Level + 1));
                foreach (var childRow in childRows)
                {
                    Children.Add(childRow);
                }
            }

            HasChildren = Children.Any();

        }

    }
}