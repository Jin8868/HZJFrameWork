//=====================================================
// - FileName:      ResourcesModule.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/05/01 17:47:51
// - Description:   资源模块，负责资源加载、卸载
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace HZJFrameWork
{
    public class ResourcesModule : ModuleBase
    {
        Dictionary<string, object> mAssetsBundleDic = new Dictionary<string, object>();

        public ResourcesModule()
        {
            Init();
        }

        public override void Init()
        {
            mModuleName = "ResourcesModule";
            mIndex = 120;
            HZJLog.LogWithGreen($"this is {mModuleName}");
        }


        /// <summary>
        /// 异步获取AB包资源
        /// </summary>
        public void LoadAssetsAsync(string assetsName)
        {

        }

        /// <summary>
        /// 同步获取AB包资源
        /// </summary>
        /// <param name="assetsName"></param>
        public void LoadAssets(string assetsName)
        {

        }

        /// <summary>
        /// 加载AB包资源依赖
        /// </summary>
        private void LoadAssetDependencies()
        {

        }

        /// <summary>
        /// 资源卸载
        /// </summary>
        /// <param name="assetsName"></param>
        public void UnLoadAssetsByName(string assetsName)
        {

        }
    }
}

