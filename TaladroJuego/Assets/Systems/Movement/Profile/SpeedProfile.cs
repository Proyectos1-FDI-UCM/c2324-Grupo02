using UnityEngine;

namespace MovementSystem.Profile
{
    [CreateAssetMenu(fileName = "Speed Profile", menuName = "Speed Profile")]
    internal class SpeedProfile : ScriptableObject, ISpeedProvider
    {
        [SerializeField] private float _speed;

        public float GetSpeed()
        {
            return _speed;
        }
    }
}