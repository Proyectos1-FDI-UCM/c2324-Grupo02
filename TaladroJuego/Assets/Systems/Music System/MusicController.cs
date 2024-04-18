using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MusicSystem
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioSource _song; 

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                ChangeVolumeUp();
            }
        }


        public void ChangeVolumeUp()
        {
            _song.volume = 1;
            
        }
        public void ChangeVolumeDown()
        {
            _song.volume = 0;
        }

        //public void LeviMusicActivator()
        //{
        //    _song.Play();
        //}
        //public void LeviMusicStopper()
        //{
        //    _song.Stop();
        //}
    }
}