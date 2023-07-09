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
using static HZJFrameWork.ResourcesModule;

namespace HZJFrameWork
{

    public class ResourcesModule : ModuleBase
    {
        private Dictionary<string, ResourcesInfoBase> mAssetsBundleDic = new Dictionary<string, ResourcesInfoBase>();

        private List<ResourcesInfoBase> mAssetsBundleAsyncList = new List<ResourcesInfoBase>();

        public delegate void LoadCompleteCallBack(ResourcesInfoBase info);

        public delegate void LoadProgressCallBack(float progress);

        /// <summary>
        /// 异步获取AB包资源
        /// </summary>
        private ResourcesInfoAsync LoadAssetsBundleAsync(string mainObjectName, string assetsName, LoadProgressCallBack loadProgressCallBack, LoadCompleteCallBack loadCompleteCallBack)
        {

            if (string.IsNullOrEmpty(assetsName))
            {
                HZJLog.LogError("输入的AB包名为空！");
                return null;
            }

            if (mAssetsBundleDic.TryGetValue(assetsName, out ResourcesInfoBase value ))
            {
                return value as ResourcesInfoAsync;
            }

            ResourcesInfoAsync resourcesInfo = new ResourcesInfoAsync();
            resourcesInfo.mainObjectName = mainObjectName;
            resourcesInfo.assetsbundleName = assetsName;
            resourcesInfo.assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(assetsName);
            resourcesInfo.loadCompleteCallBack = loadCompleteCallBack;
            resourcesInfo.loadProgressCallBack = loadProgressCallBack;

            if (resourcesInfo.assetBundleCreateRequest == null)
            {
                HZJLog.LogError("异步加载的AB包为空！");
            }
            return resourcesInfo;
        }

        /// <summary>
        /// 同步获取AB包资源
        /// </summary>
        /// <param name="assetsName"></param>
        private ResourcesInfoBase LoadAssetsBundle(string mainObjectName, string assetsName)
        {
            

            if (string.IsNullOrEmpty(assetsName))
            {
                HZJLog.LogError("输入的AB包名为空！");
                return null;
            }

            if (mAssetsBundleDic.TryGetValue(assetsName, out ResourcesInfoBase value))
            {
                return value;
            }
            ResourcesInfoBase resourcesInfo = new ResourcesInfoBase();
            resourcesInfo.mainObjectName = mainObjectName;
            resourcesInfo.assetsbundleName = assetsName;
            resourcesInfo.assetBundle = AssetBundle.LoadFromFile(assetsName);

            mAssetsBundleDic.Add(assetsName, resourcesInfo);
            return resourcesInfo;
        }

        /// <summary>
        /// 加载AB包资源依赖
        /// </summary>
        private void LoadAssetBundleDependencies()
        {

        }

        /// <summary>
        /// 资源卸载
        /// </summary>
        /// <param name="assetsName"></param>
        private void UnLoadAssetsBundleByName(string assetsName)
        {

        }

        private string GetBundlePathByPrefabsName(string prefabsName)
        {
            if (string.IsNullOrEmpty(prefabsName))
            {
                HZJLog.LogError("资源模块:传入的预制体名字为空！");
            }
            prefabsName = prefabsName.ToLower();
            string assetsBundleName = string.Format("prefabs/{0}.prefab", prefabsName);
            string assestsBunlePath = Application.streamingAssetsPath + "/" +
                Application.productName + "/" + assetsBundleName;
            assestsBunlePath = assestsBunlePath.Replace("\\", "/");
            return assestsBunlePath;
        }

        private void CheckAessetBundle()
        {
            if (mAssetsBundleAsyncList != null && mAssetsBundleAsyncList.Count > 0)
            {
                for (int i = mAssetsBundleAsyncList.Count - 1; i >= 0; --i)
                {
                    ResourcesInfoAsync tempInfo = mAssetsBundleAsyncList[i] as ResourcesInfoAsync;
                    AssetBundleCreateRequest assetBundleCreateRequest = tempInfo.assetBundleCreateRequest;
                    if (tempInfo == null)
                    {
                       
                        return;
                    }

                    if (tempInfo.loadProgressCallBack != null)
                    {
                        tempInfo.loadProgressCallBack(assetBundleCreateRequest.progress);
                    }

                    if (assetBundleCreateRequest.isDone)
                    {
                        AssetBundle assetBundle = tempInfo.assetBundleCreateRequest.assetBundle;
                        if (assetBundle == null)
                        {
                            HZJLog.LogError($"资源模块:路径加载为空！");
                            return;
                        }

                        tempInfo.assetBundle = assetBundle;
                        mAssetsBundleDic.Add(tempInfo.assetsbundleName, tempInfo);

                        if (tempInfo.loadCompleteCallBack != null)
                        {
                            tempInfo.loadCompleteCallBack(tempInfo);
                        }

                        mAssetsBundleAsyncList.Remove(tempInfo);
                    }
                }
            }
        }


        #region   对外接口
        public override void Init()
        {
            mModuleName = "ResourcesModule";
            mIndex = 120;
            HZJLog.LogWithGreen($"this is {mModuleName}");
        }

        public override void Update()
        {
            CheckAessetBundle();
        }

        /// <summary>
        /// 获得AssetsBundle的Prefabs文件夹下的资源，自动拼接路径，只需传入预制体名字
        /// </summary>
        /// <param name="prefabsName"></param>
        public void LoadPrefabs(string prefabsName, LoadCompleteCallBack loadCompleteCallBack)
        {
            string bundlePath = GetBundlePathByPrefabsName(prefabsName);
            HZJLog.LogWithGreen($"资源模块:加载[{bundlePath}]！");
            try
            {
                ResourcesInfoBase info = LoadAssetsBundle(prefabsName, bundlePath);
                if (loadCompleteCallBack != null)
                {
                    
                    loadCompleteCallBack(info);

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public void LoadPrefabsAsync(string prefabsName, LoadProgressCallBack loadProgressCallBack, LoadCompleteCallBack loadCompleteCallBack)
        {

            string bundlePath = GetBundlePathByPrefabsName(prefabsName);
            HZJLog.LogWithGreen($"资源模块:加载[{bundlePath}]！");
            if (string.IsNullOrEmpty(bundlePath))
            {
                HZJLog.LogError("资源模块：获取的AB包路径为空！");
                return;
            }
            ResourcesInfoAsync info = LoadAssetsBundleAsync(prefabsName, bundlePath, loadProgressCallBack, loadCompleteCallBack);

            mAssetsBundleAsyncList.Add(info);
        }

        public override void Dispose()
        {

        }
        #endregion
    }
}

