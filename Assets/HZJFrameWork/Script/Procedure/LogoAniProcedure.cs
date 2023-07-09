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
using Unity.VisualScripting;
using Sequence = DG.Tweening.Sequence;

namespace HZJFrameWork
{
    public class LogoAniProcedure : ProcedureBase
    {
        private GameObject LogoObj;
        private GameObject BG;

        private Sequence mLogoSeq;

        public override void OnInit()
        {
            LogoObj = GameObject.Find("LogoText");
            BG = GameObject.Find("BG");
            if (LogoObj)
            {
                Text LogoText = LogoObj.GetComponent<Text>();
                if (LogoText)
                {
                    mLogoSeq = DOTween.Sequence();
                    mLogoSeq.Append(LogoText.DOFade(1.0f, 1.5f));
                    mLogoSeq.AppendInterval(1.5f);
                    mLogoSeq.OnComplete(OnDisPose);
                }
            }
        }

        public override void OnDisPose()
        {
            GameObject.Destroy(LogoObj);
            LogoObj = null;

            GameObject.Destroy(BG);
            BG = null;

            mLogoSeq.Kill();

            HZJFrameWorkEntry.Instance.ChangeGameState(new ProcedurePreLoad());
        }
    }
}

