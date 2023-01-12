namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/RandomSelector")]
    public class RandomSelector : Composite
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

        protected override BtStatus OnUpdate()
        {
            while (true)
            {
                Status = RandomChild.Update();
                switch (Status)
                {
                    case BtStatus.Success:
                        return BtStatus.Success;
                    case BtStatus.Running:
                        return BtStatus.Running;
                    case BtStatus.Failure:
                        _randomIndex++;
                        break;
                }

                if (_randomIndex == Count)
                {
                    return BtStatus.Failure;
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