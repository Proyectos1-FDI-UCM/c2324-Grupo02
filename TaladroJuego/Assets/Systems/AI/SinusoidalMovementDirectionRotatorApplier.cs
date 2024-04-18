using UnityEngine;

namespace AISystem
{
    internal class SinusoidalMovementDirectionRotatorApplier : MonoBehaviour
    {
        [SerializeField]
        [Min(0)]
        private int _segmentCount;

        [SerializeField]
        private LineRenderer _lineRenderer;
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float _segmentSeparation;
        [SerializeField]
        private float _smoothedSpeed;

        private Vector3[] _segmentPositions;
        private Vector3[] _segementVelocities;

        private void Awake()
        {
            _lineRenderer.positionCount = _segmentCount;
            _segmentPositions = new Vector3[_segmentCount];
            _segementVelocities = new Vector3[_segmentCount];

            _segmentPositions[0] = _target.position;

            for (int i = 1; i < _segmentPositions.Length; i++)
            {
                _segmentPositions[i] = _segmentPositions[i - 1] + (_segmentPositions[i] - _segmentPositions[i - 1]).normalized * _segmentSeparation;
            }

            _lineRenderer.SetPositions(_segmentPositions);
        }

        private void Update()
        {
            _segmentPositions[0] = _target.position;

            for (int i = 1; i < _segmentPositions.Length; i++)
            {
                Vector3 targetPosition = _segmentPositions[i - 1] + (_segmentPositions[i] - _segmentPositions[i - 1]).normalized * _segmentSeparation;
                _segmentPositions[i] = Vector3.SmoothDamp(_segmentPositions[i], targetPosition, ref _segementVelocities[i], _smoothedSpeed);
            }

            _lineRenderer.SetPositions(_segmentPositions);
        }
    }

}
