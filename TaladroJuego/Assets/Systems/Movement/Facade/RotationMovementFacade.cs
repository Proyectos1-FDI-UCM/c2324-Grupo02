using Codice.CM.Common.Tree;
using MovementSystem.Profile;
using UnityEngine;

namespace MovementSystem.Facade
{
    internal class RotationMovementFacade : MonoBehaviour, IMovementFacade<Vector2>
    {
        private ISpeedProvider _speedProvider;

        [SerializeField] Transform parentTransform;

        public void Move(Vector2 input)
        {
            if(input != Vector2.zero)
            {
                float radMax = Mathf.Sign(Vector2.SignedAngle(parentTransform.up, input)) * _speedProvider.GetSpeed() * Time.deltaTime * Mathf.Rad2Deg;
                parentTransform.Rotate(Vector3.forward, radMax);


                //float deltaAngle = Mathf.Clamp(Vector2.Angle(parentTransform.up, input) * Mathf.Deg2Rad, -_speedProvider.GetSpeed() * Time.deltaTime, _speedProvider.GetSpeed() * Time.deltaTime);
                //float currentAngle = Vector2.Angle(Vector2.right, parentTransform.up) * Mathf.Deg2Rad;
                //Vector2 rotatedDirection = new Vector2(Mathf.Cos(currentAngle + deltaAngle), Mathf.Sin(currentAngle + deltaAngle));
                //parentTransform.up = rotatedDirection;
            }
              
        }

        private void Awake()
        {
            _speedProvider = parentTransform.GetComponentInChildren<ISpeedProvider>();
        }
    }
}

