namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Conditional/HasExitedCollision")]
    public class HasExitedCollision : Conditional
    {
        [UnityEngine.SerializeField] private SharedString tag = new();
        [UnityEngine.SerializeField] private SharedLayerMask layer = new();
        [UnityEngine.SerializeField] private SharedGameObject collidedGameObject = new();
        [UnityEngine.SerializeField] private CollisionMode collisionMode = CollisionMode.Tag;
        private bool _isExitedCollision;

        protected override bool IsConditional()
        {
            return _isExitedCollision;
        }

        protected override void OnEndUpdate()
        {
            _isExitedCollision = false;
        }

        public override void OnCollisionExit(UnityEngine.Collision other)
        {
            switch (collisionMode)
            {
                case CollisionMode.Tag:
                {
                    if (string.IsNullOrEmpty(tag.Value) || other.gameObject.CompareTag(tag.Value))
                    {
                        collidedGameObject.Value = other.gameObject;
                        _isExitedCollision = true;
                    }

                    break;
                }
                case CollisionMode.Layer:
                {
                    if (layer.Value == 0 || (layer.Value & other.gameObject.layer) > 0)
                    {
                        collidedGameObject.Value = other.gameObject;
                        _isExitedCollision = true;
                    }

                    break;
                }
                case CollisionMode.Both:
                {
                    if ((layer.Value == 0 || (layer.Value & other.gameObject.layer) > 0) &&
                        (string.IsNullOrEmpty(tag.Value) || other.gameObject.CompareTag(tag.Value)))
                    {
                        collidedGameObject.Value = other.gameObject;
                        _isExitedCollision = true;
                    }

                    break;
                }
            }
        }

        protected override void OnAbort()
        {
            _isExitedCollision = false;
        }

        protected override void OnReset()
        {
            _isExitedCollision = false;
        }
    }
}