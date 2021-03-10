using System.Collections.Generic;

namespace PropertyViewerControl.PropertyAnalysis
{
    public interface IProperty
    {
        object? Name { get; }

        object? Value { get; }

        IEnumerable<IProperty>? Children { get; }
    }
}