using UnityEngine;

namespace MovementSystem.Profile
{
    internal class SpeedMultiplier : ScriptableObject, ISpeedProvider
    {
        [SerializeField] private float _multiplier;

        public float GetSpeed()
        {
            return _multiplier;
        }
    }
}