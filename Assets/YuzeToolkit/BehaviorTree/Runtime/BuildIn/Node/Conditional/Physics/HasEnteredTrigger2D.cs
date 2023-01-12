namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Conditional/HasEnteredTrigger2D")]
    public class HasEnteredTrigger2D : Conditional
    {
        [UnityEngine.SerializeField] private SharedString tag = new();
        [UnityEngine.SerializeField] private SharedLayerMask layer = new();
        [UnityEngine.SerializeField] private SharedGameObject collidedGameObject = new();
        [UnityEngine.SerializeField] private CollisionMode collisionMode = CollisionMode.Tag;
        private bool _isEnteredTrigger2D;

        protected override bool IsConditional()
        {
            return _isEnteredTrigger2D;
        }

        protected override void OnEndUpdate()
        {
            _isEnteredTrigger2D = false;
        }

        public override void OnTriggerEnter2D(UnityEngine.Collider2D col)
        {
            switch (collisionMode)
            {
                case CollisionMode.Tag:
                {
                    if (string.IsNullOrEmpty(tag.Value) || col.gameObject.CompareTag(tag.Value))
                    {
                        collidedGameObject.Value = col.gameObject;
                        _isEnteredTrigger2D = true;
                    }

                    break;
                }
                case CollisionMode.Layer:
                {
                    if (layer.Value == 0 || (layer.Value & col.gameObject.layer) > 0)
                    {
                        collidedGameObject.Value = col.gameObject;
                        _isEnteredTrigger2D = true;
                    }

                    break;
                }
                case CollisionMode.Both:
                {
                    if ((layer.Value == 0 || (layer.Value & col.gameObject.layer) > 0) &&
                        (string.IsNullOrEmpty(tag.Value) || col.gameObject.CompareTag(tag.Value)))
                    {
                        collidedGameObject.Value = col.gameObject;
                        _isEnteredTrigger2D = true;
                    }

                    break;
                }
            }
        }

        protected override void OnAbort()
        {
            _isEnteredTrigger2D = false;
        }

        protected override void OnReset()
        {
            _isEnteredTrigger2D = false;
        }
    }
}