using ResourceCollectionSystem;
using System;
using TMPro;
using UISystem.MVP.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UISystem.MVP.Presenter
{
    internal class UpgradeButtonsPresenter : MonoBehaviour
    {
        [Serializable]
        private struct UpgradeButton
        {
            [field: SerializeField]
            public EventTrigger EventTrigger { get; private set; }

            [field: SerializeField]
            public TMP_Text Label { get; private set; }
        }

        [SerializeField]
        private UpgradeButton[] _upgradeButtons;

        [SerializeField]
        private DescriptibleUpgradeFlyweight[] _descriptibleUpgrades;

        [SerializeField]
        private ResourcesContainer _resourcesContainer;

        public void Present()
        {
            for (int i = 0; i < _upgradeButtons.Length && i < _descriptibleUpgrades.Length; i++)
            {
                UpgradeButton button = _upgradeButtons[i];
                DescriptibleEventTriggerView view = new DescriptibleEventTriggerView(
                    new EventTriggerView(button.EventTrigger, true),
                    new TextView(button.Label));


                DescriptibleUpgradeFlyweight model = _descriptibleUpgrades[i];
                view.TryUpdateWith(model.Capture().name);
                view.TryUpdateWith(new EventTriggerView.PressConfiuration((data) =>
                {
                    _resourcesContainer.TryPurchase(model.Create());
                }));
            }
        }
    }
}