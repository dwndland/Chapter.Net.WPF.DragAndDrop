// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.DragAndDrop;

/// <summary>
///     The command args containing the data sent with the <see cref="DropItem.Command" />.
/// </summary>
public sealed class CustomDroppedArgs
{
    internal CustomDroppedArgs(object item, object parameter)
    {
        Item = item;
        Parameter = parameter;
    }

    /// <summary>
    ///     Gets the dropped item.
    /// </summary>
    public object Item { get; }

    /// <summary>
    ///     Gets the command parameter.
    /// </summary>
    public object Parameter { get; }
}