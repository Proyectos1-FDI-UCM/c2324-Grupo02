using InteractionImplementationsSystem.BombInteraction.Interactable;
using InteractionSystem.Handler;
using InteractionSystem.Interactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionImplementationsSystem.BombInteraction.Interactor
{
    internal class ExplosiveInteractor : MonoBehaviour, IInteractor<Explosive>, IInteractor
    {
        private Interactor<Explosive> _interactor;

        public bool Accept(IInteractorHandler handler)
        {
            return _interactor.Accept(handler);
        }

        public bool InteractWith(Explosive explosive)
        {
            return explosive.Explode();
        }

        private void Awake()
        {
            _interactor = new Interactor<Explosive>(this);
        }
    }
}

