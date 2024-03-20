using AISystem.Evaluator;
using AISystem.Runner.ChaseDirection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Behaviour
{
    public class ChaseOnProximityAndTargetLowStatusBehaviour : MonoBehaviour, IBehaviour
    {
        [SerializeField] private ProximityBehaviourEvaluator _proximityEvaluator;
        [SerializeField] private IsStatusLowBehaviourEvaluator _statusEvaluator;

        [SerializeField] private ChaseDirectionBehaviourRunner _runner;

        public float GetPriority()
        {
            return _proximityEvaluator.GetPriority() * _statusEvaluator.GetPriority();
        }

        public void RunBehaviour()
        {
            _runner.RunBehaviour();
        }
    }
}

