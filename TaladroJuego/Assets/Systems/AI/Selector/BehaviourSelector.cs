using AISystem.Behaviour;
using System.Linq;
using UnityEngine;

namespace AISystem.Selector
{
    internal class BehaviourSelector : MonoBehaviour
    {
        private IBehaviour[] _behaviours;

        private void Start()
        {
            _behaviours = GetComponentsInChildren<IBehaviour>();
        }

        public void RunPriorityBehaviour()
        {
            _behaviours.OrderByDescending(b => b.GetPriority()).FirstOrDefault()?.RunBehaviour();
        }
    }
}

