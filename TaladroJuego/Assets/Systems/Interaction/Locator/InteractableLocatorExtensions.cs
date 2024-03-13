using UnityEngine;

namespace InteractionSystem.Locator
{
    internal static class InteractableLocatorExtensions
    {
        public static bool TryGetComponentInChildren<T>(GameObject parent, out T component) =>
            (component = parent.GetComponentInChildren<T>()) != null;

        public static bool InLayerMask(this GameObject gameObject, LayerMask layerMask) =>
            ((1 << gameObject.layer) & layerMask) != 0;
    }
}