//=====================================================
// - FileName:      IUIDatas.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/08/06 13:52:43
// - Description:   UI�������Ϣ
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
    /// ͨ�����д洢���������������������
    /// </summary>
    /// <returns>ֱ�ӿ��õ���������</returns>
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
