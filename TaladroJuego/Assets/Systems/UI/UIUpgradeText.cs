using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    internal class UIUpgradeText : MonoBehaviour
    {
        public  float mejora = 0;
        public bool desbloqueo1 = false;
    
        private TMP_Text _upgradeTextMesh;
        void Awake()
        {
            _upgradeTextMesh = GetComponent<TMP_Text>();
        }

        // Update is called once per frame
        void Update()
        {
            UpgradeChooser();

        }

        private void ChangeButtonText(string text)
        {
            _upgradeTextMesh.text = text;
        }
        public void MejoraCaller (float mejorar)
        {
            mejora = mejorar;
        }
        private void UpgradeChooser()
        {
            if ( mejora == 1)
            {
                string text = "Last fart 50H 30C";
                ChangeButtonText(text);
            }
            else if (mejora == 0)
            {
                string text = "No Desbloqueada";
                ChangeButtonText(text);
            }
            else if (   mejora == 2)
            {
                string text = "Velocidad  50H 30C";
                ChangeButtonText(text);
            }
            else if (mejora == 3 && desbloqueo1)
            {
                string text = "Velocidad 2 50H 30C";
                ChangeButtonText(text);
            }
            else if (mejora == 3 && !desbloqueo1)
            {
                mejora++;
            }
            else if (mejora == 4)
            {
                string text = "Mayor vida 50H 30C";
                ChangeButtonText(text);
            }



        }
            
    }
}

