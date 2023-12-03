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
                    //创建一个新的游戏对象并把单例组件挂在到游戏物体上
                    var singletonObj = new GameObject(typeof(T).ToString() + "(Singleton)");
                    instance = singletonObj.AddComponent<T>();

                    DontDestroyOnLoad(singletonObj);
                }
            }
            return instance;
        }
    }

}
