using ApplicableUpgradesSystem;
using InputSystem;
using MovementSystem.Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ApplicableUpgradesSystem.RevesibleGearUpgrade
{

    public class ReverseGerarUpdateSubscriber : MonoBehaviour
    {
        [SerializeField] private ShipMovementInput _shipMovementInput;
        [SerializeField] private ReverseGearUpgradeFlyweight[] _upgrades;

        private void Awake()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeReverseGearEvent += OnUpgradeGearEvent;
            }
        }

        private void OnDestroy()
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.UpgradeReverseGearEvent -= OnUpgradeGearEvent;
            }
        }

        private ShipMovementInput OnUpgradeGearEvent()
        {
            return _shipMovementInput;
        }
    }
}