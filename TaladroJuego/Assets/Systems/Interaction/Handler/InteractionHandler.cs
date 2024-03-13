using InteractionSystem.Locator;
using UnityEngine;

namespace InteractionSystem.Handler
{
    internal class InteractionHandler : MonoBehaviour
    {
        private IInteractableLocator _interactableLocator;

        private void Awake()
        {
            _interactableLocator = GetComponentInChildren<IInteractableLocator>();
        }

        public bool Interact()
        {
            return true;
        }
    }
}