using MVPFramework.Model;
using UISystem.MVP.Model.Data;

namespace UISystem.MVP.Model
{
    internal readonly struct DescriptibleUpgrade :
        IUpdateableModel<DescriptibleUpgrade, DescriptibleUpgradeFlyweight>,
        IUpdateableModel<DescriptibleUpgrade, bool>
    {
        public readonly DescriptibleUpgradeFlyweight descriptibleUpgradeFlyweight;
        public readonly bool purchased;

        public DescriptibleUpgrade(DescriptibleUpgradeFlyweight descriptibleUpgradeFlyweight, bool purchased)
        {
            this.descriptibleUpgradeFlyweight = descriptibleUpgradeFlyweight;
            this.purchased = purchased;
        }

        public DescriptibleUpgrade With(DescriptibleUpgradeFlyweight data) =>
            new DescriptibleUpgrade(data, purchased);

        public DescriptibleUpgrade With(bool data) =>
            new DescriptibleUpgrade(descriptibleUpgradeFlyweight, data);
    }
}