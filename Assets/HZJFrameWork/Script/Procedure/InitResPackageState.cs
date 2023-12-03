//=====================================================
// - FileName:      InitResPackageState.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/12/03 12:50:32
// - Description:   ��ʼ����Դ��
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using System;
using YooAsset;
using UnityEngine.UI;
using DG.Tweening;

namespace HZJFrameWork
{
    public class InitResPackageState : StateNodeBase
    {
        private YooAsset.EPlayMode playMode;

        private ResourcePackage package;

        private ResourcePackage tempPackage;

        public override void OnEnter()
        {
            HZJLog.LogWithGreen("�����ʼ����Դ��״̬��");
            playMode = HZJFrameWorkEntry.Instance.PlayMode;
            HZJFrameWorkEntry.Instance.ExecuteCoroutine(InitPackage());
        }

        public override void OnLeave()
        {

        }

        public override void OnUpdate()
        {

        }

        public override void OnDispose()
        {

        }


        #region  ��ʼ����Դ��
        private IEnumerator InitPackage()
        {
            string packageName = HZJFrameWorkEntry.Instance.DefaultPackageName;
            InitializationOperation defaultPackage = InitYooAssets(packageName);
            yield return defaultPackage;
            HZJLog.LogWithGreen("��ʼ��Ĭ����Դ����ɣ�");

            packageName = HZJFrameWorkEntry.Instance.RawFilePackageName;
            InitializationOperation rawFilePackage = InitYooAssets(packageName);
            yield return rawFilePackage;
            HZJLog.LogWithGreen("��ʼ��ԭ����Դ����ɣ�");

            owner.ChangeState<InitMoudleState>();
        }
        #endregion

