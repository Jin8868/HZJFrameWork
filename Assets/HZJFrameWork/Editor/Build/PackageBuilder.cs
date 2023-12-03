//=====================================================
// - FileName:      PackageBuilder.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/15 23:44:55
// - Description:   打包处理类
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using HZJFrameWork;

public class PackageBuilder
{
    private static string[] scenes =
    {
        @"Assets/scenes/GameEntry.unity",
    };

    public static void BuildAndriodDebugApk()
    {
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
        {
            return;
        }

        string buildRootPath = Application.dataPath;
        buildRootPath = buildRootPath.Replace("Assets", "Build");

        if (!Directory.Exists(buildRootPath))
        {
            Directory.CreateDirectory(buildRootPath);
        }

        string curTime = DateTime.Now.ToString("MMddHHmm");
        string projectPath = buildRootPath + "/" + Application.productName + "_" + curTime;
        HZJLog.LogWithRed(projectPath);
        AndroidDebugBuilder androidTestBuilder = new AndroidDebugBuilder();
        androidTestBuilder.outPutPath = projectPath;
        androidTestBuilder.InitBuilderJob();

        BuilerProcess.SetBuilder(androidTestBuilder);

        BuildOptions buildOptions = BuildOptions.CompressWithLz4;
        BuildReport report = BuildPipeline.BuildPlayer(scenes, projectPath, BuildTarget.Android, buildOptions);
    }
}
