namespace YuzeToolkit.BehaviorTree.Runtime
{
    public enum LogType : byte
    {
        Log,
        LogWarning,
        LogError
    }
    
    [System.Serializable]
    [AddTypeMenu("Action/DebugLog")]
    public class DebugLog : Action
    {
        [UnityEngine.SerializeField] private SharedString log = new();
        [UnityEngine.SerializeField] private LogType logType = LogType.Log;

        protected override BtStatus OnUpdate()
        {
            switch (logType)
            {
                case LogType.Log:
                    UnityEngine.Debug.Log(log.Value);
                    break;
                case LogType.LogWarning:
                    UnityEngine.Debug.LogWarning(log.Value);
                    break;
                case LogType.LogError:
                    UnityEngine.Debug.LogError(log.Value);
                    break;
            }
            
            return BtStatus.Success;
        }
    }
}
