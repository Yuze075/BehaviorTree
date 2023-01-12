using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.Manager
{
    public class SpriteManager : Singleton<SpriteManager>
    {
        private Sprite _tempSprite;

        public Sprite CreateSprite(string path)
        {
            // 判断path是否为空
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("[SpriteManager]: " + path + " is null");
                return null;
            }

            // 获取对象
            _tempSprite = AssetsManager.Instance.Get<Sprite>(path);
            // 判断对象是否为空
            if (_tempSprite == null)
            {
                Debug.Log("[SpriteManager]: " + path + " is not in AssetsManager");
            }

            return _tempSprite;
        }
    }
}