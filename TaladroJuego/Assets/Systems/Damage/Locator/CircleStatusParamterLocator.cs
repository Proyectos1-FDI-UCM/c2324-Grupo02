using StatusSystem;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.Locator
{
    internal class CircleStatusParamterLocator : MonoBehaviour, IStatusParameterLocator
    {
        [SerializeField]
        private float _radius = 1.0f;
        [SerializeField]
        private LayerMask _layerMask = 0;

        private Transform _transform;





        private void Start()
        {
            _transform = GetComponent<Transform>();
        }


        public IStatusParameter[] TryGetStatus()
        {
            Collider2D[] _objects;
            _objects = Physics2D.OverlapCircleAll(_transform.position, _radius, _layerMask);

            List<IStatusParameter> foundObjects = new List<IStatusParameter>();

            foreach (Collider2D obj in _objects)
            {
                if (TryGetComponentInChildren(obj.gameObject, out IStatusParameter statusParameter))
                {
                    foundObjects.Add(statusParameter);
                }
            }

            return foundObjects.ToArray();

        }

        private static bool TryGetComponentInChildren<T>(GameObject gameObject, out T component) =>
            (component = gameObject.GetComponentInChildren<T>()) != null;
    }
}
