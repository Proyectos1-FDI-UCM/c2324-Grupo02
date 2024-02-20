using StatusSystem;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.Locator
{
    internal class TriggerStatusParameterLocator : MonoBehaviour, IStatusParameterLocator
    {
       

        private List<IStatusParameter> _foundObjects;


        private void Awake()
        {
            _foundObjects = new List<IStatusParameter>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IStatusParameter>(out IStatusParameter statusParameter))
            {
                _foundObjects.Add(statusParameter);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IStatusParameter>(out IStatusParameter statusParameter))
            {
                _foundObjects.Remove(statusParameter);
            }
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
