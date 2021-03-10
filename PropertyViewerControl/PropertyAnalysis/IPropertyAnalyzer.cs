using System.Collections.Generic;

namespace PropertyViewerControl.PropertyAnalysis
{
    public interface IPropertyAnalyzer
    {
        IEnumerable<IProperty>? GetProperties(object? objectSource);
    }
}