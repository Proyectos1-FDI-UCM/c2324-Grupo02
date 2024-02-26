using MVPFramework.Presenter;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
        private bool _presentOnStart;

        private IPresenter<UpgradeButton, DescriptibleUpgradeFlyweight> _presenter;

        private void Awake()
        {
            _presenter = GetComponentInChildren<IPresenter<UpgradeButton, DescriptibleUpgradeFlyweight>>();
        }

        private void Start()
        {
            if (_presentOnStart)
                Present();
        }

        public void Present()
        {
            for (int i = 0; i < _upgradeButtons.Length && i < _descriptibleUpgrades.Length; i++)
                _presenter.TryPresentElementWith(_upgradeButtons[i], _descriptibleUpgrades[i]);
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