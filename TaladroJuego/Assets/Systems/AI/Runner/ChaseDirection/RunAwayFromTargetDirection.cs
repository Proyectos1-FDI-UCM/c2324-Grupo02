using UnityEngine;

namespace AISystem.Runner.ChaseDirection
{
    internal class RunAwayFromTargetDirection : MonoBehaviour, IChaseDirectionProvider
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

