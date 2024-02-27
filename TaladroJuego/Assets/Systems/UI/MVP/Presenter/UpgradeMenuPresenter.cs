using MVPFramework.Model;
using MVPFramework.Presenter;
using System;
using TMPro;
using UISystem.MVP.Model;
using UISystem.MVP.View;
using UnityEngine;
using UnityEngine.EventSystems;
using static UISystem.MVP.Presenter.DescriptionPanelPresenter;
using static UISystem.MVP.Presenter.UpgradeButtonPresenter;

namespace UISystem.MVP.Presenter
{
    internal class UpgradeMenuPresenter : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField]
        private GameObject[] _upgradeButtonRoots;
#endif
        [SerializeField]
        private UpgradeButton[] _upgradeButtons;

        [SerializeField]
        private DescriptibleUpgradeFlyweight[] _descriptibleUpgrades;

        [SerializeField]
        private DescriptionPanel _descriptionPanel;

        [SerializeField]
        private bool _presentOnStart;

        private IPresenter<UpgradeButton, DescriptibleUpgradeFlyweight, DescriptibleEventTriggerView> _upgradeButtonsPresenter;
        private IPresenter<DescriptionPanel, IModel<DescriptibleModel.Data>> _descriptionPanelPresenter;

        private void Awake()
        {
            _upgradeButtonsPresenter = GetComponentInChildren<IPresenter<UpgradeButton, DescriptibleUpgradeFlyweight, DescriptibleEventTriggerView>>();
            _descriptionPanelPresenter = GetComponentInChildren<IPresenter<DescriptionPanel, IModel<DescriptibleModel.Data>>>();
        }

        private void Start()
        {
            if (_presentOnStart)
                Present();
        }

        public void Present()
        {
            for (int i = 0; i < _upgradeButtons.Length && i < _descriptibleUpgrades.Length; i++)
            {
                DescriptibleUpgradeFlyweight model = _descriptibleUpgrades[i];
                DescriptibleEventTriggerView view = _upgradeButtonsPresenter.PresentElementWith(_upgradeButtons[i], model);
                view.TryUpdateWith(new EventTriggerView.EnterConfiguration((data) =>
                {
                    _descriptionPanel.Root.SetActive(true);
                    _descriptionPanelPresenter.TryPresentElementWith(_descriptionPanel, model);
                }));
                view.TryUpdateWith(new EventTriggerView.ExitConfiguration((data) =>
                {
                    _descriptionPanel.Root.SetActive(false);
                }));
            }
        }

        private void OnValidate()
        {
            if (_upgradeButtons != null && _upgradeButtons.Length != 0)
                return;

            _upgradeButtons = Array.ConvertAll(
                _upgradeButtonRoots,
                root => new UpgradeButton(
                    root.GetComponentInChildren<EventTrigger>(),
                    root.GetComponentInChildren<TMP_Text>()));

            _upgradeButtonRoots = new GameObject[0];
        }
    }
}