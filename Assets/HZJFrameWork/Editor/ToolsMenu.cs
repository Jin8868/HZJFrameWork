using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.IO;
using System.Diagnostics;
using System;

namespace HZJFrameWork
{
    public class ToolsMenu
    {
        #region ����ģʽ
        [MenuItem("��HZJFrameWork��/����ģʽ", false, 9)]
        public static void ChangeEmulationMode()
        {
            EmulationMode.isEmulationMode = !EmulationMode.isEmulationMode;
            Menu.SetChecked("��HZJFrameWork��/����ģʽ", EmulationMode.isEmulationMode);
        }
        #endregion

        [MenuItem("��HZJFrameWork��/��ʼ��Ϸ _F5", false, 30)]
        public static void OpenGame()
        {
            string mainScene = "Assets/Scenes/GameEntry.unity";
            OpenScene(mainScene);
            if (!EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = true;
            }

        }

        public static void OpenScene(string scenePath)
        {
            if (string.IsNullOrEmpty(scenePath))
            {
                UnityEngine.Debug.LogError("����ĳ���·��Ϊ�գ�");
                return;
            }

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }
        #region   ������AB����
        private static string ABPath = Application.dataPath + "/Bundles";
        [MenuItem("��HZJFrameWork��/������AB��Դ ", false, 10)]
        public static void ReNameABPackage()
        {
            ReNameABPackage(ABPath);
            HZJLog.LogWithGreen("������AB��Դ�ɹ���");
        }

        public static void ReNameABPackage(string ABPath)
        {
            if (string.IsNullOrEmpty(ABPath))
            {
                return;
            }

            if (Directory.Exists(ABPath))
            {
                string[] files = Directory.GetFiles(ABPath, "*.*", SearchOption.AllDirectories);
                foreach (var filepath in files)
                {
                    if (filepath.EndsWith(".meta"))
                    {
                        continue;
                    }
                    string[] tempFilePath = filepath.Split("HZJFrameWork/");
                    var importer = AssetImporter.GetAtPath(tempFilePath[1]);

                    string packageName = GetABPackageNameByPath(filepath);
                    //HZJLog.LogWithGreen(packageName);
                    importer.assetBundleName = packageName;
                }
            }
        }

        private static string GetABPackageNameByPath(string path)
        {
            string ABPackageName = string.Empty;
            if (string.IsNullOrEmpty(path))
            {
                HZJLog.LogError("AB��������:�����·��Ϊ�գ�");
                return ABPackageName;
            }
            path = path.Replace("\\", "/");
            path = path.ToLower();
            string[] pathInfo = path.Split("bundles/");
            if (pathInfo == null || pathInfo.Length < 2)
            {
                HZJLog.LogError("AB��������:�����·������ʧ�ܣ�");
                return ABPackageName;
            }

            ABPackageName = pathInfo[1];
            return ABPackageName;
        }
        #endregion


        #region ���AssetsBundles
        [MenuItem("��HZJFrameWork��/����AB��Դ", false, 11)]
        public static void CreateAssetBundles()
        {
            string outputPath = Application.streamingAssetsPath + "/" + Application.productName;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression,
                EditorUserBuildSettings.activeBuildTarget);
            HZJLog.LogWithGreen("����AB��Դ�ɹ���");
        }
        #endregion

        #region ���APK
        [MenuItem("��HZJFrameWork��/���/��׿Debug��", false, 20)]
        public static void BuildAndroidTestApk()
        {
            PackageBuilder.BuildAndriodDebugApk();
        }
        #endregion

        [MenuItem("��HZJFrameWork��/Config/�������ñ�", false, 30)]
        public static void CreateConfigData()
        {
            ConfigReader.ReadAllConfig();
        }

        [MenuItem("��HZJFrameWork��/Config/Show In Explorer", false, 30)]
        public static void ShowConfigFile()
        {
            string configPath = Application.dataPath + "/../Config";
            FileInfo file = new FileInfo(configPath);
            if (string.IsNullOrEmpty(configPath)) return;

            try
            {
                Process.Start("Explorer.exe", file.FullName);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}

