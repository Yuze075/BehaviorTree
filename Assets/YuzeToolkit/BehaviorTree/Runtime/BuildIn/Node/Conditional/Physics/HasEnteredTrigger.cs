namespace YuzeToolkit.BehaviorTree.Runtime
{
    [System.Serializable]
    [AddTypeMenu("Conditional/HasEnteredTrigger")]
    public class HasEnteredTrigger : Conditional
    {
        [UnityEngine.SerializeField] private SharedString tag = new();
        [UnityEngine.SerializeField] private SharedLayerMask layer = new();
        [UnityEngine.SerializeField] private SharedGameObject collidedGameObject = new();
        [UnityEngine.SerializeField] private CollisionMode collisionMode = CollisionMode.Tag;
        private bool _isEnteredTrigger;

        protected override bool IsConditional()
        {
            return _isEnteredTrigger;
        }

        protected override void OnEndUpdate()
        {
            _isEnteredTrigger = false;
        }

        public override void OnTriggerEnter(UnityEngine.Collider other)
        {
            switch (collisionMode)
            {
                case CollisionMode.Tag:
                {
                    if (string.IsNullOrEmpty(tag.Value) || other.gameObject.CompareTag(tag.Value))
                    {
                        collidedGameObject.Value = other.gameObject;
                        _isEnteredTrigger = true;
                    }

                    break;
                }
                case CollisionMode.Layer:
                {
                    if (layer.Value == 0 || (layer.Value & other.gameObject.layer) > 0)
                    {
                        collidedGameObject.Value = other.gameObject;
                        _isEnteredTrigger = true;
                    }

                    break;
                }
                case CollisionMode.Both:
                {
                    if ((layer.Value == 0 || (layer.Value & other.gameObject.layer) > 0) &&
                        (string.IsNullOrEmpty(tag.Value) || other.gameObject.CompareTag(tag.Value)))
                    {
                        collidedGameObject.Value = other.gameObject;
                        _isEnteredTrigger = true;
                    }

                    break;
                }
            }
        }

        protected override void OnAbort()
        {
            _isEnteredTrigger = false;
        }

        protected override void OnReset()
        {
            _isEnteredTrigger = false;
        }
    }
}