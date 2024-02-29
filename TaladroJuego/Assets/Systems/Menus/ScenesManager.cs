using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MenusSystem
{
    internal class ScenesManager : MonoBehaviour
    {
        [SerializeField]
        private Options _options;

        public void ChangeScene(string newScene)
        {
            SceneManager.LoadScene(newScene);
            _options.ShowSoundImage();
        }

        public void GameQuit()
        {
            Application.Quit();
        }
    }
}