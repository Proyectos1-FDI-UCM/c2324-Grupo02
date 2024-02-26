using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Playables;
using ResourceCollectionSystem;
using UpgradesSystem.Flyweight;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    internal class ResourceCollectionObserver : MonoBehaviour
    {
        [SerializeField] private GameObject[] _squares;
        private ResourceSlot[] _slots;
        [SerializeField] Sprite[] _sprites;
        [SerializeField] Sprite _emptySlot;
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

                _slots[i] = new ResourceSlot(square.GetComponentInChildren<TMP_Text>(), square.GetComponentInChildren<Image>(), false);
            }

            
                
            
        }
        //public void UptdateInventory(ResourceType resource, int quantity)
        //{
        //    if (quantity == 0)
        //    {
        //        _squares[(int) resource].GetComponentInChildren<TMP_Text>().text = string.Empty;
        //        _squares[(int)resource].GetComponentInChildren<Image>().sprite = _sprites[_sprites.Length - 1];
        //    }
        //    else
        //    {
        //        _squares[(int)resource].GetComponentInChildren<TMP_Text>().text = "x" + quantity.ToString();
        //        _squares[(int)resource].GetComponentInChildren<Image>().sprite = _sprites[(int)resource];
        //    }
        //}

        public void UpdateInventory(ResourceType resource, int quantity)
        {
            if(_resourceSlotPairs.TryGetValue(resource, out ResourceSlot slot))
            {
                if(quantity != 0)
                {
                    slot.Text.text = $"x{quantity}";
                    slot.Image.sprite = _sprites[(int)resource];
                }
                else
                {
                    slot.Text.text = string.Empty;
                    slot.Image.sprite = _emptySlot;
                }
            }
            else
            {
                int i = 0;
                while (i < _slots.Length && _slots[i].Occupied) i++;

                _slots[i] = new ResourceSlot(_slots[i].Text, _slots[i].Image, true);
                _resourceSlotPairs[resource] = _slots[i];

                UpdateInventory(resource, quantity);
            }
        }
    }
}

