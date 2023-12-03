//=====================================================
// - FileName:      UIBase.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/08/13 13:13:44
// - Description:   
//======================================================

using System;
using UnityEngine;
using System.Collections.Generic;
using YooAsset;

namespace HZJFrameWork
{
    public class UIBase
    {
        protected string mUIBundleName;

        protected UIOutsideInfos mUIOutsideInfos;

        protected HZJFrameWorkDefine.UIOrder UIOrder;

        protected Transform transform;

        protected GameObject gameObject;

        private YooAsset.AssetHandle mAssetBundle;


        protected virtual void Awake()
        {

        }

        public virtual void Refresh()
        {

        }

        public virtual void Dispose()
        {
            if (mUIOutsideInfos != null && mUIOutsideInfos.UIInfoList != null)
            {
                mUIOutsideInfos.UIInfoList.Clear();
                mUIOutsideInfos.UIInfoList = null;
                mUIOutsideInfos = null;
            }
            GameObject.Destroy(gameObject);
            gameObject = null;
            transform = null;
            mAssetBundle.Release();//释放AB包资源
        }

        #region 初始化组件
        protected virtual void InitComponent()
        {

        }
        #endregion

        #region 打开UI
        private string GetUIPath()
        {
            string uiName = GetType().FullName;
            int startIndex = uiName.LastIndexOf('.');
            startIndex = startIndex < 0 ? 0 : startIndex + 1;
            uiName = uiName.Substring(startIndex, uiName.Length - startIndex);
            return uiName;
        }

        internal void _OpenUI()
        {
            mAssetBundle = ModuleManager.I.GetModule<ResourcesModule>().LoadUI(GetUIPath());
            mAssetBundle.Completed += LoadCompleteCallBack;
        }

        private void LoadCompleteCallBack(AssetHandle obj)
        {
            GameObject uiPrefab = obj.GetAssetObject<GameObject>();
            Transform parentRoot = ModuleManager.I.GetModule<UIMoudule>()._GetNode(UIOrder).transform;
            GameObject uiGameObject = GameObject.Instantiate(uiPrefab, parentRoot);
            mUIOutsideInfos = uiGameObject.GetComponent<UIOutsideInfos>();
            gameObject = uiGameObject;
            transform = uiGameObject.transform;
            InitComponent();
            Awake();
            Refresh();
        }

        #endregion

        #region  获取自身组件
        protected T Get<T>(string name) where T : class
        {
            GameObject uiObject = Get(name);

            return uiObject.GetComponent<T>();
        }

        protected GameObject Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            for (int i = 0; i < mUIOutsideInfos.UIInfoList.Count; ++i)
            {
                OutSideItemData tempItem = mUIOutsideInfos.UIInfoList[i];

                if (tempItem == null || tempItem.gameObject == null)
                {
                    continue;
                }

                if (name.Equals(tempItem.objectName))
                {
                    return tempItem.gameObject;
                }
            }

            HZJLog.LogError($"UI系统：名字为[{name}]的组件，找不到请检查UI！");
            return null;
        }

        protected virtual void CloseSelf()
        {
            Dispose();
        }
        #endregion
    }
}

