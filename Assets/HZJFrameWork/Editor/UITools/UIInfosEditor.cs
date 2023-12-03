//=====================================================
// - FileName:      UIInfosEditor.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/08/06 14:02:25
// - Description:   UIInfos的编辑器脚本
//======================================================
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace HZJFrameWork
{
    [CustomEditor(typeof(UIOutsideInfos))]
    public class UIInfosEditor : Editor
    {
        private GUIStyle mGreenFontStyle;
        private GUIStyle mRedFontStyle;
        private Dictionary<GameObject, string> mOutsideInfosDic = new Dictionary<GameObject, string>();
        private int mCurTypeIndex;

        private void OnEnable()
        {
            mGreenFontStyle = new GUIStyle();
            mGreenFontStyle.fontSize = 11;
            mGreenFontStyle.fontStyle = FontStyle.Bold;
            mGreenFontStyle.normal.textColor = Color.green;

            mRedFontStyle = new GUIStyle();
            mRedFontStyle.fontSize = 11;
            mRedFontStyle.fontStyle = FontStyle.Bold;
            mRedFontStyle.normal.textColor = Color.red;
        }

        public override void OnInspectorGUI()
        {
            UIOutsideInfos uiInfos = (UIOutsideInfos)target;
            if (uiInfos.UIInfoList == null || uiInfos.UIInfoList.Count < 0)
            {
                if (GUILayout.Button("Add Item"))
                {
                    uiInfos.UIInfoList = new List<OutSideItemData>();
                    OutSideItemData itemData = new OutSideItemData();
                    uiInfos.UIInfoList.Add(itemData);

                }
            }
            else
            {
                string[] typeOptions = null;
                for (int i = 0; i < uiInfos.UIInfoList.Count; ++i)
                {
                    mCurTypeIndex = -1;

                    OutSideItemData itemData = uiInfos.UIInfoList[i];
                    bool mIsVaild = false;
                    if (itemData.gameObject != null)
                    {
                        if (!mOutsideInfosDic.ContainsKey(itemData.gameObject))
                        {
                            mOutsideInfosDic.Add(itemData.gameObject, string.Empty);
                        }
                        mIsVaild = true;

                        itemData.objectName = itemData.gameObject.name;

                        if (itemData.gameObject is GameObject)
                        {
                            GameObject itemGameObject = itemData.gameObject;
                            Component[] components = itemGameObject.GetComponents<Component>();
                            typeOptions = new string[components.Length + 1];
                            mCurTypeIndex = components.Length;

                            typeOptions[0] = itemGameObject.GetType().FullName;
                            if (typeOptions[0].Equals(itemData.typeName))
                            {
                                mCurTypeIndex = 0;
                                itemData.typeName = typeOptions[0];
                                mOutsideInfosDic[itemData.gameObject] = itemGameObject.GetType().Name;
                            }


                            for (int j = 1; j <= components.Length; j++)
                            {
                                Component component = components[j - 1];
                                typeOptions[j] = component.GetType().FullName;
                                if (typeOptions[j].Equals(itemData.typeName))
                                {
                                    mCurTypeIndex = j;
                                    itemData.typeName = typeOptions[j];
                                    mOutsideInfosDic[itemData.gameObject] = component.GetType().Name;
                                }
                            }
                        }
                    }

                    //绘制两层的UI面板
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label(new GUIContent(string.Format("Object Name：{0}", itemData.objectName)), mIsVaild ? mGreenFontStyle : mRedFontStyle);
                        if (GUILayout.Button("+", GUILayout.Width(20.0f)))
                        {
                            uiInfos.UIInfoList.Add(new OutSideItemData());
                        }
                        if (GUILayout.Button("-", GUILayout.Width(20.0f)))
                        {
                            uiInfos.UIInfoList.RemoveAt(i);
                        }
                        if (GUILayout.Button("↑", GUILayout.Width(20.0f)) && i > 0)
                        {
                            OutSideItemData temp = uiInfos.UIInfoList[i];
                            uiInfos.UIInfoList[i] = uiInfos.UIInfoList[i - 1];
                            uiInfos.UIInfoList[i - 1] = temp;
                        }
                        if (GUILayout.Button("↓", GUILayout.Width(20.0f)) && i < uiInfos.UIInfoList.Count - 1)
                        {
                            OutSideItemData temp = uiInfos.UIInfoList[i];
                            uiInfos.UIInfoList[i] = uiInfos.UIInfoList[i + 1];
                            uiInfos.UIInfoList[i + 1] = temp;
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.BeginHorizontal();
                    {
                        itemData.gameObject = (GameObject)EditorGUILayout.ObjectField(itemData.gameObject, typeof(GameObject));
                        if (itemData.gameObject != null && typeOptions != null && mCurTypeIndex > 0)
                        {

                            mCurTypeIndex = EditorGUILayout.Popup("", mCurTypeIndex, typeOptions);
                            //HZJLog.LogError($"name={itemData.gameObject.name},index = {mCurTypeIndex},lenght = {typeOptions.Length}");
                            string selectType = typeOptions[mCurTypeIndex].ToString();

                            if (!selectType.Equals(itemData.typeName))
                            {
                                itemData.typeName = selectType;
                                EditorUtility.SetDirty(target);
                            }
                        }

                    }
                    GUILayout.EndHorizontal();
                }

            }
        }
    }

}
