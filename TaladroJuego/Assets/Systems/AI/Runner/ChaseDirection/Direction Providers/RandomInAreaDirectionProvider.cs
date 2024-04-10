using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Runner.ChaseDirection
{
    internal class RandomInAreaDirectionProvider : MonoBehaviour, IDirectionProvider
    {
        [SerializeField] private float _changeDirectionTime;
        [SerializeField] private Bounds _bounds;
        [SerializeField] private Transform _transform;

        private Vector3 _point;
        public Vector2 DirectionToChase()
        {
            return (_point - _transform.position).normalized;
        }

        private void Start()
        {
            StartCoroutine(DirectionChangingCoroutine());
        }

        private IEnumerator DirectionChangingCoroutine()
        {
            while (true)
            {
                _point.x = Random.Range(_bounds.min.x, _bounds.max.x);
                _point.y = Random.Range(_bounds.min.y, _bounds.max.y);

                yield return new WaitForSeconds(_changeDirectionTime);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(_bounds.center, _bounds.size);
        }
    }

}
