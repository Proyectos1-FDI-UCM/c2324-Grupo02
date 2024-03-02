using System;
using TerrainSystem.Modifier;
using TerrainSystem.Requester;
using TerrainSystem.Source;
using UnityEngine;

namespace TerrainSystem.Test
{
    internal class TerrainDestructionTest : MonoBehaviour
    {
        [Serializable]
        public struct Data : ITerrainModificationSource
        {
            private readonly struct Configuration : ITerrainModificationConfiguration
            {
                public float Radius { get; }
                public float Strength { get; }
                public float Falloff { get; }

                public Configuration(float radius, float strength, float falloff)
                {
                    Radius = radius;
                    Strength = strength;
                    Falloff = falloff;
                }
            }

            [field: SerializeField]
            public Transform Source { get; private set; }

            [field: SerializeField]
            [field: Min(0)]
            public float Radius { get; private set; }

            [field: SerializeField]
            [field: Min(0)]
            public float Strength { get; private set; }

            [field: SerializeField]
            [field: Min(0)]
            public float Falloff { get; private set; }

            [field: SerializeField]
            public uint Type { get; private set; }

            public ITerrainModificationConfiguration GetConfiguration() =>
                new Configuration(Radius, Strength, Falloff);

            public Vector3 GetPosition() => Source.position;

            public Quaternion GetRotation() => Source.rotation;

            public uint GetTerrainType() => Type;
        }

        [SerializeField]
        private RenderTexture _terrainTexture;

        [SerializeField]
        private Data[] _data;

        [SerializeField]
        private TerrainModifierRequester _terrainModifierRequester;
        private ITerrainModifierRequestable<ITerrainModifier<TerrainModificationSource>> _terrainModifierRequestable;

        private ITerrainModifier<TerrainModificationSource> _terrainModifier;

        private void Awake()
        {
            _terrainModifierRequestable = _terrainModifierRequester;
            _terrainModifierRequester.Initialize();
            _terrainModifier = GetComponent<ITerrainModifier<TerrainModificationSource>>();
        }

        private void Start()
        {
            _terrainModifierRequestable.TryInitializeTerrainTo(0);
        }

        private void LateUpdate()
        {
            ITerrainModificationSource[] sources = Array.ConvertAll(_data, source => (ITerrainModificationSource)source);
            _terrainModifierRequestable.TryModifyWith(_terrainModifier, Array.AsReadOnly(sources));

            _terrainModifierRequester.Retrieve(_terrainTexture);

            //float[] modifications = new float[32];
            //_terrainModifierRequester.Retrieve(modifications);
            //for (int i = 0; i < modifications.Length; i++)
            //{
            //    float modification = modifications[i];
            //    Debug.Log($"Modification {i}: {modification}");
            //}
        }
    }
}