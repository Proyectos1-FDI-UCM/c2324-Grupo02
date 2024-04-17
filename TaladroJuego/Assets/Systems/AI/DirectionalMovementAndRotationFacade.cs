using MovementSystem.Facade;
using UnityEngine;

namespace AISystem
{
    internal class DirectionalMovementAndRotationFacade : MonoBehaviour, IMovementFacade<Vector2>
    {
        [SerializeField]
        private GameObject _movementFacadeObject;
        [SerializeField]
        private GameObject _rotationFacadeObject;

        private IMovementFacade<Vector2> _movementFacade;
        private IMovementFacade<Vector2> _rotationFacade;

        private void Awake()
        {
            _movementFacade = _movementFacadeObject.GetComponent<IMovementFacade<Vector2>>();
            _rotationFacade = _rotationFacadeObject.GetComponent<IMovementFacade<Vector2>>();
        }

        public void Move(Vector2 input)
        {
            _movementFacade.Move(input);
            _rotationFacade.Move(input);
        }
    }
}
