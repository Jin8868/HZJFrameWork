//=====================================================
// - FileName:      AndroidDebugBuilder.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/16 00:19:12
// - Description:   安卓测试包打包脚本
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using HZJFrameWork;
using System.Collections.Generic;

public class AndroidDebugBuilder :BuilderBase
{
    public string outPutPath;
    public override void InitBuilderJob()
    {
        base.InitBuilderJob();
        mJobList = new List<IJob>();
        mJobList.Add(new CreateBatAndRunBatJob(JobName.CreateBatAndRunBat,outPutPath));
    }
}
