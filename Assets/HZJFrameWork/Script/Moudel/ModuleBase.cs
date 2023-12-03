//=====================================================
// - FileName:      ModuleBase.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/05/01 17:33:05
// - Description:   Ä£¿é»ùÀà
//======================================================

namespace HZJFrameWork
{
    public abstract class ModuleBase
    {
        protected string mModuleName;

        protected int mIndex;

        public abstract void Init(System.Action initCompleteCallBack);

        public abstract void Update();

        public abstract void Dispose();
    }
}

