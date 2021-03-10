using System.Collections.Generic;

namespace PropertyViewerControl.PropertyAnalysis
{
    public class Property : IProperty
    {
        public object? Name { get; set; }

        public object? Value { get; set; }

        public IEnumerable<IProperty>? Children { get; set; }
    }
}