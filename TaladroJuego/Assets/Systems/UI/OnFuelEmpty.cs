using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResourceCollectionSystem;
using UpgradesSystem.Flyweight;
using Codice.Client.BaseCommands.WkStatus.Printers;

namespace UISystem
{
    public class OnFuelEmpty : MonoBehaviour
    {
        [SerializeField] private ResourcesContainer resources;
        [SerializeField] private GameObject gameOverPanel;
        public void NoFuel()
        {
            resources.ResourceQuantities.TryGetValue(ResourceType.Charcoal, out int amount);
            if (amount < 9) // No se acceder al coste de la mejora
            {
                gameOverPanel.SetActive(true);
            }
        }
    }
}