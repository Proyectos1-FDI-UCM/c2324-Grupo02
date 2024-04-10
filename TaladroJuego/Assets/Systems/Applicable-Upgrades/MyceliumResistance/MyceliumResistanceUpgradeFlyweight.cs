using DamageSystem.Damager;
using MovementSystem.Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight.Applicable;

namespace ApplicableUpgradesSystem
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/MyceliumResistance")]
    internal class MyceliumResistanceUpgradeFlyweight : ApplicableUpgradeFlyweight
    {
        [SerializeField] private float _factor;
        public event UpgradeResourceEvent<ConstantDamager[]> UpgradeMyceliumResistanceEvent;
        private readonly struct MyceliumResistanceApplicableUpgrade : IApplicableUpgrade
        {
            private readonly float _factor;
            private readonly ConstantDamager[] _damagers;

            public MyceliumResistanceApplicableUpgrade(float factor, ConstantDamager [] damagers)
            {
                _factor = factor;
                _damagers = new ConstantDamager[damagers.Length];
                for(int i= 0;i<damagers.Length; i++)
                {
                    _damagers[i] = damagers[i];
                }
            }

            public void Apply()
            {
                foreach(ConstantDamager c in _damagers)
                {
                    c.Damage *= _factor;
                }
            }
        }

        public override IApplicableUpgrade Create()
        {
            ConstantDamager[] damagers = UpgradeMyceliumResistanceEvent?.Invoke();

            if (damagers != null)
            {
                return new MyceliumResistanceApplicableUpgrade(_factor, damagers);
            }
            else return NullUpgrade.Instance;
        }
    }
}

