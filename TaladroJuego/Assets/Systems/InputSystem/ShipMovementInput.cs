using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MovementSystem.Facade;

namespace InputSystem
{
    internal class ShipMovementInput : MonoBehaviour
    {

        [SerializeField] private InputActionReference _thrustInputActionReference;


        [SerializeField] IMovementFacade<Vector2>  shipDirectionalMovementFacade;


        [SerializeField]bool _thrustActivated = false;


        private void Awake()
        {
            SubscribeMovementInputs();
            shipDirectionalMovementFacade = GetComponent<IMovementFacade<Vector2>>();

        }

        private void FixedUpdate()
        {
            if (_thrustActivated) shipDirectionalMovementFacade.Move(transform.up);
            else shipDirectionalMovementFacade.Move(Vector2.zero);
        }

        private void SubscribeMovementInputs()
        {
            _thrustInputActionReference.action.performed += OnThrustInputStarted;
        }

        private void UnsubscribeMovementInputs()
        {
            _thrustInputActionReference.action.performed -= OnThrustInputStarted;
        }

        private void OnThrustInputStarted(InputAction.CallbackContext obj)
        {
            SwitchThrust();
        }


        void SwitchThrust()
        {
            _thrustActivated = !_thrustActivated;
        }

        #region ENABLE / DISABLE INPUTACTIONS

        private void OnEnable()
        {
            _thrustInputActionReference.action.Enable();
        }

        private void OnDisable()
        {
            _thrustInputActionReference.action.Disable();
        }

        #endregion
    }
}

