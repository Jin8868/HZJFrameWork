//=====================================================
// - FileName:      EventManager.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/07/29 14:02:46
// - Description:   ��ȫ�������¼�ϵͳ
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
                HZJLog.LogError("������¼�����Ϊ�ջ����¼��ص�Ϊ��");
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
                HZJLog.LogError("������¼�����Ϊ�ջ����¼��ص�Ϊ��");
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
                HZJLog.LogError("������¼�����Ϊ��");
                return;
            }


            if (mEventList.TryGetValue(eventName, out EventCallBack value))
            {
                value.Invoke();
            }
        }

    }
}

