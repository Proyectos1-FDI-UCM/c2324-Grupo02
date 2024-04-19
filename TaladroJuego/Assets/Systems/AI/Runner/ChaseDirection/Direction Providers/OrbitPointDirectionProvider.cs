using System.Collections;
using UnityEngine;

namespace AISystem.Runner.ChaseDirection.DirectionProvider
{
    internal class OrbitPointDirectionProvider : MonoBehaviour, IDirectionProvider
    {

        [SerializeField] private Bounds _bounds;
        [SerializeField] private float _radius;
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private float _changePointTimer;
        [SerializeField] private float _angularSpeed;
        private Vector3 _startPosition;

        private Vector2 _point;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        private void Start()
        {
            StartCoroutine(GenerateRandomPoint());
        }

        public Vector2 DirectionToChase()
        {
            return ((_radius * new Vector2(Mathf.Cos(_angularSpeed * Time.time), Mathf.Sin(_angularSpeed * Time.time)) + _point) - _rigidBody.position).normalized;
        }

        private IEnumerator GenerateRandomPoint()
        {
            while (true)
            {
                Bounds bounds = GetBoundsFromLocal(_bounds, _startPosition);
                _point.x = Random.Range(bounds.min.x, bounds.max.x);
                _point.y = Random.Range(bounds.min.y, bounds.max.y);

                yield return new WaitForSeconds(_changePointTimer);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(transform.TransformPoint(_bounds.center), _bounds.size);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_point, _radius);
        }

        private static Bounds GetBoundsFromLocal(Bounds bounds, Vector3 offset)
        {
            return new Bounds(bounds.center + offset, bounds.size);
        }

        private static Bounds GetBoundsFromLocal(Bounds bounds, Transform transform)
        {
            return new Bounds(transform.TransformPoint(bounds.center), bounds.size);
        }
    }
}

