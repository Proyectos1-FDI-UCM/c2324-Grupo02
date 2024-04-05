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
        private readonly List<Cartridge> _cartridges = new List<Cartridge>();
        public UnityEvent<IReadOnlyList<Cartridge>> ContainerUpdatedEvent;

        public bool TryRegisterCartridge(Cartridge cartridge)
        {
            if (!_cartridges.Contains(cartridge))
            {
                _cartridges.Add(cartridge);
                ContainerUpdatedEvent.Invoke(_cartridges);
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

