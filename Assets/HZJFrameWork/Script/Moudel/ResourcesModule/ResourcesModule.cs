//=====================================================
// - FileName:      ResourcesModule.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/05/01 17:47:51
// - Description:   资源模块，负责资源加载、卸载
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using YooAsset;
using System;

namespace HZJFrameWork
{

    public class ResourcesModule : ModuleBase
    {
        private ResourcePackage mDefaultPackage;
        private ResourcePackage mRawFilePackage;

        private System.Action mInitComplete;


        public override void Dispose()
        {

        }

        public override void Init(System.Action initCompleteCallBack)
        {
            mInitComplete = initCompleteCallBack;

            mDefaultPackage = GetPackageByPackageName(HZJFrameWorkEntry.Instance.DefaultPackageName);
            mRawFilePackage = GetPackageByPackageName(HZJFrameWorkEntry.Instance.RawFilePackageName);

            if (mInitComplete != null)
            {
                mInitComplete.Invoke();
            }
        }

        public override void Update()
        {

        }

        #region  获取资源包
        public ResourcePackage GetDefaultPackage()
        {
            return mDefaultPackage;
        }

        public ResourcePackage GetRawFilePackage()
        {
            return mRawFilePackage;
        }
        #endregion

        #region 初始化资源包
        private ResourcePackage GetPackageByPackageName(string packageName)
        {
            if (string.IsNullOrEmpty(packageName))
            {
                return null;
            }

            ResourcePackage package = YooAssets.TryGetPackage(packageName);
            if (package == null)
            {
                HZJLog.LogError($"名字为:{packageName}的资源包获取为空！");
                return null;
            }

            return package;
        }
        #endregion


        #region 资源加载 --异步加载
        /// <summary>
        /// 加载预制体
        /// </summary>
        /// <param name="prefabName"></param>
        /// <returns></returns>
        public AssetHandle LoadPrefabAsync(string prefabName)
        {
            return LoadAssetAsync<GameObject>(prefabName);
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public AssetHandle LoadAssetAsync<T>(string assetName) where T : UnityEngine.Object
        {
            return mDefaultPackage.LoadAssetAsync<T>(assetName);
        }

        public SubAssetsHandle LoadSubAssetsASync<T>(string assetName) where T : UnityEngine.Object
        {
            return mDefaultPackage.LoadSubAssetsAsync<T>(assetName);
        }

        public AllAssetsHandle LoadAllAssetsASync<T>(string assetName) where T : UnityEngine.Object
        {
            return mDefaultPackage.LoadAllAssetsAsync<T>(assetName);
        }

        public RawFileHandle LoadRawFileAsync(string assetName)
        {
            return mRawFilePackage.LoadRawFileAsync(assetName);
        }
        #endregion

        #region  资源加载 --同步加载

        #endregion

        #region 资源卸载
        public void UnloadAssets()
        {
            mDefaultPackage.UnloadUnusedAssets();
        }
        #endregion

        #region  UI加载
        public AssetHandle LoadUI<T>() where T : UIBase
        {
            Type type = typeof(T);
            string name = type.FullName;
            HZJLog.LogWithRed(type.FullName);
            return LoadAssetAsync<GameObject>(name);
        }

        public AssetHandle LoadUI(string uiName)
        {
            if (string.IsNullOrEmpty(uiName))
            {
                HZJLog.LogError($"资源加载:传入的uiName为空字符串！");
                return null;
            }

            return LoadAssetAsync<GameObject>(uiName);
        }
        #endregion
    }
}

