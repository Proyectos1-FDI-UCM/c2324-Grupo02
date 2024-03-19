using AISystem.Evaluator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RequireAttributes;
using StatusSystem;

namespace AISystem.Evaluator
{
    public class IsStatusLowBehaviourEvaluator : MonoBehaviour, IBehaviourEvaluator
    {
        [SerializeField, RequireInterface(typeof(IStatusParameter))]private Object _statusParameterObject;
        private IStatusParameter _statusParameter;

        [SerializeField] private float _minStatus, _maxStatus;
        
        public float GetPriority()
        {
            return 1 - (_statusParameter.Value - _minStatus) / (_maxStatus - _minStatus);
        }

        private void Awake()
        {
            _statusParameter = _statusParameterObject as IStatusParameter;
        }
    }
}

