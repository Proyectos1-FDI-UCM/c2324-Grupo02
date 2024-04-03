using InteractionImplementationsSystem.CartridgeInteraction.Container;
using InteractionImplementationsSystem.CartridgeInteraction.Interactable;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UISystem
{
    internal class CartridgesInventoryDisplay : MonoBehaviour
    {
        //cartridge inventory
        [SerializeField] private CartridgesCollector _collector;

        //List containing the text of each found cartridge
        private struct CartridgeText
        {
            public string ChapterText, DescriptionText;
        }
        private List<CartridgeText> _cartridgesInfo;

        //Text in UI
        [SerializeField] private TMP_Text _topText, _bottomText;

        //Index of the cartridge displayed
        private int _index = 0;

        private void UpdateInventory(Cartridge cartridge)
        {
            //Add cartridge info
            CartridgeText ct;
            ct.ChapterText = cartridge.ChapterTextInfo;
            ct.DescriptionText = cartridge.DescriptionTextInfo;
            _cartridgesInfo.Add(ct);

            //Show cartridge
            _index = _cartridgesInfo.Count - 1;
            ChangeCartridgeDisplayed();
        }

        public void ShowNextCartridge()
        {
            if (_index + 1 <= _cartridgesInfo.Count) _index++;
            ChangeCartridgeDisplayed();
        }

        public void ShowPreviousCartridge() 
        {
            if (_index - 1 >= 0) _index--;
            ChangeCartridgeDisplayed();
        }

        public void PlayCurrentCartridge()
        {
            _collector.TryPlayCartridgeOfIndex(_index);
        }

        private void ChangeCartridgeDisplayed()
        {
            _topText.text = _cartridgesInfo[_index].ChapterText;
            _bottomText.text = _cartridgesInfo[_index].DescriptionText;
        }
    }
}

