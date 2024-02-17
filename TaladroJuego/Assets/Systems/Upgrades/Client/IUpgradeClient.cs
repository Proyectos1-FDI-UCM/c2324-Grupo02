using UpgradesSystem.Resource;

namespace UpgradesSystem.Client
{
    public interface IUpgradeClient
    {
        bool TryPurchase(IResourceUpgrade upgrade);
    }
}