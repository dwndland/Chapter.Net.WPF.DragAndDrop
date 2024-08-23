// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ItemsDroppedArgs.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

namespace Chapter.Net.WPF.DragAndDrop;

/// <summary>
///     The command args containing the data sent with the <see cref="DropItem.Command" />.
/// </summary>
public sealed class ItemsDroppedArgs
{
    internal ItemsDroppedArgs(string[] items, object parameter)
    {
        Items = items;
        Parameter = parameter;
    }

    /// <summary>
    ///     Gets the dropped items.
    /// </summary>
    public string[] Items { get; }

    /// <summary>
    ///     Gets the command parameter.
    /// </summary>
    public object Parameter { get; }
}