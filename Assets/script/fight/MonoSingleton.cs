using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    private static T t;
    public static T Intance
    {
        get
        {
            t = GameObject.FindObjectOfType(typeof(T)) as T;
            if (t == null)
            {
                GameObject go = new GameObject();
                t = go.AddComponent<T>();
                go.name = t + "Object";
            }
            //在場景切換時不要銷燬
            //DontDestroyOnLoad(t);
            return t;
        }
    }
}