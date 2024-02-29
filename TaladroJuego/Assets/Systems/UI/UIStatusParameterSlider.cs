using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    internal class UIStatusParameterSlider : MonoBehaviour
    {
        private Slider _statusParamSlider;

        public void UpdateUISlider(float quantity)
        {
            _statusParamSlider.value = quantity;
        }

        private void Awake()
        {
            _statusParamSlider = GetComponentInChildren<Slider>();
        }
    }
}

