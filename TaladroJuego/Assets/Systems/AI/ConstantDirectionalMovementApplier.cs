using MovementSystem.Facade;
using MovementSystem.Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    internal class ConstantDirectionalMovementApplier : MonoBehaviour
    {
        [SerializeField] private Vector2 _direction;
        private IMovementFacade<Vector2> _movementFacade;

        private void Awake()
        {
            _movementFacade = GetComponent<IMovementFacade<Vector2>>();
            //Debug.Log(_movementFacade);
        }

        private void FixedUpdate()
        {
            _movementFacade.Move(_direction.normalized);
            
        }
    }
}