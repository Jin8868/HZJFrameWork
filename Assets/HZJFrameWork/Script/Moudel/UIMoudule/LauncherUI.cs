//=====================================================
// - FileName:      Lachen.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/12/03 14:54:02
// - Description:   
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using DG.Tweening;

namespace HZJFrameWork
{
    public class LauncherUI : MonoBehaviour
    {
        //组件
        private GameObject gameObject;
        private Transform transform;

        private GameObject mUICanvasObj;

        private Text mLauncherText;

        public LauncherUI(GameObject uiObj)
        {
            if (uiObj == null)
            {
                HZJLog.LogWithRed("传入的uiObj为空！");
                return;
            }
            GameObject UICanvas = Resources.Load("LauncherUI/LauncherCanvas") as GameObject;
            mUICanvasObj = GameObject.Instantiate(UICanvas);
            Transform parentRoot = mUICanvasObj.transform.Find("UIRoot/UIWindows");
            gameObject = GameObject.Instantiate(uiObj, parentRoot);
            transform = gameObject.transform;
            Init();
        }

        private void Init()
        {
            mLauncherText = transform.Find("LauncherText").GetComponent<Text>();
            mLauncherText.DOText("HZJFrameWork!",1.0f);
        }

        public void OnDisPose()
        {
            if (gameObject != null)
            {
                GameObject.Destroy(gameObject);
                gameObject = null;
            }
        }
    }
}

