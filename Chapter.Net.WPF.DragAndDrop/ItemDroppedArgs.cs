// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ItemDroppedArgs.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

namespace Chapter.Net.WPF.DragAndDrop
{
    /// <summary>
    ///     The command args containing the data sent with the <see cref="DropItem.Command" />.
    /// </summary>
    public sealed class ItemDroppedArgs
    {
        internal ItemDroppedArgs(string item, object parameter)
        {
            Item = item;
            Parameter = parameter;
        }

        /// <summary>
        ///     Gets the dropped item.
        /// </summary>
        public string Item { get; }

        /// <summary>
        ///     Gets the command parameter.
        /// </summary>
        public object Parameter { get; }
    }
}