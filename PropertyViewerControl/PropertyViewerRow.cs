using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PropertyViewerControl.Cells;
using PropertyViewerControl.PropertyAnalysis;

namespace PropertyViewerControl
{
    public class PropertyViewerRow : Control
    {
        static PropertyViewerRow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyViewerRow), new FrameworkPropertyMetadata(typeof(PropertyViewerRow)));
        }


        public static readonly DependencyProperty LevelProperty = DependencyProperty.Register(
            "Level", typeof(int), typeof(PropertyViewerRow), new PropertyMetadata(default(int)));

        public int Level
        {
            get => (int)GetValue(LevelProperty);
            private set => SetValue(LevelProperty, value);
        }


        public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
            "IsExpanded", typeof(bool), typeof(PropertyViewerRow), new PropertyMetadata(false));


        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public PropertyViewerCellName? PropertyViewerNameCell { get; private set; }

        public PropertyViewerCellValue? PropertyViewerValueCell { get; private set; }

        public PropertyViewer PropertyViewer { get; }

        public IProperty Property { get; }

        private Panel? _childRowsContainer;

        public PropertyViewerRow(PropertyViewer propertyViewer, IProperty property, int level)
        {
            PropertyViewer = propertyViewer ?? throw new ArgumentNullException(nameof(propertyViewer));
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Level = level;
        }

        public IEnumerable<PropertyViewerRow> Children
        {
            get
            {
                if (_childRowsContainer == null)
                    yield break;

                foreach (var child in _childRowsContainer.Children)
                {
                    if (child is PropertyViewerRow row)
                        yield return row;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PropertyViewerNameCell = GetTemplateChild("PART_NameCell") as PropertyViewerCellName;
            PropertyViewerValueCell = GetTemplateChild("PART_ValueCell") as PropertyViewerCellValue;
            _childRowsContainer = GetTemplateChild("PART_ChildRowsContainer") as Panel;

            if (PropertyViewerNameCell != null)
                PropertyViewerNameCell.DataContext = Property.Name;

            if (PropertyViewerValueCell != null)
                PropertyViewerValueCell.DataContext = Property.Value;

            if (_childRowsContainer != null)
            {
                _childRowsContainer.Children.Clear();


                var children = Property.Children;
                if (children != null)
                {
                    var childRows = children.Select(property => PropertyViewer.GetRowForProperty(property, this.Level + 1));
                    foreach (var childRow in childRows)
                    {
                        _childRowsContainer.Children.Add(childRow);
                    }
                }
            }
        }

    }
}