using StatusSystem;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.Locator
{
    internal class TriggerStatusParameterLocator : MonoBehaviour, IStatusParameterLocator
    {
        [SerializeField] private LayerMask _layerMask;

        private List<IStatusParameter> _foundObjects;


        private void Awake()
        {
            _foundObjects = new List<IStatusParameter>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((((1 << collision.gameObject.layer) & _layerMask) != 0) 
                && TryGetStatusInChildren(collision.gameObject, out IStatusParameter statusParameter))
            {
                _foundObjects.Add(statusParameter);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
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

        //public IEnumerable<IStatusParameter> TryGetStatus2()
        //{
        //    return _foundObjects;
        //}
    }
}
