//=====================================================
// - FileName:      InitMoudleState.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/11/05 13:49:59
// - Description:   ��ʼ�����ģ��״̬�ڵ�
//======================================================



using System;
using YooAsset;
using YooAsset;
using UnityEngine;

namespace HZJFrameWork
{
    public class InitMoudleState : StateNodeBase
    {
        public override void OnDispose()
        {

        }

        public override void OnEnter()
        {
            ModuleManager.I.InitModule(ModuleManager.I.GetModule<ResourcesModule>(), null);
            ModuleManager.I.InitModule(ModuleManager.I.GetModule<UIMoudule>(), null);

            InitUpdateUI();
        }



        public override void OnLeave()
        {
            HZJLog.LogWithGreen("HZJFrameWork:���ģ���ʼ����ɣ�");
        }

        public override void OnUpdate()
        {

        }


        private void InitUpdateUI()
        {
            AssetHandle handle = ModuleManager.I.GetModule<ResourcesModule>().LoadPrefabAsync("UpdateUI");
            handle.Completed += LoadComplete;
        }

        private void LoadComplete(AssetHandle obj)
        {
           if (obj.Status == EOperationStatus.Succeed)
           {
                GameObject updateObj = obj.GetAssetObject<GameObject>();
                HZJFrameWorkEntry.Instance.InitUpdateUI(updateObj);
                owner.ChangeState<UpdateVersionState>();
            }
        }
    }
}

