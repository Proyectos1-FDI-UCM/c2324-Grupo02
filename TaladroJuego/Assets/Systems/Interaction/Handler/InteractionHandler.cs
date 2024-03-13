using InteractionSystem.Adapter;
using InteractionSystem.Locator;
using UnityEngine;

namespace InteractionSystem.Handler
{
    internal class InteractionHandler : MonoBehaviour
    {
        private IInteractorAdapter _interactorAdapter;
        private IInteractableLocator _interactableLocator;

        private void Awake()
        {
            _interactorAdapter = GetComponentInChildren<IInteractorAdapter>();
            _interactableLocator = GetComponentInChildren<IInteractableLocator>();
        }

        public bool Interact()
        {
            return _interactorAdapter.Interact(_interactableLocator);
        }
    }
}