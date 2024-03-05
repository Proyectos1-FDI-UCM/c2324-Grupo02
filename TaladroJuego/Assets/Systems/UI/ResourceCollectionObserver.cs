using ResourceCollectionSystem;
using System.Collections.Generic;
using TMPro;
using UISystem.Data;
using UnityEngine;
using UnityEngine.UI;
using UpgradesSystem.Flyweight;

namespace UISystem
{
    internal class ResourceCollectionObserver : MonoBehaviour
    {
        [SerializeField] private GameObject[] _squares;
        private ResourceSlot[] _slots;
        [SerializeField] private ResourceSpriteBinder _spriteBinder;
        [SerializeField] private Sprite _slotBackground;
        [SerializeField] private ResourcesContainer _container;


        private Dictionary<ResourceType, ResourceSlot> _resourceSlotPairs;

        private readonly struct ResourceSlot
        {
            public ResourceSlot(TMP_Text text, Image image, bool occupied)
            {
                Text = text;
                Image = image;
                Occupied = occupied;
            }

            public TMP_Text Text { get; }

            public Image Image { get; }

            public bool Occupied { get; }
        }

        private void Awake()
        {
            _container.ResourceModified.AddListener(UpdateInventory);
            _resourceSlotPairs = new Dictionary<ResourceType, ResourceSlot>();

            _slots = new ResourceSlot[_squares.Length];

            for (int i = 0; i < _squares.Length; i++)
            {
                GameObject square = _squares[i];

                _slots[i] = new ResourceSlot(square.GetComponentInChildren<TMP_Text>(), square.transform.GetChild(1).GetChild(0).GetComponent<Image>(), false);
            }
        }

        private void OnDestroy()
        {
            _container.ResourceModified.RemoveListener(UpdateInventory);
        }

        public void UpdateInventory(ResourceType resource, int quantity)
        {
            if (!_resourceSlotPairs.TryGetValue(resource, out ResourceSlot slot))
            {
                int i = 0;
                while (i < _slots.Length && _slots[i].Occupied) i++;

                _slots[i] = new ResourceSlot(_slots[i].Text, _slots[i].Image, true);
                _resourceSlotPairs[resource] = _slots[i];

                slot = _slots[i];

                _squares[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.white;
                _squares[i].transform.GetChild(1).GetComponent<Image>().sprite = _slotBackground;
            }

            if (_spriteBinder.TryGetSpriteFrom(resource, out Sprite sprite))
            {
                slot.Text.text = $"{quantity:D3}";
                slot.Image.sprite = sprite;
            }
        }
    }
}

