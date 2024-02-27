using System;

namespace ContextualDialogueSystem.Fact
{
    internal interface IFact<T>
    {
        T Value { set; get; }
    }
}
