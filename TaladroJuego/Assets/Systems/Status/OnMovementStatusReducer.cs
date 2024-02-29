using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RequireAttributes;

namespace StatusSystem
{
    internal class OnMovementStatusReducer : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _valueToReduce;
        [SerializeField, RequireInterface(typeof(IStatusParameter))] private Object _statusParameterObject;
        private IStatusParameter _statusParameter;

        private void Awake()
        {
            _statusParameter = _statusParameterObject as IStatusParameter;
        }

        private void FixedUpdate()
        {
            if (_rb.velocity != Vector2.zero) _statusParameter.ReduceValue(_valueToReduce);

        }
    }
}

