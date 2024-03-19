using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    internal class UIStatusParameterSlider : MonoBehaviour
    {
        private Image _statusParamSlider;

        public void UpdateUISlider(float quantity)
        {
            _statusParamSlider.fillAmount = quantity/100;
        }

        private void Awake()
        {
            _statusParamSlider = GetComponent<Image>();
        }
    }
}

