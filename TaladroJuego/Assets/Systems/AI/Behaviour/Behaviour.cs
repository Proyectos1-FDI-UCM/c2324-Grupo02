using AISystem.Behaviour;
using AISystem.Evaluator;
using AISystem.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Behaviour
{
    internal class Behaviour : MonoBehaviour, IBehaviour
    {
        private IBehaviourEvaluator _evaluator;
        private IBehaviourRunner _runner;
        public float GetPriority()
        {
           return _evaluator.GetPriority();
            
        }

        public void RunBehaviour()
        {
            _runner.RunBehaviour();
        }

        private void Awake()
        {
            _evaluator = GetComponentInChildren<IBehaviourEvaluator>();
            _runner = GetComponentInChildren<IBehaviourRunner>();
        }
    }

}
