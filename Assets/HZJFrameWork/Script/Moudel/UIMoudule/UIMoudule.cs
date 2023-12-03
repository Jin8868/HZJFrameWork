//=====================================================
// - FileName:      NewBehaviourScript.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/08/13 13:08:01
// - Description:   UI模块管理类
//======================================================
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using YooAsset;

namespace HZJFrameWork
{
    public class UIMoudule : ModuleBase
    {
        private List<UIBase> mUIList = new List<UIBase>();

        private GameObject mUICanvas;

        private GameObject UIRoot;

        private AssetHandle mAssetHandle;

        private System.Action mInitCompleteCallBack;

        public override void Init(System.Action initCompleteCallBack)
        {
            //UIRoot = GameObject.Find("UICanvas/UIRoot").gameObject;
            mInitCompleteCallBack = initCompleteCallBack;
            mAssetHandle = ModuleManager.I.GetModule<ResourcesModule>().LoadPrefabAsync("UICanvas");
            mAssetHandle.Completed += LoadUICanvasComplete;
        }

        public override void Update()
        {

        }

        public override void Dispose()
        {
            if (mUIList != null)
            {
                for (int i = 0; i < mUIList.Count; ++i)
                {
                    UIBase ui = mUIList[i];
                    if (ui != null)
                    {
                        ui.Dispose();
                        ui = null;
                    }
                }
                mUIList.Clear();
                mUIList = null;
            }
        }

        private void LoadUICanvasComplete(AssetHandle assetHandle)
        {
            GameObject UICanvasPrefab = assetHandle.GetAssetObject<GameObject>();
            mUICanvas = GameObject.Instantiate(UICanvasPrefab);
            UIRoot = mUICanvas.transform.Find("UIRoot").gameObject;
            GameObject.DontDestroyOnLoad(mUICanvas);
            if (mInitCompleteCallBack != null)
            {
                mInitCompleteCallBack.Invoke();
            }
        }

        #region 对外接口

        public T Show<T>() where T : UIBase, new()
        {
            T ui = null;
            if (mUIList == null)
            {
                mUIList = new List<UIBase>();
                ui = CreateUI<T>();
                ui._OpenUI();
                mUIList.Add(ui);
                return ui;
            }

            for (int i = 0; i < mUIList.Count; ++i)
            {
                if (ui == null)
                {
                    continue;
                }
                if (mUIList[i].GetType() == typeof(T))
                {
                    ui._OpenUI();
                    return mUIList[i] as T;
                }
            }

            ui = CreateUI<T>();
            ui._OpenUI();
            mUIList.Add(ui);
            return ui;
        }

        public void CloseUI<T>() where T : UIBase
        {
            if (mUIList == null)
            {
                return;
            }

            for (int i = 0; i < mUIList.Count; ++i)
            {
                UIBase ui = mUIList[i];

                if (ui == null)
                {
                    continue;
                }

                if (ui.GetType() == typeof(T))
                {
                    ui.Dispose();
                    mUIList.RemoveAt(i);
                    return;
                }
            }
        }

        private T CreateUI<T>() where T : UIBase, new()
        {
            T ui = new T();

            return ui;
        }


        internal GameObject _GetNode(HZJFrameWorkDefine.UIOrder uiOrder)
        {
            int index = (int)uiOrder;
            return UIRoot.transform.GetChild(index).gameObject;
        }
        #endregion
    }
}

