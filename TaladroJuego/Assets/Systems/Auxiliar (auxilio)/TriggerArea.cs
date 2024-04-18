using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerArea : MonoBehaviour
{
    [SerializeField]
    private LayerMask _triggerMask;
    [SerializeField]
    private bool _triggerOnce;

    [field: SerializeField]
    public UnityEvent<Collider2D> TriggerEnter { get; private set; }

    [field: SerializeField]
    public UnityEvent<Collider2D> TriggerExit { get; private set; }

    [field: SerializeField]
    public UnityEvent<Collider2D> TriggerStay { get; private set; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsInLayermask(collision.gameObject, _triggerMask))
        {
            TriggerEnter?.Invoke(collision);
            CheckOneUse();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsInLayermask(collision.gameObject, _triggerMask))
        {
            TriggerExit?.Invoke(collision);
            CheckOneUse();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (IsInLayermask(collision.gameObject, _triggerMask))
        {
            TriggerStay?.Invoke(collision);
            CheckOneUse();
        }
    }

    private static bool IsInLayermask(GameObject obj, LayerMask layerMask) => (layerMask & (1 << obj.layer)) != 0;

    private void CheckOneUse() => Destroy(_triggerOnce ? this : null);
}