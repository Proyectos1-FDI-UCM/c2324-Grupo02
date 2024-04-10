using MovementSystem.Facade;
using UnityEngine;
using RequireAttributes;

namespace AISystem.Runner.ChaseDirection
{
    public class ChaseDirectionBehaviourRunner : MonoBehaviour, IBehaviourRunner
    {
        [SerializeField, RequireInterface(typeof(IMovementFacade<Vector2>))] private Object _movementFacadeObject;
        private IMovementFacade<Vector2> _movementFacade;

        private IDirectionProvider _chaseDirectionProvider;

        public void RunBehaviour()
        {
            Vector2 direction = _chaseDirectionProvider.DirectionToChase();
            _movementFacade.Move(direction);
        }

        private void Awake()
        {
            _movementFacade = _movementFacadeObject as IMovementFacade<Vector2>;
            _chaseDirectionProvider = GetComponentInChildren<IDirectionProvider>();
        }
    }
}

