using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenusSystem
{
    public class OnOffButtonChanger : MonoBehaviour
    {
        [SerializeField] private GameObject _textOn;
        [SerializeField] private GameObject _textOff;

        private bool _change = false;

        public void OnButtonPressed()
        {
            _textOn.gameObject.SetActive(!_change);
            _textOff.gameObject.SetActive(_change);
            _change = !_change;
        }


    }
}