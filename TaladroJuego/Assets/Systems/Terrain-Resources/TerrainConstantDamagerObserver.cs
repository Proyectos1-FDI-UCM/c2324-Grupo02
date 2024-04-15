using StatusSystem;
using System.Collections;
using System.Collections.Generic;
using TerrainSystem.Requestable.Retriever.Observable;
using UnityEngine;
using UpgradesSystem.Flyweight;
using DamageSystem.Damager;

namespace TerrainResourcesSystem
{
    public class TerrainConstantDamageObserver : MonoBehaviour
    {



        private IObservableTerrainData<TerrainModification> _terrainModificator;
        [SerializeField] private OnMovementStatusReducer _healthReducer;
        [SerializeField] private int damageterraintype;
        [SerializeField] private float venomtime;
        [SerializeField] private int venomnumber;
        private void Awake()
        {



            _terrainModificator = GetComponent<IObservableTerrainData<TerrainModification>>();
            _terrainModificator.DataRetrieved += OnDataRetrieve;
        }


        private void OnDestroy()
        {
            _terrainModificator.DataRetrieved -= OnDataRetrieve;
        }
        private void OnDataRetrieve(object sender, TerrainModification modificator)
        {
            
            int tick = 0; 
            float ticktime = 0;

            while (tick != venomnumber)
             {
                
                ticktime = ticktime * Time.deltaTime;
                if (ticktime >= venomtime)
                {
                    if (modificator.terrainType == damageterraintype)
                    {
                        _healthReducer.ReduceStatus();
                    }
                    tick++;
                    ticktime = 0;
                }

             }
           
             
               
            
           
        }
    }
}