using InteractionSystem.Interactable;
using System.Collections.Generic;
using System.Linq;

namespace InteractionSystem.Locator
{
    internal readonly struct InteractableLocator : IInteractableLocator
    {
        private readonly List<IInteractable> _objects;

        public InteractableLocator(List<IInteractable> objects)
        {
            _objects = objects;
        }

        public IEnumerable<IInteractable<T>> GetInteractables<T>() => _objects.OfType<IInteractable<T>>();
    }
}