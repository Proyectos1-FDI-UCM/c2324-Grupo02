using System;

namespace MVPFramework.Model
{
    public interface IObservableModel<out TData>
    {
        event Action<TData> DataSet;
    }
}