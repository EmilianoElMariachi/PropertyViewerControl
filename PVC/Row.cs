using System;
using System.Windows;
using System.Windows.Controls;

namespace PVC
{
    public class Row : Control
    {



        static Row()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Row), new FrameworkPropertyMetadata(typeof(Row)));
        }


        public Row(PropertyEditor propertyEditor)
        {
            PropertyEditor = propertyEditor ?? throw new ArgumentNullException(nameof(propertyEditor));
        }


        public PropertyEditor? PropertyEditor { get; }



    }
}