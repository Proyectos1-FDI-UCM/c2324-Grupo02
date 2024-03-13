using Codice.CM.SEIDInfo;
using ContextualDialogueSystem.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cartridges/Collectable Cartridge", fileName = "Collectable Cartridge")]
public class CollectableCartridge : ScriptableObject
{
    [SerializeField] private DialogueEventObject _dialogEvent;

    public bool PlayCartridge()
    {
        _dialogEvent.Dispatch();
        return true;
    }
}
