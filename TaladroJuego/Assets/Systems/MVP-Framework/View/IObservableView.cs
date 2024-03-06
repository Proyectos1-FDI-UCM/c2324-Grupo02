using System;

namespace MVPFramework.View
{
    public interface IObservableView
    {
        event EventHandler ViewEvent;
    }

    public interface IObservableView<T>
    {
        event EventHandler<T> ViewEvent;
    }
}