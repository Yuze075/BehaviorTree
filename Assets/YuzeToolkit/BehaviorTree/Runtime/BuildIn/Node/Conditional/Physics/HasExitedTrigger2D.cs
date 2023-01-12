namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Conditional/HasExitedTrigger2D")]
    public class HasExitedTrigger2D : Conditional
    {
        [UnityEngine.SerializeField] private SharedString tag = new();
        [UnityEngine.SerializeField] private SharedLayerMask layer = new();
        [UnityEngine.SerializeField] private SharedGameObject collidedGameObject = new();
        [UnityEngine.SerializeField] private CollisionMode collisionMode = CollisionMode.Tag;
        private bool _isExitedTrigger2D;

        protected override bool IsConditional()
        {
            return _isExitedTrigger2D;
        }

        protected override void OnEndUpdate()
        {
            _isExitedTrigger2D = false;
        }

        public override void OnTriggerExit2D(UnityEngine.Collider2D other)
        {
            switch (collisionMode)
            {
                case CollisionMode.Tag:
                {
                    if (string.IsNullOrEmpty(tag.Value) || other.gameObject.CompareTag(tag.Value))
                    {
                        collidedGameObject.Value = other.gameObject;
                        _isExitedTrigger2D = true;
                    }

                    break;
                }
                case CollisionMode.Layer:
                {
                    if (layer.Value == 0 || (layer.Value & other.gameObject.layer) > 0)
                    {
                        collidedGameObject.Value = other.gameObject;
                        _isExitedTrigger2D = true;
                    }

                    break;
                }
                case CollisionMode.Both:
                {
                    if ((layer.Value == 0 || (layer.Value & other.gameObject.layer) > 0) &&
                        (string.IsNullOrEmpty(tag.Value) || other.gameObject.CompareTag(tag.Value)))
                    {
                        collidedGameObject.Value = other.gameObject;
                        _isExitedTrigger2D = true;
                    }

                    break;
                }
            }
        }

        protected override void OnAbort()
        {
            _isExitedTrigger2D = false;
        }

        protected override void OnReset()
        {
            _isExitedTrigger2D = false;
        }
    }
}