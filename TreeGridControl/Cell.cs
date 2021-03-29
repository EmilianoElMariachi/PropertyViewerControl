using System;
using System.Diagnostics;
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


            //this.SetBinding(WidthProperty, new Binding(nameof(Column.Width))
            //{
            //    Source = column,
            //    Mode = BindingMode.OneWay
            //});

        }

        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "Content", typeof(object), typeof(Cell), new PropertyMetadata(default(object)));

        private Thumb? _gripper;
        private double _initialDragColumnWidth;

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
                _gripper.DragStarted += OnColumnGripperDragStarted;
                _gripper.DragDelta += OnColumnResize;
                _gripper.DragCompleted += OnColumnGripperDragCompleted;
                _gripper.MouseDoubleClick += OnGripperDoubleClicked;
                //SetLeftGripperVisibility();
            }

        }

        private void OnGripperDoubleClicked(object sender, MouseButtonEventArgs e)
        {
            this.Column.AutoSize();
        }

        private void OnColumnGripperDragCompleted(object sender, DragCompletedEventArgs e)
        {
            
        }

        private void OnColumnResize(object sender, DragDeltaEventArgs e)
        {
            Column.Width += e.HorizontalChange;
        }

        private void OnColumnGripperDragStarted(object sender, DragStartedEventArgs e)
        {
            
            _initialDragColumnWidth = Column.Width;
            Trace.WriteLine("Start=" + _initialDragColumnWidth);
        }

        private void UnhookGripperEvents()
        {
            if (_gripper != null)
            {
                _gripper.DragStarted -= OnColumnGripperDragStarted;
                _gripper.DragDelta -= OnColumnResize;
                _gripper.DragCompleted -= OnColumnGripperDragCompleted;
                _gripper.MouseDoubleClick -= OnGripperDoubleClicked;
                _gripper = null;
            }
        }

        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            base.OnChildDesiredSizeChanged(child);
            Column.InvalidateActualWidth();
        }
    }
}