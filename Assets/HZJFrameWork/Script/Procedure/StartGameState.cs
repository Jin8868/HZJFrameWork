//=====================================================
// - FileName:      StateGameState.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/11/05 13:50:51
// - Description:   
//======================================================

using UnityEngine;
using YooAsset;

namespace HZJFrameWork
{
    public class StartGameState : StateNodeBase
    {
        private AssetHandle mHandle;
        public override void OnDispose()
        {
            
        }

        public override void OnEnter()
        {
            mHandle = ModuleManager.I.GetModule<ResourcesModule>().LoadPrefabAsync("GameLauncher");
            mHandle.Completed += StartGame;
        }

        private void StartGame(AssetHandle obj)
        {
            if (obj.Status == EOperationStatus.Succeed)
            {
                GameObject startObj = GameObject.Instantiate(obj.GetAssetObject<GameObject>());
                HZJFrameWorkEntry.Instance.updateUI.OnDispose();
            }
            else
            {
                HZJLog.LogWithRed("¿ªÊ¼ÓÎÏ·Ê§°Ü£¡");
            }
        }

        public override void OnLeave()
        {
            
        }

        public override void OnUpdate()
        {
            
        }
    }
}

