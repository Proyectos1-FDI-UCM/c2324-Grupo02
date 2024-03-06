using MVPFramework.Model;
using UnityEngine;
using UpgradesSystem.Flyweight;
using UpgradesSystem.Resource;
using static UISystem.MVP.Model.Data.Descriptible;
using static UISystem.MVP.Model.Data.DescriptibleUpgradeFlyweight;

namespace UISystem.MVP.Model.Data
{
    [CreateAssetMenu(fileName = "Descriptible Upgrade Flyweight", menuName = "UI/DescriptibleUpgradeFlyweight")]
    internal class DescriptibleUpgradeFlyweight : ResourceUpgradeFlyweight,
        IModel<DescriptibleUpgrade>,
        IModel<TitledDescription>
    {
        [SerializeField]
        private ResourceUpgradeFlyweight _upgradeFlyweight;

        [SerializeField]
        private Descriptible _descriptible;

        public DescriptibleUpgrade Capture() => new DescriptibleUpgrade(_descriptible.Capture(), _upgradeFlyweight.Create());
        TitledDescription IModel<TitledDescription>.Capture() => _descriptible.Capture();
        
        public override IResourceUpgrade Create()
        {
            return _upgradeFlyweight.Create();
        }

        public readonly struct DescriptibleUpgrade
        {
            public readonly string title;
            public readonly string description;
            public readonly IResourceUpgrade upgrade;

            public DescriptibleUpgrade(TitledDescription data, IResourceUpgrade upgrade)
            {
                title = data.title;
                description = data.description;
                this.upgrade = upgrade;
            }

            public DescriptibleUpgrade(string title, string description, IResourceUpgrade upgrade)
            {
                this.title = title;
                this.description = description;
                this.upgrade = upgrade;
            }

            public void Deconstruct(out string title, out string description, out IResourceUpgrade upgrade)
            {
                title = this.title;
                description = this.description;
                upgrade = this.upgrade;
            }
        }

    }
}