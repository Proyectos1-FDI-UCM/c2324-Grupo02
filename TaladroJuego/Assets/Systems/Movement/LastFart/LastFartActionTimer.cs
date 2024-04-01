using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatusSystem;
using UnityEngine.Events;

namespace MovementSystem.LastFart
{
    internal class LastFartActionTimer : MonoBehaviour
    {
        [SerializeField] private StatusParameter fuelStatus;

        [SerializeField] private float lastFartLaunchTime = 6;
        private float fartTimeLeft;
        private bool countingForFart = false;

        public UnityEvent fartTimeReached;

        public void StartFartCountDown()
        {
            countingForFart = true;
        }

        public void ConsiderFartCancelation(float fuelChangedValue)
        {
            if (fuelChangedValue <= 0) return;

            countingForFart = false;
            fartTimeLeft = lastFartLaunchTime;
        }

        void Start()
        {
            fartTimeLeft = lastFartLaunchTime;
        }

        void Update()
        {
            
        }
    }
}

