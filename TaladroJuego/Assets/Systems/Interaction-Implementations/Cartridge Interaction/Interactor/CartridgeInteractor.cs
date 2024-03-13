using Codice.CM.SEIDInfo;
using InteractionImplementationsSystem.CartridgeInteraction.Interactable;
using InteractionSystem.Handler;
using InteractionSystem.Interactor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CartridgeCollectionSystem;

namespace InteractionImplementationsSystem.CartridgeInteraction.Interactor
{
    internal class CartridgeInteractor : MonoBehaviour, IInteractor<Cartridge>, IInteractor
    {
        private Interactor<Cartridge> _interactor;
        private PauseRequesterObject _pauseRequester;
        private CartridgeContainer _container; 

        public bool Accept(IInteractorHandler handler)
        {
            return _interactor.Accept(handler);
        }

        public bool InteractWith(Cartridge cartridge)
        {
            _pauseRequester.RequestPause();
            CollectableCartridge collectableCartridge = cartridge.CollectableCartridge;
            return _container.TryAddCartridge(collectableCartridge) && collectableCartridge.PlayCartridge();
        }

        private void Awake()
        {
            _interactor = new Interactor<Cartridge>(this);
        }
    }
}

