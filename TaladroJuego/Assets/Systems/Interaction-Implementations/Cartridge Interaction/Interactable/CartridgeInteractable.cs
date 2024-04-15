using InteractionSystem.Interactable;
using InteractionSystem.Interactor;
using System.Linq;
using UnityEngine;

namespace InteractionImplementationsSystem.CartridgeInteraction.Interactable
{
    internal class CartridgeInteractable : MonoBehaviour, IInteractable<Cartridge>
    {
        [SerializeField]
        private Cartridge _cartridge;
        private IInteractable _destructionInteractable;

        private void Awake()
        {
            _destructionInteractable = GetComponentsInChildren<IInteractable>().FirstOrDefault(i => i != (IInteractable)this);
        }

        public bool Accept<TInteractor>(TInteractor interactor) where TInteractor : IInteractor<Cartridge>
        {
            return interactor.InteractWith(_cartridge) && (_destructionInteractable?.Accept(interactor) ?? true);
        }
    }
}

