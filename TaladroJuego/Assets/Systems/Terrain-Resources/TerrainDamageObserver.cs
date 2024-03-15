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
        private void Awake()
        {
            
            
           
            _modificator= GetComponent<IObservableTerrainData<TerrainModification>>();
            _modificator.DataRetrieved += OnDataRetrieve;
        }


        private void OnDestroy()
        {
            _modificator.DataRetrieved -= OnDataRetrieve;   
        }       
        private void OnDataRetrieve(object sender,TerrainModification modificator)
        {
            if (modificator.terrainType == harmfullterraintype)
            {
                _reducer.ReduceStatus();
            }
        }
    }
}

