using ResourceCollectionSystem;
using System.Collections.Generic;
using TerrainSystem.Requestable.Retriever.Observable;
using UnityEngine;
using UpgradesSystem.Flyweight;

namespace TerrainResourcesSystem
{
    internal class ResourcesModificationObserver : MonoBehaviour
    {
        [SerializeField]
        private ResourcesContainer _resourcesContainer;

        [SerializeField]
        private float _conversionRate;
        private readonly Dictionary<uint, TerrainModification> _cumulatedModifications = new Dictionary<uint, TerrainModification>();

        private IObservableTerrainData<TerrainModification> _modificationsTerrainData;

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
            TerrainModification cumulated = AccountForTerrainModification(e);
            //Debug.Log($"Cumulated: {cumulated.terrainType} - {cumulated.amount}");
            if (ConvertsToResource(cumulated, out float excess, out ResourceQuantityItem resourceQuantityItem))
                _resourcesContainer.AccountForResource(resourceQuantityItem.Resource, resourceQuantityItem.Quantity);

            _cumulatedModifications[e.terrainType] = new TerrainModification(e.terrainType, excess);

            bool ConvertsToResource(TerrainModification modification, out float excess, out ResourceQuantityItem resourceQuantityItem)
            {
                resourceQuantityItem = FromModification(modification, out excess);
                return resourceQuantityItem.Quantity > 0;
            }
        }

        private TerrainModification AccountForTerrainModification(TerrainModification e)
        {
            if (!_cumulatedModifications.TryAdd(e.terrainType, e))
                _cumulatedModifications[e.terrainType] = new TerrainModification(e.terrainType, e.amount + _cumulatedModifications[e.terrainType].amount);

            return _cumulatedModifications[e.terrainType];
        }

        private ResourceQuantityItem FromModification(TerrainModification modification, out float excess)
        {
            float convertedAmount = modification.amount * _conversionRate;
            int amount = Mathf.FloorToInt(convertedAmount);

            excess = (convertedAmount - amount) / _conversionRate;
            // TODO - Dictionary
            return new ResourceQuantityItem((ResourceType)modification.terrainType, amount);
        }
    }
}