using InteractionSystem.Interactable;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem.Locator
{
    internal class ColliderInteractableLocator : MonoBehaviour, IInteractableLocator
    {
        [SerializeField]
        private LayerMask _layerMask;

        [field: SerializeField]
        public UnityEvent<IInteractable> InteractableAdded { get; private set; }
        [field: SerializeField]
        public UnityEvent<IInteractable> InteractableRemoved { get; private set; }

        private List<IInteractable> _foundObjects;
        private InteractableLocator _interactableLocator;

        private void Awake()
        {
            _foundObjects = new List<IInteractable>();
            _interactableLocator = new InteractableLocator(_foundObjects);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (InteractableLocatorExtensions.InLayerMask(collision.gameObject, _layerMask)
                && InteractableLocatorExtensions.TryGetComponentInChildren(collision.gameObject, out IInteractable interactable))
            {
                _foundObjects.Add(interactable);
                InteractableAdded.Invoke(interactable);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (InteractableLocatorExtensions.InLayerMask(collision.gameObject, _layerMask)
                && InteractableLocatorExtensions.TryGetComponentInChildren(collision.gameObject, out IInteractable interactable))
            {
                _foundObjects.Remove(interactable);
                InteractableRemoved.Invoke(interactable);
            }
        }

        public IEnumerable<IInteractable<T>> GetInteractables<T>() =>
            _interactableLocator.GetInteractables<T>();
    }
}