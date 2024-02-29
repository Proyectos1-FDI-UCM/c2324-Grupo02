using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MenusSystem {
    internal class Options : MonoBehaviour
    {

        //sounds
        [SerializeField]
        private Slider _soundSlider;
        [SerializeField]
        private float _soundSliderValue = 0;
        [SerializeField]
        private Image _muteImage;
        [SerializeField]
        private Image _soundImage;


        //full screen 
        [SerializeField]
        private Toggle _fullScreenToggle;

        void Start()
        {
            //sound
            _soundSlider.value = PlayerPrefs.GetFloat("audioVolume", 0.5f);
            AudioListener.volume = _soundSlider.value;
            ShowSoundImage();

            //full screen
            if (Screen.fullScreen)
            {
                _fullScreenToggle.isOn = true;
            }
            else
            {
                _fullScreenToggle.isOn = false;
            }
        }



        public void ChangeSoundSlider(float value)
        {
            _soundSliderValue = value;
            PlayerPrefs.SetFloat("audioVolume", _soundSliderValue);
            AudioListener.volume = _soundSlider.value;
            ShowSoundImage();

        }

        public void ShowSoundImage()
        {
            if (_soundSliderValue == 0)
            {
                _soundImage.enabled = false;
                _muteImage.enabled = true;
            }

            else
            {
                _muteImage.enabled = false;
                _soundImage.enabled = true;
            }
        }

        public void ActivateFullScreen(bool fullScreen)
        {
            Screen.fullScreen = fullScreen;
        }
    }
}
