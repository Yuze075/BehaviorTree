namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("CSharpVariable/SharedString")]
    [System.Serializable]
    public class SharedString : SharedVariable<string>
    {
        public SharedString(string value)
        {
            Value = value;
        }


        public SharedString()
        {
        }
    }
}