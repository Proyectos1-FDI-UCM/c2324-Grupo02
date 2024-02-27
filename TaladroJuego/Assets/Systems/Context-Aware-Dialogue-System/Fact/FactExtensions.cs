using System;

namespace ContextualDialogueSystem.Fact
{
    internal static class FactExtensions
    {
        public static void Add(this IFact<int> fact, int amount) => fact.Value += amount;
        public static void Muliply(this IFact<int> fact, int amount) => fact.Value *= amount;
        public static void Mod(this IFact<int> fact, int amount) => fact.Value %= amount;

        public static void Not(this IFact<int> fact) => fact.Value = ~fact.Value;
        public static void And(this IFact<int> fact, int other) => fact.Value &= other;
        public static void Or(this IFact<int> fact, int other) => fact.Value |= other;
        public static void Xor(this IFact<int> fact, int other) => fact.Value ^= other;
        public static void Nand(this IFact<int> fact, int other) => fact.Value = ~(fact.Value & other);
        public static void Nor(this IFact<int> fact, int other) => fact.Value = ~(fact.Value | other);
        public static void Xnor(this IFact<int> fact, int other) => fact.Value = ~(fact.Value ^ other);

        public static void Add<T>(this IFact<float> fact, float amount) => fact.Value += amount;
        public static void Muliply(this IFact<float> fact, float amount) => fact.Value *= amount;
        public static void Mod(this IFact<float> fact, float amount) => fact.Value %= amount;

        public static void Not(this IFact<bool> fact) => fact.Value = !fact.Value;
        public static void And(this IFact<bool> fact, bool other) => fact.Value &= other;
        public static void Or(this IFact<bool> fact, bool other) => fact.Value |= other;
        public static void Xor(this IFact<bool> fact, bool other) => fact.Value ^= other;
        public static void Nand(this IFact<bool> fact, bool other) => fact.Value = !(fact.Value && other);
        public static void Nor(this IFact<bool> fact, bool other) => fact.Value = !(fact.Value || other);
        public static void Xnor(this IFact<bool> fact, bool other) => fact.Value = !(fact.Value ^ other);

        public static bool GetValueFrom(this IFact<int> fact) => fact.Value != 0;
        public static bool SetValueFrom(this IFact<int> fact, bool value) => (fact.Value = value ? 1 : 0) != 0;

        public static readonly Func<FactValueAction<bool>, FactValueAction<int>> s_ZeroDistinctBoolToIntAction =
            booleanAction =>
                integerValue =>
                    booleanAction(integerValue != 0);
        public static void AddActionFrom(this IObservableFact<int> fact, FactValueAction<bool> factValueAction) => fact.ValueSet += s_ZeroDistinctBoolToIntAction(factValueAction);
        public static void RemoveActionFrom(this IObservableFact<int> fact, FactValueAction<bool> factValueAction) => fact.ValueSet -= s_ZeroDistinctBoolToIntAction(factValueAction);
    }
}
