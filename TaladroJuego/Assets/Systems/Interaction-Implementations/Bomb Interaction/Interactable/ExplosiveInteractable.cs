using InteractionSystem.Interactable;
using InteractionSystem.Interactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionImplementationsSystem.BombInteraction.Interactable
{
    internal class ExplosiveInteractable : MonoBehaviour, IInteractable<Explosive>
    {
        [SerializeField] private Explosive _explosive;
        public bool Accept<TInteractor>(TInteractor interactor) where TInteractor : IInteractor<Explosive>
        {
            return interactor.InteractWith(_explosive);
        }
    }
}

