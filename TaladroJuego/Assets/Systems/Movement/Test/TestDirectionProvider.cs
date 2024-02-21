using MovementSystem.Facade;
using UnityEngine;

namespace MovementSystem.Test
{
    internal class TestDirectionProvider : MonoBehaviour
    {
        [SerializeField] private float _angle = 0.0f;
        private IMovementFacade<Vector2> _movementFacade;

        private void Awake()
        {
            _movementFacade = GetComponent<IMovementFacade<Vector2>>();
        }

        private void FixedUpdate()
        {
            float xDirection = Mathf.Cos(_angle * Mathf.Deg2Rad);
            float yDirection = Mathf.Sin(_angle * Mathf.Deg2Rad);

            Vector2 direction = new Vector2(xDirection, yDirection);
            _movementFacade.Move(direction);
        }
    }
}
