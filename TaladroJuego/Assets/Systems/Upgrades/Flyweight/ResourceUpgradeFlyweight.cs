using UnityEngine;
using UpgradesSystem.Resource;

namespace UpgradesSystem.Flyweight
{
    public abstract class ResourceUpgradeFlyweight : ScriptableObject
    {
        public abstract IResourceUpgrade Create();
    }
}