using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresistentSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }  // 设置访问器, 可以被其他类访问, 但只能通过本类来赋值

    protected virtual void Awake()  // 可被子类重写
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else if (Instance != this)  // 例如加载了新场景, 并在新场景中已经有一个挂载了相同脚本的游戏对象时
        {
            Destroy(gameObject);  // 将原来脚本挂载的对象摧毁掉, 防止出现两个相同的静态实例造成冲突
        }

        DontDestroyOnLoad(gameObject);  // DontDestroyOnLoad()加载新场景时告诉引擎不要摧毁参数所传入的对象
    }
}
