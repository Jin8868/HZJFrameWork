//=====================================================
// - FileName:      LogoAniProcedure.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/04/30 16:59:44
// - Description:   ´¦Àí¿ò¼ÜLogo¶¯»­
//======================================================
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using static Reporter;

namespace HZJFrameWork
{
    public class LogoAniProcedure : ProcedureBase
    {
        private GameObject LogoObj;


        public override void OnInit()
        {
            LogoObj = GameObject.Find("LogoText");
            if (LogoObj)
            {
                Text LogoText = LogoObj.GetComponent<Text>();
                if (LogoText)
                {
                    //LogoText.text = string.Empty;
                    //LogoText.DOText("HZJFrameWork", 1.0f, false);
                    LogoText.DOFade(1.0f, 1.5f).OnComplete(OnDisPose);
                }
            }
        }

        public override void OnDisPose()
        {
            LogoObj = null;
        }
    }
}

