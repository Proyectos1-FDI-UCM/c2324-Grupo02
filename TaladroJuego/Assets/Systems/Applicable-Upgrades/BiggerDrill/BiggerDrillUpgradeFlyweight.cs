using MovementSystem.Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight.Applicable;

namespace ApplicableUpgradesSystem
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/BiggerDrill")]
    internal class BiggerDrillUpgradeFlyweight : ApplicableUpgradeFlyweight
    {
        [SerializeField, Min(1.0f)] private float _factor;
        public event UpgradeResourceEvent<Transform> UpgradeBiggerDrillEvent;
        private readonly struct BiggerDrillApplicableUpgrade : IApplicableUpgrade
        {
            private readonly float _factor;
            private readonly Transform _drillTransform;

            public BiggerDrillApplicableUpgrade(float factor, Transform drillTransform)
            {
                _factor = factor;
                _drillTransform = drillTransform;
            }

            public void Apply()
            {
                _drillTransform.localScale *= _factor;
            }
        }

        public override IApplicableUpgrade Create()
        {
            Transform drillTransform = UpgradeBiggerDrillEvent?.Invoke();

            if (drillTransform != null)
            {
                 return new BiggerDrillApplicableUpgrade(_factor, drillTransform);
            }
            else return NullUpgrade.Instance;
        }
    }
}

