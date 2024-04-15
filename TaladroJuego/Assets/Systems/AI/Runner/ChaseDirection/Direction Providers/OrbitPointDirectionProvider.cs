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

        private Vector2 _point;

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
                _point.x = Random.Range(_bounds.min.x, _bounds.max.x);
                _point.y = Random.Range(_bounds.min.y, _bounds.max.y);

                yield return new WaitForSeconds(_changePointTimer);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_point, _radius);
        }
    }
}

