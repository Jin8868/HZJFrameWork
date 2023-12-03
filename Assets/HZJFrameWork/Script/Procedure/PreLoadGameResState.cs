//=====================================================
// - FileName:      PreLoadGameResState.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/11/05 13:50:32
// - Description:   ��ϷԤ���ؽڵ�
//======================================================

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using YooAsset;

namespace HZJFrameWork
{
    public class PreLoadGameResState : StateNodeBase
    {
        private AssetHandle mHandle;

        public override void OnDispose()
        {

        }

        public override void OnEnter()
        {

            HZJLog.LogWithGreen("������ԴԤ����");
            LoadDll();

        }

        public override void OnLeave()
        {


        }

        public override void OnUpdate()
        {

        }

        private void LoadDll()
        {
            // Editor�����£�HotUpdate.dll.bytes�Ѿ����Զ����أ�����Ҫ���أ��ظ����ط���������⡣
#if !UNITY_EDITOR
           LoadDllByYoo();
#else
            // Editor��������أ�ֱ�Ӳ��һ��HotUpdate����
            Assembly hotUpdateAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "HotFix");
            //LoadDllByYoo();
            owner.ChangeState<StartGameState>();
#endif

        }

        private void LoadDllByYoo()
        {
            ResourcePackage package = ModuleManager.I.GetModule<ResourcesModule>().GetRawFilePackage();
            RawFileHandle handle = package.LoadRawFileAsync("HotFix.dll");
            handle.Completed += loadComplete;
        }

        private void loadComplete(RawFileHandle obj)
        {
            if (obj.Status == EOperationStatus.Succeed)
            {
                byte[] dllData = obj.GetRawFileData();
                Assembly hotUpdateAss = Assembly.Load(dllData);
                owner.ChangeState<StartGameState>();
            }
            else
            {
                HZJLog.LogError("ԭ���ļ�Assembly-CSharp.dll.bytes��ȡʧ�ܣ�");
            }
        }
    }
}

