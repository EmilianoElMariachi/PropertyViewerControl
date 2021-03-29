using System.Collections.ObjectModel;

namespace TreeGridControl.Demo
{
    public interface IItem
    {

        string Name { get; }

        string? Value { get; }

        ObservableCollection<IItem> Children { get; }

    }

    public class Item : IItem
    {
        public string? Name { get; set; }

        public string? Value { get; set; }

        public ObservableCollection<IItem> Children { get; } = new ();
    }
}
