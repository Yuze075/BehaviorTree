namespace YuzeToolkit.BehaviorTree.Runtime
{
    public enum CollisionMode : byte
    {
        Tag = 1,
        Layer = 1 << 1,
        Both = Tag | Layer
    }

    [System.Serializable]
    [AddTypeMenu("Conditional/HasEnteredCollision")]
    public class HasEnteredCollision : Conditional
    {
        [UnityEngine.SerializeField] private SharedString tag = new();
        [UnityEngine.SerializeField] private SharedLayerMask layer = new();
        [UnityEngine.SerializeField] private SharedGameObject collidedGameObject = new();
        [UnityEngine.SerializeField] private CollisionMode collisionMode = CollisionMode.Tag;
        private bool _isEnteredCollision;

        protected override bool IsConditional()
        {
            return _isEnteredCollision;
        }

        protected override void OnEndUpdate()
        {
            _isEnteredCollision = false;
        }

        public override void OnCollisionEnter(UnityEngine.Collision collision)
        {
            switch (collisionMode)
            {
                case CollisionMode.Tag:
                {
                    if (string.IsNullOrEmpty(tag.Value) || collision.gameObject.CompareTag(tag.Value))
                    {
                        collidedGameObject.Value = collision.gameObject;
                        _isEnteredCollision = true;
                    }

                    break;
                }
                case CollisionMode.Layer:
                {
                    if (layer.Value == 0 || (layer.Value & collision.gameObject.layer) > 0)
                    {
                        collidedGameObject.Value = collision.gameObject;
                        _isEnteredCollision = true;
                    }

                    break;
                }
                case CollisionMode.Both:
                {
                    if ((layer.Value == 0 || (layer.Value & collision.gameObject.layer) > 0) &&
                        (string.IsNullOrEmpty(tag.Value) || collision.gameObject.CompareTag(tag.Value)))
                    {
                        collidedGameObject.Value = collision.gameObject;
                        _isEnteredCollision = true;
                    }

                    break;
                }
            }
        }

        protected override void OnAbort()
        {
            _isEnteredCollision = false;
        }

        protected override void OnReset()
        {
            _isEnteredCollision = false;
        }
    }
}