//=====================================================
// - FileName:      EventManager.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/07/29 14:02:46
// - Description:   完全独立的事件系统
//======================================================
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

namespace HZJFrameWork
{
    public class EventManager
    {
        public delegate void EventCallBack(params object[] message);

        private static Dictionary<string, EventCallBack> mEventList = new Dictionary<string, EventCallBack>();

        public static void AddEvent(string eventName, EventCallBack eventCallBack)
        {
            if (string.IsNullOrEmpty(eventName) || eventCallBack == null)
            {
                HZJLog.LogError("输入的事件名字为空或者事件回调为空");
                return;
            }

            if (mEventList.TryGetValue(eventName, out EventCallBack value))
            {
                value += eventCallBack;
                mEventList[eventName] = value;
            }
            else
            {
                mEventList.Add(eventName, eventCallBack);
            }

        }

        public static void RemoveEvent(string eventName, EventCallBack eventCallBack)
        {
            if (string.IsNullOrEmpty(eventName) || eventCallBack == null)
            {
                HZJLog.LogError("输入的事件名字为空或者事件回调为空");
                return;
            }

            if (mEventList.TryGetValue(eventName, out EventCallBack value))
            {
                value -= eventCallBack;
                if (value != null)
                {
                    mEventList[eventName] = value;
                }
                else
                {
                    mEventList.Remove(eventName);
                }
            }
        }

        public static void Dispatch(string eventName)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                HZJLog.LogError("输入的事件名字为空");
                return;
            }


            if (mEventList.TryGetValue(eventName, out EventCallBack value))
            {
                value.Invoke();
            }
        }

    }
}

