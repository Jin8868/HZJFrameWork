//=====================================================
// - FileName:      IStateNode.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/11/04 14:55:48
// - Description:   状态机节点接口
//======================================================
namespace HZJFrameWork
{
    public interface IStateNode
    {
        void OnCreate(StateMachine owner);

        void OnUpdate();

        void OnEnter();

        void OnLeave();

        void OnDispose();
    }
}

