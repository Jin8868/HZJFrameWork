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
        #region 仿真模式
        [MenuItem("★HZJFrameWork★/仿真模式", false, 9)]
        public static void ChangeEmulationMode()
        {
            EmulationMode.isEmulationMode = !EmulationMode.isEmulationMode;
            Menu.SetChecked("★HZJFrameWork★/仿真模式", EmulationMode.isEmulationMode);
        }
        #endregion

        [MenuItem("★HZJFrameWork★/开始游戏 _F5", false, 30)]
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
                UnityEngine.Debug.LogError("传入的场景路径为空！");
                return;
            }

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }
        #region   重命名AB包名
        private static string ABPath = Application.dataPath + "/Bundles";
        [MenuItem("★HZJFrameWork★/重命名AB资源 ", false, 10)]
        public static void ReNameABPackage()
        {
            ReNameABPackage(ABPath);
            HZJLog.LogWithGreen("重命名AB资源成功！");
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
                HZJLog.LogError("AB包重命名:传入的路径为空！");
                return ABPackageName;
            }
            path = path.Replace("\\", "/");
            path = path.ToLower();
            string[] pathInfo = path.Split("bundles/");
            if (pathInfo == null || pathInfo.Length < 2)
            {
                HZJLog.LogError("AB包重命名:传入的路径解析失败！");
                return ABPackageName;
            }

            ABPackageName = pathInfo[1];
            return ABPackageName;
        }
        #endregion


        #region 打包AssetsBundles
        [MenuItem("★HZJFrameWork★/生成AB资源", false, 11)]
        public static void CreateAssetBundles()
        {
            string outputPath = Application.streamingAssetsPath + "/" + Application.productName;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.ChunkBasedCompression,
                EditorUserBuildSettings.activeBuildTarget);
            HZJLog.LogWithGreen("生成AB资源成功！");
        }
        #endregion

        #region 打包APK
        [MenuItem("★HZJFrameWork★/打包/安卓Debug包", false, 20)]
        public static void BuildAndroidTestApk()
        {
            PackageBuilder.BuildAndriodDebugApk();
        }
        #endregion

        [MenuItem("★HZJFrameWork★/Config/生成配置表", false, 30)]
        public static void CreateConfigData()
        {
            ConfigReader.ReadAllConfig();
        }

        [MenuItem("★HZJFrameWork★/Config/Show In Explorer", false, 30)]
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

