using AISystem.Evaluator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Evaluator
{
    public class ProximityBehaviourEvaluator : MonoBehaviour, IBehaviourEvaluator
    {
        [SerializeField] private AnimationCurve _evaluationCurve;
        [SerializeField] private Transform _proximityTransform;
        private Transform _transform;

        [SerializeField] private float _maxDistance, _minDistance;
        private void Awake()
        {
            _transform = transform;
        }
        public float GetPriority()
        {
            float distance = (_proximityTransform.position - _transform.position).magnitude;
            return 1 - (distance - _minDistance) / (_maxDistance - _minDistance);
        }
    }
}

