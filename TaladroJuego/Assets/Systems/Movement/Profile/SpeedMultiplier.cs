using System.Linq;
using UnityEngine;

namespace MovementSystem.Profile
{
    internal class SpeedMultiplier : MonoBehaviour, ISpeedProvider
    {
        [SerializeField] private float _multiplier;
        private ISpeedProvider _provider;
        public float GetSpeed()
        {
            return _provider.GetSpeed() * _multiplier;
        }

        private void Awake()
        {
            //FindSpeedProvider();
            _provider = GetComponentsInChildren<ISpeedProvider>().FirstOrDefault(s => s != (ISpeedProvider)this);
        }

        private void FindSpeedProvider()
        {
            ISpeedProvider[] providers = GetComponentsInChildren<ISpeedProvider>();

            int i = 0;
            while (i < providers.Length && _provider == null)
            {
                if (providers[i] != (ISpeedProvider)this) _provider = providers[i];
            }
        }
    }
}