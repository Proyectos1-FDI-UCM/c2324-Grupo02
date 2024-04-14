using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusSystem;
using MovementSystem.Facade;

namespace MovementSystem.LastFart
{
    public class LastFartLauncher : MonoBehaviour
    {
        private bool lastFartEnabled = false;

        private bool isFartEnabled = false;
        public bool IsFartEnabled { get => isFartEnabled; set => isFartEnabled = value; }

        [SerializeField] private Transform parentTransform;
        [SerializeField] private GameObject rotationSystem;
        private ImpulseMovementFacade impulseMovement;

        [SerializeField] private float launchPreparationTime = 6;
        [SerializeField] private float fartingTime = 4;

        

        private Coroutine fart;

        


        public void EnableLastFart(bool value)
        {
            lastFartEnabled = value;
        }

        public void TryLaunchFart()
        {
            if (!lastFartEnabled) return;
            Debug.Log("Trying fart");

            fart ??= StartCoroutine(LaunchFart());
        }

        private IEnumerator LaunchFart()
        {
            yield return new WaitForSeconds(launchPreparationTime);

            rotationSystem.SetActive(false);

            Vector2 impulseDir = parentTransform.up;
            impulseMovement.Move(impulseDir);

            yield return new WaitForSeconds(fartingTime);

            impulseMovement.Move(-impulseDir);
            rotationSystem.SetActive(true);
            IsFartEnabled = false;

            fart = null;
        }

        void Start()
        {
            impulseMovement = GetComponent<ImpulseMovementFacade>();
        }
    }
}

