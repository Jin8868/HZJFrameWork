using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HZJFrameWork;
using DG.Tweening;
using System.Numerics;
using System;

public class Test : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //LogText();
        //BigNumber bigNumber1 = new BigNumber("123456789");
        //BigNumber bigNumber2 = new BigNumber("987654321");
        //BigNumber bigNumber3 = bigNumber1 * bigNumber2;


        //BigNumber bigNumber4 = new BigNumber("1234567891011121314151617181920212223242526272829303132");
        //BigNumber bigNumber5 = new BigNumber("123456789");
        //BigNumber bigNumber6 = bigNumber4 * bigNumber5;
        ////HZJLog.LogWithRed($"{bigNumber1.ToString()} * {bigNumber2.ToString()} = {bigNumber3.ToString()}");
        //HZJLog.LogWithRed($"{bigNumber4.ToString()} * {bigNumber5.ToString()} = {bigNumber6.ToString()}");
        LoadModuleTest();
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

    private void LoadModuleTest()
    {
        ModuleManager.I.GetModule<ResourcesModule>().LoadPrefabsAsync("cube", loadingCallBack, loadCompleteCallback);

    }

    private void loadCompleteCallback(ResourcesInfoBase info)
    {
        GameObject.Instantiate(info.mainObject);
    }

    private void loadingCallBack(float progress)
    {
        HZJLog.LogWithGreen(progress);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
