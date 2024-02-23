using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    internal class UIUpgradeText : MonoBehaviour
    {
        private int mejora = 0;
        bool desbloqueo1 = false;
    
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
        private void UpgradeChooser()
        {
            if (desbloqueo1 && mejora == 1)
            {

            }
            else if (mejora == 0)
            {
                string text = "No Desbloqueada";
                ChangeButtonText(text);
            }
            else if ( desbloqueo1 &&  mejora == 2)
            {

            }


           
        }
            
    }
}

