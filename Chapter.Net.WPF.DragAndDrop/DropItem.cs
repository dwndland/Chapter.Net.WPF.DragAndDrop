// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DropItem.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Chapter.Net.WPF.DragAndDrop
{
    /// <summary>
    ///     The drop item attached on a control using the <see cref="Drop.DropItemProperty" /> or
    ///     <see cref="Drop.DropItemsProperty" />.
    /// </summary>
    public sealed class DropItem : Freezable
    {
        /// <summary>
        ///     Identifies the Command dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(DropItem), new PropertyMetadata(null));

        /// <summary>
        ///     Identifies the DropType dependency property.
        /// </summary>
        public static readonly DependencyProperty DropTypeProperty =
            DependencyProperty.Register(nameof(DropType), typeof(DropType), typeof(DropItem), new PropertyMetadata(DropType.Files));

        /// <summary>
        ///     Identifies the DragDropEffect dependency property.
        /// </summary>
        public static readonly DependencyProperty DragDropEffectProperty =
            DependencyProperty.Register(nameof(DragDropEffect), typeof(DragDropEffects), typeof(DropItem), new PropertyMetadata(DragDropEffects.Copy));

        /// <summary>
        ///     Identifies the CommandParameter dependency property.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(DropItem), new PropertyMetadata(null));

        private UIElement _element;

        /// <summary>
        ///     Gets or sets the command to be executed if the user dropped data.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        ///     Gets or sets the type the user is allowed to drop.
        /// </summary>
        public DropType DropType
        {
            get => (DropType)GetValue(DropTypeProperty);
            set => SetValue(DropTypeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the mouse cursor on drag over.
        /// </summary>
        public DragDropEffects DragDropEffect
        {
            get => (DragDropEffects)GetValue(DragDropEffectProperty);
            set => SetValue(DragDropEffectProperty, value);
        }

        /// <summary>
        ///     Gets or sets the optional command parameter data.
        /// </summary>
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
            e.Effects = HasData(e) ? DragDropEffect : DragDropEffects.None;
            e.Handled = true;
        }

        private bool HasData(DragEventArgs dragEventArgs)
        {
            return dragEventArgs.Data.GetDataPresent(DataFormats.FileDrop, true);
        }

        private void OnPreviewDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop, true))
                return;

            if (!(e.Data.GetData(DataFormats.FileDrop, true) is string[] content))
                return;

            switch (DropType)
            {
                case DropType.File:
                    if (content.Length == 1 && File.Exists(content[0]))
                        OnCommand(content[0]);
                    break;
                case DropType.Files:
                    OnCommand(content.Where(File.Exists).ToArray());
                    break;
                case DropType.Folder:
                    if (content.Length == 1 && Directory.Exists(content[0]))
                        OnCommand(content[0]);
                    break;
                case DropType.Folders:
                    OnCommand(content.Where(Directory.Exists).ToArray());
                    break;
                case DropType.FilesAndFolders:
                    OnCommand(content.Where(value => File.Exists(value) || Directory.Exists(value)).ToArray());
                    break;
            }
        }

        private void OnCommand(string item)
        {
            var args = new ItemDroppedArgs(item, CommandParameter);
            if (Command != null && Command.CanExecute(args))
                Command.Execute(args);
        }

        private void OnCommand(string[] items)
        {
            if (items.Length == 0)
                return;

            var args = new ItemsDroppedArgs(items, CommandParameter);
            if (Command != null && Command.CanExecute(args))
                Command.Execute(args);
        }

        /// <inheritdoc />
        protected override Freezable CreateInstanceCore()
        {
            return this;
        }
    }
}