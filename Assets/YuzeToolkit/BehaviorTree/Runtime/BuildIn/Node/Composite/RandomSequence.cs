namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/RandomSequence")]
    public class RandomSequence : Composite
    {
        private System.Collections.Generic.List<int> _number = new();
        private System.Collections.Generic.List<int> _randomNumber = new();
        private int _randomIndex;
        private INode RandomChild => Children[_randomNumber[_randomIndex]];

        public override void Awake()
        {
            var count = Count;
            for (var i = 0; i < count; i++)
            {
                _number.Add(i);
            }
        }

        protected override void OnStartUpdate()
        {
            var random = new System.Random();

            foreach (var item in _number)
            {
                _randomNumber.Insert(random.Next(_randomNumber.Count), item);
            }

            _randomIndex = 0;
        }

        protected override BtState OnUpdate()
        {
            while (true)
            {
                State = RandomChild.Update();
                switch (State)
                {
                    case BtState.Failure:
                        return BtState.Failure;
                    case BtState.Running:
                        return BtState.Running;
                    case BtState.Success:
                        _randomIndex++;
                        break;
                }

                if (_randomIndex == Count)
                {
                    return BtState.Success;
                }
            }
        }

        protected override void OnEndUpdate()
        {
            _randomNumber.Clear();
        }

        protected override void OnAbort()
        {
            _randomIndex = 0;
        }

        protected override void OnReset()
        {
            _randomIndex = 0;
        }
    }
}