using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCtest : MonoBehaviour
{
    private static NPCtest m_Instance;
    public static NPCtest Instance() { return m_Instance; }
    public NPCtest()
    {
        m_Instance = this;
    }

    public GameObject player;
    public GameObject NPC;
    public GameObject MissionT;
    public GameObject Exx2;
    public GameObject Exx3;
    public GameObject hExx2;
    public GameObject hExx3;
    public GameObject MissionP;//任務清單
    public GameObject MTip;

    public Camera Mcamera;

    private GameObject Talk;  //對話圖示
    private bool bMissionOK;    //任務完成沒
    private bool bOpenTalk; //開啓對話系統
    private bool bCompleteOnce; //完成任務開啓？icon一次
    public bool bTalking; //鎖CAMERA用

    public Dialogue dialogue; //對話系統
    public Dialogue dialogue2;//對話系統任務未完成
    public Dialogue dialogueComplete;//對話系統任務完成
    public Dialogue MissionComplete; // 任務結束

    [HideInInspector]
    public int iMsr;    //殺死香菇數量
    [HideInInspector]
    public int iDragon; //殺死火龍數量
    [HideInInspector]
    public GameObject Msrkill;   //殺死香菇數量text
    [HideInInspector]
    public GameObject Dragonkill;//殺死火龍數量text
    [HideInInspector]
    public bool bUnityChanJoined; // UnityChan加入沒

    void Start()
    {
        Msrkill = GameObject.Find("msrkill"); //TEXT
        Dragonkill = GameObject.Find("dragonkill");//TEXT
        MissionP.SetActive(false);
        Msrkill.SetActive(false);
        Dragonkill.SetActive(false);
        MTip.SetActive(false);
    }

    void Update()
    {
        if(bUnityChanJoined && !bOpenTalk)
        {
            bOpenTalk = true; //開啓一次
            ShowTalk();//對話按鈕
            Talk.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(iMsr+" "+iDragon);
            iMsr++;
            iDragon++;
            MissionUpdate();
            Debug.Log(bMissionOK);
        }

        if (TotalStatic.objCamera1 && bUnityChanJoined)
        {
            Vector3 pos = NPC.transform.position;
            pos.y += 2;
            Vector3 vPos = TotalStatic.objCamera1.GetComponent<Camera>().WorldToScreenPoint(pos);
            Talk.transform.position = vPos;

            Vector3 vec = this.transform.position - player.transform.position;
            float dist = vec.magnitude;
            if (dist < 4)
            {
                Talk.SetActive(true);
            }
            else
            {
                Talk.SetActive(false);
            }
        }
        if(!TotalStatic.objCamera1.activeSelf && Talk) Talk.SetActive(false);

        if(!Mcamera.enabled && Talk) Talk.SetActive(false);
    }
    void ShowTalk()
    {
        Object o = Resources.Load("Talk");
        Talk = GameObject.Instantiate(o) as GameObject;
        Talk.transform.SetParent(GameObject.Find("Canvas").transform);

        //注冊對話按鈕
        Talk.GetComponent<Button>().onClick.AddListener(() =>
        {
            TriggerDialogue();
            Talk.SetActive(false);
        });
    }
    public void MissionGet()
    {
        if (!MissionP.activeSelf && !bMissionOK)//還沒接到任務前
        {
            MenuClick.GetSomething.SetActive(true);
            MenuClick.GetText.GetComponent<Text>().text = "任務：消滅附近怪物";
            Invoke("GetItemF", 2f);
            MissionP.SetActive(true);
            MissionT.SetActive(true);
            Msrkill.SetActive(true);
            Dragonkill.SetActive(true);
            Exx2.SetActive(false);//驚嘆號關閉
            hExx2.SetActive(false);
            iMsr = iDragon = 0;
            MissionT.GetComponent<Text>().text = "任務";
            Msrkill.GetComponent<Text>().color = new Color(255f/255f, 64f/255f, 64f/255f); 
            Msrkill.GetComponent<Text>().text = "- " + iMsr + "/1" + "  消滅香菇怪";
            Dragonkill.GetComponent<Text>().text = "- " + iDragon + "/1" + "  消滅小火龍";
        }

        if (iMsr == 1 && iDragon == 1 && !bMissionOK)//任務完成
        {
            bMissionOK = true;//任務對話完畢
            MissionP.SetActive(false);//關閉任務文字
            MissionT.SetActive(false);
            Msrkill.SetActive(false);
            Dragonkill.SetActive(false);
            Exx3.SetActive(false);
            hExx3.SetActive(false);
            // get skill
            MenuClick.GetSomething.SetActive(true);
            MenuClick.GetText.GetComponent<Text>().text = "AcquireChan學得 閃電！";
            MenuClick.Lightning.SetActive(true);//開啟閃電技能
            Invoke("UnityChanGetSkill",2f);
        }
        TotalStatic.MapAI.SetActive(true);//打開地圖怪物AI
        bTalking = false;
        TotalStatic.bCameraMove = true;
    }

    public void MissionUpdate()
    {
        if (iMsr > 1) iMsr = 1;
        if (iDragon > 1) iDragon = 1;
 
        Msrkill.GetComponent<Text>().text ="- " + iMsr + "/1" + "  消滅香菇怪";
        Dragonkill.GetComponent<Text>().text = "- " + iDragon + "/1" + "  消滅小火龍";

        if(iMsr == 1) Msrkill.GetComponent<Text>().color = Color.white;
        if(iDragon == 1)Dragonkill.GetComponent<Text>().color = Color.white;
        if (iMsr == 1 && iDragon == 1)
        {
            if (!bCompleteOnce)
            {
                bCompleteOnce = true;
                Exx3.SetActive(true);//問號出現
                hExx3.SetActive(true);
                MissionT.GetComponent<Text>().text = "任務（完成）";
            }
        }           
    }

    public void TriggerDialogue()//觸發對話
    {
        Sounds.Instance().NPCHello();
        TotalStatic.MapAI.SetActive(false);//關閉地圖怪物AI
        bTalking = true;
        TotalStatic.bCameraMove = false;
        if (!MissionP.activeSelf && !bMissionOK)//還沒接到任務前
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else if(MissionP.activeSelf && iMsr != 1 || iDragon != 1)
        {
            FindObjectOfType<DialogueManager>().SecondDialogue(dialogue2);
        }
        else if (iMsr == 1 && iDragon == 1 && !bMissionOK)//任務完成
        {
            FindObjectOfType<DialogueManager>().SecondDialogue(dialogueComplete);
        }
        else if(bMissionOK)
        {
            FindObjectOfType<DialogueManager>().SecondDialogue(MissionComplete);
        }
    }
    void UnityChanGetSkill()
    {
        TotalStatic.iRedWater_item += 5; //get item
        TotalStatic.iBlueWater_item += 3;
        TotalStatic.iRevive_item += 1;
        MenuClick.mOPSeed.SetActive(true);
        Invoke("GetItem", 2f);
    }
    void GetItem()
    {
        MenuClick.GetText.GetComponent<Text>().text = "獲得道具: \n藥草 5 個 魔力水滴 3 個";
        Invoke("GetItem2", 2f);
    }
    void GetItem2()
    {
        MenuClick.GetText.GetComponent<Text>().text = "獲得 世界樹之葉 1 個 \n惡魔果實 1 個";
        Invoke("CloseText", 2f);
        TimeLineM.bGateOpen = true;
    }
    void GetItemF()
    {
        TotalStatic.iRedWater_item += 3; //get item
        TotalStatic.iBlueWater_item += 1;
        MenuClick.GetSomething.SetActive(true);
        MenuClick.GetText.GetComponent<Text>().text = "獲得道具: \n藥草 3 個 魔力水滴 1 個";
        Invoke("CloseText", 2f);
    }
    void CloseText()
    {
        MenuClick.GetSomething.SetActive(false);
    }
}
