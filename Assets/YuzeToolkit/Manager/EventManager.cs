using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YuzeToolkit.Manager
{
    #region EventName

    // public static class EventName
    // {
    //     public const string ActionName = nameof(ActionName);
    // }

    #endregion
    
    #region EventInfo

    public interface IEventInfo
    {
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction Actions;

        public EventInfo(UnityAction action)
        {
            Actions += action;
        }
    }

    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> Actions;

        public EventInfo(UnityAction<T> action)
        {
            Actions += action;
        }
    }

    public class EventInfo<T1, T2> : IEventInfo
    {
        public UnityAction<T1, T2> Actions;

        public EventInfo(UnityAction<T1, T2> action)
        {
            Actions += action;
        }
    }

    public class EventInfo<T1, T2, T3> : IEventInfo
    {
        public UnityAction<T1, T2, T3> Actions;

        public EventInfo(UnityAction<T1, T2, T3> action)
        {
            Actions += action;
        }
    }

    public class EventInfo<T1, T2, T3, T4> : IEventInfo
    {
        public UnityAction<T1, T2, T3, T4> Actions;

        public EventInfo(UnityAction<T1, T2, T3, T4> action)
        {
            Actions += action;
        }
    }

    #endregion

    #region EventManager

    [System.Serializable]
    public class EventManager : Singleton<EventManager>
    {
        private Dictionary<string, IEventInfo> _actionDictionary = new Dictionary<string, IEventInfo>();

        #region Add

        /// <summary>
        /// 添加监听事件
        /// </summary>
        public void Add(string actionName, UnityAction action)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                ((EventInfo)_actionDictionary[actionName]).Actions += action;
            }
            else
            {
                _actionDictionary.Add(actionName, new EventInfo(action));
            }
        }

        /// <summary>
        /// 添加监听事件
        /// </summary>
        public void Add<T>(string actionName, UnityAction<T> action)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                ((EventInfo<T>)_actionDictionary[actionName]).Actions += action;
            }
            else
            {
                _actionDictionary.Add(actionName, new EventInfo<T>(action));
            }
        }

        /// <summary>
        /// 添加监听事件
        /// </summary>
        public void Add<T1, T2>(string actionName, UnityAction<T1, T2> action)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                ((EventInfo<T1, T2>)_actionDictionary[actionName]).Actions += action;
            }
            else
            {
                _actionDictionary.Add(actionName, new EventInfo<T1, T2>(action));
            }
        }

        /// <summary>
        /// 添加监听事件
        /// </summary>
        public void Add<T1, T2, T3>(string actionName, UnityAction<T1, T2, T3> action)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                ((EventInfo<T1, T2, T3>)_actionDictionary[actionName]).Actions += action;
            }
            else
            {
                _actionDictionary.Add(actionName, new EventInfo<T1, T2, T3>(action));
            }
        }

        /// <summary>
        /// 添加监听事件
        /// </summary>
        public void Add<T1, T2, T3, T4>(string actionName, UnityAction<T1, T2, T3, T4> action)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                ((EventInfo<T1, T2, T3, T4>)_actionDictionary[actionName]).Actions += action;
            }
            else
            {
                _actionDictionary.Add(actionName, new EventInfo<T1, T2, T3, T4>(action));
            }
        }

        #endregion

        #region Remove

        /// <summary>
        /// 移除监听事件
        /// </summary>
        public void Remove(string actionName, UnityAction action)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                ((EventInfo)_actionDictionary[actionName]).Actions -= action;
            }
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        public void Remove<T>(string actionName, UnityAction<T> action)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                ((EventInfo<T>)_actionDictionary[actionName]).Actions -= action;
            }
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        public void Remove<T1, T2>(string actionName, UnityAction<T1, T2> action)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                ((EventInfo<T1, T2>)_actionDictionary[actionName]).Actions -= action;
            }
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        public void Remove<T1, T2, T3>(string actionName, UnityAction<T1, T2, T3> action)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                ((EventInfo<T1, T2, T3>)_actionDictionary[actionName]).Actions -= action;
            }
        }

        /// <summary>
        /// 移除监听事件
        /// </summary>
        public void Remove<T1, T2, T3, T4>(string actionName, UnityAction<T1, T2, T3, T4> action)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                ((EventInfo<T1, T2, T3, T4>)_actionDictionary[actionName]).Actions -= action;
            }
        }

        #endregion

        #region Trigger

        /// <summary>
        /// 触发监听事件
        /// </summary>
        public void Trigger(string actionName)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                (_actionDictionary[actionName] as EventInfo)?.Actions.Invoke();
            }
        }

        /// <summary>
        /// 触发监听事件
        /// </summary>
        public void Trigger<T>(string actionName, T info)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                (_actionDictionary[actionName] as EventInfo<T>)?.Actions.Invoke(info);
            }
        }

        /// <summary>
        /// 触发监听事件
        /// </summary>
        public void Trigger<T1, T2>(string actionName, T1 infoOne, T2 infoTwo)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                (_actionDictionary[actionName] as EventInfo<T1, T2>)?.Actions.Invoke(infoOne, infoTwo);
            }
        }

        /// <summary>
        /// 触发监听事件
        /// </summary>
        public void Trigger<T1, T2, T3>(string actionName, T1 infoOne, T2 infoTwo, T3 infoThree)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                (_actionDictionary[actionName] as EventInfo<T1, T2, T3>)?.Actions.Invoke(infoOne, infoTwo, infoThree);
            }
        }

        /// <summary>
        /// 触发监听事件
        /// </summary>
        public void Trigger<T1, T2, T3, T4>(string actionName, T1 infoOne, T2 infoTwo, T3 infoThree, T4 infoFour)
        {
            if (_actionDictionary.ContainsKey(actionName))
            {
                (_actionDictionary[actionName] as EventInfo<T1, T2, T3, T4>)?.Actions.Invoke(infoOne, infoTwo,
                    infoThree, infoFour);
            }
        }

        #endregion

        /// <summary>
        /// 清楚所有监听事件
        /// </summary>
        public void Clear()
        {
            _actionDictionary.Clear();
        }
    }

    #endregion
}