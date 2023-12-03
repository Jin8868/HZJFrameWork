//=====================================================
// - FileName:      GameStart.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/12/02 15:21:16
// - Description:   
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using HZJFrameWork;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ModuleManager.I.GetModule<UIMoudule>().Show<TestUI>();
    }


}
