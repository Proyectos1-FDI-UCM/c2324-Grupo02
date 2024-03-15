using InteractionSystem.Interactor;
using InteractionSystem.Locator;
using UnityEngine;

namespace InteractionSystem.Handler
{
    internal class InteractionHandler : MonoBehaviour
    {
        private IInteractor _interactor;
        private IInteractorHandler _interactorHandler;

        private void Awake()
        {
            _interactorHandler = new InteractorHandler(GetComponentInChildren<IInteractableLocator>());
            _interactor = GetComponentInChildren<IInteractor>();
        }

        public bool TryInteract() => _interactor.Accept(_interactorHandler);
        public void Interact() => TryInteract();
    }
}