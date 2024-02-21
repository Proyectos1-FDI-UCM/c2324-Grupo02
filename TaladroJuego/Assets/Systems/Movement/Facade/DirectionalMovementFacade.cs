using MovementSystem.Profile;
using UnityEngine;

namespace MovementSystem.Facade
{
    internal class DirectionalMovementFacade : MonoBehaviour, IMovementFacade<Vector2>
    {

        private Rigidbody2D _myRigidboy;
        private ISpeedProvider _speedProvider;

        public void Move(Vector2 input)
        {
            print(input);
            //F = (m * dv)/dt
            Vector2 force = (_myRigidboy.mass * (_speedProvider.GetSpeed() * input - _myRigidboy.velocity)) / Time.fixedDeltaTime;
            _myRigidboy.AddForce(force / 2);
            print(_speedProvider.GetSpeed());
        }

        private void Awake()
        {
            _myRigidboy = GetComponent<Rigidbody2D>();
            _speedProvider = GetComponentInChildren<ISpeedProvider>();
        }
    }
}

