using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicSystem
{
    internal class LeviMusicActivator : MonoBehaviour
    {

        [SerializeField] private Transform _leviTransform;
        [SerializeField] private Camera _camera;
        [SerializeField] private MusicController _musicController;

        // Update is called once per frame
        void Update()
        {
 

            if (InViewport(_leviTransform.position, _camera))
            {
                _musicController.ChangeVolumeUp();           
            }
            else
            {
                _musicController.ChangeVolumeDown();             
            }
        }

        private static bool InViewport(Vector3 positionWS, Camera camera)
        {
            Vector3 viewportPoint = camera.WorldToViewportPoint(positionWS);
            return viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;
        }
    }
}
