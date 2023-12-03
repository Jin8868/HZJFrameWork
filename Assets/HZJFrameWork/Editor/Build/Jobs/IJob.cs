//=====================================================
// - FileName:      IJob.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/15 17:21:24
// - Description:   ������̻���
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;

namespace HZJFrameWork
{

    public enum JobName
    {
        None = 0,

        BuildOver = 100,//100֮�����������֮�������

        CreateBatAndRunBat,//���������д���������ļ�

    }

    public class IJob
    {
        public JobName mJobName = JobName.None;


        public virtual void StartJob() { }
    }
}

