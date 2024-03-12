using InteractionImplementationsSystem.CartridgeInteraction.Interactable;
using InteractionSystem.Interactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionImplementationsSystem.CartridgeInteraction.Interactor
{
    internal class CartridgeInteractor : MonoBehaviour, IInteractor<Cartridge>
    {
        public bool InteractWith(Cartridge cartridge)
        {
            return cartridge.PlayCartridge();
        }
    }
}

