using System.Windows;

// ReSharper disable once CheckNamespace
namespace Chapter.Net.WPF.DragAndDrop;

public class ObjectDragHandler : DragHandler
{
    private readonly FrameworkElement _element;

    public ObjectDragHandler(FrameworkElement element)
    {
        _element = element;
    }

    public override bool CanDrag(Drags drags, object source)
    {
        return _element.DataContext != null;
    }

    public override object GetDragData(Drags drags)
    {
        return _element.DataContext;
    }
}