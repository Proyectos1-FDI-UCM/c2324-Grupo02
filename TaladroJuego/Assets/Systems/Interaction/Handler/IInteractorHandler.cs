using InteractionSystem.Interactor;

namespace InteractionSystem.Handler
{
    public interface IInteractorHandler
    {
        bool Handle<T>(IInteractor<T> interactor);
    }
}