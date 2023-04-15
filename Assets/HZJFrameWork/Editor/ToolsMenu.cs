using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using HZJFrameWork;
using UnityEditor.SceneManagement;

public class ToolsMenu
{
    [MenuItem("★HZJFrameWork★/开始游戏 _F5", false, 30)]
    public static void OpenGame()
    {
        string mainScene = "Assets/Scenes/SampleScene.unity";
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

    #region 打包
    [MenuItem("★HZJFrameWork★/打包/安卓Debug包",false,20)]
    public static void BuildAndroidTestApk()
    {
        PackageBuilder.BuildAndriodDebugApk();
    }
    #endregion
}
