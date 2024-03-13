using MovementSystem.Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight.Applicable;
using InputSystem;

namespace ApplicableUpgradesSystem.RevesibleGearUpgrade
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/ReverseGear")]
    internal class ReverseGearUpgradeFlyweight : ApplicableUpgradeFlyweight
    {
        public event UpgradeResourceEvent<ShipMovementInput> UpgradeReverseGearEvent;
        private readonly struct ReverseGearApplicableUpgrade : IApplicableUpgrade
        {
            private readonly ShipMovementInput _shipMovementInput;

            public ReverseGearApplicableUpgrade(ShipMovementInput shipMovementInput)
            {
                _shipMovementInput = shipMovementInput;
            }

            public void Apply()
            {
                _shipMovementInput.SetReverseGearAvailability(true);
            }
        }

        public override IApplicableUpgrade Create()
        {
            ShipMovementInput shipMovementInput = UpgradeReverseGearEvent?.Invoke();

            if (shipMovementInput != null)
            {
                return new ReverseGearApplicableUpgrade(shipMovementInput);
            }
            else return NullUpgrade.Instance;
        }
    }
}
