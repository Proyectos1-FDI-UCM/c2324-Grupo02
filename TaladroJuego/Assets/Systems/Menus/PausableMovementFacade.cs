using MovementSystem.Facade;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MenusSystem
{
    internal class PausableMovementFacade : MonoBehaviour, IMovementFacade<Vector2>
    {
        [SerializeField]
        private PauseRequesterObject _requesterObject;

        private IMovementFacade<Vector2> _movementFacade;

        


        private void Awake()
        {
            IMovementFacade<Vector2>[] movementFacades = GetComponentsInChildren<IMovementFacade<Vector2>>();
            int i = 0;
            while (movementFacades[i]==(IMovementFacade<Vector2>)this)
            {
                i++;
            }
            _movementFacade = movementFacades[i];

           // _movementFacade = GetComponentsInChildren<IMovementFacade<Vector2>>().FirstOrDefault(d => d!= (IMovementFacade<Vector2>)this);
        }

        public void Move(Vector2 input)
        {
            if (_requesterObject.IsPaused)
            {
                _movementFacade.Move(Vector2.zero);
            }
            else
            {
                _movementFacade.Move(input);
            }
        }


    }
}