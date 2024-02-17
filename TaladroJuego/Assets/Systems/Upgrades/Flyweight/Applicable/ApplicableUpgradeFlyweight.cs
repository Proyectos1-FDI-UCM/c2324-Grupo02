using UnityEngine;
using UpgradesSystem.Applicable;

namespace UpgradesSystem.Flyweight.Applicable
{
    public abstract class ApplicableUpgradeFlyweight : ScriptableObject
    {
        public abstract IApplicableUpgrade Create();
    }
}