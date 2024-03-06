using MVPFramework.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UISystem.MVP.Model
{
    internal readonly struct PurchaseableUpgradesModel : 
        IModel<ObservableCollection<DescriptibleUpgrade>>,
        IModel<IEnumerable<DescriptibleUpgrade>>,
        IUpdateableModel<PurchaseableUpgradesModel, DescriptibleUpgrade>
    {
        private readonly ObservableCollection<DescriptibleUpgrade> _upgrades;

        public PurchaseableUpgradesModel(IEnumerable<DescriptibleUpgrade> upgrades)
        {
            _upgrades = new ObservableCollection<DescriptibleUpgrade>(upgrades);
        }

        public ObservableCollection<DescriptibleUpgrade> Capture() => _upgrades;

        IEnumerable<DescriptibleUpgrade> IModel<IEnumerable<DescriptibleUpgrade>>.Capture() => _upgrades;

        public PurchaseableUpgradesModel With(DescriptibleUpgrade data)
        {
            _upgrades.Add(data);
            return this;
        }
    }
}