//=====================================================
// - FileName:      ProcedurePreLoad.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/07/09 13:55:08
// - Description:   
//======================================================
using UnityEngine;
using HZJFrameWork;
using System;

public class ProcedurePreLoad : ProcedureBase
{

    public override void OnInit()
    {
        ModuleManager.I.GetModule<ResourcesModule>().LoadPrefabs("testObj", LoadCompleteCallBack) ;
    }

    private void LoadCompleteCallBack(ResourcesInfoBase info)
    {
        
        GameObject.Instantiate(info.mainObject);
    }

    public override void OnDisPose()
    {


    }
}
