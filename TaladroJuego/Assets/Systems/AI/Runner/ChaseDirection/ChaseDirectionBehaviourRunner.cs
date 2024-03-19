using MovementSystem.Facade;
using UnityEngine;

namespace AISystem.Runner.ChaseDirection
{
    public class ChaseDirectionBehaviourRunner : MonoBehaviour, IBehaviourRunner
    {
        private IMovementFacade<Vector2> _movementFacade;
        private IChaseDirectionProvider _chaseDirectionProvider;

        public void RunBehaviour()
        {
            _movementFacade.Move(_chaseDirectionProvider.DirectionToChase());
        }

        private void Awake()
        {
            _movementFacade = GetComponentInChildren<IMovementFacade<Vector2>>();
            _chaseDirectionProvider = GetComponentInChildren<IChaseDirectionProvider>();
        }
    }
}

