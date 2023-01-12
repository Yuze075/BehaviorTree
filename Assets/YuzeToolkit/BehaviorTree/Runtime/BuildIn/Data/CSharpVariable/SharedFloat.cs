namespace YuzeToolkit.BehaviorTree.Runtime
{
    [AddTypeMenu("CSharpVariable/SharedFloat")]
    [System.Serializable]
    public class SharedFloat : SharedVariable<float>
    {
        public SharedFloat(float value)
        {
            Value = value;
        }


        public SharedFloat()
        {
        }
    }
}