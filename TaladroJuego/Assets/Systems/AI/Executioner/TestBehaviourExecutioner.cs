using AISystem.Selector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Executioner
{
    internal class TestBehaviourExecutioner : MonoBehaviour
    {
        [SerializeField] private float _updateTime;
        [SerializeField] private BehaviourSelector _selector;
       
        private IEnumerator UpdateBehaviour()
        {
            while (true)
            {
                yield return new WaitForSeconds(_updateTime);
                _selector.RunPriorityBehaviour();             
            }
        }
        private void Start()
        {
            StartCoroutine(UpdateBehaviour());  
        }

    }

}

