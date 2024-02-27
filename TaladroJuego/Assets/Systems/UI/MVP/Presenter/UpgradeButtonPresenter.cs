using MVPFramework.Presenter;
using ResourceCollectionSystem;
using System;
using TMPro;
using UISystem.MVP.Model;
using UISystem.MVP.View;
using UnityEngine;
using UnityEngine.EventSystems;

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

        public bool TryPresentElementWith(UpgradeButton element, DescriptibleUpgradeFlyweight model)
        {
            DescriptibleEventTriggerView view = new DescriptibleEventTriggerView(
                new EventTriggerView(element.EventTrigger, true),
                new TextView(element.Label));

            return view.TryUpdateWith(model.Capture().name)
                && view.TryUpdateWith(new EventTriggerView.PressConfiuration((data) =>
                {
                    _resourcesContainer.TryPurchase(model.Create());
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
                _resourcesContainer.TryPurchase(model.Create());
            }));

            return view;
        }
    }
}