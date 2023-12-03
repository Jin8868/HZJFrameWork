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
        // ����SDK����־��ӡ�������汾����عر�
        BuglyAgent.ConfigDebugMode(true);
        // ע����־�ص����滻ʹ�� 'Application.RegisterLogCallback(Application.LogCallback)'ע����־�ص��ķ�ʽ
        // BuglyAgent.RegisterLogCallback (CallbackDelegate.Instance.OnApplicationLogCallbackHandler);

#if UNITY_IPHONE || UNITY_IOS
        BuglyAgent.InitWithAppId ("Your App ID");
#elif UNITY_ANDROID
        BuglyAgent.InitWithAppId(mBugly_APPID);
#endif

        // �����ȷ�����ڶ�Ӧ��iOS���̻�Android�����г�ʼ��SDK����ô�ڽű���ֻ������C#�쳣�����ϱ����ܼ���
        BuglyAgent.EnableExceptionHandler();
    }
}

