using System.Windows;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.DragAndDrop;

/// <summary>
///     Allows to attach drag items to a control to enable start dragging of data.
/// </summary>
public static class Drag
{
    /// <summary>
    ///     The DragItems attached property.
    /// </summary>
    public static readonly DependencyProperty DragItemsProperty =
        DependencyProperty.RegisterAttached("DragItems", typeof(DragItemCollection), typeof(Drag), new PropertyMetadata(OnDragItemsChanged));

    /// <summary>
    ///     The DragItem attached property.
    /// </summary>
    public static readonly DependencyProperty DragItemProperty =
        DependencyProperty.RegisterAttached("DragItem", typeof(DragItem), typeof(Drag), new PropertyMetadata(OnDragItemChanged));

    /// <summary>
    ///     Gets the drag item collection containing all the drag items for a control.
    /// </summary>
    /// <param name="obj">The element from which the property value is read.</param>
    /// <returns>The drag item collection containing all the drag items for a control.</returns>
    public static DragItemCollection GetDragItems(DependencyObject obj)
    {
        return (DragItemCollection)obj.GetValue(DragItemsProperty);
    }

    /// <summary>
    ///     Sets the drag item collection containing all the drag items for a control.
    /// </summary>
    /// <param name="obj">The element from which the property value is set to.</param>
    /// <param name="value">The drag item collection containing all the drag items for a control.</param>
    public static void SetDragItems(DependencyObject obj, DragItemCollection value)
    {
        obj.SetValue(DragItemsProperty, value);
    }

    /// <summary>
    ///     Gets the drag item for a control.
    /// </summary>
    /// <param name="obj">The element from which the property value is read.</param>
    /// <returns>The drag item for a control.</returns>
    public static DragItem GetDragItem(DependencyObject obj)
    {
        return (DragItem)obj.GetValue(DragItemProperty);
    }

    /// <summary>
    ///     Sets the drag item for a control.
    /// </summary>
    /// <param name="obj">The element from which the property value is set to.</param>
    /// <param name="value">The drag item for a control.</param>
    public static void SetDragItem(DependencyObject obj, DragItem value)
    {
        obj.SetValue(DragItemProperty, value);
    }

    private static void OnDragItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = d as FrameworkElement;

        if (e.OldValue != null)
            foreach (var dropItem in (DragItemCollection)e.OldValue)
                dropItem.Detach();

        if (e.NewValue != null)
            foreach (var dropItem in (DragItemCollection)e.NewValue)
                dropItem.Attach(control);
    }

    private static void OnDragItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = d as FrameworkElement;

        ((DragItem)e.OldValue)?.Detach();
        ((DragItem)e.NewValue)?.Attach(control);
    }
}