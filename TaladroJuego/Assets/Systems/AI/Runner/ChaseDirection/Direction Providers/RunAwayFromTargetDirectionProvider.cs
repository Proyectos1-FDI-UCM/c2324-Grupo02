using UnityEngine;

namespace AISystem.Runner.ChaseDirection.DirectionProvider
{
    internal class RunAwayFromTargetDirectionProvider : MonoBehaviour, IDirectionProvider
    {
        [SerializeField] private Transform _target;
        private Transform _transform;

        public Vector2 DirectionToChase()
        {
            return (_transform.position - _target.position).normalized;
        }

        private void Awake()
        {
            _transform = transform;
        }
    }
}

