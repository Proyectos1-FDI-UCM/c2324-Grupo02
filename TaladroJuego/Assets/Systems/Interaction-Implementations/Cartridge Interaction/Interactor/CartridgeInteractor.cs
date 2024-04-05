using InteractionImplementationsSystem.BombInteraction.Interactable;
using InteractionImplementationsSystem.CartridgeInteraction.Container;
using InteractionImplementationsSystem.CartridgeInteraction.Interactable;
using InteractionSystem.Handler;
using InteractionSystem.Interactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionImplementationsSystem.CartridgeInteraction.Interactor
{
    internal class CartridgeInteractor : MonoBehaviour, IInteractor<Cartridge>, IInteractor
    {
        [SerializeField] private CartridgesCollector _cartridgesCollector;
        private Interactor<Cartridge> _interactor;

        private void Awake()
        {
            _interactor = new Interactor<Cartridge>(this);
        }

        public bool InteractWith(Cartridge cartridge)
        {
            Debug.Log("Hemos llegado al interactor");

            return _cartridgesCollector.TryRegisterCartridge(cartridge) && cartridge.PlayCartridge();
        }

        public bool Accept(IInteractorHandler handler)
        {
            return _interactor.Accept(handler);
        }
    }
}

