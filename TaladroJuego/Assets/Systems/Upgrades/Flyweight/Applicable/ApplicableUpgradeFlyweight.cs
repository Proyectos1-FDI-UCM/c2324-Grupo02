using UnityEngine;
using UpgradesSystem.Applicable;

namespace UpgradesSystem.Flyweight.Applicable
{
    public abstract class ApplicableUpgradeFlyweight : ScriptableObject
    {
        public abstract IApplicableUpgrade Create();

        protected readonly struct NullUpgrade : IApplicableUpgrade
        {
            public void Apply()
            {
                //jaja lol
            }

            public static readonly NullUpgrade Instance = new NullUpgrade();
        }
    }


}