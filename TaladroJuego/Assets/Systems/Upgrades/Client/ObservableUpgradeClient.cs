using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UpgradesSystem.Resource;

namespace UpgradesSystem.Client
{
    internal class ObservableUpgradeClient : MonoBehaviour, IUpgradeClient
    {
        private IUpgradeClient _upgradeClient;

        [field: SerializeField]
        public UnityEvent PurchasedSuccessfully { get; private set; }

        [field: SerializeField]
        public UnityEvent PurchaseFailed { get; private set; }

        private void Awake()
        {
            _upgradeClient = GetComponentsInChildren<IUpgradeClient>().FirstOrDefault(c => c != (IUpgradeClient)this);
        }

        public bool TryPurchase(IResourceUpgrade upgrade)
        {
            bool success = _upgradeClient.TryPurchase(upgrade);
            if (success)
                PurchasedSuccessfully.Invoke();
            else
                PurchaseFailed.Invoke();

            return success;
        }
    }
}