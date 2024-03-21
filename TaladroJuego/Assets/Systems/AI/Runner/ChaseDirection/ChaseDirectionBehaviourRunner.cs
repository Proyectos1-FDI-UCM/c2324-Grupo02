using MovementSystem.Facade;
using UnityEngine;
using RequireAttributes;

namespace AISystem.Runner.ChaseDirection
{
    public class ChaseDirectionBehaviourRunner : MonoBehaviour, IBehaviourRunner
    {
        [SerializeField, RequireInterface(typeof(IMovementFacade<Vector2>))] private Object _movementFacadeObject;
        private IMovementFacade<Vector2> _movementFacade;

        private IChaseDirectionProvider _chaseDirectionProvider;

        public void RunBehaviour()
        {
            _movementFacade.Move(_chaseDirectionProvider.DirectionToChase());
        }

        private void Awake()
        {
            _movementFacade = _movementFacadeObject as IMovementFacade<Vector2>;
            _chaseDirectionProvider = GetComponentInChildren<IChaseDirectionProvider>();
        }
    }
}

