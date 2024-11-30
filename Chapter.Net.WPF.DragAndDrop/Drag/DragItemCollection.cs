using System.Windows;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.DragAndDrop;

/// <summary>
///     The drag item collection to attach on a control using the <see cref="Drop.DragItemsProperty" />.
/// </summary>
public sealed class DragItemCollection : FreezableCollection<DragItem>;