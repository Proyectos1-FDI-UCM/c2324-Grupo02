using MovementSystem.Profile;
using StatusSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using RequireAttributes;
namespace ApplicableUpgradesSystem
{
    internal class StatusUpUpgradeSubscriber : MonoBehaviour
    {
        [SerializeField, RequireInterface(typeof(IStatusParameter))]private Object _statusParameterObject; 
        private IStatusParameter _statusParameter;
        [SerializeField] private StatusUpUpgradeFlyweight[] _upgrades;

        private void Awake()
        {
            _statusParameter = _statusParameterObject as IStatusParameter;
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeStatusUpEvent += OnUpgradeStatusUpEvent;
            }
        }

        private void OnDestroy()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeStatusUpEvent += OnUpgradeStatusUpEvent;
            }
        }

        private IStatusParameter OnUpgradeStatusUpEvent()
        {
            return _statusParameter;
        }
    }
}

