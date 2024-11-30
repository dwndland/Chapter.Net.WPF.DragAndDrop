// -----------------------------------------------------------------------------------------------------------------
// <copyright file="DataGridDragHandler.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows.Controls;

// ReSharper disable once CheckNamespace
namespace Chapter.Net.WPF.DragAndDrop;

internal class DataGridDragHandler : DragHandler
{
    private readonly DataGrid _dataGrid;
    private DataGridRow _dragContainer;

    public DataGridDragHandler(DataGrid dataGrid)
    {
        _dataGrid = dataGrid;
    }

    public override bool CanDrag(Drags drags, object source)
    {
        switch (drags)
        {
            case Drags.DataContext:
            {
                _dragContainer = GetContainer<DataGridRow>(source);
                return _dragContainer is { DataContext: not null };
            }
            case Drags.SelectedItem:
                return _dataGrid.SelectedItem != null;
            case Drags.SelectedItems:
                return _dataGrid.SelectedItems is { Count: > 0 };
        }

        return false;
    }

    public override object GetDragData(Drags drags)
    {
        return drags switch
        {
            Drags.DataContext => _dataGrid.DataContext,
            Drags.SelectedItem => _dataGrid.SelectedItem,
            Drags.SelectedItems => _dataGrid.SelectedItems,
            _ => null
        };
    }
}