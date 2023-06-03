using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HZJFrameWork
{
    public static class UIExtension
    {
        public static void AddClick(this Button btn, UnityEngine.Events.UnityAction clickAction)
        {
            if (btn && clickAction != null)
            {
                btn.onClick.AddListener(clickAction);
            }
        }
    }
}

