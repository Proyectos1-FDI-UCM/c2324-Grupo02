using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem.Evaluator
{
    internal class FarFromHomeBehaviourEvaluator : MonoBehaviour, IBehaviourEvaluator
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private AnimationCurve _evaluationCurve;
        [SerializeField] private Transform _transform;
        private Vector3 _home;

        public float GetPriority()
        {
            float distance = Mathf.Abs((_home - _transform.position).magnitude);
            float value = _evaluationCurve.Evaluate(Mathf.Clamp(distance / _maxDistance, 0, 1));
            Debug.Log(value);
            return value;
        }

        private void Awake()
        {
            _transform = transform;
            _home = _transform.position;
        }
    }
}

