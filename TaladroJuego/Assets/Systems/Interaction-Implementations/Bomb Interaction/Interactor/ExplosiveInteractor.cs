using InteractionImplementationsSystem.BombInteraction.Interactable;
using InteractionSystem.Interactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionImplementationsSystem.BombInteraction.Interactor
{
    internal class ExplosiveInteractor : MonoBehaviour, IInteractor<Explosive>
    {
        public bool InteractWith(Explosive explosive)
        {
            return explosive.Explode();
        }
    }
}

