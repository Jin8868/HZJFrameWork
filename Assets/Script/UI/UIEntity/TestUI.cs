
using UnityEngine;
using HZJFrameWork;
using System;
using YooAsset;
using DG.Tweening;

public partial class TestUI : UIBase
{
    private AssetHandle mCubeAssetHandle;

    public TestUI()
    {
        UIOrder = HZJFrameWorkDefine.UIOrder.UIPopup;
    }

    protected override void Awake()
    {
        base.Awake();
        AddClick();
        OpenAnimation();
    }

    public override void Refresh()
    {
        base.Refresh();
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    private void OpenAnimation()
    {
        BtnContent.SetActive(false);
        Sequence seq = DOTween.Sequence();
        seq.Append(FrameWorkText.DOText("Welcome To HZJFrameWork!", 2.0f));
        seq.AppendInterval(1.5f);
        seq.OnComplete(() =>
        {
            BtnContent.SetActive(true);
            HZJFrameWork.SetActive(false);
        });
    }

    private void AddClick()
    {
        Button.onClick.AddListener(LoadCube);
        CloseBtn.onClick.AddListener(CloseSelf);
        UnLoadBtn.onClick.AddListener(unload);
    }

    private void unload()
    {
        //ModuleManager.I.GetModule<ResourcesModule>().UnLoadAssetsBundleByName("prefabs/capsule.prefab");
        mCubeAssetHandle.Release();
    }

    private void LoadCube()
    {
        mCubeAssetHandle = ModuleManager.I.GetModule<ResourcesModule>().LoadPrefabAsync("Cube");
        mCubeAssetHandle.Completed += loadCompleteCallBack;
    }

    private void loadCompleteCallBack(AssetHandle info)
    {
        GameObject obj =  GameObject.Instantiate(info.GetAssetObject<GameObject>());
        obj.transform.DOScale(Vector3.one * 0.5f, 0.5f).SetLoops(-1);
    }

}


