using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YuzeToolkit.Manager
{
    public class AssetsManager : Singleton<AssetsManager>
    {
        // 缓存字典
        private Dictionary<string, Object> _assetsCache;

        protected AssetsManager()
        {
            _assetsCache = new Dictionary<string, Object>();
        }

        /// <summary>
        /// 获取<see cref="T"/>类型的素材, 如果没有通过<see cref="UnityEngine.Resources.Load(string)"/>加载
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T Get<T>(string path) where T : Object
        {
            // 判断path是否为空
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("[AssetsManager.Get]: " + path + " is null");
                return null;
            }

            // 先查看缓存池中有没有这个资源
            if (_assetsCache.ContainsKey(path))
            {
                // 直接将这个资源返回
                return _assetsCache[path] as T;
            }
            else
            {
                // 通过Resources.Load去加载资源
                T assets = Resources.Load<T>(path);

                // 判断对象是否为空
                if (assets != null)
                {
                    // 将新资源加载到缓存池里
                    _assetsCache.Add(path, assets);
                }
                else
                {
                    Debug.Log("[AssetsManager.Get]: " + path + " is not in Resources");
                }

                // 返回资源
                return assets;
            }
        }

        /// <summary>
        /// 卸载未使用的资源, 通过<see cref="UnityEngine.Resources.UnloadUnusedAssets()"/>
        /// </summary>
        public void Unload()
        {
            // 卸载
            Resources.UnloadUnusedAssets();
        }
    }
}