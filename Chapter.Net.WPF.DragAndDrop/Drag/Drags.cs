// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.DragAndDrop;

/// <summary>
///     Defines what item to drag.
/// </summary>
public enum Drags
{
    /// <summary>
    ///     The item from the sender data context.
    /// </summary>
    DataContext,

    /// <summary>
    ///     The selected item from the sender collection.
    /// </summary>
    SelectedItem,

    /// <summary>
    ///     The selected items from the sender collection.
    /// </summary>
    SelectedItems
}