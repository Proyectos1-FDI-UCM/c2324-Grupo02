using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicSystem
{
    internal class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioSource _songFase1; //Cancion Fase 1
        [SerializeField] private AudioSource _songFase2; //Cancion Fase 2
        [SerializeField] private AudioSource _songFase3; //Cancion Fase 3
        [SerializeField] private AudioSource _songFase4; //Cancion Fase 4


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision != null)
            {
                _songFase2.volume = 1;
            }
        }
    }
}