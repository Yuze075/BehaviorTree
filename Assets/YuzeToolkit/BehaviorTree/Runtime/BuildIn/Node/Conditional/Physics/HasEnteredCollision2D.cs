namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Conditional/HasEnteredCollision2D")]
    public class HasEnteredCollision2D : Conditional
    {
        [UnityEngine.SerializeField] private SharedString tag = new();
        [UnityEngine.SerializeField] private SharedLayerMask layer = new();
        [UnityEngine.SerializeField] private SharedGameObject collidedGameObject = new();
        [UnityEngine.SerializeField] private CollisionMode collisionMode = CollisionMode.Tag;
        private bool _isEnteredCollision2D;

        protected override bool IsConditional()
        {
            return _isEnteredCollision2D;
        }

        protected override void OnEndUpdate()
        {
            _isEnteredCollision2D = false;
        }

        public override void OnCollisionEnter2D(UnityEngine.Collision2D col)
        {
            switch (collisionMode)
            {
                case CollisionMode.Tag:
                {
                    if (string.IsNullOrEmpty(tag.Value) || col.gameObject.CompareTag(tag.Value))
                    {
                        collidedGameObject.Value = col.gameObject;
                        _isEnteredCollision2D = true;
                    }

                    break;
                }
                case CollisionMode.Layer:
                {
                    if (layer.Value == 0 || (layer.Value & col.gameObject.layer) > 0)
                    {
                        collidedGameObject.Value = col.gameObject;
                        _isEnteredCollision2D = true;
                    }

                    break;
                }
                case CollisionMode.Both:
                {
                    if ((layer.Value == 0 || (layer.Value & col.gameObject.layer) > 0) &&
                        (string.IsNullOrEmpty(tag.Value) || col.gameObject.CompareTag(tag.Value)))
                    {
                        collidedGameObject.Value = col.gameObject;
                        _isEnteredCollision2D = true;
                    }

                    break;
                }
            }
        }

        protected override void OnAbort()
        {
            _isEnteredCollision2D = false;
        }

        protected override void OnReset()
        {
            _isEnteredCollision2D = false;
        }
    }
}