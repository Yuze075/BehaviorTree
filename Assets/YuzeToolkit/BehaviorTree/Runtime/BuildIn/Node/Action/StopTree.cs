using UnityEngine;

namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Action/StopTree")]
    public class StopTree : Action
    {
        [SerializeField] private SharedGameObject stopGameObject = new();

        protected override BtState OnUpdate()
        {
            if (stopGameObject.Value == null)
            {
                behaviorTree.PauseStatus = true;
            }
            else
            {
                if (stopGameObject.Value.GetComponent<BehaviorTree>() == null)
                {
                    behaviorTree.PauseStatus = true;
                }
                else
                {
                    stopGameObject.Value.GetComponent<BehaviorTree>().PauseStatus = true;
                }
            }

            return BtState.Success;
        }
    }
}