//=====================================================
// - FileName:      IJob.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/15 17:21:24
// - Description:   打包流程基类
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;

namespace HZJFrameWork
{

    public enum JobName
    {
        None = 0,

        BuildOver = 100,//100之后代表构建结束之后的流程

        CreateBatAndRunBat,//构建并运行打包批处理文件

    }

    public class IJob
    {
        public JobName mJobName = JobName.None;


        public virtual void StartJob() { }
    }
}

