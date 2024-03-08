using System;
using TMPro;
using UISystem.Data;
using UISystem.MVP.Model;
using UISystem.MVP.View;
using MVPFramework.Model;
using UnityEngine;
using UnityEngine.EventSystems;
using static UISystem.MVP.Presenter.DescriptionPanelPresenter;
using static UISystem.MVP.Presenter.UpgradeButtonPresenter;
using static UISystem.MVP.Presenter.LabeledIconPresenter;

using ButtonPresenter = MVPFramework.Presenter.IPresenter
    <UISystem.MVP.Presenter.UpgradeButtonPresenter.UpgradeButton, UISystem.MVP.Model.DescriptibleUpgradeFlyweight, UISystem.MVP.View.DescriptibleEventTriggerView>;

using PanelPresenter = MVPFramework.Presenter.IPresenter
    <UISystem.MVP.Presenter.DescriptionPanelPresenter.DescriptionPanel, MVPFramework.Model.IModel<UISystem.MVP.Model.DescriptibleModel.Data>>;

using LabelIconPresenter = MVPFramework.Presenter.IPresenter
    <UISystem.MVP.Presenter.LabeledIconPresenter.LabeledIcon, MVPFramework.Model.IModel<(UnityEngine.Sprite icon, string label)>>;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using UpgradesSystem.Flyweight;

namespace UISystem.MVP.Presenter
{
    internal class UpgradeMenuPresenter : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField]
        private GameObject[] _upgradeButtonRoots;

        [SerializeField]
        private GameObject[] _resourceIconRoots;
#endif
        [SerializeField]
        private UpgradeButton[] _upgradeButtons;

        [SerializeField]
        private DescriptibleUpgradeFlyweight[] _descriptibleUpgrades;

        [SerializeField]
        private DescriptionPanel _descriptionPanel;

        [SerializeField]
        private LabeledIcon[] _resourceIcons;

        [SerializeField]
        private ResourceSpriteBinder _resourceSpriteBinder;

        [SerializeField]
        private bool _presentOnStart;

        private ButtonPresenter _upgradeButtonsPresenter;
        private PanelPresenter _descriptionPanelPresenter;
        private LabelIconPresenter _resourceIconPresenter;

        private void Awake()
        {
            _upgradeButtonsPresenter = GetComponentInChildren<ButtonPresenter>();
            _descriptionPanelPresenter = GetComponentInChildren<PanelPresenter>();
            _resourceIconPresenter = GetComponentInChildren<LabelIconPresenter>();
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

                    KeyValuePair<ResourceType, int>[] upgradeCosts = model.Create().PurchaseCost.ToArray();
                    for (int j = 0; j < _resourceIcons.Length; j++)
                    {
                        LabeledIcon labeledIcon = _resourceIcons[j];
                        labeledIcon.Root.SetActive(j < upgradeCosts.Length);

                        if (j >= upgradeCosts.Length)
                            continue;
                        KeyValuePair<ResourceType, int> resourceCost = upgradeCosts[j];
                        _resourceIconPresenter.TryPresentElementWith(
                            labeledIcon, (Model<(Sprite, string)>)(
                                _resourceSpriteBinder.TryGetSpriteFrom(resourceCost.Key, out Sprite s) ? s : null,
                                $"x{resourceCost.Value}"));
                    }
                    
                }));
                view.TryUpdateWith(new EventTriggerView.ExitConfiguration((data) =>
                {
                    _descriptionPanel.Root.SetActive(false);
                }));
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_upgradeButtons == null || _upgradeButtons.Length == 0)
                _upgradeButtons = Array.ConvertAll(
                            _upgradeButtonRoots,
                            root => new UpgradeButton(
                                root.GetComponentInChildren<EventTrigger>(),
                                root.GetComponentInChildren<TMP_Text>()));

            if (_resourceIcons == null || _resourceIcons.Length == 0)
                _resourceIcons = Array.ConvertAll(
                    _resourceIconRoots,
                    root => new LabeledIcon(
                        root,
                        root.GetComponentInChildren<Image>(),
                        root.GetComponentInChildren<TMP_Text>()));

            _upgradeButtonRoots = new GameObject[0];
            _resourceIconRoots = new GameObject[0];
        }
#endif
    }
}