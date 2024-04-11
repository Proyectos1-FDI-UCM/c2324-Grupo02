using UnityEngine;

namespace AISystem.Runner.ChaseDirection
{
    internal class FollowTargetDirection : MonoBehaviour, IChaseDirectionProvider
    {
        [SerializeField] private Transform _target;
        private Transform _transform;
        public Vector2 DirectionToChase()
        {
            return (_target.position - _transform.position).normalized;
        }

        private void Awake()
        {
            _transform = transform;
        }
    }
}

