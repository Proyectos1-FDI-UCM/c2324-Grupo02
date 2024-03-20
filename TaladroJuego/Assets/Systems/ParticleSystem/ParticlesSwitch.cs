using UnityEngine;

using TerrainSystem.Requestable.Retriever.Observable;
using System.Collections.Generic;
using UpgradesSystem.Flyweight;
using System;

namespace Particles
{
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


        // Referencia a la interfaz para recoger los datos emitidos por la modificiación del terreno
        // TerrainModification es un struct con:
        //      - Tipo de terreno (su correspondiente int)
        //      - Cantidad modificada
        [SerializeField] private IObservableTerrainData<TerrainModification> terrainData;


        // Struct que tiene como objetivo simular un diccionario serializable.
        // Esta struct es declarada como un array, el cual se puede serializar sin problema.
        // La struct también tiene un método DictionaryFrom para convertir el array de struct en un diccionario de verdad.
        [Serializable]
        private struct TerrainType_ParticleSystem_Pair
        {
            [field: SerializeField]
            public TerrainType TerrainType { get; private set; }

            [field: SerializeField]
            public ParticleSystem TerrainParticles { get; private set; }

            public static Dictionary<TerrainType, ParticleSystem> DictionaryFrom(IEnumerable<TerrainType_ParticleSystem_Pair> pairs) {
                Dictionary<TerrainType, ParticleSystem> dictionary = new Dictionary<TerrainType, ParticleSystem>();
                foreach (var pair in pairs)
                    dictionary[pair.TerrainType] = pair.TerrainParticles;
                return dictionary;
            }
        }

        // Array del struct anteriormente definido.
        [SerializeField]
        private TerrainType_ParticleSystem_Pair[] _resourceTerrainTypePairs;

        // Diccionario con los distintos tipos de terreno y su valor int correspondiente.
        private Dictionary<TerrainType, ParticleSystem> terrainTypeParticleDictionary;


        private void OnEnable() {

            // Generamos el diccionario con los distintos terrenos a partir del array serializado.
            terrainTypeParticleDictionary = TerrainType_ParticleSystem_Pair.DictionaryFrom(_resourceTerrainTypePairs);

        }


        private void Awake() {

            terrainData = GetComponent<IObservableTerrainData<TerrainModification>>();

            // Nos subscribimos al evento del ObservableTerrainData para recibir sus llamadas
            terrainData.DataRetrieved += TerrainData;
        }

        // Método subscrito al evento del Observable. Se llama cada vez que recibimos datos del minado de terreno
        private void TerrainData(object sender, TerrainModification e) {

            // Obtenemos el sistema de partículas correspondiente al terreno minado y lo reproducimos
            if(terrainTypeParticleDictionary.TryGetValue((TerrainType)e.terrainType, out ParticleSystem value))
            {
                print((TerrainType)e.terrainType);
                value.Play();
            }     
        }
    }
}