using MovementSystem.Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight.Applicable;

namespace ApplicableUpgradesSystem
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/SpeedMultiplier")]
    internal class SpeedMultiplierUpgradeFlyweight : ApplicableUpgradeFlyweight
    {
        [SerializeField, Min(1.0f)] private float _factor;
        public event UpgradeResourceEvent<SpeedMultiplier> UpgradeSpeedMultiplierEvent;
        private readonly struct SpeedMultiplierApplicableUpgrade : IApplicableUpgrade
        {
            private readonly float _factor;
            private readonly SpeedMultiplier _multiplier;

            public SpeedMultiplierApplicableUpgrade(float factor, SpeedMultiplier multiplier)
            {
                _factor = factor;
                _multiplier = multiplier;
            }

            public void Apply()
            {
                _multiplier.Multiplier *= _factor;
            }
        }

        public override IApplicableUpgrade Create()
        {
            SpeedMultiplier multiplier = UpgradeSpeedMultiplierEvent?.Invoke();

            if (multiplier != null)
            {
                return new SpeedMultiplierApplicableUpgrade(_factor, multiplier);
            }
            else return NullUpgrade.Instance;
        }
    }
}

