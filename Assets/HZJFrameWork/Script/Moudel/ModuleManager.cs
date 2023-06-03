//=====================================================
// - FileName:      ModuleManager.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/05/01 17:27:08
// - Description:   模块管理器
//======================================================
using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace HZJFrameWork
{
    public class ModuleManager
    {
        private static ModuleManager mInstance;
        private List<ModuleBase> mMoudleList = new List<ModuleBase>();
        public static ModuleManager I
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new ModuleManager();

                }
                return mInstance;
            }

        }

        public T GetMoudel<T>() where T: ModuleBase,new()
        {
            if (mMoudleList != null)
            {
                foreach (var item in mMoudleList)
                {
                    if (item.GetType() == typeof(T))
                    {
                        return item as T;
                    }
                }
                T newModule = CreateMoudel<T>();
                mMoudleList.Add(newModule);
                return newModule;
            }
            HZJLog.LogError("moduleList为空！");
            return default(T);
        }

        private T CreateMoudel<T>() where T : ModuleBase, new()
        {
            T newModule = new T();
            return newModule;
        }
    }
}

