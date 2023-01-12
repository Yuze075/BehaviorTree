using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Action/StartTree")]
    public class StartTree : Action
    {
        [SerializeField] private SharedGameObject stopGameObject = new();
        protected override BtStatus OnUpdate()
        {
            if (stopGameObject.Value == null)
            {
                behaviorTree.PauseStatus = false;
            }
            else
            {
                if (stopGameObject.Value.GetComponent<BehaviorTree>() == null)
                {
                    behaviorTree.PauseStatus = true;
                }
                else
                {
                    stopGameObject.Value.GetComponent<BehaviorTree>().PauseStatus = false;
                }
            }

            return BtStatus.Success;
        }
    }
}