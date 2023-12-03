//=====================================================
// - FileName:      NewBehaviourScript.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/08/27 17:02:35
// - Description:   
//======================================================
using UnityEngine;
using UnityEditor;

namespace HZJFrameWork
{

    public class EmulationMode 
    {
        private static bool mIsEmulationMode;
        /// <summary>
        /// �Ƿ����ģʽ��ģ�����AB����Դ
        /// </summary>
        public static bool isEmulationMode
        {
            get {
#if UNITY_EDITOR

                return EditorPrefs.GetInt("isEmulationMode") == 1;
#endif
                return false;
            }
            set
            {
#if UNITY_EDITOR
                mIsEmulationMode = value;
                EditorPrefs.SetInt("isEmulationMode", mIsEmulationMode ? 1 : 0);
#endif

            }
        }
    }
}

