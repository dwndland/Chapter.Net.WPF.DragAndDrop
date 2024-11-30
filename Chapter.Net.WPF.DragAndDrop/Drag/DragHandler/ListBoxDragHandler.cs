using System.Windows;
using System.Windows.Controls;

// ReSharper disable once CheckNamespace
namespace Chapter.Net.WPF.DragAndDrop;

public class ListBoxDragHandler : DragHandler
{
    private readonly ListBox _listBox;
    private ListBoxItem _dragContainer;

    public ListBoxDragHandler(ListBox listBox)
    {
        _listBox = listBox;
    }

    public override bool CanDrag(Drags drags, object source)
    {
        switch (drags)
        {
            case Drags.DataContext:
            {
                _dragContainer = GetContainer<ListBoxItem>(source);
                return _dragContainer is { DataContext: not null };
            }
            case Drags.SelectedItem:
                return _listBox.SelectedItem != null;
            case Drags.SelectedItems:
                return _listBox.SelectedItems is { Count: > 0 };
        }

        return false;
    }

    public override object GetDragData(Drags drags)
    {
        return drags switch
        {
            Drags.DataContext => _dragContainer.DataContext,
            Drags.SelectedItem => _listBox.SelectedItem,
            Drags.SelectedItems => _listBox.SelectedItems,
            _ => null
        };
    }
}