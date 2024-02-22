using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class UIStatusParameterSlider : MonoBehaviour
    {
        private Slider statusParamSlider;

        public void UpdateUISlider(float quantity)
        {
            statusParamSlider.value = quantity;
        }

        private void Awake()
        {
            statusParamSlider = GetComponent<Slider>();
        }
    }
}

