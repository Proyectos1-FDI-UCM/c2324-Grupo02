using StatusSystem;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.Locator
{
    internal class ColliderStatusParameterLocator : MonoBehaviour, IStatusParameterLocator
    {
        [SerializeField] private LayerMask _layerMask;
        private List<IStatusParameter> _foundObjects;


        private void Awake()
        {
            _foundObjects = new List<IStatusParameter>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if ((((1 << collision.gameObject.layer) & _layerMask) != 0)
                && TryGetStatusInChildren(collision.gameObject, out IStatusParameter statusParameter))
            {
                _foundObjects.Add(statusParameter);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if ((((1 << collision.gameObject.layer) & _layerMask) != 0)
                && TryGetStatusInChildren(collision.gameObject, out IStatusParameter statusParameter))
            {
                _foundObjects.Remove(statusParameter);
            }
        }

        private bool TryGetStatusInChildren(GameObject parent, out IStatusParameter statusParameter)
        {
            return (statusParameter = parent.GetComponentInChildren<IStatusParameter>()) != null;
        }

        public IStatusParameter[] TryGetStatus()
        {
            return _foundObjects.ToArray();
        }
    }
}
