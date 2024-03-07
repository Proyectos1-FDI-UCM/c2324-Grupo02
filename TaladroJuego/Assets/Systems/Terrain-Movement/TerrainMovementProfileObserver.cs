using MovementSystem.Profile;
using System;
using System.Collections.Generic;
using TerrainSystem.Requestable.Retriever.Observable;
using UnityEngine;

namespace TerrainMovementSystem
{
    internal class TerrainMovementProfileObserver : MonoBehaviour
    {
        [Serializable]
        private struct MovementProfileTerrainTypePair
        {
            [field: SerializeField]
            public SpeedProfile MovementProfile { get; private set; }

            [field: SerializeField]
            [field: Min(0)]
            public int TerrainType { get; private set; }
            public MovementProfileTerrainTypePair(SpeedProfile movementProfile, int terrainType)
            {
                MovementProfile = movementProfile;
                TerrainType = terrainType;
            }

            public static Dictionary<uint, SpeedProfile> DictionaryFrom(IEnumerable<MovementProfileTerrainTypePair> pairs)
            {
                var dictionary = new Dictionary<uint, SpeedProfile>();
                foreach (var pair in pairs)
                    dictionary[(uint)pair.TerrainType] = pair.MovementProfile;
                return dictionary;
            }
        }

        [SerializeField]
        private MovementProfileTerrainTypePair[] _terrainTypeSpeedProfilesPairs;
        [SerializeField]
        private SpeedRegister _speedRegister;

        private IObservableTerrainData<TerrainModification> _observable;
        private Dictionary<uint, SpeedProfile> _terrainTypeSpeedProfiles;

        private void Awake()
        {
            _terrainTypeSpeedProfiles = MovementProfileTerrainTypePair.DictionaryFrom(_terrainTypeSpeedProfilesPairs);

            _observable = GetComponentInChildren<IObservableTerrainData<TerrainModification>>();
            _observable.DataRetrieved += OnDataRetrieved;
        }

        private void OnDestroy()
        {
            _observable.DataRetrieved -= OnDataRetrieved;
        }

        private void OnDataRetrieved(object sender, TerrainModification e)
        {
            if (_terrainTypeSpeedProfiles.TryGetValue(e.terrainType, out var speedProfile))
                _speedRegister.SpeedProvider = speedProfile;
        }
    }
}