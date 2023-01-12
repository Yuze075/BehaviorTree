namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("CSharpVariable/SharedInt")]
    [System.Serializable]
    public class SharedInt : SharedVariable<int>
    {
        public SharedInt(int value)
        {
            Value = value;
        }


        public SharedInt()
        {
        }
    }
}