        #region YooAssets��ʼ��
        private InitializationOperation InitYooAssets(string packageName)
        {
            YooAssets.Initialize();
            YooAssets.SetOperationSystemMaxTimeSlice(30);


            tempPackage = YooAssets.TryGetPackage(packageName);
            if (tempPackage == null)
            {
                tempPackage = YooAssets.CreatePackage(packageName);
                if (packageName.Equals("DefaultPackage"))
                {
                    package = tempPackage;
                    YooAssets.SetDefaultPackage(package);
                }

            }

            // �༭���µ�ģ��ģʽ
            InitializationOperation result = null;
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                var createParameters = new EditorSimulateModeParameters();
                createParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), packageName);
                result = tempPackage.InitializeAsync(createParameters);
            }

            // ��������ģʽ
            if (playMode == EPlayMode.OfflinePlayMode)
            {
                var createParameters = new OfflinePlayModeParameters();
                createParameters.DecryptionServices = new GameDecryptionServices();
                result = tempPackage.InitializeAsync(createParameters);
            }

            // ��������ģʽ
            if (playMode == EPlayMode.HostPlayMode)
            {
                string defaultHostServer = GetHostServerURL(packageName);
                string fallbackHostServer = GetHostServerURL(packageName);
                var createParameters = new HostPlayModeParameters();
                createParameters.DecryptionServices = new GameDecryptionServices();
                createParameters.BuildinQueryServices = new GameQueryServices();
                createParameters.DeliveryQueryServices = new DefaultDeliveryQueryServices();
                createParameters.DeliveryLoadServices = new DefaultDeliveryLoadServices();
                createParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                result = tempPackage.InitializeAsync(createParameters);
            }

            // WebGL����ģʽ
            if (playMode == EPlayMode.WebPlayMode)
            {
                string defaultHostServer = GetHostServerURL(packageName);
                string fallbackHostServer = GetHostServerURL(packageName);
                var createParameters = new WebPlayModeParameters();
                createParameters.DecryptionServices = new GameDecryptionServices();
                createParameters.BuildinQueryServices = new GameQueryServices();
                createParameters.RemoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                result = tempPackage.InitializeAsync(createParameters);
            }


            return result;

        }


        /// <summary>
        /// ��ȡ��Դ��������ַ
        /// </summary>
        private string GetHostServerURL(string packageName)
        {
            //string hostServerIP = "http://10.0.2.2"; //��׿ģ������ַ
            string hostServerIP = "http://192.168.2.116/E%3A/WorkSpace/Project_Unity/HZJFrameWork/Server";//"http://127.0.0.1";
            string appVersion = "v1.1";

#if UNITY_EDITOR
            if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
                return $"{hostServerIP}/{appVersion}/{packageName}";//$"{hostServerIP}/CDN/Android/{appVersion}";
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
                return $"{hostServerIP}/CDN/IPhone/{appVersion}";
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
                return $"{hostServerIP}/CDN/WebGL/{appVersion}";
            else
                return $"{hostServerIP}/{Application.dataPath}/../Bundles";//$"{hostServerIP}/CDN/PC/{appVersion}";
#else
		if (Application.platform == RuntimePlatform.Android)
			return $"{hostServerIP}/{appVersion}/{packageName}";
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
			return $"{hostServerIP}/CDN/IPhone/{appVersion}";
		else if (Application.platform == RuntimePlatform.WebGLPlayer)
			return $"{hostServerIP}/CDN/WebGL/{appVersion}";
		else
			return $"{hostServerIP}/CDN/PC/{appVersion}";
#endif
        }

        /// <summary>
        /// Զ����Դ��ַ��ѯ������
        /// </summary>
        private class RemoteServices : IRemoteServices
        {
            private readonly string _defaultHostServer;
            private readonly string _fallbackHostServer;

            public RemoteServices(string defaultHostServer, string fallbackHostServer)
            {
                _defaultHostServer = defaultHostServer;
                _fallbackHostServer = fallbackHostServer;
            }
            string IRemoteServices.GetRemoteMainURL(string fileName)
            {
                return $"{_defaultHostServer}/{fileName}";
            }
            string IRemoteServices.GetRemoteFallbackURL(string fileName)
            {
                return $"{_fallbackHostServer}/{fileName}";
            }
        }

        /// <summary>
        /// ��Դ�ļ����ܷ�����
        /// </summary>
        private class GameDecryptionServices : IDecryptionServices
        {
            public ulong LoadFromFileOffset(DecryptFileInfo fileInfo)
            {
                return 32;
            }

            public byte[] LoadFromMemory(DecryptFileInfo fileInfo)
            {
                throw new NotImplementedException();
            }

            public Stream LoadFromStream(DecryptFileInfo fileInfo)
            {
                Stream bundleStream = new FileStream(fileInfo.FileLoadPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                return bundleStream;
                //return null;
            }

            public uint GetManagedReadBufferSize()
            {
                return 1024;
            }

            public AssetBundle LoadAssetBundle(DecryptFileInfo fileInfo, out Stream managedStream)
            {
                throw new NotImplementedException();
            }

            public AssetBundleCreateRequest LoadAssetBundleAsync(DecryptFileInfo fileInfo, out Stream managedStream)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Ĭ�ϵķַ���Դ��ѯ������
        /// </summary>
        private class DefaultDeliveryQueryServices : IDeliveryQueryServices
        {
            public DeliveryFileInfo GetDeliveryFileInfo(string packageName, string fileName)
            {
                throw new NotImplementedException();
            }

            public string GetFilePath(string packageName, string fileName)
            {
                throw new NotImplementedException();
            }

            public bool Query(string packageName, string fileName)
            {
                return false;
            }

            public bool QueryDeliveryFiles(string packageName, string fileName)
            {
                return false;
            }
        }

        private class DefaultDeliveryLoadServices : IDeliveryLoadServices
        {
            public AssetBundle LoadAssetBundle(DeliveryFileInfo fileInfo)
            {
                throw new NotImplementedException();
            }

            public AssetBundleCreateRequest LoadAssetBundleAsync(DeliveryFileInfo fileInfo)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}

