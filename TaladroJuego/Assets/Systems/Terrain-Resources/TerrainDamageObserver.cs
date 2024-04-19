using StatusSystem;
using System.Collections;
using System.Collections.Generic;
using TerrainSystem.Requestable.Retriever.Observable;
using UnityEngine;
using UpgradesSystem.Flyweight;
using DamageSystem.Damager;

namespace TerrainResourcesSystem
{
    public class TerrainDamageObserver : MonoBehaviour
    {
        
       
       
        private IObservableTerrainData<TerrainModification> _modificator;
        [SerializeField] private OnMovementStatusReducer _reducer;
        [SerializeField] private int harmfullterraintype;

        [SerializeField]
        private int[] _damageableModificationSourcesIndices;
        private HashSet<int> _damageables;

        private void Awake()
        {
            _damageables = new HashSet<int>(_damageableModificationSourcesIndices);
            
           
            _modificator= GetComponent<IObservableTerrainData<TerrainModification>>();
            _modificator.DataRetrieved += OnDataRetrieve;
        }


        private void OnDestroy()
        {
            _modificator.DataRetrieved -= OnDataRetrieve;   
        }       
        private void OnDataRetrieve(object sender,TerrainModification modificator)
        {
            if (_damageables.Contains((int)modificator.modificationSourceIndex)
                && modificator.terrainType == harmfullterraintype)
            {
                _reducer.ReduceStatus();
            }
        }
    }
}

