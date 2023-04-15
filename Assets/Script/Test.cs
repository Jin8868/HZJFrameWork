using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HZJFrameWork;
using DG.Tweening;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LogText();

        if (transform != null)
        {
            transform.DOScale(Vector3.one * 0.5f,1.0f).SetLoops(-1,LoopType.Yoyo);
        }
    }

    private void LogText()
    {
        HZJLog.Log(string.Format("这里是{0}测试", "原色"));
        HZJLog.LogWithRed(string.Format("这里是{0}测试", "红色"));
        HZJLog.LogWithYellow(string.Format("这里是{0}测试", "黄色"));
        HZJLog.LogWithGreen(string.Format("这里是{0}测试", "绿色"));
        HZJLog.LogWithNetwork("这里是网络打印");
        HZJLog.LogWithIap("这里是内购打印");


    }

    // Update is called once per frame
    void Update()
    {

    }
}
