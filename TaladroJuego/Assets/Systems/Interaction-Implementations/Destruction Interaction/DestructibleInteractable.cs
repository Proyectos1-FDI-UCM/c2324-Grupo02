using InteractionSystem.Interactable;
using InteractionSystem.Interactor;
using UnityEngine;

namespace InteractionImplementationsSystem.DesctructionInteraction.Interactable
{
    internal class DestructibleInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private GameObject _gameObject;

        [SerializeField]
        [Min(0.0f)]
        private float _destructionDelay;

        public bool Accept<T>(IInteractor<T> interactor)
        {
            Destroy(_gameObject, _destructionDelay);
            return true;
        }
    }
}

