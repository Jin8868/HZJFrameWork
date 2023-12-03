//=====================================================
// - FileName:      StateNodeBase.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/11/05 13:33:13
// - Description:   状态机节点基类
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
                HZJLog.LogError("传入的状态机实体为空！");
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

