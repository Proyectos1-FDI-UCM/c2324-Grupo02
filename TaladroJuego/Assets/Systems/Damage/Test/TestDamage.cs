using DamageSystem.Handler;
using StatusSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DamageSystem.Test
{
    internal class TestDamage : MonoBehaviour
    {
        private DamageHandler _handler;

        private void Awake()
        {
            _handler = GetComponent<DamageHandler>();
        }

        [ContextMenu("testButton")]
        private void Test()
        {
            IStatusParameter[] damagedObjects; 
            damagedObjects = _handler.Damage();

            print(damagedObjects.Length);
        }

    }
}
