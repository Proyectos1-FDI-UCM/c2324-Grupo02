using MovementSystem.Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight.Applicable;
using TerrainResourcesSystem;

namespace ApplicableUpgradesSystem.MiningEfficiency
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/MiningEfficiency")]
    internal class MiningEfficiencyUgradeFlyweight : ApplicableUpgradeFlyweight 
    {
        [SerializeField, Min(1.0f)] private float _factor;
        public event UpgradeResourceEvent<ResourcesModificationObservable> UpgradeMiningEfficiencyEvent;
        private readonly struct MiningEfficiencyApplicableUpgrade : IApplicableUpgrade
        {
            private readonly float _factor;
            private readonly ResourcesModificationObservable _observableResource;

            public MiningEfficiencyApplicableUpgrade(float factor, ResourcesModificationObservable multiplier)
            {
                _factor = factor;
                _observableResource = multiplier;
            }

            public void Apply()
            {
                _observableResource.ConversionRate *= _factor;
            }
        }

        public override IApplicableUpgrade Create()
        {
            ResourcesModificationObservable multiplier = UpgradeMiningEfficiencyEvent?.Invoke();

            if (multiplier != null)
            {
                return new MiningEfficiencyApplicableUpgrade(_factor, multiplier);
            }
            else return NullUpgrade.Instance;
        }
    }
}

