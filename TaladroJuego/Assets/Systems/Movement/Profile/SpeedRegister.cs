﻿using UnityEngine;

namespace MovementSystem.Profile
{
    public class SpeedRegister : MonoBehaviour, ISpeedProvider
    {
        [SerializeField] private SpeedProfile _speedProvider;

        public SpeedProfile SpeedProvider
        {
            set
            {
                _speedProvider = value;
            }
        }

        public float GetSpeed()
        {
            return _speedProvider.GetSpeed();
        }
    }
}