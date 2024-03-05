using ResourceCollectionSystem;
using TerrainSystem.Requestable.Retriever.Observable;
using UnityEngine;
using UpgradesSystem.Flyweight;

namespace TerrainResourcesSystem
{
    internal class TerrainResourcesObserver : MonoBehaviour
    {
        [SerializeField]
        private ResourcesContainer _resourcesContainer;

        private IObservableTerrainData<ResourceQuantityItem> _observable;

        private void Awake()
        {
            _observable = GetComponentInChildren<IObservableTerrainData<ResourceQuantityItem>>();
            _observable.DataRetrieved += OnDataRetrieved;
        }

        private void OnDestroy()
        {
            _observable.DataRetrieved -= OnDataRetrieved;
        }

        private void OnDataRetrieved(object sender, ResourceQuantityItem e) =>
            _resourcesContainer.AccountForResource(e.Resource, e.Quantity);
    }
}