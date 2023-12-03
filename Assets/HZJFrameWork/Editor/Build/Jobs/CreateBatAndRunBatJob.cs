//=====================================================
// - FileName:      CreateBatAndRunBatJob.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/15 23:59:30
// - Description:   ��������������ļ���������
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using HZJFrameWork;
using System.Diagnostics;
using System;

public class CreateBatAndRunBatJob : IJob
{
    private string mOutPutPath;
    public CreateBatAndRunBatJob(JobName jobName,string outPutPath)
    {
        mJobName = jobName;
        mOutPutPath = outPutPath;
    }

    public override void StartJob()
    {
        HZJFrameWork.HZJLog.LogWithGreen("������·��=" + mOutPutPath);
        if (string.IsNullOrEmpty(mOutPutPath) || !Directory.Exists(mOutPutPath))
        {
            HZJLog.LogError("�����·��Ϊ�ջ����ļ��в�����");
            return;
        }
        string batPath = CreateBuilBat(mOutPutPath);
        RunBat(batPath);
    }

    private string CreateBuilBat(string outPutPath)
    {
        string batStr = "@echo off\r\n" +
            "echo ��ʼ����**********\r\n" +
            "gradle assembleDebug\r\n" +
            "echo ��ʼ���**********\r\n" +
            "pause";
        string batPath = outPutPath + "/Build.bat";
        File.WriteAllText(batPath, batStr);
        HZJLog.LogWithGreen("��������������ļ��ɹ���");
        return outPutPath;
    }

    private void RunBat(string batPath)
    {
        Process proc = null;
        try
        {
            string targetDir = batPath + "/";
            proc = new Process();
            proc.StartInfo.WorkingDirectory = targetDir;
            proc.StartInfo.FileName = "Build.bat";
            proc.StartInfo.Arguments = string.Empty;
            proc.StartInfo.CreateNoWindow = false;
            proc.StartInfo.ErrorDialog = true;
            HZJLog.LogWithGreen("��ʼ���д��������");
            proc.Start();
            proc.WaitForExit();
            HZJLog.LogWithGreen(batPath);
            Process.Start("Explorer.exe", batPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
        }
    }
}
