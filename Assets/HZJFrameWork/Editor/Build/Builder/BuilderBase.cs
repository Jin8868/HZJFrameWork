//=====================================================
// - FileName:      BuilderBase.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/15 23:26:40
// - Description:   构建基类
//======================================================
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace HZJFrameWork
{
    public class BuilderBase
    {
        protected List<IJob> mJobList;

        public virtual void InitBuilderJob() { }
        
        public void PreProcessJob()
        {
            if (mJobList == null)
            {
                Debug.LogError("构建任务列表为空！");
                return;
            }
            HZJLog.LogWithGreen("处理打包前任务...");
            for (int i = 0;i<mJobList.Count;++i)
            {
                if (mJobList[i].mJobName < JobName.BuildOver)
                {
                    mJobList[i].StartJob();
                }
            }
        }

        public void PostProcessJob()
        {
            if (mJobList == null)
            {
                Debug.LogError("构建任务列表为空！");
                return;
            }
            HZJLog.LogWithGreen($"处理打包后任务,一共有{mJobList.Count}个...");
            for (int i = 0; i < mJobList.Count; ++i)
            {
                if (mJobList[i].mJobName > JobName.BuildOver)
                {
                    HZJLog.LogWithGreen(mJobList[i].mJobName);
                    mJobList[i].StartJob();
                }
            }
        }
    }

    public class BuilerProcess : IPostprocessBuildWithReport, IPreprocessBuildWithReport
    {

        private static BuilderBase mBuiler;

        int IOrderedCallback.callbackOrder => 10;

        public static void SetBuilder(BuilderBase builder)
        {
            mBuiler = builder;
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            if (mBuiler != null)
            {
                mBuiler.PostProcessJob();
            }
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (mBuiler !=null)
            {
                mBuiler.PreProcessJob();
            }
        }
    }
}

