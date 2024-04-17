using MovementSystem.Facade;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AISystem
{
    internal class SinusoidalMovementDirectionRotatorApplier : MonoBehaviour
    {
        [SerializeField] private Transform[] _bones;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private AnimationCurve _curve;
        private (IMovementFacade<Vector2> movementFacade, Transform transform)[] _movementFacades;

        private void Awake()
        {
            _movementFacades = _bones.Select(bone => (bone.GetComponent<IMovementFacade<Vector2>>(), bone)).ToArray();
        }

        private void Update()
        {
            Transform previousTransform = _movementFacades[0].transform;
            for(int i = 0; i < _movementFacades.Length; i++)
            {
                float t = 1 - (i / _movementFacades.Length);
                (IMovementFacade<Vector2> mF, Transform tF) = _movementFacades[i];
                Vector3 boneDirection = Vector3.Slerp(tF.right, previousTransform.InverseTransformDirection(_rigidbody.velocity.normalized), _curve.Evaluate(t + Mathf.Sin(1 - t) * 2 * Mathf.PI));
                mF.Move(boneDirection);  
                previousTransform = tF;
            }
        }
    }

}
