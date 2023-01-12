using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Action/ResetTree")]
    public class ResetTree : Action
    {
        [SerializeField] private SharedGameObject resetGameObject = new();

        protected override BtStatus OnUpdate()
        {
            if (resetGameObject.Value == null)
            {
                behaviorTree.ResetStatus = true;
            }
            else
            {
                var bt = resetGameObject.Value.GetComponent<BehaviorTree>();
                if (bt == null)
                {
                    behaviorTree.ResetStatus = true;
                }
                else
                {
                    bt.ResetStatus = true;
                }
            }

            return BtStatus.Success;
        }
    }
}