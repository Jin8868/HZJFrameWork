//=====================================================
// - FileName:      UpdateVersionState.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/11/12 13:44:58
// - Description:   ���°汾
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using YooAsset;
using System;
using DG.Tweening;
using UnityEngine.UI;

namespace HZJFrameWork
{
    public class UpdateVersionState : StateNodeBase
    {
        private ResourceDownloaderOperation mDownLoader;

        private UpdateUI mUpdateUI;



        public override void OnEnter()
        {
            HZJLog.LogWithGreen($"��������Դ���汾״̬��");
            HZJFrameWorkEntry.Instance.ExecuteCoroutine(UpdatePackage());
        }
        public override void OnUpdate()
        {

        }

        public override void OnLeave()
        {
            HZJLog.LogWithRed("������Դ���ɹ���");
        }

        public override void OnDispose()
        {

        }

        private void SetUpdateUI()
        {
            mUpdateUI = HZJFrameWorkEntry.Instance.updateUI;
            mUpdateUI.SetDescBoxActive(false);
            mUpdateUI.SetProgressInfoTextActive(false);

            mUpdateUI.SetActive(true);
            mUpdateUI.SetProgressSlider(0.0f);
            mUpdateUI.SetUpdateText("��Դ��������");
        }

        #region  ������Դ��
        private IEnumerator UpdatePackage()
        {
            yield return new WaitForSecondsRealtime(1.0f);
            HZJFrameWorkEntry.Instance.launcherUI.OnDisPose();
            SetUpdateUI();

            ResourcePackage defaultPackage = ModuleManager.I.GetModule<ResourcesModule>().GetDefaultPackage();
            yield return UpdatePackage(defaultPackage);

            ResourcePackage rawFilePackage = ModuleManager.I.GetModule<ResourcesModule>().GetRawFilePackage();
            yield return UpdatePackage(rawFilePackage);

            ResourceDownloaderOperation downloadOperation = CreateDownloader();
            //yield return downloadOperation;
        }
        #endregion


        #region  ��Դ��������

        private IEnumerator UpdatePackage(ResourcePackage package)
        {
            if (package == null)
            {
                HZJLog.LogWithRed("��ȡ�ĸ��°�packageΪ�գ�");
                yield break;
            }
            UpdatePackageVersionOperation updateVersionOperation = package.UpdatePackageVersionAsync();

            yield return updateVersionOperation;

            UpdatePackageManifestOperation updateManifestOperation = UpdateManifest(package, updateVersionOperation.PackageVersion);
            yield return updateManifestOperation;

        }


        private UpdatePackageManifestOperation UpdateManifest(ResourcePackage package, string packageVersion)
        {
            bool savePackageVersion = true;
            UpdatePackageManifestOperation operation = package.UpdatePackageManifestAsync(packageVersion, savePackageVersion);

            return operation;
        }

        private ResourceDownloaderOperation CreateDownloader()
        {

            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            ResourceDownloaderOperation downloader = YooAssets.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            mDownLoader = downloader;
            if (downloader.TotalDownloadCount <= 0)
            {
                Debug.Log("Not found any download files !");
                mUpdateUI.GetSlider().DOValue(1.0f, 1.0f).OnComplete(() => 
                {
                    owner.ChangeState<PreLoadGameResState>();
                });
               
            }
            else
            {
                //A total of 10 files were found that need to be downloaded
                HZJLog.Log($"Found total {downloader.TotalDownloadCount} files that need download ��");
                // �����¸����ļ��󣬹�������ϵͳ
                // ע�⣺��������Ҫ������ǰ�����̿ռ䲻��
                //int totalDownloadCount = downloader.TotalDownloadCount;
                //long totalDownloadBytes = downloader.TotalDownloadBytes;
                //mDownLoader.BeginDownload();
                ShowUpdateUI();
                downloader.OnDownloadProgressCallback += DownLoadProgress;
                downloader.Completed += DownloadOver;
            }
            return downloader;
        }

        private void DownloadOver(AsyncOperationBase obj)
        {
            owner.ChangeState<PreLoadGameResState>();
        }

        private void DownLoadProgress(int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
        {
            if (mUpdateUI == null)
            {
                return;
            }
            float progress = 1.0f * (currentDownloadBytes / totalDownloadBytes) * 100;
            mUpdateUI.SetProgressInfoText($"{currentDownloadBytes}/{totalDownloadBytes}Bytes,{progress}%");
            mUpdateUI.SetProgressSlider(progress);
        }
        #endregion
        private void ShowUpdateUI()
        {
            int totalDownloadCount = mDownLoader.TotalDownloadCount;
            long totalDownloadBytes = mDownLoader.TotalDownloadBytes;

            string tile = "���ظ���";
            string desc = $"���θ���{totalDownloadCount}���ļ����ۼ�{totalDownloadBytes}Bytes";
            string clickBtnDes = "���£�";
            mUpdateUI.SetProgressInfoTextActive(true);
            mUpdateUI.SetPanelData(tile, desc, clickBtnDes);
            mUpdateUI.SetDescBoxActive(true);
            mUpdateUI.SetClickAction(() =>
            {
                mUpdateUI.SetDescBoxActive(false);
                mDownLoader.BeginDownload();
            });
        }
    }
}

