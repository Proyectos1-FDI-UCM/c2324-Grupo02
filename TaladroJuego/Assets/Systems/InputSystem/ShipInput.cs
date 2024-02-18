using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    InputActions inputActions;

    // Referencia al ShipMovement
    //ShipMovement shipMovement;

    //——————————————————————————————————————————————————
    // Estas dos variables deben ir en el shipMovement
    bool _thrustActivated = false;
    Vector2 _targetDirection = Vector2.zero;
    //——————————————————————————————————————————————————

    void Awake()
    {
        inputActions = new InputActions();


        //Sistema de detección de inputs.

        inputActions.ShipMovement.Thrust.started += ctx => SwitchThrust();

        inputActions.ShipMovement.Rotate.performed += ctx => _targetDirection = ctx.ReadValue<Vector2>();
        inputActions.ShipMovement.Rotate.canceled += ctx => _targetDirection = Vector2.zero;
    }


    //——————————————————————————————————————————————————
    // Esta función va en el shipMovement
    void SwitchThrust() {
        _thrustActivated = !_thrustActivated;
    }
    //——————————————————————————————————————————————————

    #region ENABLE / DISABLE INPUTACTIONS

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    #endregion
}
