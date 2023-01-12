namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("CSharpVariable/SharedUInt")]
    [System.Serializable]
    public class SharedUInt : SharedVariable<uint>
    {
        public SharedUInt(uint value)
        {
            Value = value;
        }


        public SharedUInt()
        {
        }
    }
}