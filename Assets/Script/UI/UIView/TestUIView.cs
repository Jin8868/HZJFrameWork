using UnityEngine;
using UnityEngine.UI;
using HZJFrameWork;
//////////////////////
///代码生成，请勿修改///
//////////////////////
public partial class TestUI : UIBase
{
    private Text Text;
    private Button Button;
    private Image bg;
    private Button CloseBtn;
    private Button UnLoadBtn;
    private GameObject HZJFrameWork;
    private Text FrameWorkText;
    private GameObject BtnContent;


    protected override void InitComponent()
    {
         Text = Get<Text>("Text");
         Button = Get<Button>("Button");
         bg = Get<Image>("bg");
         CloseBtn = Get<Button>("CloseBtn");
         UnLoadBtn = Get<Button>("UnLoadBtn");
        HZJFrameWork = Get("HZJFrameWork");
         FrameWorkText = Get<Text>("FrameWorkText");
        BtnContent = Get("BtnContent");

    }
}
