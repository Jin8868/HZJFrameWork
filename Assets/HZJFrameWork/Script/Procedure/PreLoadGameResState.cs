//=====================================================
// - FileName:      PreLoadGameResState.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/11/05 13:50:32
// - Description:   游戏预加载节点
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

            HZJLog.LogWithGreen("进入资源预加载");
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
            // Editor环境下，HotUpdate.dll.bytes已经被自动加载，不需要加载，重复加载反而会出问题。
#if !UNITY_EDITOR
           LoadDllByYoo();
#else
            // Editor下无需加载，直接查找获得HotUpdate程序集
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
                HZJLog.LogError("原生文件Assembly-CSharp.dll.bytes读取失败！");
            }
        }
    }
}

