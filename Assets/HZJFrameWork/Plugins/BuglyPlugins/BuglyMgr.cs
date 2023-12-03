using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuglyMgr : MonoBehaviour
{
    private const string mBugly_APPID = "070751bbc3";
    // Start is called before the first frame update
    void Start()
    {
        InitBugly();
    }

    private void InitBugly()
    {
        // 开启SDK的日志打印，发布版本请务必关闭
        BuglyAgent.ConfigDebugMode(true);
        // 注册日志回调，替换使用 'Application.RegisterLogCallback(Application.LogCallback)'注册日志回调的方式
        // BuglyAgent.RegisterLogCallback (CallbackDelegate.Instance.OnApplicationLogCallbackHandler);

#if UNITY_IPHONE || UNITY_IOS
        BuglyAgent.InitWithAppId ("Your App ID");
#elif UNITY_ANDROID
        BuglyAgent.InitWithAppId(mBugly_APPID);
#endif

        // 如果你确认已在对应的iOS工程或Android工程中初始化SDK，那么在脚本中只需启动C#异常捕获上报功能即可
        BuglyAgent.EnableExceptionHandler();
    }
}

