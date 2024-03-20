using DamageSystem.Damager;
using DamageSystem.Locator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusSystem;

namespace DamageSystem.Handler
{
    public class DamageHandler : MonoBehaviour
    {
        
        private IStatusParameterLocator _statusLocator;
        public IStatusParameterLocator StatusLocator { set { _statusLocator = value; } }

        private IDamager _damager;


        private void Awake()
        {
            _statusLocator = GetComponent<IStatusParameterLocator>();
            _damager = GetComponent<IDamager>();
        }

        public IStatusParameter[] Damage()
        {
            IStatusParameter[] statusParameters;
            statusParameters = _statusLocator.TryGetStatus();

            List<IStatusParameter> damagedStatusParameters = new List<IStatusParameter>();

            foreach (IStatusParameter statusParameter in statusParameters)
            {

               if(_damager.TryDamage(statusParameter))
                {
                    damagedStatusParameters.Add(statusParameter);
                    Debug.Log($"{_damager} damaged {statusParameter}", this);
                }
            }

            return damagedStatusParameters.ToArray();           
        }
       
    }
}

