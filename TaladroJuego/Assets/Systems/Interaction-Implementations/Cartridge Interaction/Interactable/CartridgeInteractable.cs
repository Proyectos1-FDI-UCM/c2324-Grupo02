using InteractionSystem.Interactable;
using InteractionSystem.Interactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionImplementationsSystem.CartridgeInteraction.Interactable
{
    internal class CartridgeInteractable : MonoBehaviour, IInteractable<Cartridge>
    {
        [SerializeField] private Cartridge _cartridge;   
        public bool Accept<TInteractor>(TInteractor interactor) where TInteractor : IInteractor<Cartridge>
        {
            return interactor.InteractWith(_cartridge);
        }
    }
}

