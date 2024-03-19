
using UnityEngine;
using RequireAttributes;

namespace StatusSystem
{
    public class OnMovementStatusReducer : MonoBehaviour
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
            ReduceStatus();

        }

        public void ReduceStatus()
        {
            if (_rb.velocity != Vector2.zero) _statusParameter.Value -= _valueToReduce;
        }


    }
}

