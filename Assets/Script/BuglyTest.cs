using HZJFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuglyTest : MonoBehaviour
{
    private Button mBtn;
    private List<int> mList;
    // Start is called before the first frame update
    void Start()
    {
        mBtn = transform.GetComponent<Button>();

        mBtn.AddClick(BtnClick);
        mList = new List<int>();
    }


    private void BtnClick()
    {
        HZJLog.LogWithRed("这里是按钮点击~");
        //for (int i = 0; i < 100000000000; ++i)
        //{
        //    for (int j = 0; j < 100; ++j)
        //    {
        //        mList.Add(1);
        //    }
        //}
    }

}
