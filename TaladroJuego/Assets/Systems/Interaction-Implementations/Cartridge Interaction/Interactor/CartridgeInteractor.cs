using InteractionImplementationsSystem.CartridgeInteraction.Container;
using InteractionImplementationsSystem.CartridgeInteraction.Interactable;
using InteractionSystem.Interactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionImplementationsSystem.CartridgeInteraction.Interactor
{
    internal class CartridgeInteractor : MonoBehaviour, IInteractor<Cartridge>
    {
        [SerializeField] private CartridgesCollector _cartridgesCollector;
        public bool InteractWith(Cartridge cartridge)
        {
            return _cartridgesCollector.TryRegisterCartridge(cartridge) && cartridge.PlayCartridge();
        }
    }
}

