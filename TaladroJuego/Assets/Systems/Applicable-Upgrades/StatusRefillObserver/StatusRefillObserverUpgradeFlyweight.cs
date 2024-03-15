using ResourceCollectionSystem;
using StatusSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UpgradesSystem.Applicable;
using UpgradesSystem.Flyweight.Applicable;
using UpgradesSystem.Flyweight;

namespace ApplicableUpgradesSystem.StatusRefillObserver
{
    [CreateAssetMenu(menuName = "Upgrades/Flyweight/Applicable/StatusRefillObserver")]
    internal class StatusRefillObserverUpgradeFlyweight : ApplicableUpgradeFlyweight
    {
        public event UpgradeResourceEvent<StatusParameter> UpgradeStatusRefillObserverEvent;
        [SerializeField] private float _valueForPurchase;
        [SerializeField] private QuotaResourceUpgradeFlyweight _quotaResources;
        [SerializeField] private ResourcesContainer _resourcesContainer;
        private readonly struct StatusMaxValueUpApplicableUpgrade : IApplicableUpgrade
        {
            private readonly StatusParameter _statusParameter;
            private readonly float _valueForPurchase;
            private readonly ResourcesContainer _resourcesContainer;
            private readonly QuotaResourceUpgradeFlyweight _quotaResources;

            public StatusMaxValueUpApplicableUpgrade(StatusParameter statusParameter, float valueForPurchase, 
                                                     ResourcesContainer resourcesContainer, QuotaResourceUpgradeFlyweight quotaResourceUpgradeFlyweight)
            {
                _statusParameter = statusParameter;
                _valueForPurchase = valueForPurchase;
                _resourcesContainer = resourcesContainer;
                _quotaResources = quotaResourceUpgradeFlyweight;
            }

            public void Apply()
            {
                _statusParameter.ValueSet.RemoveListener(OnValueSet);
                _statusParameter.ValueSet.AddListener(OnValueSet);
            }

            private void OnValueSet(float value)
            {
                if(value <= _valueForPurchase) _resourcesContainer.TryPurchase(_quotaResources.Create());
            }
        }

        public override IApplicableUpgrade Create()
        {
            StatusParameter statusParameter = UpgradeStatusRefillObserverEvent?.Invoke();

            if (statusParameter != null)
            {
                return new StatusMaxValueUpApplicableUpgrade(statusParameter, _valueForPurchase, _resourcesContainer, _quotaResources);
            }
            else
            {
                return NullUpgrade.Instance;
            }
        }
    }
}

