using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MovementSystem.Facade;

namespace InputSystem
{
    internal class ShipRotationInput : MonoBehaviour
    {

        [SerializeField] private InputActionReference _rotateInputActionReference;
        [SerializeField] private InputActionReference _rotateCanceledInputActionReference;

        IMovementFacade<Vector2>  shipRotationMovementFacade;

        [SerializeField] Vector2 _targetDirection;

        private void Awake()
        {
            SubscribeMovementInputs();
            shipRotationMovementFacade = GetComponent<IMovementFacade<Vector2>>();

        }

        private void FixedUpdate()
        {
            shipRotationMovementFacade.Move(_targetDirection);
        }

        private void SubscribeMovementInputs()
        {
            _rotateInputActionReference.action.performed += OnRotateInputStarted;
            _rotateCanceledInputActionReference.action.performed += OnRotateInputCanceled;
        }

        private void UnsubscribeMovementInputs()
        {
            _rotateInputActionReference.action.performed -= OnRotateInputStarted;
            _rotateCanceledInputActionReference.action.performed -= OnRotateInputCanceled;
        }

        private void OnRotateInputStarted(InputAction.CallbackContext obj)
        {
            _targetDirection = obj.ReadValue<Vector2>(); 
        }

        private void OnRotateInputCanceled(InputAction.CallbackContext obj)
        {
            _targetDirection = Vector2.zero;
        }

        #region ENABLE / DISABLE INPUTACTIONS

        private void OnEnable()
        {
            _rotateInputActionReference.action.Enable();
            _rotateCanceledInputActionReference.action.Enable();
        }

        private void OnDisable()
        {
            _rotateInputActionReference.action.Disable();
            _rotateCanceledInputActionReference.action.Disable();
        }

        #endregion
    }
}

