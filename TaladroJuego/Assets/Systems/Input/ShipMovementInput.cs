using UnityEngine;
using UnityEngine.InputSystem;
using MovementSystem.Facade;

namespace InputSystem
{
    public class ShipMovementInput : MonoBehaviour
    {

        [SerializeField] private InputActionReference _thrustInputActionReference;
        [SerializeField] private InputActionReference _revertGearInputActionReference;


        private IMovementFacade<Vector2>  shipDirectionalMovementFacade;


        private bool _thrustActivated = true;
        private bool _reverseGearActivated = false;

        private bool _fuelIsEmpty = false;
        private bool _reverseGearAvailable = false;

        [SerializeField] private GameObject _button;

        [SerializeField] private float _minFuel;

        private void Awake()
        {
            SubscribeMovementInputs();
            shipDirectionalMovementFacade = GetComponentInChildren<IMovementFacade<Vector2>>();
        }

        private void OnEnable()
        {
            _thrustInputActionReference.action.Enable();
            _revertGearInputActionReference.action.Enable();
        }

        private void OnDisable()
        {
            _thrustInputActionReference.action.Disable();
            _revertGearInputActionReference.action.Disable();
        }

        private void OnDestroy()
        {
            UnsubscribeMovementInputs();
        }

        private void FixedUpdate()
        {
            int directionSense = (_reverseGearActivated)? -1 : 1;

            if (_thrustActivated && !_fuelIsEmpty) shipDirectionalMovementFacade.Move(transform.up * directionSense);
            else shipDirectionalMovementFacade.Move(Vector2.zero);
        }

        private void SubscribeMovementInputs()
        {
            _thrustInputActionReference.action.performed += OnThrustInputStarted;
            _revertGearInputActionReference.action.performed += OnRevertGearInputApplied;
        }

        private void UnsubscribeMovementInputs()
        {
            _thrustInputActionReference.action.performed -= OnThrustInputStarted;
            _revertGearInputActionReference.action.performed -= OnRevertGearInputApplied;
        }

        private void OnThrustInputStarted(InputAction.CallbackContext obj)
        {
            SwitchThrust();
        }

        private void OnRevertGearInputApplied(InputAction.CallbackContext obj)
        {
            SwitchGearDirectionalSense();
            Debug.Log(_reverseGearActivated);
        }

        public void StopEngine()
        {
             _fuelIsEmpty = true;
        }

        public void TryStartEngine(float value)
        {
            if (_fuelIsEmpty && value > _minFuel) _fuelIsEmpty = false;
        }

        public void SetReverseGearAvailability(bool value)
        {
            _reverseGearAvailable = value;
            _button.SetActive(value);
            Debug.Log(_reverseGearAvailable);
        }


        public void SwitchThrust()
        {
            _thrustActivated = !_thrustActivated;
        }

        public void SwitchGearDirectionalSense()
        {
            if (!_reverseGearAvailable) return;

            _reverseGearActivated = !_reverseGearActivated;
        }

       

        

    }
}

