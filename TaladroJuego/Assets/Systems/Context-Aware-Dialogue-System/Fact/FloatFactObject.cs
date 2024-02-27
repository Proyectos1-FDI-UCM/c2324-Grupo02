using UnityEngine;

namespace ContextualDialogueSystem.Fact
{
    [CreateAssetMenu(fileName = OBJECT_NAME, menuName = OBJECT_PATH)]
    public class FloatFactObject : ScriptableObject, IFact<float>,
                                                     IObservableFact<float>
    {
        private const string OBJECT_NAME = "Float Fact Object";
        private const string OBJECT_PATH = "Context-Aware-Dialogue-System/Fact/" + OBJECT_NAME;

        [SerializeField]
        private ObservableFact<float> _observableFact;
        public float Value { get => _observableFact.Value; set => _observableFact.Value = value; }

        public event FactValueAction<float> ValueSet { add => _observableFact.ValueSet += value; remove => _observableFact.ValueSet -= value; }
    }
}
