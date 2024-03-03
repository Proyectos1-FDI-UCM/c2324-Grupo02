using MovementSystem.Profile;
using StatusSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using RequireAttributes;
namespace ApplicableUpgradesSystem.StatusMaxVaueUp
{
    internal class StatusMaxValueUpUpUpgradeSubscriber : MonoBehaviour
    {
        [SerializeField] private ClampedStatusParameter _statusParameter;
        [SerializeField] private StatusMaxValueUpUpgradeFlyweight[] _upgrades;

        private void Awake()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeStatusMaxValueUpEvent += OnUpgradeStatusUpEvent;
            }
        }

        private void OnDestroy()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeStatusMaxValueUpEvent += OnUpgradeStatusUpEvent;
            }
        }

        private ClampedStatusParameter OnUpgradeStatusUpEvent()
        {
            return _statusParameter;
        }
    }
}

