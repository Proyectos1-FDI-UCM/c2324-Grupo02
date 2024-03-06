using System;
using TMPro;
using UISystem.MVP.Model;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.MVP.View
{
    internal class PurchaseButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private TMP_Text _text;

        public event EventHandler OnClicked;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked() =>
            OnClicked?.Invoke(this, EventArgs.Empty);

        public void UpdateWith(DescriptibleUpgrade upgrade)
        {
            _text.text = upgrade.descriptibleUpgradeFlyweight.Capture().title;
        }
    }
}