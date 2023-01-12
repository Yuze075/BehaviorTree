namespace YuzeToolkit.BehaviorTree.Runtime
{
    public interface ISharedVariable
    {
        public string Name
        {
            get;
#if UNITY_EDITOR
            set;
#endif
        }

        public void GetValue(out object value);
        public void SetValue(object value);
        public void BindValue(ISharedVariable other);
    }
}