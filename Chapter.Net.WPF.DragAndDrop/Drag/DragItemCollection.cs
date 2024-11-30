// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DragItemCollection.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows;

// ReSharper disable once CheckNamespace
namespace Chapter.Net.WPF.DragAndDrop;

/// <summary>
///     The drag item collection to attach on a control using the <see cref="Drag.DragItemsProperty" />.
/// </summary>
public sealed class DragItemCollection : FreezableCollection<DragItem>;