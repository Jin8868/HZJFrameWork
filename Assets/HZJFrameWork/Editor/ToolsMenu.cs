using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using HZJFrameWork;
using UnityEditor.SceneManagement;
using System.IO;
using static UnityEditor.Progress;

public class ToolsMenu
{
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
            Debug.LogError("传入的场景路径为空！");
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

    }
    #endregion

    #region 打包APK
    [MenuItem("★HZJFrameWork★/打包/安卓Debug包", false, 20)]
    public static void BuildAndroidTestApk()
    {
        PackageBuilder.BuildAndriodDebugApk();
    }
    #endregion
}
