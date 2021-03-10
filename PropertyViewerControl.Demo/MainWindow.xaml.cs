using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using PropertyViewerControl.PropertyAnalysis;

namespace PropertyViewerControl.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PropertyViewer.PropertyAnalyzer = new CustomPropertyAnalyzer();

            var items = new ObservableCollection<IItem>
            {
                new Item
                {
                    Name = "Toto",
                    Value = "123"
                },
                new Item
                {
                    Name = "Tata",
                    Value = "456"
                },
                new Item
                {
                    Name = "Titi",
                    Value = "789",
                    Children = {
                        new Item
                        {
                            Name = "OK",
                            Value = "012",

                        }
                    }
                }

            };

            PropertyViewer.ObjectSource = items;

            DataGrid.ItemsSource = new[] { "ok", "PL", "okpok", "pokpok", " okpok", "pokpok" };
        }
    }

    public class CustomPropertyAnalyzer : IPropertyAnalyzer
    {
        public IEnumerable<IProperty>? GetProperties(object? objectSource)
        {
            if (!(objectSource is IEnumerable<IItem> items))
                yield break;

            foreach (var item in items)
            {
                var children = GetProperties(item.Children)?.ToArray();
                var property = new Property
                {
                    Name = item.Name,
                    Value = item.Value,
                    Children = children
                };

                yield return property;
            }
        }
    }
}
