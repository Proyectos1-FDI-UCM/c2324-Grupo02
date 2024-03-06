using System;

namespace MVPFramework.Model
{
    //public interface IUpdateableModel<out TData>
    //{
    //    IModel<UData> With<UData>(Func<TData, IModel<UData>> binding);
    //}

    public interface IUpdateableModel<in TData>
    {
        void With(TData data);
    }

    public interface IUpdateableModel<out TModel, in TData> : IUpdateableModel<TData>
    {
        void IUpdateableModel<TData>.With(TData data) => With(data);
        new TModel With(TData data);
    }

    //public readonly struct Monad<T>
    //{
    //    public T Value { get; }

    //    public Monad(T value) => Value = value;

    //    public Monad<U> Bind<U>(Func<T, Monad<U>> func) => func(Value);

    //    public static implicit operator T(Monad<T> monad) => monad.Value;
    //    public static implicit operator Monad<T>(T value) => new Monad<T>(value);
    //}
}