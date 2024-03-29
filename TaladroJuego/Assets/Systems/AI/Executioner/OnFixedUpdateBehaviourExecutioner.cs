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
            _selector.RunPriorityBehaviour();
        }
    }
}

