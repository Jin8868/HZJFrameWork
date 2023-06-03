//=====================================================
// - FileName:      ProcedureBase.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/30 16:54:48
// - Description:   流程基类
//======================================================
using UnityEngine;

namespace HZJFrameWork
{
    public abstract class ProcedureBase
    {
        protected string mProcedureName;

        public ProcedureBase()
        {
            OnInit();
        }

        /// <summary>
        /// 流程初始化
        /// </summary>
        public abstract void OnInit();

        /// <summary>
        /// 流程进入
        /// </summary>
        public virtual void OnBegin() { }

        /// <summary>
        /// 流程刷新函数
        /// </summary>
        public virtual void OnUpdate() { }

        /// <summary>
        /// 流程退出
        /// </summary>
        public virtual void OnLeave() { }

        /// <summary>
        /// 流程释放资源
        /// </summary>
        public abstract void OnDisPose();
    }
}

