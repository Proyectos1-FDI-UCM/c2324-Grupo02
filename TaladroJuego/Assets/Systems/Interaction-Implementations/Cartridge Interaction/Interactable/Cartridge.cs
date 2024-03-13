using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextualDialogueSystem.Event;

namespace InteractionImplementationsSystem.CartridgeInteraction.Interactable
{
    internal class Cartridge : MonoBehaviour
    {
        [SerializeField] private DialogueEventObject _dialogEvent;
        [SerializeField] private PauseRequesterObject _pauseRequester;
        //inventario de cartuchos

        public bool PlayCartridge()
        {
            //añadir cartucho al inventario
            _pauseRequester.RequestPause();
            _dialogEvent.Dispatch();

            return true;
        }
    }
}

