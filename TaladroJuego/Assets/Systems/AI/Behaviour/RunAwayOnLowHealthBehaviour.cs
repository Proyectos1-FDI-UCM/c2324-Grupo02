using AISystem.Evaluator;
using AISystem.Runner.ChaseDirection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AISystem.Behaviour
{
    public class RunAwayOnLowHealthBehaviour : MonoBehaviour, IBehaviour
    {
        [SerializeField] private IsStatusLowBehaviourEvaluator _evaluator;
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

