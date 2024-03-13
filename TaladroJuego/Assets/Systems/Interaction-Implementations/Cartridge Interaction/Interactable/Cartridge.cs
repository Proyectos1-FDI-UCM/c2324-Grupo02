using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractionImplementationsSystem.CartridgeInteraction.Interactable
{
    internal class Cartridge : MonoBehaviour
    {
        [SerializeField] private CollectableCartridge _collectableCartridge;

        public CollectableCartridge CollectableCartridge => _collectableCartridge;
    }
}

