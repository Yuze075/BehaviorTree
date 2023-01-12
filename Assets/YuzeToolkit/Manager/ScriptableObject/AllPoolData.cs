using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit.Manager
{
    [CreateAssetMenu(menuName = "YuzeTools/AllPoolData", 
        fileName="AllPoolData", order = 01)]
    public class AllPoolData : ScriptableObject
    {
        public List<PoolData> AllData;
    }
    [System.Serializable]
    public class PoolData
    {
        public List<GameObject> Prefabs;
        public int InitialSize;
        public int MaxSize;
        public int TempMaxSize;
    }
}