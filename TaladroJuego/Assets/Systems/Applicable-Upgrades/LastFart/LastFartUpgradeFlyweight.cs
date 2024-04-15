using MovementSystem.LastFart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight.Applicable;

namespace ApplicableUpgradesSystem
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/LastFart")]
    internal class LastFartUpgradeFlyweight : ApplicableUpgradeFlyweight
    {
        public event UpgradeResourceEvent<LastFartLauncher> UpgradeLastFartEvent;
        private readonly struct LastFartApplicableUpgrade : IApplicableUpgrade
        {
            private readonly LastFartLauncher _lastFartLauncher;
            
            public LastFartApplicableUpgrade(LastFartLauncher lastFartLauncher)
            {
                _lastFartLauncher = lastFartLauncher;
            }

            public void Apply()
            {
                
                _lastFartLauncher.IsFartEnabled = true;
            }
        }

        public override IApplicableUpgrade Create()
        {
            LastFartLauncher lastFartLauncher = UpgradeLastFartEvent?.Invoke();

            if (lastFartLauncher != null)
            {
                return new LastFartApplicableUpgrade(lastFartLauncher);
            }
            else return NullUpgrade.Instance;
        }
    }
}

