//=====================================================
// - FileName:      ResourcesModule.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/05/01 17:47:51
// - Description:   ��Դģ�飬������Դ���ء�ж��
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

        #region  ��ȡ��Դ��
        public ResourcePackage GetDefaultPackage()
        {
            return mDefaultPackage;
        }

        public ResourcePackage GetRawFilePackage()
        {
            return mRawFilePackage;
        }
        #endregion

        #region ��ʼ����Դ��
        private ResourcePackage GetPackageByPackageName(string packageName)
        {
            if (string.IsNullOrEmpty(packageName))
            {
                return null;
            }

            ResourcePackage package = YooAssets.TryGetPackage(packageName);
            if (package == null)
            {
                HZJLog.LogError($"����Ϊ:{packageName}����Դ����ȡΪ�գ�");
                return null;
            }

            return package;
        }
        #endregion


        #region ��Դ���� --�첽����
        /// <summary>
        /// ����Ԥ����
        /// </summary>
        /// <param name="prefabName"></param>
        /// <returns></returns>
        public AssetHandle LoadPrefabAsync(string prefabName)
        {
            return LoadAssetAsync<GameObject>(prefabName);
        }

        /// <summary>
        /// ����
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

        #region  ��Դ���� --ͬ������

        #endregion

        #region ��Դж��
        public void UnloadAssets()
        {
            mDefaultPackage.UnloadUnusedAssets();
        }
        #endregion

        #region  UI����
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
                HZJLog.LogError($"��Դ����:�����uiNameΪ���ַ�����");
                return null;
            }

            return LoadAssetAsync<GameObject>(uiName);
        }
        #endregion
    }
}

