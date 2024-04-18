using System;
using System.Collections.Generic;
using TerrainSystem.Requestable.Retriever.Observable;
using UnityEngine;
using UpgradesSystem.Flyweight;

namespace TerrainResourcesSystem
{
    public class ResourcesModificationObservable : MonoBehaviour, IObservableTerrainData<ResourceQuantityItem>
    {
        [SerializeField]
        private ResourceTerrainTypeBinder _resourceTerrainTypeBinder;

        [SerializeField]
        private float _conversionRate;
        private readonly Dictionary<uint, TerrainModification> _cumulatedModifications = new Dictionary<uint, TerrainModification>();

        private IObservableTerrainData<TerrainModification> _modificationsTerrainData;

        public float ConversionRate
        {
            get => _conversionRate;
            set => _conversionRate = value; 
        }

        public event EventHandler<ResourceQuantityItem> DataRetrieved;

        private void Awake()
        {
            _modificationsTerrainData = GetComponentInChildren<IObservableTerrainData<TerrainModification>>();
            _modificationsTerrainData.DataRetrieved += OnDataRetrieved;
        }

        private void OnDestroy()
        {
            _modificationsTerrainData.DataRetrieved -= OnDataRetrieved;
        }

        private void OnDataRetrieved(object sender, TerrainModification e)
        {
            if (e.modificationSourceIndex != 2) return;

            TerrainModification cumulated = AccountForTerrainModification(e);
            if (ConvertsToResource(cumulated, out float excess, out ResourceQuantityItem resourceQuantityItem))
                DataRetrieved?.Invoke(this, resourceQuantityItem);

            _cumulatedModifications[e.terrainType] = e.WithAmount(excess);
        }

        private TerrainModification AccountForTerrainModification(TerrainModification e)
        {
            if (!_cumulatedModifications.TryAdd(e.terrainType, e))
                _cumulatedModifications[e.terrainType] = e.WithAmount(e.amount + _cumulatedModifications[e.terrainType].amount);

            return _cumulatedModifications[e.terrainType];
        }

        private bool ConvertsToResource(TerrainModification modification, out float excess, out ResourceQuantityItem resourceQuantityItem) =>
            TryGetFromModification(modification, out excess, out resourceQuantityItem)
            && resourceQuantityItem.Quantity > 0;

        private bool TryGetFromModification(TerrainModification modification, out float excess, out ResourceQuantityItem resourceQuantityItem)
        {
            float convertedAmount = modification.amount * ConversionRate;
            int amount = Mathf.FloorToInt(convertedAmount);

            excess = (convertedAmount - amount) / ConversionRate;

            bool exists = _resourceTerrainTypeBinder.TryGetResourceTypeFrom(modification.terrainType, out ResourceType type);
            resourceQuantityItem = new ResourceQuantityItem(type, amount);
            return exists;
        }
    }
}