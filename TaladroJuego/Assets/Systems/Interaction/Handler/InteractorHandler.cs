using InteractionSystem.Interactable;
using InteractionSystem.Interactor;
using InteractionSystem.Locator;

namespace InteractionSystem.Handler
{
    internal readonly struct InteractorHandler : IInteractorHandler
    {
        private readonly IInteractableLocator _interactableLocator;

        public InteractorHandler(IInteractableLocator interactableLocator)
        {
            _interactableLocator = interactableLocator;
        }

        public bool Handle<T>(IInteractor<T> interactor)
        {
            bool success = true;
            foreach (IInteractable<T> interactable in _interactableLocator.GetInteractables<T>())
                success &= interactable.Accept(interactor);
            return success;
        }
    }
}