using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    public GameObject m_Character;//主要操控角色
    public GameObject Player1, Player2;
    private GameObject m_camera;
    public GameObject SaveA;
    public GameObject SaveB;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_Character)
        {
            SaveSystems.Instance().SaveHappenedEvent();//儲存觸發過的動畫、事件、任務
            SaveSystems.Instance().SaveData(m_Character);//儲存移動角色
            SaveSystems.Instance().SaveData(Player1);//儲存戰鬥角色1
            SaveSystems.Instance().SaveData(Player2);//儲存戰鬥角色2

            SaveA.SetActive(false);
            SaveB.SetActive(true);

            MenuClick.GetSomething.SetActive(true);
            MenuClick.GetText.GetComponent<Text>().text = "保存目前進度";
            Invoke("CloseText", 2f);
        }
    }

    public void LoadData()
    {
        ///讀取移動角色資料+初始化攝影機
        m_camera = GameObject.Find("Camera");
        m_Character.GetComponent<CharacterController>().enabled = false;
        SaveSystems.Instance().LoadData(m_Character);
        m_camera.transform.position = m_Character.transform.position + m_Character.transform.forward * -2.5f + transform.up * 1.8f;
        m_Character.GetComponent<CharacterController>().enabled = true;

        SaveSystems.Instance().LoadHappenedEvent();//讀取觸發過的動畫、事件、任務
        SaveSystems.Instance().LoadData(Player1);//讀取戰鬥角色1
        SaveSystems.Instance().LoadData(Player2);//讀取戰鬥角色2
    }

    void CloseText()
    {
        MenuClick.GetSomething.SetActive(false);
    }
}