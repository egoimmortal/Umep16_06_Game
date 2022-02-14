using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalStatic : MonoBehaviour
{
    static public bool bCameraMove = true;//攝影機鎖定開關
    static public GameObject Player1, Player2, AcqChan;//角色1
    //紅水，藍水，復活道具
    static public int iRedWater_item, iBlueWater_item, iRevive_item;
    static public GameObject UI_canvas;//總UI
    static public bool UI_switch;//總UI開關
    /// <summary>
    /// 大地圖
    /// </summary>
    //地圖AI，地圖UI
    static public GameObject MapAI, MapUI;
    static public GameObject objCamera1, objCamera2;//大地圖攝影機
    /// <summary>
    /// 場景切換
    /// </summary>
    static public GameObject BlackFade, GameOver;
    static public Image BlackFade_image, GameOver_image;
    /// <summary>
    /// 物件池
    /// </summary>
    static public GameObject Monster1, Monster2, Monster3;
    /// <summary>
    /// 戰鬥
    /// </summary>
    //判斷是否正在戰鬥，是否開始戰鬥，是否戰鬥結束 是否對話中
    static public bool bFight, bStartFight, bOverFight, bTalking;
    //戰鬥結果
    static public string bFightResult;
    //戰鬥結束後是否有人升等，王的戰鬥判斷，王的戰鬥結束
    static public bool bLevelUp, BossFight, BossFightOver;
    //角色升等結束數量判斷，暫存戰鬥結束的經驗值
    static public int iLvUpOver, iTemExp;
    //戰鬥後掉落的物品
    static public List<string> lTemItem = new List<string>();
    //怪物回合，玩家回合
    static public bool bMonster_turn, bPlayer_turn;
    //回合結束結算
    static public bool bGameOverSettlement;
    //回合開關
    static public bool Turn_switch;
    //現在的行動回合
    static public string Turn_now;
    //時間表開關與行動表開關
    static public bool Time_switch, Action_switch;
    //怪物AI開關
    static public bool MonsterAI_switch;
    //怪物AI 要進行攻擊的怪物，要攻擊的目標
    static public GameObject Monster_now, TargetPlayer;
    //怪物AI 怪物行動模式
    static public string MonsterActionType;
    //玩家行動開關
    static public bool PlayerAction_switch;
    //現在玩家列表
    static public List<GameObject> PlayerList = new List<GameObject>();
    //現在怪物列表
    static public List<GameObject> MonsterList = new List<GameObject>();
    //現在角色的狀態列表
    static public List<CharacterState> StateList = new List<CharacterState>();
    /// <summary>
    /// 戰鬥UI
    /// </summary>
    //升級UI，角色1狀態欄，角色2狀態欄
    static public GameObject LvUpState, State1, State2;
    //玩家圓圈特效，圓圈特效來源
    static public GameObject PlayerCircle;
    //暫存時間表頭像
    static public GameObject Character1_TimeBar, Character2_TimeBar,
        Monster1_TimeBar, Monster2_TimeBar, Monster3_TimeBar;
    //戰鬥用狀態UI，戰鬥和地圖共用UI，掉落物品的panel，掉落經驗值，掉落物品
    static public GameObject StateChangeTime, FightMapUI, DropItem, GetExp_AfterFight, GetItem_AfterFight;
    //滑鼠選擇icon，怪物選擇icon，玩家選擇icon
    static public GameObject MouseChange_icon, MonsterChange_icon, PlayerChange_icon;
    //角色1狀態欄位置，角色1狀態欄初始位置，角色2狀態欄位置，角色2狀態欄初始位置
    static public Vector3 State1_position, State1_origin_position, State2_position, State2_origin_position;
    //玩家行動選單
    static public GameObject FightMenu;

    private void Awake()
    {
        ///取得物件
        ///
        AcqChan = GameObject.Find("AcquireChan");//map角色1
        Player1 = GameObject.Find("Character1");//角色1
        Player2 = GameObject.Find("Character2");//角色2
        objCamera1 = GameObject.Find("Camera");//主攝影機
        objCamera2 = GameObject.Find("FightCamera");
        UI_canvas = GameObject.Find("Canvas");//UI總畫面
        MapAI = GameObject.Find("AI");//地圖怪物的AI
        MapUI = GameObject.Find("MapUI");//地圖UI
        BlackFade = GameObject.Find("BlackFade");//黑畫面
        GameOver = GameObject.Find("GameOver");//角色都死亡的失敗畫面
        LvUpState = GameObject.Find("LvUpState");//升等的UI
        FightMenu = GameObject.Find("P1FightMainPanel");//玩家行動選單
        State1 = GameObject.Find("State1");//角色1狀態欄
        State2 = GameObject.Find("State2");//角色2狀態欄
        Character1_TimeBar = GameObject.Find("Character1_TimeBar");//取得角色1時間表頭像
        Character2_TimeBar = GameObject.Find("Character2_TimeBar");//取得角色2時間表頭像
        Monster1_TimeBar = GameObject.Find("Monster1_TimeBar");//取得怪物1時間表頭像
        Monster2_TimeBar = GameObject.Find("Monster2_TimeBar");//取得怪物2時間表頭像
        Monster3_TimeBar = GameObject.Find("Monster3_TimeBar");//取得怪物2時間表頭像
        MouseChange_icon = GameObject.Find("Mouse_Change");//滑鼠選擇icon
        MonsterChange_icon = GameObject.Find("Monster_Change");//怪物選擇icon
        PlayerChange_icon = GameObject.Find("Character_Change");//玩家選擇icon
        StateChangeTime = GameObject.Find("StateChangeTime");//戰鬥用狀態UI
        FightMapUI = GameObject.Find("FightMapUI");//共用UI
        GetExp_AfterFight = GameObject.Find("GetExp");//戰鬥結束後的取得經驗值欄位
        GetItem_AfterFight = GameObject.Find("GetItem");//戰鬥結束後的取得物品欄位
        DropItem = GameObject.Find("DropItem");//掉落物品的panel
    }

    void Start()
    {
        ///取得image元件
        BlackFade_image = BlackFade.GetComponent<Image>();//黑畫面
        GameOver_image = GameOver.GetComponent<Image>();//失敗畫面image
        ///取得位置
        State1_position = State1.transform.position;//取得現在角色1狀態欄位置
        State1_origin_position = State1.transform.position;//取得初始角色1狀態欄位置
        State2_position = State2.transform.position;//取得現在角色2狀態欄位置
        State2_origin_position = State2.transform.position;//取得初始角色2狀態欄位置
        ///一開始進入遊戲先關閉
        GameOver.GetComponent<Image>().CrossFadeAlpha(0, 0f, false);//關閉失敗畫面
        State1.SetActive(false);//角色1資訊欄隱藏
        State2.SetActive(false);//角色2資訊欄隱藏
        Character1_TimeBar.GetComponent<Image>().enabled = false;//隱藏角色1時間表頭像
        Character1_TimeBar.transform.GetChild(0).GetComponent<Image>().enabled = false;//隱藏角色1時間表頭像的移動點
        Character2_TimeBar.GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像
        Character2_TimeBar.transform.GetChild(0).GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像的移動點
        Monster1_TimeBar.GetComponent<Image>().enabled = false;//隱藏角色1時間表頭像
        Monster1_TimeBar.transform.GetChild(0).GetComponent<Image>().enabled = false;//隱藏角色1時間表頭像的移動點
        Monster2_TimeBar.GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像
        Monster2_TimeBar.transform.GetChild(0).GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像的移動點
        Monster3_TimeBar.GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像
        Monster3_TimeBar.transform.GetChild(0).GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像的移動點
        LvUpState.SetActive(false);
        DropItem.SetActive(false);
        MouseChange_icon.SetActive(false); 
        MonsterChange_icon.SetActive(false);
        PlayerChange_icon.SetActive(false);
        StateChangeTime.SetActive(false);
        FightMapUI.SetActive(false);
        MapAI.SetActive(false);//接完任務 地圖怪物才出現 先關閉
        Player2.SetActive(false);//一開始沒有P2
        ///一進入遊戲就淡出特效
        FadeInOut.FadeOut();
    }
    #region 回合結束的時候角色UI歸位
    public static void TurnOver()
    {
        State1_position = State1_origin_position;
        State2_position = State2_origin_position;
        State1.transform.position = State1_origin_position;
        State2.transform.position = State2_origin_position;
    }
    #endregion
}