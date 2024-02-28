using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UISystem
{
    internal class CompassDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform _pivot;
        [SerializeField] private Camera _camera;
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            

            Vector2 mousePosition = _camera.ScreenToWorldPoint(Pointer.current.position.ReadValue());
            Vector2 pivotToMouseDisplacement = mousePosition - (Vector2)_pivot.position;

            _pivot.rotation = Quaternion.LookRotation(_pivot.forward, pivotToMouseDisplacement);

            Debug.Log(mousePosition);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log(eventData);
        }
    }

}
