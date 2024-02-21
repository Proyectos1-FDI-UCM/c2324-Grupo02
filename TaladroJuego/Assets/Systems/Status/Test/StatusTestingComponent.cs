using UnityEngine;

namespace StatusSystem.Test
{
    internal class StatusTestingComponent : MonoBehaviour
    {
        [SerializeField] private ClampedStatusParameter _clampedStatusParameter;

        [SerializeField] private float _valueToChange;


        [ContextMenu(nameof(AugmentValue))]
        private void AugmentValue()
        {
            _clampedStatusParameter.AugmentValue(_valueToChange);
        }

        [ContextMenu(nameof(ReduceValue))]
        private void ReduceValue()
        {
            _clampedStatusParameter.ReduceValue(_valueToChange);
        }
    }
}