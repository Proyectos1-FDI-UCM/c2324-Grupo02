using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusSystem;
using MovementSystem.Facade;

namespace MovementSystem.LastFart
{
    public class LastFartLauncher : MonoBehaviour
    {
        private bool lastFartEnabled = true;

        [SerializeField] private Transform parentTransform;
        private ImpulseMovementFacade impulseMovement;

        [SerializeField] private float launchPreparationTime = 6;
        [SerializeField] private float fartingTime = 4;

        public void EnableLastFart(bool value)
        {
            lastFartEnabled = value;
        }

        public void TryLaunchFart()
        {
            if (!lastFartEnabled) return;

            StartCoroutine(LaunchFart());
        }

        private IEnumerator LaunchFart()
        {
            yield return new WaitForSeconds(launchPreparationTime);

            impulseMovement.Move(parentTransform.up);

            yield return new WaitForSeconds(fartingTime);

            impulseMovement.Move(Vector2.zero);
        }

        void Start()
        {
            impulseMovement = GetComponent<ImpulseMovementFacade>();
        }
    }
}

