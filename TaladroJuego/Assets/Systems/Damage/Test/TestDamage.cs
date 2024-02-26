using DamageSystem.Handler;
using StatusSystem;
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

        [ContextMenu(nameof(DebugNearbyDamagedEntities))]
        private void DebugNearbyDamagedEntities()
        {
            IStatusParameter[] damagedObjects; 
            damagedObjects = _handler.Damage();

            print(damagedObjects.Length);
        }

    }
}
