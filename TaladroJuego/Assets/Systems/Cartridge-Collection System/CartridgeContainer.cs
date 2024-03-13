using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CartridgeCollectionSystem
{
    [CreateAssetMenu(menuName = "Cartridges/Cartridge Container", fileName = "Cartridge Container")]
    public class CartridgeContainer : ScriptableObject
    {
        private HashSet<CollectableCartridge> _collectableCartridges; 
        public bool TryAddCartridge(CollectableCartridge cartridge)
        {
            return _collectableCartridges.Add(cartridge);
        }
    }
}

