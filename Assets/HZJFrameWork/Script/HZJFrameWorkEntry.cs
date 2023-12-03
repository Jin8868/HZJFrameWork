//=====================================================
// - FileName:      HZJFrameWorkEntry.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/30 16:36:08
// - Description:   ¿ò¼ÜÈë¿Ú
//======================================================
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace HZJFrameWork
{
    public class HZJFrameWorkEntry : MonoSingletonBase<HZJFrameWorkEntry>
    {

        public YooAsset.EPlayMode PlayMode;

        private StateMachine mBootStateMachine;//¿ò¼ÜÆô¶¯×´Ì¬»ú

        [HideInInspector]
        public string DefaultPackageName = "DefaultPackage";

        [HideInInspector]
        public string RawFilePackageName = "RawFilePackage";

        [HideInInspector]
        public UpdateUI updateUI;

        [HideInInspector]
        public LauncherUI launcherUI;


        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            InitLauncherUI();
            mBootStateMachine = new StateMachine(this);
            mBootStateMachine.AddNode<InitResPackageState>();
            mBootStateMachine.AddNode<InitMoudleState>();
            mBootStateMachine.AddNode<UpdateVersionState>();
            mBootStateMachine.AddNode<PreLoadGameResState>();
            mBootStateMachine.AddNode<StartGameState>();
            mBootStateMachine.Run<InitResPackageState>();
            
        }

        // Update is called once per frame
        void Update()
        {
            ModuleManager.I.Update();
        }


        public void ExecuteCoroutine(IEnumerator coroutine)
        {
            if (coroutine == null)
            {
                return;
            }
            StartCoroutine(coroutine);
        }

        private void InitLauncherUI()
        {
            GameObject updateUIObj = Resources.Load("LauncherUI/LauncherUI") as GameObject;
            launcherUI = new LauncherUI(updateUIObj);
        }

        public void InitUpdateUI(GameObject updateUIObj)
        {
            updateUI = new UpdateUI(updateUIObj);
            updateUI.SetActive(false);
        }
    }
}

