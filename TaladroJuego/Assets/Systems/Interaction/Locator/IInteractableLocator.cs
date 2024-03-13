using InteractionSystem.Interactable;
using System.Collections.Generic;

namespace InteractionSystem.Locator
{
    public interface IInteractableLocator
    {
        IEnumerable<IInteractable<T>> GetInteractables<T>();
    }
}