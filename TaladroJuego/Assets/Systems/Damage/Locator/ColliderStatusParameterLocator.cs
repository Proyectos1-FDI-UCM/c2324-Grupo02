using StatusSystem;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.Locator
{
    internal class ColliderStatusParameterLocator : MonoBehaviour, IStatusParameterLocator
    {
        private List<IStatusParameter> _foundObjects;


        private void Awake()
        {
            _foundObjects = new List<IStatusParameter>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IStatusParameter>(out IStatusParameter statusParameter))
            {
                _foundObjects.Add(statusParameter);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IStatusParameter>(out IStatusParameter statusParameter))
            {
                _foundObjects.Remove(statusParameter);
            }
        }

      

        public IStatusParameter[] TryGetStatus()
        {
            return _foundObjects.ToArray();
        }
    }
}
