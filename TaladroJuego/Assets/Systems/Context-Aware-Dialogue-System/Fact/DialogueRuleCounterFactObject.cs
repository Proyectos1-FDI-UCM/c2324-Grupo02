using ContextualDialogueSystem.Rule;
using ContextualDialogueSystem.Rule.Criteria;
using ContextualDialogueSystem.RuleHandler;
using RequireAttributes;
using System.Threading.Tasks;
using UnityEngine;

namespace ContextualDialogueSystem.Fact
{
    [CreateAssetMenu(fileName = OBJECT_NAME, menuName = OBJECT_PATH)]
    public class DialogueRuleCounterFactObject : ScriptableObject, IDialogueRuleHandler<object>, IFact<int>, IObservableFact<int>,
                                                                                                 IFact<bool>, IObservableFact<bool>
    {
        private const string OBJECT_NAME = "Dialogue Rule Counter Object";
        private const string OBJECT_PATH = "Context-Aware-Dialogue-System/Fact/" + OBJECT_NAME;

        [RequireInterface(typeof(IDialogueRule<object, ICriteria>), typeof(ScriptableObject))]
        [SerializeField]
        private ScriptableObject _ruleObject;
        private IDialogueRule<object, ICriteria> _rule;

        [SerializeField]
        private ObservableFact<int> _dispatchCountFact;

        public int Value { get => _dispatchCountFact.Value; set => _dispatchCountFact.Value = value; }
        bool IFact<bool>.Value { get => _dispatchCountFact.GetValueFrom(); set => _dispatchCountFact.SetValueFrom(value); }

        public event FactValueAction<int> ValueSet { add => _dispatchCountFact.ValueSet += value; remove => _dispatchCountFact.ValueSet -= value; }

        event FactValueAction<bool> IObservableFact<bool>.ValueSet { add => _dispatchCountFact.AddActionFrom(value); remove => _dispatchCountFact.RemoveActionFrom(value); }

        public Task<bool> HandleRule(IDialogueRule<object, ICriteria> dialogueRule)
        {
            _rule ??= _ruleObject as IDialogueRule<object, ICriteria>;
            if (dialogueRule == _rule)
                ++Value;

            return Task.FromResult(true);
        }
    }
}
