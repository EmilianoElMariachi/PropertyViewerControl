using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;

namespace TreeGridControl
{
    [ContentProperty(nameof(Content))]
    public class Cell : Control
    {
        static Cell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Cell), new FrameworkPropertyMetadata(typeof(Cell)));
        }

        public Row Row { get; }

        public Column Column { get; }

        public Cell(Row row, Column column)
        {
            Row = row ?? throw new ArgumentNullException(nameof(row));
            Column = column ?? throw new ArgumentNullException(nameof(column));
        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(object), typeof(Cell), new PropertyMetadata(default(object)));

        private Thumb? _gripper;

        public object? Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            HookupGripperEvents();
        }

        private void HookupGripperEvents()
        {
            UnhookGripperEvents();

            _gripper = GetTemplateChild("PART_Gripper") as Thumb;

            if (_gripper != null)
            {
                _gripper.DragDelta += OnColumnResize;
                _gripper.MouseDoubleClick += OnGripperDoubleClicked;
                //SetLeftGripperVisibility();
            }

        }

        private void OnGripperDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            Column.AutoSize();
        }


        private void OnColumnResize(object sender, DragDeltaEventArgs e)
        {
            Column.Width = new GridLength(Column.ActualWidth + e.HorizontalChange);
        }


        private void UnhookGripperEvents()
        {
            if (_gripper != null)
            {
                _gripper.DragDelta -= OnColumnResize;
                _gripper.MouseDoubleClick -= OnGripperDoubleClicked;
                _gripper = null;
            }
        }


        /// <summary>
        /// Method called when at least one of the children wants to change its size
        /// </summary>
        /// <param name="child"></param>
        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            base.OnChildDesiredSizeChanged(child);
            Column.InvalidateActualWidth();
            Debug.WriteLine($"{this.GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
        }
    }
}