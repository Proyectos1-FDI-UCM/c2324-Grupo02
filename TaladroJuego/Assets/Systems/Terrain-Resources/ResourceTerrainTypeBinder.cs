using System;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Flyweight;

namespace TerrainResourcesSystem
{   
    [CreateAssetMenu(fileName = "ResourceTerrainTypeBinder", menuName = "Terrain-Resources/ResourceTerrainTypeBinder")]
    internal class ResourceTerrainTypeBinder : ScriptableObject
    {
        [Serializable]
        private struct ResourceTerrainTypePair
        {
            [field: SerializeField]
            public ResourceType ResourceType { get; private set; }

            [field: SerializeField]
            [field: Min(0)]
            public int TerrainType { get; private set; }

            public static Dictionary<ResourceType, uint> DictionaryFrom(IEnumerable<ResourceTerrainTypePair> pairs)
            {
                Dictionary<ResourceType, uint> dictionary = new Dictionary<ResourceType, uint>();
                foreach (var pair in pairs)
                    dictionary[pair.ResourceType] = (uint)pair.TerrainType;
                return dictionary;
            }
        }

        [SerializeField]
        private ResourceTerrainTypePair[] _resourceTerrainTypePairs;

        private Dictionary<ResourceType, uint> _pairs;
        private Dictionary<uint, ResourceType> _reversePairs;

        private void OnEnable()
        {
            _pairs = ResourceTerrainTypePair.DictionaryFrom(_resourceTerrainTypePairs);
            _reversePairs = new Dictionary<uint, ResourceType>();
            foreach (var pair in _pairs)
                _reversePairs[pair.Value] = pair.Key;
        }

        public bool TryGetTerrainTypeFrom(ResourceType type, out uint terrainType) =>
            _pairs.TryGetValue(type, out terrainType);

        public bool TryGetResourceTypeFrom(uint terrainType, out ResourceType type) =>
            _reversePairs.TryGetValue(terrainType, out type);
    }
}