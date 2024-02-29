using DamageSystem.Damager;
using DamageSystem.Handler;
using System.Collections;
using StatusSystem;
using System.Collections.Generic;
using UnityEngine;
namespace DamageSystem.Executioner
{
    internal class PeriodicDamageExecutioner : MonoBehaviour
    {
        [SerializeField] private DamageHandler _damageHandler;
        [SerializeField] private float _interval;
        private Coroutine _coroutine;
        [SerializeField] private bool _startOnStart; //lol?

        public void StartDamageCoroutine()
        {
            _coroutine = StartCoroutine(DamageCoroutine());
        }
        public void StopDamageCoroutine()
        {
            if(_coroutine != null) StopCoroutine(_coroutine);
        }
        private void Start()
        {
            if (_startOnStart) StartDamageCoroutine();
        }
        private void OnDisable()
        {
            StopDamageCoroutine();
        }

        private IEnumerator DamageCoroutine()
        {
            WaitForSeconds wait = new WaitForSeconds(_interval);

            while (true)
            {
                foreach (IStatusParameter status in _damageHandler.Damage())
                {
                    Debug.Log(status);
                }
                yield return wait;
            }
        }
    }
}

