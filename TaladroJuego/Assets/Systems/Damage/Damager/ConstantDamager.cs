using StatusSystem;
using UnityEngine;

namespace DamageSystem.Damager
{
    public class ConstantDamager : MonoBehaviour, IDamager
    {
        [SerializeField]
        private float _damage = 1.0f;
        public float Damage { get => _damage; set => _damage = value; }


        public bool TryDamage(IStatusParameter status)
        {
            status.Value -= _damage;
            return true;
        }

    }
}