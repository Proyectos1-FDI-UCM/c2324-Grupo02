using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Evaluator
{
    internal class ConstantValueBehaviourEvaluator : MonoBehaviour, IBehaviourEvaluator
    {
        [SerializeField] private float _value;
        public float GetPriority() => _value;
    }
}
