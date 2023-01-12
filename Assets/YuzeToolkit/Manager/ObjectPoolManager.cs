using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.Manager
{
    /// <summary>
    /// 存储每个池子的数据, 并通过<see cref="Stack"/>存储对象
    /// </summary>
    public class Pool
    {
        public readonly Stack<GameObject> ObjectPool;
        public readonly GameObject Prefab;
        public int MaxSize;
        public int TempMaxSize;
        public const int DefaultMaxSize = 128;
        public const int DefaultTempMaxSize = 256;
#if UNITY_EDITOR
        public int CountAll { get; internal set; }

        public int CountActive => CountAll - CountInactive;

        public int CountInactive => ObjectPool.Count;
#endif

        public Pool(GameObject gameObject)
        {
            ObjectPool = new Stack<GameObject>();
            Prefab = gameObject;
            MaxSize = DefaultMaxSize;
            TempMaxSize = DefaultTempMaxSize;
            
        }

        public Pool(GameObject gameObject, int defaultTempMaxSize, int maxSize = 0)
        {
            ObjectPool = new Stack<GameObject>();
            Prefab = gameObject;
            MaxSize = maxSize;
            TempMaxSize = defaultTempMaxSize;
        }
    }

    /// <summary>
    /// 对象池单例, 可以直接调用预加载的<see cref="UnityEngine.GameObject"/>,
    /// 也可以在场景中手动加载<see cref="UnityEngine.GameObject"/>进对象池
    /// </summary>
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        [SerializeField] private AllPoolData allPoolData;

        // 对象池, 通过字典构造
        private readonly Dictionary<string, Pool> _pools;
        private GameObject _tempGameObject;

        protected ObjectPoolManager()
        {
            _pools = new Dictionary<string, Pool>();
        }

        // 初始化字典
        protected override void Awake()
        {
            base.Awake();
            foreach (var poolData in allPoolData.AllData)
            {
                foreach (var prefab in poolData.Prefabs)
                {
                    _pools[prefab.name] = new Pool(prefab,
                        poolData.MaxSize,
                        poolData.TempMaxSize);
                    for (int i = 0; i < poolData.InitialSize; i++)
                    {
                        _tempGameObject = UnityEngine.Object.Instantiate(prefab);
                        _tempGameObject.name = prefab.name;
                        _tempGameObject.SetActive(false);
                        _pools[prefab.name].ObjectPool.Push(_tempGameObject);
#if UNITY_EDITOR
                        _pools[prefab.name].CountAll++;
#endif
                    }
                }
            }
        }

        /// <summary>
        /// 通过字典, 比较<see cref="string"/>获取<see cref="UnityEngine.GameObject"/><br/>
        /// 可以进行<see cref="UnityEngine.Transform.position"/>/
        /// <see cref="UnityEngine.Transform.rotation"/>/
        /// <see cref="UnityEngine.Transform.parent"/>的初始化<br/>
        /// 在初始化<see cref="UnityEngine.Transform.parent"/>时,
        /// 传入参数为<see cref="UnityEngine.Transform.localPosition"/>和
        /// <see cref="UnityEngine.Transform.localRotation"/><br/>
        /// 如果没有对应物体, 则返回空
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public GameObject Get(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("[ObjectPoolManager.Get]: " + path + " is null");
                return null;
            }

            // 查看是否有该名字所对应的子池
            if (_pools.ContainsKey(path))
            {
                if (_pools[path].ObjectPool.Count > 0)
                {
                    // 将堆栈最上面的对象返回
                    _tempGameObject = _pools[path].ObjectPool.Pop();
                }
                else
                {
                    // 直接通过Instantiate生成
                    _tempGameObject = UnityEngine.Object.Instantiate(_pools[path].Prefab);
                    _tempGameObject.name = path;
                }
#if UNITY_EDITOR
                // 对象池计数
                ++_pools[path].CountAll;
#endif
                // 设置为激活
                _tempGameObject.SetActive(true);
            }
            else
            {
                Debug.Log("[ObjectPoolManager.Get]: " + path + " is not in dictionary");
                // 没有返回空
                _tempGameObject = null;
            }

            // 返回
            return _tempGameObject;
        }

        /// <summary>
        /// 通过字典, 比较<see cref="string"/>获取<see cref="UnityEngine.GameObject"/><br/>
        /// 可以进行<see cref="UnityEngine.Transform.position"/>/
        /// <see cref="UnityEngine.Transform.rotation"/>/
        /// <see cref="UnityEngine.Transform.parent"/>的初始化<br/>
        /// 在初始化<see cref="UnityEngine.Transform.parent"/>时,
        /// 传入参数为<see cref="UnityEngine.Transform.localPosition"/>和
        /// <see cref="UnityEngine.Transform.localRotation"/><br/>
        /// 如果没有对应物体, 则返回空
        /// </summary>
        /// <param name="path"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public GameObject Get(string path, Vector3 position)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("[ObjectPoolManager.Get]: " + path + " is null");
                return null;
            }

            // 生成对象
            _tempGameObject = Get(path);
            // 设置坐标
            _tempGameObject.transform.position = position;
            // 返回
            return _tempGameObject;
        }


        /// <summary>
        /// 通过字典, 比较<see cref="string"/>获取<see cref="UnityEngine.GameObject"/><br/>
        /// 可以进行<see cref="UnityEngine.Transform.position"/>/
        /// <see cref="UnityEngine.Transform.rotation"/>/
        /// <see cref="UnityEngine.Transform.parent"/>的初始化<br/>
        /// 在初始化<see cref="UnityEngine.Transform.parent"/>时,
        /// 传入参数为<see cref="UnityEngine.Transform.localPosition"/>和
        /// <see cref="UnityEngine.Transform.localRotation"/><br/>
        /// 如果没有对应物体, 则返回空
        /// </summary>
        /// <param name="path"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public GameObject Get(string path, Vector3 position, Quaternion rotation)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("[ObjectPoolManager.Get]: " + path + " is null");
                return null;
            }

            // 生成对象
            _tempGameObject = Get(path);
            // 设置坐标和旋转
            _tempGameObject.transform.position = position;
            _tempGameObject.transform.rotation = rotation;
            // 返回
            return _tempGameObject;
        }

        /// <summary>
        /// 通过字典, 比较<see cref="string"/>获取<see cref="UnityEngine.GameObject"/><br/>
        /// 可以进行<see cref="UnityEngine.Transform.position"/>/
        /// <see cref="UnityEngine.Transform.rotation"/>/
        /// <see cref="UnityEngine.Transform.parent"/>的初始化<br/>
        /// 在初始化<see cref="UnityEngine.Transform.parent"/>时,
        /// 传入参数为<see cref="UnityEngine.Transform.localPosition"/>和
        /// <see cref="UnityEngine.Transform.localRotation"/><br/>
        /// 如果没有对应物体, 则返回空
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parent"></param>
        /// <param name="localPosition"></param>
        /// <param name="localRotation"></param>
        /// <returns></returns>
        public GameObject Get(string path, Transform parent, Vector3 localPosition, Quaternion localRotation)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("[ObjectPoolManager.Get]: " + path + " is null");
                return null;
            }

            // 生成对象
            _tempGameObject = Get(path);
            // 设置父物体
            _tempGameObject.transform.SetParent(parent);
            // 设置坐标和旋转
            _tempGameObject.transform.localPosition = localPosition;
            _tempGameObject.transform.localRotation = localRotation;
            // 返回
            return _tempGameObject;
        }

        /// <summary>
        /// 将传入的<see cref="UnityEngine.GameObject"/>回收,
        /// 如果超出<see cref="YuzeToolkit.Manager.Pool.TempMaxSize"/>就销毁<see cref="UnityEngine.GameObject"/><br/>
        /// 如果传入<see cref="UnityEngine.GameObject"/>不在<see cref="_pools"/>中,
        /// 则直接通过<see cref="UnityEngine.Object.Destroy(Object)"/>销毁物体
        /// </summary>
        /// <param name="obj"></param>
        public void Release(GameObject obj)
        {
            // 查看是否有该名字所对应的子池
            if (_pools.ContainsKey(obj.name))
            {
                // 防止被看到，设置为非激活哦
                obj.SetActive(false);
                if (_pools[obj.name].ObjectPool.Count < _pools[obj.name].TempMaxSize)
                {
                    // 将当前对象放入对象子池
                    _pools[obj.name].ObjectPool.Push(obj);
                }
                else
                {
                    UnityEngine.Object.Destroy(obj);
#if UNITY_EDITOR
                    // 对象池计数
                    --_pools[obj.name].CountAll;
#endif
                }
            }
            else
            {
                Debug.Log("[ObjectPoolManager.Release]: " + obj.name + " is not in dictionary");
                UnityEngine.Object.Destroy(obj);
            }
        }

        /// <summary>
        /// 在游戏中动态创建对象池, 通过<see cref="UnityEngine.GameObject"/>来创建对象池, 初始数量为0, 默认最大数量为256
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="tempMaxSize"></param>
        public void Set(GameObject prefab, int tempMaxSize = 256)
        {
            if (!_pools.ContainsKey(prefab.name))
            {
                _pools[prefab.name] = new Pool(prefab, tempMaxSize);
            }
            else
            {
                Debug.Log("[ObjectPoolManager.Set]: " + prefab.name + " is in dictionary");
            }
        }

        /// <summary>
        /// 在游戏中动态创建对象池, 通过名称通过<see cref="UnityEngine.Resources.Load(string)"/>来创建对象池, 初始数量为0, 默认最大数量为256
        /// </summary>
        /// <param name="path"></param>
        /// <param name="tempMaxSize"></param>
        public void Set(string path, int tempMaxSize = 256)
        {
            if (!_pools.ContainsKey(path))
            {
                // 判断path是否为空
                if (string.IsNullOrEmpty(path))
                {
                    Debug.Log("[ObjectPoolManager.Set]: " + path + " is null");
                    return;
                }

                _tempGameObject = Resources.Load<GameObject>(path);
                if (_tempGameObject != null)
                {
                    _pools[path] = new Pool(_tempGameObject, tempMaxSize);
                }
                else
                {
                    Debug.Log("[ObjectPoolManager.Set]: " + path + " is  not in Resources");
                }
            }
            else
            {
                Debug.Log("[ObjectPoolManager.Set]: " + path + " is in dictionary");
            }
        }

        /// <summary>
        /// 清除改名称下对象池中所有的<see cref="UnityEngine.GameObject"/>
        /// </summary>
        /// <param name="path"></param>
        public void Clear(string path)
        {
            if (_pools.ContainsKey(path))
            {
                foreach (var obj in _pools[path].ObjectPool)
                {
                    UnityEngine.Object.Destroy(obj);
                    _pools[path].ObjectPool.Clear();
#if UNITY_EDITOR
                    _pools[path].CountAll = 0;
#endif
                }
            }
            else
            {
                Debug.Log("[ObjectPoolManager.Clear]: " + path + " is not in dictionary");
            }
        }
    }
}