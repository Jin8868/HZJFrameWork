//=====================================================
// - FileName:      ExcelField.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/10/15 13:35:32
// - Description:   
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;

namespace HZJFrameWork
{
    public class ExcelField 
    {
        //×Ö¶ÎÃû
        public string fieldName;
        //×Ö¶ÎÀàÐÍ
        public string fieldType;
        //×Ö¶ÎÃèÊö
        public string fieldDesc;

        public ExcelField(string fieldName, string fieldType ,string fieldDesc)
        {
            this.fieldName = fieldName;
            this.fieldType = fieldType;
            this.fieldDesc = fieldDesc;
        }
    }
}

