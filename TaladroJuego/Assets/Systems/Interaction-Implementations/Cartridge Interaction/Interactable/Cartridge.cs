using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContextualDialogueSystem.Event;

namespace InteractionImplementationsSystem.CartridgeInteraction.Interactable
{
    public class Cartridge : MonoBehaviour
    {
        [SerializeField] private DialogueEventObject _dialogEvent;
        [SerializeField] private PauseRequesterObject _pauseRequester;
        [SerializeField] private GameObject _cartridgeCanvas;
        [SerializeField] private string _chapterTextInfo, _descriptionTextInfo;

        public string ChapterTextInfo {get => _chapterTextInfo;}
        public string DescriptionTextInfo {get => _descriptionTextInfo;}

        public bool PlayCartridge()
        {
            _pauseRequester.RequestPause();
            _cartridgeCanvas.SetActive(true);
            _dialogEvent.Dispatch();
            if(gameObject != null) Destroy(gameObject, 0.05f);
            return true;
        }
    }
}

