using AISystem.Behaviour;
using System.Linq;
using UnityEngine;

namespace AISystem.Selector
{
    internal class BehaviourSelector : MonoBehaviour
    {
        private IBehaviour[] _behaviours;

        private void Awake()
        {
            _behaviours = GetComponentsInChildren<IBehaviour>();
        }

        public IBehaviour GetPriorityBehaviour()
        {
            IBehaviour priorityBehaviour = _behaviours[0];

            for (int i = 1; i < _behaviours.Length; i++) 
               if (_behaviours[i].GetPriority() > priorityBehaviour.GetPriority()) priorityBehaviour = _behaviours[i];

            return priorityBehaviour;
        }
    }
}

