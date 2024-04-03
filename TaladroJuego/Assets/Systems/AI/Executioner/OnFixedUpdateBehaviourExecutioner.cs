using AISystem.Behaviour;
using AISystem.Selector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Executioner
{
    internal class OnFixedUpdateBehaviourExecutioner : MonoBehaviour
    {
        [SerializeField] BehaviourSelector _selector;

        private void FixedUpdate()
        {
            IBehaviour behaviour = _selector.GetPriorityBehaviour();
            Debug.Log(behaviour); 
            behaviour.RunBehaviour();
        }
    }
}

