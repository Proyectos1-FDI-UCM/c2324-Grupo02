using System;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Flyweight;

namespace UISystem.Data
{
    [CreateAssetMenu(fileName = "ResourceSpriteBinder", menuName = "UI/Data/ResourceSpriteBinder")]
    internal class ResourceSpriteBinder : ScriptableObject
    {
        [Serializable]
        private struct ResourceSpritePair
        {
            [field: SerializeField]
            public ResourceType Type { get; private set; }

            [field: SerializeField]
            public Sprite Sprite { get; private set; }

            public static Dictionary<ResourceType, Sprite> DictionaryFrom(IEnumerable<ResourceSpritePair> pairs)
            {
                Dictionary<ResourceType, Sprite> dictionary = new Dictionary<ResourceType, Sprite>();
                foreach (var pair in pairs)
                    dictionary[pair.Type] = pair.Sprite;
                return dictionary;
            }
        }

        [SerializeField]
        private ResourceSpritePair[] _resourceSpritePairs;
        private Dictionary<ResourceType, Sprite> _pairs;

        private void OnEnable()
        {
            _pairs = ResourceSpritePair.DictionaryFrom(_resourceSpritePairs);
        }

        public bool TryGetSpriteFrom(ResourceType type, out Sprite sprite) =>
            _pairs.TryGetValue(type, out sprite);
    }
}