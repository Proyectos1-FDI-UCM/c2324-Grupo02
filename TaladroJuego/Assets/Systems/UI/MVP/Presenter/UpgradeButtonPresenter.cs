using MVPFramework.Presenter;
using ResourceCollectionSystem;
using System;
using TMPro;
using UISystem.MVP.Model;
using UISystem.MVP.View;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UpgradesSystem.Flyweight;

namespace UISystem.MVP.Presenter
{
    internal class UpgradeButtonPresenter : MonoBehaviour,
        IPresenter<UpgradeButtonPresenter.UpgradeButton, DescriptibleUpgradeFlyweight>,
        IPresenter<UpgradeButtonPresenter.UpgradeButton, DescriptibleUpgradeFlyweight, DescriptibleEventTriggerView>
    {
        [Serializable]
        public struct UpgradeButton
        {
            [field: SerializeField]
            public EventTrigger EventTrigger { get; private set; }

            [field: SerializeField]
            public TMP_Text Label { get; private set; }

            public UpgradeButton(EventTrigger eventTrigger, TMP_Text label)
            {
                EventTrigger = eventTrigger;
                Label = label;
            }
        }

        [SerializeField]
        private ResourcesContainer _resourcesContainer;

        [field: SerializeField]
        public UnityEvent<ResourceUpgradeFlyweight> PurchasedSuccessfully { get; private set; }

        [field: SerializeField]
        public UnityEvent<ResourceUpgradeFlyweight> PurchaseFailed { get; private set; }

        public bool TryPresentElementWith(UpgradeButton element, DescriptibleUpgradeFlyweight model)
        {
            DescriptibleEventTriggerView view = new DescriptibleEventTriggerView(
                new EventTriggerView(element.EventTrigger, true),
                new TextView(element.Label));

            return view.TryUpdateWith(model.Capture().name)
                && view.TryUpdateWith(new EventTriggerView.PressConfiuration((data) =>
                {
                    if (_resourcesContainer.TryPurchase(model.Create()))
                        PurchasedSuccessfully.Invoke(model);
                    else
                        PurchaseFailed.Invoke(model);
                }));
        }

        public DescriptibleEventTriggerView PresentElementWith(UpgradeButton element, DescriptibleUpgradeFlyweight model)
        {
            DescriptibleEventTriggerView view = new DescriptibleEventTriggerView(
                new EventTriggerView(element.EventTrigger, false),
                new TextView(element.Label));

            view.TryUpdateWith(model.Capture().name);
            view.TryUpdateWith(new EventTriggerView.PressConfiuration((data) =>
            {
                if (_resourcesContainer.TryPurchase(model.Create()))
                    PurchasedSuccessfully.Invoke(model);
                else
                    PurchaseFailed.Invoke(model);
            }));

            return view;
        }
    }
}