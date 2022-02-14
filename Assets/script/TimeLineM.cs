using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class TimeLineM : MonoBehaviour
{
    public PlayableDirector tStartTL; //取得第一個Timeline
    public PlayableDirector tUnityChanTL;//取得UnityChan Timeline
    public PlayableDirector GateTL;
    public PlayableDirector BossTL;
    public PlayableDirector GameOverTL;

    ///動畫開關
    public static bool bStartTL;         //Starttimeline結束開關
    public static bool bUnityChanTL;     //UnityChantimeline結束開關
    public static bool bGateTL;
    public static bool bBossTL;

    private GameObject Tcamera;       //timeline Camera
    public Camera Mcamera;            //Main Camera

    public GameObject m1;   //假香菇
    public GameObject m2;   //假香菇
    public static GameObject UChan;//UnityChan
    public GameObject StartImage; //最開始畫面
    public GameObject EXX1; //驚嘆號1
    public GameObject EXX2; //驚嘆號2
    public GameObject hEXX2;
    public GameObject EXX4; //door!
    public GameObject GateDoor;
    public GameObject SavePoint;
    public GameObject vcam1;
    public GameObject vcam2;
    public GameObject vcam3;
    public GameObject vcam4;
    public GameObject vcam5;
    public GameObject vcam6;
    public GameObject vcam7;
    public GameObject vcam9;
    public GameObject vcam10;
    public GameObject vcam11;
    public GameObject vcam12;
    public GameObject vcam13;
    public GameObject vcam14;
    public GameObject pp1;
    public GameObject pp2;
    public GameObject BF;

    private GameObject UnityChanTL;
    public static bool bUChanTLtrigger;
    public static bool bGateOpen;
    public static bool bBossGO;
    public static bool bGameOver;
    private bool UChanTLEnd;

    void Start()
    {
        Tcamera = GameObject.Find("TCamera");
        Tcamera.SetActive(false);
        UnityChanTL = GameObject.Find("UnityChanJoinTL");
        UChan = GameObject.Find("TimeLineunitychan");
        SavePoint.SetActive(false);
        vcam14.SetActive(false);
    }

    void Update()
    {
        if(Tcamera.activeSelf) Cursor.visible = false;//關滑鼠

        if (FightCamera.camera2.activeSelf) Cursor.visible = true;

        if (bUChanTLtrigger)
        {
            if (UChanTLEnd == false)//只進來一次
            {
                UChanTLEnd = true;
                FadeInOut.FadeIn();
                Invoke("UnityChanFadePlay", 2f);
                EXX1.SetActive(false);
                //Cursor.visible = false;//滑鼠游標隱藏
            }
        }
        
        //結束START Timeline
        if (tStartTL.state == PlayState.Paused && bStartTL == true)
        {
            bStartTL = false;
            FadeInOut.FadeIn();
            Invoke("StartEndPlay", 2f);
            pp1.SetActive(false);
            pp2.SetActive(false);
        }

        //結束UnityChan的Timeline
        if (tUnityChanTL.state == PlayState.Paused && bUnityChanTL == true)
        {
            bUnityChanTL = false;
            UChan.SetActive(false);//UnityChan消失
            FadeInOut.FadeIn();
            Invoke("UnityChanEndPlay", 2f);
            EXX2.SetActive(true);//驚嘆號2出現

            hEXX2.SetActive(true);
        }

        
        if (Input.GetKey(KeyCode.Z))//結束START Timeline快速鍵
        {
            bStartTL = false;
            FadeInOut.FadeIn();
            Invoke("StartEndPlay", 2f);
        }
        if (Input.GetKey(KeyCode.X))//結束UnityChan的Timeline 快速鍵
        {
            bUnityChanTL = false;
            UChan.SetActive(false);//UnityChan消失
            FadeInOut.FadeIn();
            Invoke("UnityChanEndPlay", 2f);
            EXX2.SetActive(true);//驚嘆號2出現
            hEXX2.SetActive(true);
        }
        if (Input.GetKey(KeyCode.C))//結束GATEDOOR的Timeline 快速鍵
        {
            bGateTL = false;
            FadeInOut.FadeIn();
            Invoke("GateEndPlay", 2f);
        }

        if (bGateOpen)
        {
            bGateOpen = false;
            FadeInOut.FadeIn();
            Invoke("GateOpen", 2);
            MenuClick.MainMenuButton.SetActive(false);
            MenuClick.MiniMap.SetActive(false);
            UChan.SetActive(true);
            Sounds.Instance().PlayNowMusic("GateOpen");
            //Cursor.visible = false;//滑鼠游標隱藏
        }

        if (GateTL.state == PlayState.Paused && bGateTL)
        {
            bGateTL = false;
            FadeInOut.FadeIn();
            Invoke("GateEndPlay", 2f);
        }

        if(bBossGO)
        {
            bBossGO = false;
            FadeInOut.FadeIn();
            Invoke("BossGo", 2);
            MenuClick.MainMenuButton.SetActive(false);
            MenuClick.MiniMap.SetActive(false);
            UChan.SetActive(true);

            Sounds.Instance().PlayNowMusic("BossAppear");
            //Cursor.visible = false;//滑鼠游標隱藏
            //Sounds.Instance().PlayNowMusic("GateOpen");Come some Music!!!!!!!!!
        }
        if(BossTL.state == PlayState.Paused && bBossTL)
        {
            bBossTL = false;
            FadeInOut.FadeIn();
            Invoke("BossEndPlay", 2f);
            vcam9.SetActive(false);
            vcam13.SetActive(false);
            vcam10.SetActive(false);
            vcam11.SetActive(false);
            vcam12.SetActive(false);
        }
        //if (bGameOver)
        if(Input.GetKey(KeyCode.N) || TotalStatic.BossFightOver)
        {
            Sounds.Instance().StopMusic();
            bGameOver = false;
            FadeInOut.FadeIn();
            Invoke("GameOver88", 2);
            UChan.SetActive(true);
            vcam14.SetActive(true);
            MenuClick.MainMenuButton.SetActive(false);
            MenuClick.MiniMap.SetActive(false);
            //Cursor.visible = false;//滑鼠游標隱藏
        }
    }

    public void StartGame()//遊戲開頭點擊START
    {
        FadeInOut.FadeIn();
        Invoke("StartFadePlay", 2f);
        //Cursor.visible = true;//滑鼠游標顯示
        //Cursor.visible = false;//滑鼠游標隱藏
    }

    void StartFadePlay() //開始的TimeLine
    {
        Sounds.Instance().PlayNowMusic("FindUnity");
        StartImage.SetActive(false);//開始選單消失
        FadeInOut.FadeOut();
        Tcamera.SetActive(true);
        Mcamera.enabled = false;
        tStartTL.Play();
        bStartTL = true;   //進入timeline開關
    }
    void UnityChanFadePlay() //UnityChan的TimeLine
    {
        FadeInOut.FadeOut();
        Tcamera.SetActive(true);
        Mcamera.enabled = false;
        tUnityChanTL.Play();
        bUnityChanTL = true;   //進入timeline開關
        NPCtest.Instance().MissionP.SetActive(false);
        NPCtest.Instance().MissionT.SetActive(false);
        NPCtest.Instance().MTip.SetActive(false);
    }

    void StartEndPlay() //開始的TL 結束
    {
        FadeInOut.FadeOut();
        Tcamera.SetActive(false);
        Mcamera.enabled = true;
        MenuClick.MainMenuButton.SetActive(true);
        MenuClick.MiniMap.SetActive(true);
        Destroy(m1.gameObject, 1); //假香菇摧毀
        Destroy(m2.gameObject, 1);
        MenuClick.GetSomething.SetActive(true);
        MenuClick.GetText.GetComponent<Text>().text = "第一章 : 放開那個女孩";
        Invoke("CloseText", 2);
        NPCtest.Instance().MissionP.SetActive(true);
        NPCtest.Instance().MissionT.SetActive(true);
        NPCtest.Instance().MissionT.GetComponent<Text>().text = "任務提示";
        NPCtest.Instance().MTip.SetActive(true);
        NPCtest.Instance().MTip.GetComponent<Text>().text = "前往小地圖標示處";
        
        vcam2.SetActive(false);
        vcam3.SetActive(false);
        vcam4.SetActive(false);
        vcam5.SetActive(false);
        Cursor.visible = true;//滑鼠游標顯示
    }
    void UnityChanEndPlay()//UnityChan的TimeLine結束
    {
        FadeInOut.FadeOut();
        Tcamera.SetActive(false);
        Mcamera.enabled = true;
        TotalStatic.MapUI.SetActive(true);//關閉MapUI
        MenuClick.MainMenuButton.SetActive(true);
        MenuClick.MiniMap.SetActive(true);
        TotalStatic.Player2.SetActive(true);//油膩醬加入了隊伍
        MenuClick.GetSomething.SetActive(true);
        MenuClick.GetText.GetComponent<Text>().text = "UnityChan加入了隊伍!!!!";
        NPCtest.Instance().bUnityChanJoined = true; //UnityChan加入隊伍 任務開啓
        Invoke("CloseText", 3);
        vcam1.SetActive(false);
        vcam6.SetActive(false);
        vcam7.SetActive(false);
        Cursor.visible = true;//滑鼠游標顯示
    }
    void CloseText()
    {
        MenuClick.GetSomething.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void GateOpen() //
    {
        FadeInOut.FadeOut();
        Tcamera.SetActive(true);
        Mcamera.enabled = false;
        GateTL.Play();
        bGateTL = true;
        TotalStatic.MapAI.SetActive(false);
    }

    void GateEndPlay()
    {
        FadeInOut.FadeOut();
        Tcamera.SetActive(false);
        Mcamera.enabled = true;
        MenuClick.MainMenuButton.SetActive(true);//包包顯示開關
        MenuClick.MiniMap.SetActive(true);//小地圖顯示開關
        GateDoor.SetActive(true);
        SavePoint.SetActive(true);
        EXX4.SetActive(true);
        UChan.SetActive(false);
        //Sounds.Instance().PlayNowMusic("Main");
        MenuClick.GetSomething.SetActive(true);
        MenuClick.GetText.GetComponent<Text>().text = "第二章 : 奇怪的傳送門";
        Invoke("CloseText", 3);
        NPCtest.Instance().MissionP.SetActive(true);
        NPCtest.Instance().MissionT.SetActive(true);
        NPCtest.Instance().MissionT.GetComponent<Text>().text = "任務提示";
        NPCtest.Instance().MTip.SetActive(true);
        NPCtest.Instance().MTip.GetComponent<Text>().text = "前往奇怪的傳送門";
        TotalStatic.MapAI.SetActive(true);
        Cursor.visible = true;//滑鼠游標顯示
    }

    void BossGo()
    {
        FadeInOut.FadeOut();
        Tcamera.SetActive(true);
        Mcamera.enabled = false;
        BossTL.Play();
        bBossTL = true;
        TotalStatic.MapAI.SetActive(false);
    }
    void BossEndPlay()
    {
        FadeInOut.FadeOut();
        Tcamera.SetActive(false);
        UChan.SetActive(false);
        NewBehaviour.attacking = true;
        NewBehaviour.monster_type = "Boss";
        Cursor.visible = true;//滑鼠游標顯示
    }

    void GameOver88()
    {
        FadeInOut.FadeOut();
        BF.SetActive(false);
        Tcamera.SetActive(true);
        Mcamera.enabled = false;
        GameOverTL.Play();
        //Sounds.Instance().PlayNowMusic("Menu");
    }
}