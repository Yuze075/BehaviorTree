namespace YuzeToolkit.BehaviorTree.Runtime
{
    public static class BlackBoardExtend
    {
        public static void BindValue(this IBlackBoard blackBoard, ISharedVariable variable)
        {
            if (variable==null)
            {
                UnityEngine.Debug.LogWarning($"the sharedVariable is null");
                return;
            }
            
            if (string.IsNullOrEmpty(variable.Name))
            {
                UnityEngine.Debug.Log($"{variable.GetType()}'s name is null or empty");
                return;
            }

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