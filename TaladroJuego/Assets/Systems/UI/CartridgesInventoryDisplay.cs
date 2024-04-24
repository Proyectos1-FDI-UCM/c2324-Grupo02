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
        private readonly struct CartridgeText
        {
            public readonly string ChapterText, DescriptionText;

            public CartridgeText(string chapterText, string descriptionText)
            {
                ChapterText = chapterText;
                DescriptionText = descriptionText;
            }
        }
        private readonly List<CartridgeText> _cartridgesInfo = new List<CartridgeText>();

        //Text in UI
        [SerializeField] private TMP_Text _topText, _bottomText;
        [SerializeField] private Transform[] navigationHintArray;
        [SerializeField] private Transform hintSelector;

        //Index of the cartridge displayed
        private int _index = 0;
        private void Awake()
        {
            _collector.ContainerUpdatedEvent.AddListener(UpdateInventory);
        }

        private void UpdateInventory(IReadOnlyList<Cartridge> cartridges)
        {
            _cartridgesInfo.Clear();
            CartridgeText[] cartridgeTexts = new CartridgeText[cartridges.Count];
            for(int i = 0; i < cartridges.Count; i++)
            {
                cartridgeTexts[i] = new CartridgeText(cartridges[i].ChapterTextInfo, cartridges[i].DescriptionTextInfo);
            }
            _cartridgesInfo.AddRange(cartridgeTexts);

            //Show cartridge
            _index = _cartridgesInfo.Count - 1;
            ChangeCartridgeDisplayed();
            ChangeNavigationHint();
        }

        public void ShowNextCartridge()
        {
            if (_index + 1 < _cartridgesInfo.Count)
            {
                _index++;
                ChangeCartridgeDisplayed();
                ChangeNavigationHint();
            }
        }

        public void ShowPreviousCartridge() 
        {
            if (_index - 1 >= 0)
            {
                _index--;
                ChangeCartridgeDisplayed();
                ChangeNavigationHint();
            } 
            
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

        private void ChangeNavigationHint()
        {
            hintSelector.SetParent(navigationHintArray[_index], false);
            //hintSelector.position = Vector3.zero;
        }
    }
}

