using InteractionSystem.Handler;

namespace InteractionSystem.Interactor
{
    internal readonly struct Interactor<T> : IInteractor
    {
        private readonly IInteractor<T> interactor;

        public Interactor(IInteractor<T> interactor)
        {
            this.interactor = interactor;
        }

        public bool Accept(IInteractorHandler handler) => handler.Handle(interactor);
    }
}