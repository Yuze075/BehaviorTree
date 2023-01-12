namespace YuzeToolkit.BehaviorTree.Runtime
{
    public static class SharedVariableExtend
    {
        public static void BindValueFromBlackboard(this ISharedVariable variable, IBlackBoard blackBoard)
        {
            if (string.IsNullOrEmpty(variable.Name)) return;
            if (blackBoard==null) return;

            var value = blackBoard.GetSharedVariable(variable.Name);
            if (value == null)
            {
                UnityEngine.Debug.LogWarning($"{variable.Name} get null in the blackBoard");
                return;
            }

            variable.BindValue(value);
        }
    }
}