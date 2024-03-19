using UnityEngine;

using TerrainSystem.Requestable.Retriever.Observable;
using System.Collections.Generic;
using UpgradesSystem.Flyweight;
using System;

namespace Particles
{
    [RequireComponent(typeof(ParticleSystem))]
    internal class ParticlesSwitch : MonoBehaviour
    {

        // Enumerador para definir todos los tipos de terreno.
        // Debería estar asociado al archivo "Terrain Texture Array"
        [Serializable]
        public enum TerrainType
        {
            Ground1,
            Ground2,
            Ground3,
            Charcoal,
            Iron,
            Cobalt,
            Nickel,
            Mercury,
            Lead,
            Manganese,
        }

  
        private ParticleSystem ps;


        // Referencia a la interfaz para recoger los datos emitidos por la modificiación del terreno
        // TerrainModification es un struct con:
        //      - Tipo de terreno (su correspondiente int)
        //      - Cantidad modificada
        [SerializeField] private IObservableTerrainData<TerrainModification> terrainData;


        // Struct que tiene como objetivo simular un diccionario serializable.
        // Esta struct es declarada como un array, el cual se puede serializar sin problema.
        // La struct también tiene un método DictionaryFrom para convertir el array de struct en un diccionario de verdad.
        [Serializable]
        private struct TerrainTypePair
        {
            [field: SerializeField]
            public TerrainType TerrainType { get; private set; }

            [field: SerializeField]
            [field: Min(0)]
            public int TerrainID { get; private set; }

            public static Dictionary<TerrainType, uint> DictionaryFrom(IEnumerable<TerrainTypePair> pairs) {
                Dictionary<TerrainType, uint> dictionary = new Dictionary<TerrainType, uint>();
                foreach (var pair in pairs)
                    dictionary[pair.TerrainType] = (uint)pair.TerrainID;
                return dictionary;
            }
        }

        // Array del struct anteriormente definido.
        [SerializeField]
        private TerrainTypePair[] _resourceTerrainTypePairs;

        // Diccionario con los distintos tipos de terreno y su valor int correspondiente.
        private Dictionary<TerrainType, uint> terrainType;


        private void OnEnable() {

            // Generamos el diccionario con los distintos terrenos a partir del array serializado.
            terrainType = TerrainTypePair.DictionaryFrom(_resourceTerrainTypePairs);

        }


        private void Awake() {

            ps = GetComponent<ParticleSystem>();

            terrainData = GetComponent<IObservableTerrainData<TerrainModification>>();

            // Nos subscribimos al evento del ObservableTerrainData para recibir sus llamadas
            terrainData.DataRetrieved += TerrainData;
        }

        private void TerrainData(object sender, TerrainModification e) {
            print(e.amount);
            print(e.terrainType);
            if (e.amount > 0) ps.Play();
            else ps.Stop();
        }
    }
}