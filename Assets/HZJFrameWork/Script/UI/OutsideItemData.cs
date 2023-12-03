//=====================================================
// - FileName:      IUIDatas.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/08/06 13:52:43
// - Description:   UI的组件信息
//======================================================
using System;
using UnityEngine;

[Serializable]
public class OutSideItemData
{
    public string typeName;
    public string objectName;
    public GameObject gameObject;

    /// <summary>
    /// 通过类中存储的完整的类型命获得类型
    /// </summary>
    /// <returns>直接可用的类型名字</returns>
    public string GetShortTypeName()
    {
        if (string.IsNullOrEmpty(typeName))
        {
            return string.Empty;
        }

        string shortTypeName = typeName.Substring(typeName.LastIndexOf(".") + 1);
        return shortTypeName;
    }
}
