using InteractionSystem.Interactor;

namespace InteractionSystem.Interactable
{
    public interface IInteractable<out T>
    {
        bool Accept<TInteractor>(TInteractor interactor) where TInteractor : IInteractor<T>;
    }
    //public interface IInteractable<in TInteractor, out T> where TInteractor : IInteractor<T>
    //{
    //    bool Accept(TInteractor interactor);
    //}
}
