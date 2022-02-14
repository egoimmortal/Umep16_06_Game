using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerTest
{
    private static SceneManagerTest m_Instance;
    public static SceneManagerTest Instance() { return m_Instance; }

    public SceneManagerTest()
    {
        m_Instance = this;
    }

    public void ChangeScene(string sSceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sSceneName, mode);
    }

    public void ChangeScene(int iid)
    {
        SceneManager.LoadScene(iid);
       
    }
    
}
