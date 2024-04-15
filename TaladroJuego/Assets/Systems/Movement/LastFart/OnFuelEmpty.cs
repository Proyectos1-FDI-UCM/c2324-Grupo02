using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResourceCollectionSystem;
using UpgradesSystem.Flyweight;
using Codice.Client.BaseCommands.WkStatus.Printers;
using MovementSystem.LastFart;

namespace MovementSystem.LastFart
{
    public class OnFuelEmpty : MonoBehaviour
    {

        [SerializeField ]private LastFartLauncher  _lastFartLauncher;

        [SerializeField] private ResourcesContainer resources;
        [SerializeField] private GameObject gameOverPanel;

        [SerializeField] private PauseRequesterObject _pauseRequester;
        public void NoFuel()
        {
            resources.ResourceQuantities.TryGetValue(ResourceType.Charcoal, out int amount);
            if (amount < 9 && !_lastFartLauncher.IsFartEnabled) // No se acceder al coste de la mejora
            {
                gameOverPanel.SetActive(true);
                _pauseRequester.RequestPause();
            }
        }
    }
}