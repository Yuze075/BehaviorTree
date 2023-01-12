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

        protected override BtStatus OnUpdate()
        {
            while (true)
            {
                Status = FlipChild.Update();
                switch (Status)
                {
                    case BtStatus.Success:
                        return BtStatus.Success;
                    case BtStatus.Running:
                        return BtStatus.Running;
                    case BtStatus.Failure:
                        _flipIndex++;
                        break;
                }

                if (_flipIndex == Count)
                {
                    return BtStatus.Failure;
                }
            }
        }

        protected override void OnEndUpdate()
        {
            if (Status != BtStatus.Success) return;
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