namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    public class SharedVariable<T> : ISharedVariable
    {
        #region SharedVariable

        public SharedVariable(T variable)
        {
            this.variable = variable;
        }

        public SharedVariable()
        {
        }

        #endregion

        #region Data

        [UnityEngine.SerializeField] private string name;
        [UnityEngine.SerializeField] private T variable;

        private System.Func<T> _getter;
        private System.Action<T> _setter;

        public string Name
        {
            get => name;
#if UNITY_EDITOR
            set => name = value;
#endif
        }

        public T Value
        {
            get => _getter == null ? variable : _getter();
            set
            {
                if (_setter != null)
                {
                    _setter(value);
                }
                else
                {
                    variable = value;
                }
            }
        }

        #endregion

        #region Function

        public void GetValue(out object value)
        {
            value = _getter == null ? variable : _getter();
        }

        public void SetValue(object value)
        {
            if (value is not T tValue) return;
            if (_setter != null)
            {
                _setter(tValue);
            }
            else
            {
                variable = tValue;
            }
        }

        public void BindValue(ISharedVariable other)
        {
            if (other is not SharedVariable<T>)
            {
                UnityEngine.Debug.LogWarning($"{Name}: {other.GetType()} is not {nameof(SharedVariable<T>)}");
                return;
            }

            _getter = () => ((SharedVariable<T>)other).Value;
            _setter = value => ((SharedVariable<T>)other).Value = value;
        }

        #endregion
    }
}