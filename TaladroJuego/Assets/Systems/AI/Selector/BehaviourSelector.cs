using AISystem.Behaviour;
using System.Linq;
using UnityEngine;

namespace AISystem.Selector
{
    internal class BehaviourSelector : MonoBehaviour
    {
        private IBehaviour[] _behaviours;
        [SerializeField] private int _numberOfBehaviours;

        private void Start()
        {
            _behaviours = new IBehaviour[_numberOfBehaviours];
            for (int i = 0; i < _numberOfBehaviours; i++)
            {
                _behaviours[i] = GetComponentInChildren<IBehaviour>();
            }
        }

        public void RunPriorityBehaviour()
        {
            _behaviours.OrderByDescending(b => b.GetPriority()).FirstOrDefault()?.RunBehaviour();
        }
    }
}

