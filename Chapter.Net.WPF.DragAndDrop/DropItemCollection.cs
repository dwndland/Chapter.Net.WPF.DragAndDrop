// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DropItemCollection.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows;

namespace Chapter.Net.WPF.DragAndDrop;

/// <summary>
///     The drop item collection to attach on a control using the <see cref="Drop.DropItemsProperty" />.
/// </summary>
public sealed class DropItemCollection : FreezableCollection<DropItem>
{
}