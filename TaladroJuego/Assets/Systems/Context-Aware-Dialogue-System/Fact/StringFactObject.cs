using UnityEngine;

namespace ContextualDialogueSystem.Fact
{
    [CreateAssetMenu(fileName = OBJECT_NAME, menuName = OBJECT_PATH)]
    public class StringFactObject : ScriptableObject, IFact<string>,
                                                      IObservableFact<string>
    {
        private const string OBJECT_NAME = "String Fact Object";
        private const string OBJECT_PATH = "Context-Aware-Dialogue-System/Fact/" + OBJECT_NAME;

        [SerializeField]
        private ObservableFact<string> _observableFact;
        public string Value { get => _observableFact.Value; set => _observableFact.Value = value; }

        public event FactValueAction<string> ValueSet { add => _observableFact.ValueSet += value; remove => _observableFact.ValueSet -= value; }
    }
}
