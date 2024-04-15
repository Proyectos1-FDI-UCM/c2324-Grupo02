using MovementSystem.Profile;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight.Applicable;

namespace ApplicableUpgradesSystem.RadarIncrease
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/Radar Increase")]
    internal class RadarIncreaseUpgradeFlyweight : ApplicableUpgradeFlyweight
    {
        [SerializeField] private Material _radarMaterial;
        [SerializeField, Range(0.0f, 1.0f)] private float _value;

        public event UpgradeResourceEvent<Material> UpgradeRadarIncreaseEvent;

        private readonly struct RadarIncreaseApplicableUpgrade : IApplicableUpgrade
        {
            private readonly Material _radarMaterial;
            private readonly float _value;

            public RadarIncreaseApplicableUpgrade(Material radarMaterial, float value)
            {
                _radarMaterial = radarMaterial;
                _value = value;
            }

            public void Apply()
            {
                _radarMaterial.SetFloat("_VisibilityRadius", _value);
            }
        }

        public override IApplicableUpgrade Create()
        {
            Material material = UpgradeRadarIncreaseEvent?.Invoke();

            if (material != null)
            {
                return new RadarIncreaseApplicableUpgrade(_radarMaterial, _value);
            }
            else return NullUpgrade.Instance;
        }
    }
}

