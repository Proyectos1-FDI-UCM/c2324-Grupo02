using InteractionSystem.Interactor;

namespace InteractionSystem.Interactable
{
    public interface IInteractable
    {
        bool Accept<T>(IInteractor<T> interactor);
    }

    public interface IInteractable<out T> : IInteractable
    {
        bool IInteractable.Accept<U>(IInteractor<U> interactor) =>
            interactor is IInteractor<T> tInteractor
            && Accept(tInteractor);

        bool Accept<TInteractor>(TInteractor interactor) where TInteractor : IInteractor<T>;
    }

    //public interface IInteractable<in TInteractor, out T> where TInteractor : IInteractor<T>
    //{
    //    bool Accept(TInteractor interactor);
    //}
}
