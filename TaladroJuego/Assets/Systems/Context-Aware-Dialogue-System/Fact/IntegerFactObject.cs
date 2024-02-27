using UnityEngine;

namespace ContextualDialogueSystem.Fact
{
    [CreateAssetMenu(fileName = OBJECT_NAME, menuName = OBJECT_PATH)]
    public class IntegerFactObject : ScriptableObject, IFact<int>, IFact<bool>,
                                                       IObservableFact<int>, IObservableFact<bool>
    {
        private const string OBJECT_NAME = "Integer Fact Object";
        private const string OBJECT_PATH = "Context-Aware-Dialogue-System/Fact/" + OBJECT_NAME;

        [SerializeField]
        private ObservableFact<int> _observableFact;

        public int Value { get => _observableFact.Value; set => _observableFact.Value = value; }
        bool IFact<bool>.Value { get => this.GetValueFrom(); set => this.SetValueFrom(value); }

        public event FactValueAction<int> ValueSet { add => _observableFact.ValueSet += value; remove => _observableFact.ValueSet -= value; }

        event FactValueAction<bool> IObservableFact<bool>.ValueSet { add => this.AddActionFrom(value); remove => this.RemoveActionFrom(value); }
    }
}
