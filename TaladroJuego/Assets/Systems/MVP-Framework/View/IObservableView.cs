using System;

namespace MVPFramework.View
{
    public interface IObservableView<TDelegate>
        where TDelegate : Delegate
    {
        void Subscribe<UDelegate>(UDelegate observer)
            where UDelegate : TDelegate;
        void Unsubscribe<UDelegate>(UDelegate observer)
            where UDelegate : TDelegate;
    }
}