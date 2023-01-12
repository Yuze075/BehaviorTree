namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("CSharpVariable/SharedBool")]
    [System.Serializable]
    public class SharedBool : SharedVariable<bool>
    {
        public SharedBool(bool value)
        {
            Value = value;
        }
        
        public SharedBool()
        {
        }
    }
}