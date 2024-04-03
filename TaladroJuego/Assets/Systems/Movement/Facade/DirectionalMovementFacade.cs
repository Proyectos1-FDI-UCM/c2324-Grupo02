using Codice.Client.Common.GameUI;
using MovementSystem.Profile;
using UnityEngine;

namespace MovementSystem.Facade
{
    internal class DirectionalMovementFacade : MonoBehaviour, IMovementFacade<Vector2>
    {
        [SerializeField]
        private Rigidbody2D _myRigidboy;
        private ISpeedProvider _speedProvider;

        public void Move(Vector2 input)
        {
            //F = (m * dv)/dt
            float debugSpeed = _speedProvider.GetSpeed();


            Debug.Log($"direction: {input}");

            Debug.Log($"velocity: {_myRigidboy.velocity}");

            Vector2 force = (_myRigidboy.mass * (_speedProvider.GetSpeed() * input - _myRigidboy.velocity)) / Time.fixedDeltaTime;

            _myRigidboy.AddForce(force / 2);
        }

        private void Awake()
        {
            _speedProvider = GetComponentInChildren<ISpeedProvider>();
        }
    }
}

