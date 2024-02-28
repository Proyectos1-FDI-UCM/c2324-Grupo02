using System.Linq;
using UnityEngine;

namespace MovementSystem.Profile
{
    public class SpeedMultiplier : MonoBehaviour, ISpeedProvider
    {
        [SerializeField] private float _multiplier;
        private ISpeedProvider _provider;

        public float Multiplier
        {
            get { return _multiplier; }
            set
            {
                _multiplier = value;
            }
        }

        public float GetSpeed()
        {
            return _provider.GetSpeed() * _multiplier;
        }

        private void Awake()
        {
            _provider = GetComponentsInChildren<ISpeedProvider>().FirstOrDefault(s => s != (ISpeedProvider)this);
        }
    }
}