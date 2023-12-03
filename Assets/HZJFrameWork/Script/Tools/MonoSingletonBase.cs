//=====================================================
// - FileName:      MonoSingletonBase.cs
// - Created:       HeZhiJin
// - CreateTime:    2023/07/09 13:59:56
// - Description:   
//======================================================
using UnityEngine;

public class MonoSingletonBase<T> : MonoBehaviour where T : MonoBehaviour

{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    //����һ���µ���Ϸ���󲢰ѵ���������ڵ���Ϸ������
                    var singletonObj = new GameObject(typeof(T).ToString() + "(Singleton)");
                    instance = singletonObj.AddComponent<T>();

                    DontDestroyOnLoad(singletonObj);
                }
            }
            return instance;
        }
    }

}
