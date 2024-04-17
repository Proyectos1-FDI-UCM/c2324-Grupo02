using InteractionImplementationsSystem.BombInteraction.Interactable;
using InteractionSystem.Handler;
using InteractionSystem.Interactor;
using UnityEngine;

namespace InteractionImplementationsSystem.BombInteraction.Interactor
{
    internal class ExplosiveInteractor : MonoBehaviour, IInteractor, IInteractor<Explosive>
    {
        private Interactor<Explosive> _interactor;

        private void Awake()
        {
            _interactor = new Interactor<Explosive>(this);
        }

        public bool InteractWith(Explosive explosive)
        {
            Debug.Log("aqui", this);
            return explosive.Explode();
        }

        public bool Accept(IInteractorHandler handler)
        {
            return _interactor.Accept(handler);
        }
    }
}

