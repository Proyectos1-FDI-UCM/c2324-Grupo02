using InteractionSystem.Interactable;
using InteractionSystem.Interactor;
using System.Linq;
using UnityEngine;

namespace InteractionImplementationsSystem.BombInteraction.Interactable
{
    internal class ExplosiveInteractable : MonoBehaviour, IInteractable<Explosive>
    {
        [SerializeField]
        private Explosive _explosive;
        private IInteractable _destructionInteractable;

        private void Awake()
        {
            _destructionInteractable = GetComponentsInChildren<IInteractable>().FirstOrDefault(i => i != (IInteractable)this);
        }

        public bool Accept<TInteractor>(TInteractor interactor) where TInteractor : IInteractor<Explosive>
        {
            Debug.Log("Aqui", this);
            return interactor.InteractWith(_explosive) && _destructionInteractable.Accept(interactor);
        }
    }
}

