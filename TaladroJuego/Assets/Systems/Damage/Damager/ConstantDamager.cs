using StatusSystem;
using UnityEngine;

namespace DamageSystem.Damager
{
    internal class ConstantDamager : MonoBehaviour, IDamager
    {
        [SerializeField]
        private float _damage = 1.0f;
        public float Damage { set =>  _damage = value;  }


        public bool TryDamage(IStatusParameter status)
        {
            status.Value -= _damage;
            return true;
        }

    }
}