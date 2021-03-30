using System.Collections.ObjectModel;
using System.Windows;

namespace TreeGridControl.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

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

            MyTreeGrid.ItemsSource = items;
        }
        
    }
}
