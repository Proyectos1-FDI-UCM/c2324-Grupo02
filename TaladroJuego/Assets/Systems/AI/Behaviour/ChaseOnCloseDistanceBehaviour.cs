using AISystem.Evaluator;
using AISystem.Runner.ChaseDirection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Behaviour 
{
    public class ChaseOnCloseDistanceBehaviour : MonoBehaviour, IBehaviour
    {
        [SerializeField] private ProximityBehaviourEvaluator _evaluator;
        [SerializeField] private ChaseDirectionBehaviourRunner _runner;
        public float GetPriority()
        {
            return _evaluator.GetPriority();
        }

        public void RunBehaviour()
        {
            _runner.RunBehaviour();
        }
    }
}

