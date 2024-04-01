using MovementSystem.Profile;
using UnityEngine;

namespace MovementSystem.Facade
{
    internal class ImpulseMovementFacade : MonoBehaviour, IMovementFacade<Vector2>
    {
        [SerializeField] Rigidbody2D _myRigidbody;
        private ISpeedProvider _speedProvider;
        /// <summary>
        /// Make sure the speed provided for this script is a fixed acceleration and not a maximum speed
        /// </summary>
        /// <param name="input"></param>
        public void Move(Vector2 input)
        {
            //F = (m * dv)/dt
            Vector3 force = (_myRigidbody.mass * (_speedProvider.GetSpeed() * input)) / Time.fixedDeltaTime;
            _myRigidbody.AddForce(force);
        }

        private void Awake()
        {
            //_myRigidbody = GetComponent<Rigidbody2D>();
            _speedProvider = GetComponentInChildren<ISpeedProvider>();
        }
    }
}

