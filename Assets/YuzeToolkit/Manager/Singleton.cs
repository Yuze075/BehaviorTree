using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.Manager
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance => _instance;

        /// <summary>
        /// 返回是否创建了单例
        /// </summary>
        public static bool IsInitialized => Instance != null;

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(this);
            }
            else
            {
                _instance = (T)this;
            }
            //DontDestroyOnLoad(this);
        }
        
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}