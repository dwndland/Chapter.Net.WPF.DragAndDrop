using System.Windows;

// ReSharper disable once CheckNamespace
namespace Chapter.Net.WPF.DragAndDrop;

public abstract class DragHandler
{
    public abstract bool CanDrag(Drags drags, object source);
    public abstract object GetDragData(Drags drags);

    protected T GetContainer<T>(object source) where T : DependencyObject
    {
        if (source is T item)
            return item;

        return VisualTreeAssist.FindParent<T>(source as DependencyObject);
    }
}