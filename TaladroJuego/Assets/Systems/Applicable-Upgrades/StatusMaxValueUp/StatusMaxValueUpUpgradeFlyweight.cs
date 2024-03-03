using StatusSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight.Applicable;

namespace ApplicableUpgradesSystem.StatusMaxVaueUp
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/StatusMaxValueUp")]
    internal class StatusMaxValueUpUpgradeFlyweight : ApplicableUpgradeFlyweight
    {
        public event UpgradeResourceEvent<ClampedStatusParameter> UpgradeStatusMaxValueUpEvent;
        [SerializeField] private float _valueToAdd;
        private readonly struct StatusMaxValueUpApplicableUpgrade : IApplicableUpgrade
        {
            private readonly ClampedStatusParameter _statusParameter;
            private readonly float _valueToAdd;

            public StatusMaxValueUpApplicableUpgrade(ClampedStatusParameter statusParameter, float valueToAdd)
            {
                _statusParameter = statusParameter;
                _valueToAdd = valueToAdd;
            }
            public void Apply()
            {
                _statusParameter.MaxValue += _valueToAdd;
            }
        }
        public override IApplicableUpgrade Create()
        {
            ClampedStatusParameter statusParameter = UpgradeStatusMaxValueUpEvent?.Invoke();

            if (statusParameter != null)
            {
                Debug.Log("Subiendo Status");
                return new StatusMaxValueUpApplicableUpgrade(statusParameter, _valueToAdd);
            }
            else
            {
                Debug.Log(UpgradeStatusMaxValueUpEvent);
                return NullUpgrade.Instance;
            }
        }
    }
}

