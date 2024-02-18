using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    InputActions inputActions;

    // Referencia al ShipMovement
    //ShipMovement shipMovement;

    //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧
    // Estas dos variables deben ir en el shipMovement
    bool _thrustActivated = false;
    Vector2 _targetDirection = Vector2.zero;
    //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧

    void Awake()
    {
        inputActions = new InputActions();


        //Sistema de detecci�n de inputs.

        inputActions.ShipMovement.Thrust.started += ctx => SwitchThrust();

        inputActions.ShipMovement.Rotate.performed += ctx => _targetDirection = ctx.ReadValue<Vector2>();
        inputActions.ShipMovement.Rotate.canceled += ctx => _targetDirection = Vector2.zero;
    }


    //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧
    // Esta funci�n va en el shipMovement
    void SwitchThrust() {
        _thrustActivated = !_thrustActivated;
    }
    //覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧覧

    #region ENABLE / DISABLE INPUTACTIONS

    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    #endregion
}
