using InteractionImplementationsSystem.CartridgeInteraction.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InteractionImplementationsSystem.CartridgeInteraction.Container
{
    [CreateAssetMenu(menuName = "Cartridges Collector", fileName = "Cartridges Collector")]
    public class CartridgesCollector : ScriptableObject
    {
        private List<Cartridge> _cartridges;
        public UnityEvent<Cartridge> ContainerUpdatedEvent;

        public bool TryRegisterCartridge(Cartridge cartridge)
        {
            if (_cartridges.IndexOf(cartridge) != -1)
            {
                _cartridges.Add(cartridge);
                ContainerUpdatedEvent.Invoke(cartridge);
                return true;
            }
            return false;
        }

        public bool TryPlayCartridgeOfIndex(int index)
        {
            if(index < _cartridges.Count)
            {
                return _cartridges[index].PlayCartridge();
            }
            return false;
        }
    }
}

