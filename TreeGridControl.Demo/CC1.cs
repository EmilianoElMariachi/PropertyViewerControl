using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeGridControl.Demo
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TreeGridControl.Demo"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:TreeGridControl.Demo;assembly=TreeGridControl.Demo"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CC1/>
    ///
    /// </summary>
    [ContentProperty("Content")]
    public class CC1 : Control
    {
        static CC1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CC1), new FrameworkPropertyMetadata(typeof(CC1)));
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(object), typeof(CC1), new PropertyMetadata(default(object)));

        public object Content
        {
            get { return (object) GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var measureOverride = base.MeasureOverride(constraint);
            Debug.WriteLine($"{DateTime.Now.Millisecond} MeasureOverride: {constraint}");
            return measureOverride;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var measureOverride = base.ArrangeOverride(arrangeBounds);
            Debug.WriteLine($"{DateTime.Now.Millisecond} ArrangeOverride: {arrangeBounds}");
            return measureOverride;
        }
    }
}
