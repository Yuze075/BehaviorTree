using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuzeToolkit
{
    public class Timer
    {
        private float _startTime;
        private float _makeUpTime;
        public float ElapsedTime => Time.time - _startTime + _makeUpTime;

        public Timer()
        {
            _makeUpTime = 0;
            _startTime = Time.time;
        }
        
        public void Reset()
        {
            _makeUpTime = 0;
            _startTime = Time.time;
        }
        
        /// <param name="stopState">输入ture代表暂停, 输入false代表解除暂停</param>
        public void SetStop(bool stopState)
        {
            if (stopState)
            {
                _makeUpTime = ElapsedTime;
            }
            else
            {
                _startTime = Time.time;
            }
        }
        
        public static bool operator >(Timer timer, float duration)
            => timer.ElapsedTime > duration;

        public static bool operator <(Timer timer, float duration)
            => timer.ElapsedTime < duration;

        public static bool operator >=(Timer timer, float duration)
            => timer.ElapsedTime >= duration;

        public static bool operator <=(Timer timer, float duration)
            => timer.ElapsedTime <= duration;
    }
}