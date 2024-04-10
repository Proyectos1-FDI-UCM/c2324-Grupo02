using UnityEngine;

namespace AISystem.Runner.ChaseDirection.DirectionProvider
{
    internal class FollowTargetDirectionProvider : MonoBehaviour, IDirectionProvider
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

