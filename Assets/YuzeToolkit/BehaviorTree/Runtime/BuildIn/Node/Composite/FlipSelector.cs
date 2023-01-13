using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Composite/FlipSelector")]
    public class FlipSelector : Composite
    {
        private List<int> _flipNumber = new();
        private int _flipIndex;
        private INode FlipChild => Children[_flipNumber[_flipIndex]];

        private int _count;
        private int _temp;

        public override void Awake()
        {
            _count = Count;
            for (var i = 0; i < _count; i++)
            {
                _flipNumber.Add(i);
            }
        }

        protected override void OnStartUpdate()
        {
            _flipIndex = 0;
        }

        protected override BtState OnUpdate()
        {
            while (true)
            {
                State = FlipChild.Update();
                switch (State)
                {
                    case BtState.Success:
                        return BtState.Success;
                    case BtState.Running:
                        return BtState.Running;
                    case BtState.Failure:
                        _flipIndex++;
                        break;
                }

                if (_flipIndex == Count)
                {
                    return BtState.Failure;
                }
            }
        }

        protected override void OnEndUpdate()
        {
            if (State != BtState.Success) return;
            _temp = _flipNumber[_count - 1];
            _flipNumber[_count - 1] = _flipNumber[_flipIndex];
            _flipNumber[_flipIndex] = _temp;
        }

        protected override void OnAbort()
        {
            _flipIndex = 0;
        }

        protected override void OnReset()
        {
            _flipIndex = 0;
        }
    }
}