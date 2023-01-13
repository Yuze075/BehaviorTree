namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/PrioritySelector")]
    public class PrioritySelector : Composite
    {
        [UnityEngine.SerializeField] private SharedIntList nodePriority = new();
        private System.Collections.Generic.List<int> _priorityNumber = new();
        private int _priorityIndex;
        private INode PriorityChild => Children[_priorityNumber[_priorityIndex]];

        private System.Collections.Generic.List<int> _tempList = new();
        private int _beforeMax;
        private int _max;
        private int _maxIndex;
        private int _count;

        public override void Awake()
        {
            _count = Count;
        }

        protected override void OnStartUpdate()
        {
            _tempList.Clear();
            for (var i = 0; i < _count; i++)
            {
                _tempList.Add(i < nodePriority.Value.Count ? nodePriority.Value[i] : 0);
            }

            for (var i = 0; i < _count; i++)
            {
                _max = _tempList[i];
                _maxIndex = i;
                for (var j = 1; j < _count; j++)
                {
                    if (_tempList[j] <= _max || (_tempList[j] > _beforeMax && i != 0)) continue;
                    _max = _tempList[j];
                    _maxIndex = j;
                }

                _priorityNumber.Add(_maxIndex);
                _beforeMax = _max;
            }

            _priorityIndex = 0;
        }

        protected override BtState OnUpdate()
        {
            while (true)
            {
                State = PriorityChild.Update();
                switch (State)
                {
                    case BtState.Success:
                        return BtState.Success;
                    case BtState.Running:
                        return BtState.Running;
                    case BtState.Failure:
                        _priorityIndex++;
                        break;
                }

                if (_priorityIndex == Count)
                {
                    return BtState.Failure;
                }
            }
        }

        protected override void OnAbort()
        {
            _priorityIndex = 0;
        }

        protected override void OnReset()
        {
            _priorityIndex = 0;
        }
    }
}