using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using MovementSystem.Facade;
using UnityEngine.UIElements;

namespace UISystem
{
    internal class UIShipDirectioner : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Transform ship;
        [SerializeField] private GameObject movementFacade;
        private IMovementFacade<Vector2> shipRotationMovementFacade;

        private bool directionerDragged = false;

        private void Awake()
        {
            shipRotationMovementFacade = movementFacade.GetComponent<IMovementFacade<Vector2>>();
        }

        private void Start()
        {
            transform.up = ship.up;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            directionerDragged = true;
            print("pointerdown");
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            directionerDragged = false;
            print("pointerup");
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.up = (Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue()) - new Vector3(0.87f, 0.18f, 0.0f)).normalized;
            print("ondrag");
        }

        private void FixedUpdate()
        {
            if (shipRotationMovementFacade != null)
            {
                if (directionerDragged)
                    shipRotationMovementFacade.Move(transform.up);
                else { transform.up = ship.up;
                     }
            }
        }
    }
}


