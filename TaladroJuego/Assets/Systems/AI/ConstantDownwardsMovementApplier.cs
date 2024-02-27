using MovementSystem.Facade;
using MovementSystem.Profile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    public class ConstantDownwardsMovementApplier : MonoBehaviour
    {
        private IMovementFacade<Vector2> _movementFacade;

        private void Awake()
        {
            _movementFacade = GetComponentInChildren<IMovementFacade<Vector2>>();
        }

        private void FixedUpdate()
        {
            _movementFacade.Move(Vector2.down);
        }
    }
}