using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.Manager
{
    public class PrefabManager : Singleton<PrefabManager>
    {
        private GameObject _tempPrefab;

        public GameObject Get(string path)
        {
            // 判断path是否为空
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("[PrefabManager.Get]: " + path + " is null");
                return null;
            }

            // 生成对象
            _tempPrefab = AssetsManager.Instance.Get<GameObject>(path);
            // 判断对象是否为空
            if (_tempPrefab == null)
            {
                Debug.LogWarning("[PrefabManager.Get]: " + path + " is not in AssetsManager");
            }

            return _tempPrefab;
        }

        public GameObject Get(string path, Vector3 position)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("[PrefabManager]: " + path + " is null");
                return null;
            }

            // 生成对象
            _tempPrefab = Get(path);
            // 设置坐标
            _tempPrefab.transform.position = position;
            // 返回
            return _tempPrefab;
        }

        public GameObject Get(string path, Vector3 position, Quaternion rotation)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("[PrefabManager]: " + path + " is null");
                return null;
            }

            // 生成对象
            _tempPrefab = Get(path);
            // 设置坐标和旋转
            _tempPrefab.transform.position = position;
            _tempPrefab.transform.rotation = rotation;
            // 返回
            return _tempPrefab;
        }

        public GameObject Get(string path, Transform parent, Vector3 localPosition, Quaternion localRotation)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("[CreatePrefab]: " + path + " is null");
                return null;
            }

            // 生成对象
            _tempPrefab = Get(path);
            // 设置父物体
            _tempPrefab.transform.SetParent(parent);
            // 设置坐标和旋转
            _tempPrefab.transform.localPosition = localPosition;
            _tempPrefab.transform.localRotation = localRotation;
            // 返回
            return _tempPrefab;
        }
    }
}