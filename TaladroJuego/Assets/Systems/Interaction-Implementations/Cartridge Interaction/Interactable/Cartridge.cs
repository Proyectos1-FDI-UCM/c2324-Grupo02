using UnityEngine;
using ContextualDialogueSystem.Event;

namespace InteractionImplementationsSystem.CartridgeInteraction.Interactable
{
    public class Cartridge : MonoBehaviour
    {
        [SerializeField] private DialogueEventObject _dialogEvent;
        [SerializeField] private PauseRequesterObject _pauseRequester;
        [SerializeField] private string _chapterTextInfo, _descriptionTextInfo;

        public string ChapterTextInfo {get => _chapterTextInfo;}
        public string DescriptionTextInfo {get => _descriptionTextInfo;}

        public bool PlayCartridge()
        {
            _pauseRequester.RequestPause();
            _dialogEvent.Dispatch();
            return true;
        }
    }
}

