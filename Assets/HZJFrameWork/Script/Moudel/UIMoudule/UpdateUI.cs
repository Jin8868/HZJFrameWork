//=====================================================
// - FileName:      UpdateUI.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/11/26 14:00:13
// - Description:   
//======================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using HZJFrameWork;

public class UpdateUI
{
    //组件
    private GameObject gameObject;
    private Transform transform;

    private GameObject mUICanvasObj;
    private GameObject mDescBox;
    private Text mTitleText;
    private Text mDescText;
    private Button mClickBtn;
    private Text mClickBtnText;
    private Slider mProgressSlider;
    private Text mProgressInfoText;
    private Text mUpdateText;

    public UpdateUI(GameObject uiObj)
    {
        if (uiObj == null)
        {
            HZJLog.LogWithRed("传入的uiObj为空！");
            return;
        }
        GameObject UICanvas = Resources.Load("LauncherUI/LauncherCanvas") as GameObject;
        mUICanvasObj = GameObject.Instantiate(UICanvas);
        gameObject = GameObject.Instantiate(uiObj, mUICanvasObj.transform);
        transform = gameObject.transform;
        Init();
    }


    #region 初始化
    private void Init()
    {
        InitComponent();
    }

    private void InitComponent()
    {
        mDescBox = transform.Find("DescBox").gameObject;
        mTitleText = transform.Find("DescBox/Title").GetComponent<Text>();
        mDescText = transform.Find("DescBox/Desc").GetComponent<Text>();
        mClickBtn = transform.Find("DescBox/ClickBtn").GetComponent<Button>();
        mClickBtnText = transform.Find("DescBox/ClickBtn/BtnText").GetComponent<Text>();
        mProgressSlider = transform.Find("Slider").GetComponent<Slider>();
        mProgressInfoText = transform.Find("Slider/ProgressInfo").GetComponent<Text>();
        mUpdateText = transform.Find("Slider/UpdateText").GetComponent<Text>();
    }

    #endregion

    #region   设置信息

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public void SetDescBoxActive(bool isActive)
    {
        mDescBox.SetActive(isActive);
    }

    public void SetProgressInfoTextActive(bool isActive)
    {
        mProgressInfoText.gameObject.SetActive(isActive);
    }

    public void SetPanelData(string title,string desc,string clickBtnDesc)
    {
        mTitleText.text = title;
        mDescText.text = desc;
        mClickBtnText.text = clickBtnDesc;
    }

    public void SetClickAction(UnityEngine.Events.UnityAction clickAction)
    {
        mClickBtn.AddClick(clickAction);
    }

    public void SetProgressSlider(float progress)
    {
        mProgressSlider.value = progress;
    }

    public Slider GetSlider()
    {
        return mProgressSlider;
    }

    public void SetProgressInfoText(string text)
    {
        mProgressInfoText.text = text;
    }

    public void SetUpdateText(string text)
    {
        mUpdateText.text = text;
    }
    #endregion

    public void OnDispose()
    {
        if (mUICanvasObj != null)
        {
            GameObject.Destroy(mUICanvasObj);
            mUICanvasObj = null;
        }
    }
}
