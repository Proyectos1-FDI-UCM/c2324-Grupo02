using ApplicableUpgradesSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusSystem;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight.Applicable;
using MovementSystem.Profile;

namespace ApplicableUpgradesSystem.StatusUp
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/StatusUp")]
    internal class StatusUpUpgradeFlyweight : ApplicableUpgradeFlyweight
    {
        public event UpgradeResourceEvent<IStatusParameter> UpgradeStatusUpEvent;
        [SerializeField] private float _valueToAdd;

        private readonly struct StatusUpApplicableUpgrade : IApplicableUpgrade
        {
            private readonly IStatusParameter _statusParameter;
            private readonly float _valueToAdd;

            public StatusUpApplicableUpgrade(IStatusParameter statusParameter, float valueToAdd)
            {
                _statusParameter = statusParameter;
                _valueToAdd = valueToAdd;
            }

            public void Apply()
            {
                _statusParameter.Value += _valueToAdd;
            }
        }

        public override IApplicableUpgrade Create()
        {
            IStatusParameter statusParameter = UpgradeStatusUpEvent?.Invoke();

            if (statusParameter != null)
            {
                return new StatusUpApplicableUpgrade(statusParameter, _valueToAdd);
            }
            else
            {
                Debug.Log(UpgradeStatusUpEvent);
                return NullUpgrade.Instance;
            }
        }
    }
}

