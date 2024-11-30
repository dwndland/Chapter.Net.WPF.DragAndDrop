// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DropItem.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF.DragAndDrop;

/// <summary>
///     The drop item attached on a control using the <see cref="Drop.DropItemProperty" /> or
///     <see cref="Drop.DropItemsProperty" />.
/// </summary>
public sealed class DropItem : Freezable
{
    /// <summary>
    ///     Identifies the Drops dependency property.
    /// </summary>
    public static readonly DependencyProperty DropsProperty =
        DependencyProperty.Register(nameof(Drops), typeof(Drops), typeof(DropItem), new PropertyMetadata(Drops.Files));

    /// <summary>
    ///     Identifies the Effect dependency property.
    /// </summary>
    public static readonly DependencyProperty EffectProperty =
        DependencyProperty.Register(nameof(Effect), typeof(DragDropEffects), typeof(DropItem), new PropertyMetadata(DragDropEffects.Copy));

    /// <summary>
    ///     Identifies the Format dependency property.
    /// </summary>
    public static readonly DependencyProperty FormatProperty =
        DependencyProperty.Register(nameof(Format), typeof(string), typeof(DropItem), new PropertyMetadata(null));

    /// <summary>
    ///     Identifies the Command dependency property.
    /// </summary>
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(DropItem), new PropertyMetadata(null));

    /// <summary>
    ///     Identifies the CommandParameter dependency property.
    /// </summary>
    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(DropItem), new PropertyMetadata(null));

    private UIElement _element;

    /// <summary>
    ///     Gets or sets the type the user is allowed to drop.
    /// </summary>
    /// <value>Default: Drops.Files.</value>
    [DefaultValue(Drops.Files)]
    public Drops Drops
    {
        get => (Drops)GetValue(DropsProperty);
        set => SetValue(DropsProperty, value);
    }

    /// <summary>
    ///     Gets or sets the mouse cursor on drag over.
    /// </summary>
    /// <value>Default: DragDropEffects.Copy.</value>
    [DefaultValue(DragDropEffects.Copy)]
    public DragDropEffects Effect
    {
        get => (DragDropEffects)GetValue(EffectProperty);
        set => SetValue(EffectProperty, value);
    }

    /// <summary>
    /// Gets or sets the format accepting on drop.
    /// </summary>
    /// <value>Default: null.</value>
    [DefaultValue(null)]
    public string Format
    {
        get => (string)GetValue(FormatProperty);
        set => SetValue(FormatProperty, value);
    }

    /// <summary>
    ///     Gets or sets the command to be executed if the user dropped data.
    /// </summary>
    /// <value>Default: null.</value>
    [DefaultValue(null)]
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    ///     Gets or sets the optional command parameter data.
    /// </summary>
    /// <value>Default: null.</value>
    [DefaultValue(null)]
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    /// <summary>
    ///     Attaches to the events for the given element.
    /// </summary>
    /// <param name="element">The element the data can be dropped onto.</param>
    public void Attach(UIElement element)
    {
        if (_element != null)
            Detach();

        _element = element;
        _element.PreviewDragOver += OnPreviewDragOver;
        _element.PreviewDrop += OnPreviewDrop;
    }

    /// <summary>
    ///     Removes the events from the previous attached element.
    /// </summary>
    public void Detach()
    {
        if (_element == null)
            return;

        _element.PreviewDragOver -= OnPreviewDragOver;
        _element.PreviewDrop -= OnPreviewDrop;
        _element = null;
    }

    private void OnPreviewDragOver(object sender, DragEventArgs e)
    {
        e.Effects = HasData(e) ? Effect : DragDropEffects.None;
        e.Handled = true;
    }

    private bool HasData(DragEventArgs dragEventArgs)
    {
        switch (Drops)
        {
            case Drops.File:
            case Drops.Files:
            case Drops.Folder:
            case Drops.Folders:
            case Drops.FilesAndFolders:
                return dragEventArgs.Data.GetDataPresent(DataFormats.FileDrop, true);
            case Drops.Custom:
                return dragEventArgs.Data.GetDataPresent(Format, true);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private object[] GetData(DragEventArgs dragEventArgs)
    {
        switch (Drops)
        {
            case Drops.File:
            case Drops.Files:
            case Drops.Folder:
            case Drops.Folders:
            case Drops.FilesAndFolders:
                if (dragEventArgs.Data.GetData(DataFormats.FileDrop, true) is string[] content)
                    return content.Cast<object>().ToArray();
                break;
            case Drops.Custom:
                if (dragEventArgs.Data.GetData(Format, true) is { } item)
                    return [item];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return null;
    }

    private void OnPreviewDrop(object sender, DragEventArgs e)
    {
        if (!HasData(e))
            return;

        var dropData = GetData(e);
        if (dropData == null)
            return;

        switch (Drops)
        {
            case Drops.File:
                if (dropData.Length == 1)
                {
                    var file = dropData[0].ToString();
                    if (File.Exists(file))
                        OnCommand(file);
                }
                break;
            case Drops.Files:
                OnCommand(dropData.Select(x => x.ToString()).Where(File.Exists).ToArray());
                break;
            case Drops.Folder:
                if (dropData.Length == 1)
                {
                    var file = dropData[0].ToString();
                    if (Directory.Exists(file))
                        OnCommand(file);
                }
                break;
            case Drops.Folders:
                OnCommand(dropData.Select(x => x.ToString()).Where(Directory.Exists).ToArray());
                break;
            case Drops.FilesAndFolders:
                OnCommand(dropData.Select(x => x.ToString()).Where(x => Directory.Exists(x) || File.Exists(x)).ToArray());
                break;
            case Drops.Custom:
                if (dropData.Length == 1)
                    OnCommand(dropData[0]);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnCommand(string item)
    {
        var args = new FileFolderDroppedArgs(item, CommandParameter);
        if (Command != null && Command.CanExecute(args))
            Command.Execute(args);
    }

    private void OnCommand(string[] items)
    {
        if (items.Length == 0)
            return;

        var args = new FilesFoldersDroppedArgs(items, CommandParameter);
        if (Command != null && Command.CanExecute(args))
            Command.Execute(args);
    }

    private void OnCommand(object item)
    {
        var args = new CustomDroppedArgs(item, CommandParameter);
        if (Command != null && Command.CanExecute(args))
            Command.Execute(args);
    }

    /// <inheritdoc />
    protected override Freezable CreateInstanceCore()
    {
        return this;
    }
}