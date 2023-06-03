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
        Dictionary<string, AssetBundle> mAssetsBundleDic = new Dictionary<string, AssetBundle>();

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
        private void LoadAssetsBundleAsync(string assetsName)
        {

        }

        /// <summary>
        /// 同步获取AB包资源
        /// </summary>
        /// <param name="assetsName"></param>
        private AssetBundle LoadAssetsBundle(string assetsName)
        {
            AssetBundle bundle = null;

            if (string.IsNullOrEmpty(assetsName))
            {
                HZJLog.LogError("输入的AB包名为空！");
                return bundle;
            }

            if (mAssetsBundleDic.TryGetValue(assetsName, out AssetBundle value))
            {
                bundle = value;
                return bundle;
            }

            bundle = AssetBundle.LoadFromFile(assetsName);
            mAssetsBundleDic.Add(assetsName, bundle);
            return bundle;
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

        #region   对外接口
        /// <summary>
        /// 获得AssetsBundle的Prefabs文件夹下的资源，自动拼接路径，只需传入预制体名字
        /// </summary>
        /// <param name="prefabsName"></param>
        public GameObject LoadPrefabs(string prefabsName, bool isInstantiate = true)
        {
            GameObject gameObject = null;
            if (string.IsNullOrEmpty(prefabsName))
            {
                HZJLog.LogError("资源模块:传入的预制体名字为空！");
                return gameObject;
            }
            prefabsName = prefabsName.ToLower();
            string assetsBundleName = string.Format("prefabs/{0}.prefab", prefabsName);
            string assestsBunlePath = Application.streamingAssetsPath + "/" + 
                Application.productName + "/" + assetsBundleName;
            assestsBunlePath.Replace("\\", "/");
            try
            {
                AssetBundle assetBundle = LoadAssetsBundle(assestsBunlePath);
                gameObject = assetBundle.LoadAsset<GameObject>(prefabsName);
                if (isInstantiate)
                {
                    GameObject.Instantiate(gameObject);
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            return gameObject;
        }
        #endregion
    }
}

