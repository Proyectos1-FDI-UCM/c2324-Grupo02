using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.Handler;
using TerrainSystem.Queue;
using TerrainSystem.Data.Flyweight;
using TerrainSystem.Data;

namespace InteractionImplementationsSystem.BombInteraction.Interactable
{
    internal class Explosive : MonoBehaviour
    {
        [SerializeField] private DamageHandler _damageHandler;
        private ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>> _terrainModificationEnqueuer;
        [SerializeField] private QueueableTerrainModification _terrainModification;

        private void Awake()
        {
            _terrainModificationEnqueuer = GetComponent<ITerrainModificationEnqueuer<ITerrainModificationSourceFlyweight<TerrainModificationSource>>>();
        }

        public bool Explode()
        {
            //_damageHandler.Damage();
            foreach (var damageable in _damageHandler.Damage())
            {
                Debug.Log(damageable);
            }
            _terrainModification.AcceptEnqeue(_terrainModificationEnqueuer);
            StartCoroutine(DeQueue());

            return true;
        }

        private IEnumerator DeQueue()
        {
            yield return new WaitForEndOfFrame();
            _terrainModification.AcceptDequeue(_terrainModificationEnqueuer);
        }
    }
}

