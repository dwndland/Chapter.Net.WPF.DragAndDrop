using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.DragAndDrop;

/// <summary>
///     The drop item attached on a control using the <see cref="Drag.DragItemProperty" /> or
///     <see cref="Drag.DragItemsProperty" />.
/// </summary>
public sealed class DragItem : Freezable
{
    /// <summary>
    ///     Defines the Drags dependency property.
    /// </summary>
    public static readonly DependencyProperty DragsProperty =
        DependencyProperty.Register(nameof(Drags), typeof(Drags), typeof(DragItem), new PropertyMetadata(Drags.DataContext));

    /// <summary>
    ///     Defines the Format dependency property.
    /// </summary>
    public static readonly DependencyProperty FormatProperty =
        DependencyProperty.Register(nameof(Format), typeof(string), typeof(DragItem), new PropertyMetadata(null));

    /// <summary>
    ///     Defines the Effect dependency property.
    /// </summary>
    public static readonly DependencyProperty EffectProperty =
        DependencyProperty.Register(nameof(Effect), typeof(DragDropEffects), typeof(DragItem), new PropertyMetadata(DragDropEffects.Copy));

    private DragHandler _dragHandler;
    private UIElement _element;
    private Point _startPosition;

    /// <summary>
    ///     Gets or sets what to start drag with.
    /// </summary>
    /// <value>Default: Drags.DataContext.</value>
    [DefaultValue(Drags.DataContext)]
    public Drags Drags
    {
        get => (Drags)GetValue(DragsProperty);
        set => SetValue(DragsProperty, value);
    }

    /// <summary>
    ///     Gets or sets the format to start the drag and drop with.
    /// </summary>
    /// <value>Default: null.</value>
    [DefaultValue(null)]
    public string Format
    {
        get => (string)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    /// <summary>
    ///     Gets or sets the effect on the mouse when dragging.
    /// </summary>
    /// <value>Default: DragDropEffects.Copy.</value>
    [DefaultValue(DragDropEffects.Copy)]
    public DragDropEffects Effect
    {
        get => (DragDropEffects)GetValue(EffectProperty);
        set => SetValue(EffectProperty, value);
    }

    /// <summary>
    ///     Attaches to events on the given control.
    /// </summary>
    /// <param name="element">The sender to start drag from.</param>
    public void Attach(FrameworkElement element)
    {
        _dragHandler = element switch
        {
            ListBox listBox => new ListBoxDragHandler(listBox),
            DataGrid dataGrid => new DataGridDragHandler(dataGrid),
            _ => new ObjectDragHandler(element)
        };

        if (_element != null)
            Detach();

        _element = element;
        _element.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        _element.MouseMove += OnMouseMove;
    }

    /// <summary>
    ///     Detaches from the event of the control previously attached to.
    /// </summary>
    public void Detach()
    {
        if (_element == null)
            return;

        _element.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
        _element.MouseMove -= OnMouseMove;
        _element = null;
    }

    private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _startPosition = e.GetPosition(null);
    }

    private void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
    {
        e.Handled = true;

        if (e.LeftButton != MouseButtonState.Pressed ||
            e.OriginalSource is Thumb ||
            !_dragHandler.CanDrag(Drags, e.OriginalSource))
            return;

        var mousePos = e.GetPosition(null);
        var diff = _startPosition - mousePos;

        if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
            Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
        {
            var data = new DataObject(Format, _dragHandler.GetDragData(Drags));
            DragDrop.DoDragDrop(_element, data, Effect);
        }
    }

    /// <inheritdoc />
    protected override Freezable CreateInstanceCore()
    {
        return this;
    }
}