//=====================================================
// - FileName:      StateMacine.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/11/04 14:59:29
// - Description:   ״̬��������
//======================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;

namespace HZJFrameWork
{
    public class StateMachine
    {
        private Dictionary<string, IStateNode> mStateNodeDic = new Dictionary<string, IStateNode>();

        private System.Object mOwner;

        private IStateNode mCurState;

        public System.Object Owner { get { return mOwner; } }

        public IStateNode CurState { get { return mCurState; } }

        public StateMachine(System.Object owner)
        {
            mOwner = owner;
        }

        public void Update()
        {
            if (mCurState != null)
            {
                mCurState.OnUpdate();
            }
        }

        public void Run<TNode>() where TNode : IStateNode
        {
            Type nodeType = typeof(TNode);
            Run(nodeType.FullName);
        }

        public void Run(string nodeName)
        {
            IStateNode tempNode = TryGetNode(nodeName);
            if (tempNode == null)
            {
                HZJLog.LogError($"״̬��״̬{nodeName}������");
                return;
            }
            mCurState = tempNode;
            mCurState.OnEnter();
        }

        public void AddNode<TNode>() where TNode : IStateNode
        {
            Type nodeType = typeof(TNode);
            string typeName = nodeType.FullName;
            if (TryGetNode(typeName) == null)
            {
                IStateNode node = Activator.CreateInstance(nodeType) as IStateNode;
                node.OnCreate(this);
                mStateNodeDic.Add(typeName, node);
            }
            else
            {
                HZJLog.LogError($"״̬�����Ѿ���{typeName},�����ظ���ӣ�");
            }
        }

        public void AddNode(IStateNode stateNode)
        {
            if (stateNode == null)
            {
                HZJLog.LogError($"�����״̬����Ϊ��!");
                return;
            }
            Type nodeType = stateNode.GetType();
            string typeName = nodeType.FullName;
            if (!mStateNodeDic.ContainsKey(typeName))
            {
                stateNode.OnCreate(this);
                mStateNodeDic.Add(typeName, stateNode);
            }
            else
            {
                HZJLog.LogError($"״̬�����Ѿ���{typeName},�����ظ���ӣ�");
            }
        }

        public void ChangeState<TNode>() where TNode : IStateNode
        {
            Type nodeType = typeof(TNode);
            string typeName = nodeType.FullName;
            ChangeState(typeName);
        }

        public void ChangeState(IStateNode stateNode)
        {
            if (stateNode == null)
            {
                HZJLog.LogError($"�����״̬����Ϊ��!");
                return;
            }
            Type nodeType = stateNode.GetType();
            string typeName = nodeType.FullName;
            ChangeState(typeName);
        }

        public void ChangeState(string stateNodeName)
        {
            IStateNode stateNode = TryGetNode(stateNodeName);

            if (stateNode == null)
            {
                HZJLog.LogError($"״̬��״̬{stateNodeName}������");
                return;
            }

            mCurState.OnLeave();
            mCurState = stateNode;
            mCurState.OnEnter();
        }

        public void OnDispose()
        {
            if (mStateNodeDic != null)
            {
                foreach (IStateNode item in mStateNodeDic.Values)
                {
                    if (item == null)
                    {
                        continue; 
                    }
                    item.OnDispose();
                }
                mStateNodeDic.Clear();
                mStateNodeDic = null;
            }

            mOwner = null;
            mCurState = null;
        }

        private IStateNode TryGetNode(string nodeName)
        {
            mStateNodeDic.TryGetValue(nodeName, out IStateNode Value);

            return Value;
        }
    }
}

