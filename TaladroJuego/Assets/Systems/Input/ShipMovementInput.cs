using UnityEngine;
using UnityEngine.InputSystem;
using MovementSystem.Facade;

namespace InputSystem
{
    public class ShipMovementInput : MonoBehaviour
    {

        [SerializeField] private InputActionReference _thrustInputActionReference;


        private IMovementFacade<Vector2>  shipDirectionalMovementFacade;


        private bool _thrustActivated = false;
        private bool _fuelIsEmpty = false;

        [SerializeField] private float _minFuel;


        private void Awake()
        {
            SubscribeMovementInputs();
            shipDirectionalMovementFacade = GetComponentInChildren<IMovementFacade<Vector2>>();

        }

        private void FixedUpdate()
        {
            if (_thrustActivated && !_fuelIsEmpty) shipDirectionalMovementFacade.Move(transform.up);
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

        public void StopEngine()
        {
             _fuelIsEmpty = true;
        }

        public void TryStartEngine(float value)
        {
            if (_fuelIsEmpty && value > _minFuel) _fuelIsEmpty = false;
        }


        public void SwitchThrust()
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

