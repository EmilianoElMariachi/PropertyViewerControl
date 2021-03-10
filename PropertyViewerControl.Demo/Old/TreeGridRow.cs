using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PropertyViewerControl.Demo.Old
{
    public class TreeGridRow : DataGridRow
    {

        public static readonly DependencyProperty LevelMarginProperty = DependencyProperty.Register(
            "LevelMargin", typeof(Thickness), typeof(TreeGridRow), new PropertyMetadata(default(Thickness)));

        public Thickness LevelMargin
        {
            get => (Thickness) GetValue(LevelMarginProperty);
            private set => SetValue(LevelMarginProperty, value);
        }

        public int Level { get; }

        public TreeGridRow(object boundData, int level)
        {
            Item = boundData;
            DataContext = boundData;
            Level = level;
            LevelMargin = new Thickness(level * 10, 0, 0, 0);
        }

        public List<TreeGridRow> Children { get; } = new();

    }
}