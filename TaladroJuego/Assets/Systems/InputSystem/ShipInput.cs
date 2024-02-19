using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class ShipInput : MonoBehaviour
    {
        //Sustituir por cσdigo comentado

        //[SerializeField] private InputActionReference _rotateInputActionReference,
        //                                              _thrustInputActionReference;

        InputActions inputActions;

        // Referencia al ShipMovement
        //ShipMovement shipMovement;

        //
        // Estas dos variables deben ir en el shipMovement
        bool _thrustActivated = false;
        Vector2 _targetDirection = Vector2.zero;
        //

        void Awake()
        {
            inputActions = new InputActions();


            //Sistema de detecciσn de inputs.

            inputActions.ShipMovement.Thrust.started += ctx => SwitchThrust();

            inputActions.ShipMovement.Rotate.performed += ctx => _targetDirection = ctx.ReadValue<Vector2>();
            inputActions.ShipMovement.Rotate.canceled += ctx => _targetDirection = Vector2.zero;

            //Sustituir por cσdigo comentado

            //_thrustInputActionReference.action.started += ctx => SwitchThrust();

            //_rotateInputActionReference.action.performed += ctx => _targetDirection = ctx.ReadValue<Vector2>();
            //_rotateInputActionReference.action.canceled += ctx => _targetDirection = Vector2.zero;
        }

        //
        // Esta funciσn va en el shipMovement
        void SwitchThrust()
        {
            _thrustActivated = !_thrustActivated;
        }
        //

        #region ENABLE / DISABLE INPUTACTIONS

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        #endregion
    }
}

