//=====================================================
// - FileName:      HZJFrameWorkEntry.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/30 16:36:08
// - Description:   框架入口
//======================================================
using UnityEngine;

namespace HZJFrameWork
{
    public class HZJFrameWorkEntry : MonoSingletonBase<HZJFrameWorkEntry>
    {
        public GameObject UIRoot;

        private ProcedureBase procedureBase;

        private void Awake()
        {
            InitModule();
        }

        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(this);
            if (UIRoot)
            {
                DontDestroyOnLoad(UIRoot);
            }
            ProcedureBase logoAni = new LogoAniProcedure();
            procedureBase = logoAni;
        }

        // Update is called once per frame
        void Update()
        {
            ModuleManager.I.Update();
        }

        private void InitModule()
        {
            ModuleManager.I.InitModule(ModuleManager.I.GetModule<ResourcesModule>());
        }

        #region   对外接口

        public void ChangeGameState(ProcedureBase procedure)
        {
            procedureBase = procedure;
        }

        #endregion
    }
}

