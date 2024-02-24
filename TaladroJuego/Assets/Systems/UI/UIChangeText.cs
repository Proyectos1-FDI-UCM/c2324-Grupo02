using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    internal class UIChangeText : MonoBehaviour
    {
        private Slider _slider;
        private UIUpgradeText _upgradeText;

    // Start is called before the first frame update
        void Awake()
        {
            _slider = GetComponent<Slider>();
            _upgradeText = GetComponent<UIUpgradeText>();
        }

        // Update is called once per frame
        void Update()
        {
            _upgradeText.MejoraCaller(_slider.value);
        }
    }
}
