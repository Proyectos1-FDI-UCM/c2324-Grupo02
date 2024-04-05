using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
namespace MusicSystem
{
    public class MusicSystem : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] AudioClip _songMenu; //Canción Menu
        [SerializeField] AudioClip _songFase1; //Cancion Fase 1
        [SerializeField] AudioClip _songFase2; //Cancion Fase 2
        [SerializeField] AudioClip _songFase3; //Cancion Fase 3
        [SerializeField] AudioClip _songFase4; //Cancion Fase 4
        [SerializeField] AudioClip _songLeviatan; //Cancion Leviathan
        private int _songChooser;
        private void Awake()
        {
            _musicSource = GetComponent<AudioSource>();
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_songChooser">
        /// _songChooser = 0 :Canción Menu
        /// _songChooser = 1 :Cancion Fase 1
        /// _songChooser = 2 :Cancion Fase 2
        /// _songChooser = 3 :Cancion Fase 3
        /// _songChooser = 4 :Cancion Fase 4
        /// _songChooser = 5 :Cancion Leviathan
        /// </param>
        public void SongPlayer(int _songChooser)
         {
            if (_songChooser == 0) 
            {
                _musicSource.PlayOneShot(_songMenu);
            }
            if (_songChooser == 1)
            {
                _musicSource.PlayOneShot(_songFase1);
            }
            if ( _songChooser == 2)
            {
                _musicSource.PlayOneShot(_songFase2);
            }
            if (_songChooser == 3)
            {
                _musicSource.PlayOneShot(_songFase3);
            }
            if (_songChooser == 4)
            {
                _musicSource.PlayOneShot(_songFase4);
            }
            if(_songChooser == 5)
            {
                _musicSource.PlayOneShot(_songLeviatan);
            }
         }  
   

    }

}
