using UISystem.MVP.Model.Data;
using UnityEngine;

namespace UISystem.MVP.View
{
    internal class PurchaseButtonsView : MonoBehaviour
    {
        [SerializeField]
        private PurchaseButton[] _purchaseButtons;

        [SerializeField]
        private DescriptibleUpgradeFlyweight[] _descriptibleUpgrades;

        private void Awake()
        {
            for (int i = 0; i < _purchaseButtons.Length; i++)
            {
                if (i < _descriptibleUpgrades.Length)
                {
                    _purchaseButtons[i].OnClicked += PurchaseButtonsView_OnClicked;
                }
                else
                {
                    _purchaseButtons[i].gameObject.SetActive(false);
                }
            }
        }

        private void PurchaseButtonsView_OnClicked(object sender, System.EventArgs e)
        {
            _descriptibleUpgrades[i].Create().TryPurchase();
        }
    }
}