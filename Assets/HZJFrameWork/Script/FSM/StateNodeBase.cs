//=====================================================
// - FileName:      StateNodeBase.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/11/05 13:33:13
// - Description:   ״̬���ڵ����
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;

namespace HZJFrameWork
{
    public abstract class StateNodeBase : IStateNode
    {
        private StateMachine mStateMachine;

        public StateMachine owner { get { return mStateMachine; } }

        public void OnCreate(StateMachine owner)
        {
            if (owner == null)
            {
                HZJLog.LogError("�����״̬��ʵ��Ϊ�գ�");
                return;
            }
            mStateMachine = owner;
        }

        public abstract void OnUpdate();

        public abstract void OnEnter();

        public abstract void OnLeave();

        public abstract void OnDispose();
    }
}

