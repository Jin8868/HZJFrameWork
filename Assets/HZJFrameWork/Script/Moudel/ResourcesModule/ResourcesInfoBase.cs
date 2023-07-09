//=====================================================
// - FileName:      ResourcesInfo.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/07/08 22:44:19
// - Description:   资源加载信息基类
//======================================================
using UnityEngine;
using HZJFrameWork;

public class ResourcesInfoBase
{
    public string mainObjectName;

    public string assetsbundleName;

    public AssetBundle assetBundle;


    public ResourcesModule.LoadCompleteCallBack loadCompleteCallBack;

    protected UnityEngine.Object mMainObject;

    public UnityEngine.Object mainObject
    {
        get
        {
            if (assetBundle == null)
            {
                HZJLog.LogError("资源模块:加载的AB包为空！");
                return null;
            }
            if (mMainObject == null)
            {
                mMainObject = assetBundle.LoadAsset(mainObjectName);
            }

            return mMainObject;
        }
    }
}
