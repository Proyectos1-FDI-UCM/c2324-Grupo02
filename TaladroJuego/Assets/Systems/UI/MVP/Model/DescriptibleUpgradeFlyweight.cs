﻿using MVPFramework.Model;
using UnityEngine;
using UpgradesSystem.Flyweight;
using UpgradesSystem.Resource;

namespace UISystem.MVP.Model
{
    [CreateAssetMenu(fileName = "Descriptible Upgrade Flyweight", menuName = "UI/DescriptibleUpgradeFlyweight")]
    internal class DescriptibleUpgradeFlyweight : ResourceUpgradeFlyweight, IModel<DescriptibleModel.Data>,
        IModel<(string name, string description)>

    {
        [SerializeField]
        private ResourceUpgradeFlyweight _upgradeFlyweight;

        [SerializeField]
        private DescriptibleModel _descriptibleModel;

        public DescriptibleModel.Data Capture()
        {
            return ((IModel<DescriptibleModel.Data>)_descriptibleModel).Capture();
        }
        (string name, string description) IModel<(string name, string description)>.Capture()
        {
            return ((IModel<(string name, string description)>)_descriptibleModel).Capture();
        }

        public override IResourceUpgrade Create()
        {
            return _upgradeFlyweight.Create();
        }
    }
}