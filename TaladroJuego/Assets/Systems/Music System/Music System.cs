using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
namespace MusicSystem
{
    public class MusicSystem : MonoBehaviour
    {

        [SerializeField] private AudioSource _musicSource1;
        [SerializeField] private AudioSource _musicSource2; 
        [SerializeField] private AudioSource _musicSource3;
        [SerializeField] private AudioSource _musicSource4;
        [SerializeField] private AudioSource _musicSource5;
        [SerializeField] private AudioSource _musicSource6;
        [SerializeField] AudioClip _songMenu; //Canción Menu
        [SerializeField] AudioClip _songFase1; //Cancion Fase 1
        [SerializeField] AudioClip _songFase2; //Cancion Fase 2
        [SerializeField] AudioClip _songFase3; //Cancion Fase 3
        [SerializeField] AudioClip _songFase4; //Cancion Fase 4
        [SerializeField] AudioClip _songLeviatan; //Cancion Leviathan
        private int _songChooser;
        private void Awake()
        {
            //SongPlayer(_songChooser);
            _musicSource2.volume = 0;
            _musicSource3.volume = 0;
            _musicSource4.volume = 0;
            _musicSource5.volume = 0;
            _musicSource6.volume = 0;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_songChooser">
        /// _songChooser = 0:Empieza cancion Fase 1
        /// _songChooser = 1:Empieza cancion Fase 2
        /// _songChooser = 2:Empieza cancion Fase 3
        /// _songChooser = 3:Empieza cancion Fase 4
        /// _songChooser = 4:Empieza cancion Leviatan
        /// </param>
        public void SongPlayer(int _songChooser)
         {
            //


            if (_songChooser == 0) 
            {
                 _musicSource2.volume = 1;
            }
            if (_songChooser == 1)
            {
                _musicSource3.volume = 1;
            }
            if ( _songChooser == 2)
            {
                _musicSource4.volume = 1;
            }
            if (_songChooser == 3)
            {
                _musicSource5.volume = 1;
            }
            if (_songChooser == 4)
            {
                _musicSource6.volume = 1;
            }          
         }  
   

    }

}
