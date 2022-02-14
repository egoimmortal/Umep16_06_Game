using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResoucesManager
{
    public static ResoucesManager m_Instance;
    public static ResoucesManager Instance() { return m_Instance; }

    public ResoucesManager()
    {
        m_Instance = this;
    }

    public GameObject LoadGameObject(string s_Name)
    {
        Object o = Resources.Load(s_Name);
        GameObject go = GameObject.Instantiate(o) as GameObject;
        return go;
       
    }
}
