using InteractionSystem.Handler;

namespace InteractionSystem.Interactor
{
    public interface IInteractor
    {
        bool Accept(IInteractorHandler handler);
    }

    public interface IInteractor<in T>
    {
        bool InteractWith(T resource);
    }
}