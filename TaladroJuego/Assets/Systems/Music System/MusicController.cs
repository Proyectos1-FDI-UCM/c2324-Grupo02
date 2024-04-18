using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace MusicSystem
{
    internal class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioSource _song; 

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                ChangeVolume();
            }
        }

        private void ChangeVolume()
        {
            _song.volume = 1;
        }
    }
}