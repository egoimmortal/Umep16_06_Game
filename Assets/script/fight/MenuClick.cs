using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MenuClick : MonoBehaviour
{
    /// <summary>
    /// 該script附加在DemoScene中的FightScript上
    /// script說明 : 
    /// </summary>

    /// <summary>
    /// 公有變數
    /// </summary>
    public static GameObject Fight_menu, SkillP, Fire, Water, Heal, Lightning,
        SkillP2, P2Atk1, P2Atk2, P2Atk3,
        ItemP, RedWater, BlueWater, Revive,
        TargetP, Target1, Target2, Target3,
        PlayerP, Player1, Player2,
        Target_Monster, Now_Character,
        introduction,
        MiniMap, MainMenuButton;
    public static string action_type, mapAction_type;
    //掉落經驗值與物品和遊戲提示
    static public GameObject GetSomething, GetText, mOPSeed;
    /// <summary>
    /// 私有變數
    /// </summary>
    //戰鬥選單
    //private GameObject Fight_menu;
    private GameObject introduction_text, introduction_mp, MIntroduction_text, MIntroduction_mptext,
        fightcamera2;//戰鬥攝影機
    public Camera FightCamera2;//戰鬥攝影機
    //角色現在狀態
    private CharacterState PlayerState_now, ValueState;
    //技能文字顏色
    private Text FireText, WaterText, HealText, LightningText,
        P2Atk1Text, P2Atk2Text, P2Atk3Text;
    //技能開關
    private bool skill_switch;
    //按鈕按下去時選擇的目標
    private string target_change;
    //MapUI的按鈕
    private GameObject MainMenuP, MStatusP, P1stat, P2stat,
                       MSkillP, MP1SName, MSkillP1, MP1Heal, mRedwater, mBluewater, mRevive, 
                       MTargetP, P1target, P2target, MItemP, MIntroductionP,
                       ExitP, Zero, mZero, partical;
    //怪物頭上名字, LV
    public Text M1, M2, M3,LV1 ,LV2;

    public GameObject UUII;
    private int iUI;
    private bool bUIopen;

    void Start()
    {
        #region 將所有ui放入變數
        //fightcamera2 = GameObject.Find("FightCamera2");
        //FightCamera2 = FightCamera.camera2.GetComponent<Camera>();//取得戰鬥攝影機
        Fight_menu = GameObject.Find("P1FightMainPanel");

        SkillP = GameObject.Find("SkillP");
        Fire = GameObject.Find("FireBtn");
        Water = GameObject.Find("WaterBtn");
        Heal = GameObject.Find("HealBtn");
        Lightning = GameObject.Find("LightningBtn");

        SkillP2 = GameObject.Find("SkillP2");
        P2Atk1 = GameObject.Find("P2Atk1");
        P2Atk2 = GameObject.Find("P2Atk2");
        P2Atk3 = GameObject.Find("P2Atk3");

        //放入文字顏色判斷
        FireText = Fire.transform.GetChild(0).gameObject.GetComponent<Text>();
        WaterText = Water.transform.GetChild(0).gameObject.GetComponent<Text>();
        HealText = Heal.transform.GetChild(0).gameObject.GetComponent<Text>();
        LightningText = Lightning.transform.GetChild(0).gameObject.GetComponent<Text>();
        P2Atk1Text = P2Atk1.transform.GetChild(0).gameObject.GetComponent<Text>();
        P2Atk2Text = P2Atk2.transform.GetChild(0).gameObject.GetComponent<Text>();
        P2Atk3Text = P2Atk3.transform.GetChild(0).gameObject.GetComponent<Text>();

        ItemP = GameObject.Find("ItemP");
        RedWater = GameObject.Find("RedwaterBtn");
        BlueWater = GameObject.Find("BluewaterBtn");
        Revive = GameObject.Find("ReviveBtn");

        TargetP = GameObject.Find("TargetP");
        Target1 = GameObject.Find("Target1");
        Target2 = GameObject.Find("Target2");
        Target3 = GameObject.Find("Target3");

        PlayerP = GameObject.Find("PlayerP");
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");

        introduction = GameObject.Find("introduction");//介紹欄位
        introduction_text = GameObject.Find("introduction_text");//介紹欄位文字
        introduction_mp = GameObject.Find("introduction_mp");//介紹MP消耗
        //取得戰鬥用UI
        
        GetSomething = GameObject.Find("GetSomething");//遊戲劇情提示panel
        GetText = GameObject.Find("GetText");//遊戲劇情提示text
        //大地圖按鈕
        MainMenuButton = GameObject.Find("MainMenuButton");
        MainMenuP = GameObject.Find("MainMenuP");
        MiniMap = GameObject.Find("MiniMapImage");
        MStatusP = GameObject.Find("MStatusP");
        MSkillP = GameObject.Find("MSkillP");
        MSkillP1 = GameObject.Find("MSkillP1");
        MP1Heal = GameObject.Find("MP1Heal");
        MTargetP = GameObject.Find("MTargetP");
        MItemP = GameObject.Find("MItemP");
        mRedwater = GameObject.Find("mRedwaterBtn");
        mBluewater = GameObject.Find("mBluewaterBtn");
        mRevive = GameObject.Find("mReviveBtn");
        mOPSeed = GameObject.Find("mOPSeedBtn");
        MIntroductionP = GameObject.Find("MIntroductionP");
        MIntroduction_text = GameObject.Find("MIntroduction_text");
        MIntroduction_mptext = GameObject.Find("MIntroduction_mptext");
        ExitP = GameObject.Find("ExitP");
        Zero = GameObject.Find("Zero");//沒道具時
        mZero = GameObject.Find("mZero");//沒道具時
        P2stat = GameObject.Find("P2stat");//判斷U加入沒
        P2target = GameObject.Find("P2target");
        #endregion

        #region 將按鈕加入LIST
        List<string> btnsName = new List<string>();
        //選擇行動模式
        btnsName.Add("AtkBtn");
        btnsName.Add("SkillBtn");
        btnsName.Add("DefBtn");
        btnsName.Add("ItemBtn");
        //選敵人的目標，要改成怪物名稱
        btnsName.Add("Target1");
        btnsName.Add("Target2");
        btnsName.Add("Target3");
        //選自己的目標，要改成人物名稱
        btnsName.Add("Player1");
        btnsName.Add("Player2");
        //P1技能
        btnsName.Add("FireBtn");
        btnsName.Add("WaterBtn");
        btnsName.Add("HealBtn");
        btnsName.Add("LightningBtn");
        //P2技能
        btnsName.Add("P2Atk1");
        btnsName.Add("P2Atk2");
        btnsName.Add("P2Atk3");
        //道具
        btnsName.Add("RedwaterBtn");
        btnsName.Add("BluewaterBtn");
        btnsName.Add("ReviveBtn");
        //返回的按鈕，為了讓目標頭上的icon或是角色頭上的icon消失
        btnsName.Add("P1_S_Back");
        btnsName.Add("P2_S_Back");
        btnsName.Add("T_Back");
        btnsName.Add("P_Back");
        //MapUI
        btnsName.Add("MainMenuButton");
        btnsName.Add("MStatusBtn");
        btnsName.Add("MSkillBtn");
        btnsName.Add("MItemBtn");
        btnsName.Add("MSystemBtn");
        btnsName.Add("P1stat");//狀態
        btnsName.Add("P2stat");//狀態
        btnsName.Add("MP1SName");//P1技能
        btnsName.Add("MP1Heal");//P1技能補血
        btnsName.Add("P1target");//選擇P1
        btnsName.Add("P2target");//選擇P2
        btnsName.Add("mRedwaterBtn");//
        btnsName.Add("mBluewaterBtn");//
        btnsName.Add("mReviveBtn");//
        btnsName.Add("mOPSeedBtn");//
        btnsName.Add("ExitY");//
        btnsName.Add("ExitN");//
        #endregion

        #region 註冊按鈕事件
        foreach (string btnName in btnsName)
        {
            GameObject btnObj = GameObject.Find(btnName);
            Button btn = btnObj.GetComponent<Button>();
            UIEventListener btnListener = btnObj.gameObject.AddComponent<UIEventListener>();


            ///註冊滑鼠點擊按鈕的事件
            btnListener.OnClick += delegate (GameObject target)
            {
                OnClick(target);//點擊按鈕事件
                TotalStatic.MouseChange_icon.SetActive(false);//隱藏選擇的icon
                Sounds.Instance().PlayMouseClick();//按按鈕的音效

                //選擇敵人的BACK
                if (target.name == "T_Back")
                {
                    TotalStatic.MonsterChange_icon.SetActive(false);//隱藏選擇敵人的icon
                    M1.gameObject.SetActive(false);
                    M2.gameObject.SetActive(false);
                    M3.gameObject.SetActive(false);
                }
                   
                if (target.name == "P_Back")
                    TotalStatic.PlayerChange_icon.SetActive(false);//隱藏選擇玩家的icon

                //如果點下去的按鈕是 Target 或 Player 的話才執行，為了是把按下去的目標存入使用目標內
                if (target.name.Length > 5)
                {
                    if (target.name.Substring(0, 6) == "Target" || target.name.Substring(0, 6) == "Player")
                    {
                        Fight_menu.SetActive(false);//隱藏行動選單
                        SkillP.SetActive(false);//隱藏P1技能選單
                        SkillP2.SetActive(false);//隱藏P2技能選單
                        ItemP.SetActive(false);//隱藏道具選單
                        TargetP.SetActive(false);//隱藏怪物目標選單
                        PlayerP.SetActive(false);//隱藏玩家目標選單
                        TotalStatic.MonsterChange_icon.SetActive(false);//隱藏選擇敵人的icon
                        TotalStatic.PlayerChange_icon.SetActive(false);//隱藏操控角色頭上的icon
                        M1.gameObject.SetActive(false);
                        M2.gameObject.SetActive(false);
                        M3.gameObject.SetActive(false);

                        //判斷按下去的按鈕目標是Monster的還是Player的
                        if (target.name.Substring(0, 6) == "Target")
                            target_change = "Monster" + target.name.Substring(target.name.Length - 1, 1);
                        else
                            target_change = "Character" + target.name.Substring(target.name.Length - 1, 1);

                        Target_Monster = GameObject.Find(target_change);//設定按下去的目標
                        //FightCamera.target_monster = Target_Monster;//目標攝影機設為選擇的目標
                    }
                }
            };
            ///註冊滑鼠進入按鈕的事件
            btnListener.OnMouseEnter += delegate (GameObject target)
            {
                OnMouseEnter(target);//進入按鈕事件
                TotalStatic.MouseChange_icon.SetActive(true);//顯示選擇的icon

                ///<summy>
                ///設定滑鼠進入的地方旁邊的icon
                ///</summy>
                Vector3 t_t_p = target.transform.position;//取得按鈕的位置
                TotalStatic.MouseChange_icon.transform.position = new Vector3(t_t_p.x-70, t_t_p.y, t_t_p.z);//選擇的icon移動到按鈕的位置

                ///判斷如果滑鼠進入選擇目標的按鈕
                if(target.name.Length > 5)
                {
                    if (target.name.Substring(0, 6) == "Target")
                    {
                        TotalStatic.MonsterChange_icon.SetActive(true);//顯示選擇敵人的指標
                        M1.gameObject.SetActive(true);//敵人頭上名字
                        M2.gameObject.SetActive(true);
                        M3.gameObject.SetActive(true);
                        string monster_number = "Monster" + target.name.Substring(target.name.Length - 1, 1);
                        ///取得敵人的位置，然後將選擇敵人的icon放到敵人頭上
                        Vector3 pos = GameObject.Find(monster_number).transform.position;
                        pos.y += 3;
                        Vector3 vPos = FightCamera2.GetComponent<Camera>().WorldToScreenPoint(pos);
                        TotalStatic.MonsterChange_icon.transform.position = vPos;

                    }
                    else if (target.name.Substring(0, 6) == "Player")
                    {
                        TotalStatic.PlayerChange_icon.SetActive(true);//顯示選擇自己人的指標
                        string player_number = "Character" + target.name.Substring(target.name.Length - 1, 1);
                        ///取得角色的位置，然後將選擇角色的icon放到角色頭上
                        Vector3 pos = GameObject.Find(player_number).transform.position;
                        pos.y += 2f;
                        Vector3 vPos = FightCamera2.GetComponent<Camera>().WorldToScreenPoint(pos);
                        TotalStatic.PlayerChange_icon.transform.position = vPos;
                    }
                }
            };
            ///註冊滑鼠離開按鈕的事件
            btnListener.OnMouseExit += delegate (GameObject target)
            {
                introduction.SetActive(false);//關閉說明欄
                MIntroductionP.SetActive(false);
            };
        }
        #endregion

        #region 一開始隱藏選單和icon
        Fight_menu.SetActive(false);//隱藏行動選單
        Water.SetActive(false);//鎖住P1第二招
        Heal.SetActive(false);//鎖住P1第三招
        Lightning.SetActive(false);//鎖住P1第四招
        SkillP.SetActive(false);//隱藏P1的技能選單
        P2Atk2.SetActive(false);//鎖住P1第二招
        P2Atk3.SetActive(false);//鎖住P1第三招
        SkillP2.SetActive(false);//隱藏P2的技能選單
        ItemP.SetActive(false);//隱藏道具的選單
        TargetP.SetActive(false);//隱藏選擇target選單
        PlayerP.SetActive(false);//隱藏選擇player選單
        introduction.SetActive(false);//隱藏說明欄位
        MainMenuP.SetActive(false);//隱藏MapUI選單
        MStatusP.SetActive(false);
        MSkillP.SetActive(false);
        MSkillP1.SetActive(false);
        MTargetP.SetActive(false);
        MItemP.SetActive(false);
        MIntroductionP.SetActive(false);
        MainMenuButton.SetActive(false);//隱藏主UI
        MiniMap.SetActive(false);//隱藏小地圖
        GetSomething.SetActive(false);//隱藏遊戲提示 
        ExitP.SetActive(false);
        //後面才會學到的技能
        Water.SetActive(false);
        P2Atk3.SetActive(false);
        mOPSeed.SetActive(false);//OPseed
        //隱藏怪物名稱
        M1.gameObject.SetActive(false);
        M2.gameObject.SetActive(false);
        M3.gameObject.SetActive(false);
        //隱藏UnityChan
        P2stat.SetActive(false);
        P2target.SetActive(false);
        #endregion
    }

    void Update()
    {
        #region 角色回合
        if (TotalStatic.bPlayer_turn && TimeTwig.now_turn != null && TotalStatic.PlayerAction_switch)
        {
            Now_Character = GameObject.Find(TimeTwig.now_turn);//設定現在的行動者
            PlayerState_now = Now_Character.GetComponent<CharacterState>();
            TotalStatic.PlayerAction_switch = false;//進來角色回合只能觸發一次

            Fight_menu.SetActive(true);//開啟行動選單

            ///如果正在防禦就回到原本的姿勢，增加的防禦回復原本
            if (PlayerState_now.bDefense)
            {
                PlayerState_now.bDefense = false;
                PlayerState_now.Def -= 10;
                PlayerState_now.Mdef -= 10;
                Now_Character.GetComponent<Animator>().SetTrigger("DefToIdle");
            }
        }
        #endregion

        #region 點選技能選單時，設定輪到誰時顯示誰的技能欄位，魔力不夠施放的技能會變成灰色
        if (skill_switch && Now_Character)
        {
            foreach (var value in TotalStatic.PlayerList)
            {
                if (Now_Character == value)
                {
                    ValueState = value.GetComponent<CharacterState>();
                    if (value.name == "Character1")
                    {
                        SkillP.SetActive(true);//顯示角色1技能欄位
                        LightningText.color = Color.white;
                        FireText.color = Color.white;
                        WaterText.color = Color.white;
                        HealText.color = Color.white;

                        if (ValueState.Mp < 10)
                        {
                            HealText.color = Color.gray;
                            WaterText.color = Color.gray;
                            FireText.color = Color.gray;
                            LightningText.color = Color.gray;
                            return;
                        }
                        if (ValueState.Mp < 15)
                        {
                            WaterText.color = Color.gray;
                            FireText.color = Color.gray;
                            LightningText.color = Color.gray;
                            return;
                        }
                        if (ValueState.Mp < 20)
                        {
                            FireText.color = Color.gray;
                            LightningText.color = Color.gray;
                            return;
                        }
                        if (ValueState.Mp < 25)
                        {
                            LightningText.color = Color.gray;
                            return;
                        }
                    }
                    else if (value.name == "Character2")
                    {
                        SkillP2.SetActive(true);//顯示角色2技能欄位
                        P2Atk1Text.color = Color.white;
                        P2Atk2Text.color = Color.white;
                        P2Atk3Text.color = Color.white;

                        if (ValueState.Mp < 10)
                        {
                            P2Atk1Text.color = Color.gray;
                            P2Atk2Text.color = Color.gray;
                            P2Atk3Text.color = Color.gray;
                            return;
                        }
                        if (ValueState.Mp < 20)
                        {
                            P2Atk2Text.color = Color.gray;
                            P2Atk3Text.color = Color.gray;
                            return;
                        }
                        if (ValueState.Mp < 25)
                        {
                            P2Atk3Text.color = Color.gray;
                            return;
                        }
                    }
                }
            }
        }
        else//點選技能欄位關閉的時候會把兩個技能欄位一起關閉
        {
            SkillP.SetActive(false);
            SkillP2.SetActive(false);
        }
        #endregion

        #region 判斷目標是否可以被按
        if (TargetP.activeInHierarchy)
        {
            if (GameObject.Find("Monster1"))
            {
                GameObject MM1 = GameObject.Find("Monster1");
                M1.text = MM1.GetComponent<CharacterState>().sName;
                Vector3 pos = MM1.transform.position;
                pos.y += 2;
                Vector3 vPos = FightCamera2.GetComponent<Camera>().WorldToScreenPoint(pos);
                M1.gameObject.transform.position = vPos;
                Target1.SetActive(true);
                Target1.transform.GetChild(0).GetComponent<Text>().text = MM1.GetComponent<CharacterState>().sName;
            }
            else
            {
                Target1.SetActive(false);
                M1.text = "";
            }
                
            if (GameObject.Find("Monster2"))
            {
                GameObject MM2 = GameObject.Find("Monster2");
                M2.text = MM2.GetComponent<CharacterState>().sName;

                Vector3 pos = MM2.transform.position;
                pos.y += 2;
                Vector3 vPos = FightCamera2.GetComponent<Camera>().WorldToScreenPoint(pos);
                M2.gameObject.transform.position = vPos;
                Target2.SetActive(true);
                Target2.transform.GetChild(0).GetComponent<Text>().text = MM2.GetComponent<CharacterState>().sName;
            }
            else
            {
                Target2.SetActive(false);
                M2.text = "";
            }
               
            if (GameObject.Find("Monster3"))
            {
                GameObject MM3 = GameObject.Find("Monster3");
                M3.text = MM3.GetComponent<CharacterState>().sName;

                Vector3 pos = MM3.transform.position;
                pos.y += 2;
                Vector3 vPos = FightCamera2.GetComponent<Camera>().WorldToScreenPoint(pos);
                M3.gameObject.transform.position = vPos;
                Target3.SetActive(true);
                Target3.transform.GetChild(0).GetComponent<Text>().text = MM3.GetComponent<CharacterState>().sName;
            }
            else
            {
                Target3.SetActive(false);
                M3.text = "";
            }
                
        }
        #endregion

        #region MapSkill 魔力不夠就變灰色
        if (MSkillP1.activeInHierarchy)
        {
            if(TotalStatic.Player1.GetComponent<CharacterState>().Mp < 10)
            {
                MP1Heal.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.gray;
            }
            else
            {
                MP1Heal.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
            }
        }
        #endregion

        #region 道具數量小於等於0就不會顯示
        if (ItemP.activeInHierarchy)
        {
            if (TotalStatic.iRedWater_item <= 0)
                RedWater.SetActive(false);
            else
                RedWater.SetActive(true);
            if (TotalStatic.iBlueWater_item <= 0)
                BlueWater.SetActive(false);
            else
                BlueWater.SetActive(true);
            if (TotalStatic.iRevive_item <= 0)
                Revive.SetActive(false);
            else
                Revive.SetActive(true);
            if(TotalStatic.iRedWater_item <= 0 && TotalStatic.iBlueWater_item <= 0 && TotalStatic.iRevive_item <= 0)
                Zero.SetActive(true);
            else
                Zero.SetActive(false);
        }
        
        #endregion

        #region Map道具數量小於等於0就不會顯示
        if (MItemP.activeInHierarchy)
        {
            if (TotalStatic.iRedWater_item <= 0) mRedwater.SetActive(false);
            else mRedwater.SetActive(true);

            if (TotalStatic.iBlueWater_item <= 0) mBluewater.SetActive(false);
            else mBluewater.SetActive(true);

            if (TotalStatic.iRevive_item <= 0) mRevive.SetActive(false);
            else mRevive.SetActive(true);

            if (TotalStatic.iRedWater_item <= 0 && TotalStatic.iBlueWater_item <= 0 && TotalStatic.iRevive_item <= 0)
                mZero.SetActive(true);
            else
                mZero.SetActive(false);
        }
        #endregion
        
        #region 判斷按下補血技能或道具跟復活道具的差別
        if (PlayerP.activeInHierarchy)
        {
            foreach (var value in TotalStatic.PlayerList)
            {
                if (action_type == "ReviveItem")//復活道具
                {
                    if (value.GetComponent<CharacterState>().Hp <= 0)
                    {
                        if (value.name == "Character1")
                            Player1.SetActive(true);
                        if (value.name == "Character2")
                            Player2.SetActive(true);
                    }
                    else
                    {
                        if (value.name == "Character1")
                            Player1.SetActive(false);
                        if (value.name == "Character2")
                            Player2.SetActive(false);
                    }
                }
                else//補血補魔道具
                {
                    if (value.GetComponent<CharacterState>().Hp > 0)
                    {
                        if (value.name == "Character1")
                            Player1.SetActive(true);
                        if (value.name == "Character2")
                            Player2.SetActive(true);
                    }
                    else
                    {
                        if (value.name == "Character1")
                            Player1.SetActive(false);
                        if (value.name == "Character2")
                            Player2.SetActive(false);
                    }
                }
            }
        }
        #endregion

        #region icon持續旋轉
        if (TotalStatic.MouseChange_icon)
            IconRotate(TotalStatic.MouseChange_icon, "Horizontal");
        if(TotalStatic.PlayerChange_icon)
            IconRotate(TotalStatic.PlayerChange_icon, "Vertical");
        if (TotalStatic.MonsterChange_icon)
            IconRotate(TotalStatic.MonsterChange_icon, "Vertical");
        #endregion
    }

    #region 滑鼠進入按鈕的事件
    public void OnMouseEnter(GameObject target)
    {
        switch (target.name)
        {
            case "AtkBtn":
                break;

            case "SkillBtn":
                break;

            case "DefBtn":
                break;

            case "ItemBtn":
                break;

            case "FireBtn":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "招喚火焰，攻擊單一敵人。";
                introduction_mp.GetComponent<Text>().text = "消耗MP  20";
                break;

            case "WaterBtn":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "招喚冰椎，攻擊單一敵人。";
                introduction_mp.GetComponent<Text>().text = "消耗MP  15";
                break;

            case "HealBtn":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "治療我方一名隊友。";
                introduction_mp.GetComponent<Text>().text = "消耗MP  15";
                break;
            case "LightningBtn":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "利用閃電之力攻擊敵方全體。";
                introduction_mp.GetComponent<Text>().text = "消耗MP  25";
                break;
            case "P2Atk1":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "其實只是看球賽的時候學的鏟球";
                introduction_mp.GetComponent<Text>().text = "消耗MP  10";
                break;

            case "P2Atk2":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "邊玩快打旋風邊踢隔壁朋友練成的踢技";
                introduction_mp.GetComponent<Text>().text = "消耗MP  15";
                break;

            case "P2Atk3":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "邊玩快打旋風邊毆打隔壁朋友練成的絕世武功";
                introduction_mp.GetComponent<Text>().text = "消耗MP  20";
                break;

            case "RedwaterBtn":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "回復我方隊友小量HP";
                introduction_mp.GetComponent<Text>().text = "數量 " + TotalStatic.iRedWater_item;
                break;

            case "BluewaterBtn":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "回復我方隊友小量MP";
                introduction_mp.GetComponent<Text>().text = "數量 " + TotalStatic.iBlueWater_item;
                break;

            case "ReviveBtn":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "復活我方一名隊友";
                introduction_mp.GetComponent<Text>().text = "數量 " + TotalStatic.iRevive_item;
                break;

            case "Target1":
                introduction.SetActive(true);
                //if(何種怪物)
                string m1 = GameObject.Find("Monster1").GetComponent<CharacterState>().sName;
                if (m1 == "蘑菇")
                    introduction_text.GetComponent<Text>().text = "路邊隨處可見的香菇怪，吃了也不會多一命。";
                else if (m1 == "小火龍")
                    introduction_text.GetComponent<Text>().text = "小火龍不是寶可夢的小火龍。";
                else if (m1 == "骷髏將軍")
                    introduction_text.GetComponent<Text>().text = "很殘暴。";

                introduction_mp.GetComponent<Text>().text = "";
                break;

            case "Target2":
                introduction.SetActive(true);
                //if(何種怪物)
                string m2 = GameObject.Find("Monster2").GetComponent<CharacterState>().sName;
                if (m2 == "蘑菇")
                    introduction_text.GetComponent<Text>().text = "路邊隨處可見的香菇怪，吃了也不會多一命。";
                else if (m2 == "小火龍")
                    introduction_text.GetComponent<Text>().text = "小火龍不是寶可夢的小火龍。";
                else if (m2 == "骷髏將軍")
                    introduction_text.GetComponent<Text>().text = "很殘暴。";
                introduction_mp.GetComponent<Text>().text = "";
                break;
            case "Target3":
                introduction.SetActive(true);
                //if(何種怪物)
                string m3 = GameObject.Find("Monster3").GetComponent<CharacterState>().sName;
                if (m3 == "蘑菇")
                    introduction_text.GetComponent<Text>().text = "路邊隨處可見的香菇怪，吃了也不會多一命。";
                else if (m3 == "小火龍")
                    introduction_text.GetComponent<Text>().text = "小火龍不是寶可夢的小火龍。";
                else if (m3 == "骷髏將軍")
                    introduction_text.GetComponent<Text>().text = "很殘暴。";
                introduction_mp.GetComponent<Text>().text = "";
                break;

            case "Player1":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "Acquire醬";
                introduction_mp.GetComponent<Text>().text = "";
                break;

            case "Player2":
                introduction.SetActive(true);
                introduction_text.GetComponent<Text>().text = "Unity醬";
                introduction_mp.GetComponent<Text>().text = "";
                break;
            //MapUI
            case "MP1Heal":
                MIntroductionP.SetActive(true);
                MIntroduction_text.GetComponent<Text>().text = "治療我方一名隊友";
                MIntroduction_mptext.GetComponent<Text>().text = "消耗MP  15";
                break;
            case "mRedwaterBtn":
                MIntroductionP.SetActive(true);
                MIntroduction_text.GetComponent<Text>().text = "回復我方隊友小量HP";
                MIntroduction_mptext.GetComponent<Text>().text = "數量" + TotalStatic.iRedWater_item;
                break;
            case "mBluewaterBtn":
                MIntroductionP.SetActive(true);
                MIntroduction_text.GetComponent<Text>().text = "回復我方隊友小量MP";
                MIntroduction_mptext.GetComponent<Text>().text = "數量" + TotalStatic.iBlueWater_item;
                break;
            case "mReviveBtn":
                MIntroductionP.SetActive(true);
                MIntroduction_text.GetComponent<Text>().text = "復活我方一名隊友";
                MIntroduction_mptext.GetComponent<Text>().text = "數量 " + TotalStatic.iRevive_item;
                break;
            case "mOPSeedBtn":
                MIntroductionP.SetActive(true);
                MIntroduction_text.GetComponent<Text>().text = "神秘的果實";
                MIntroduction_mptext.GetComponent<Text>().text = "數量 " + 1;
                break;
            default:
                break;
        }
    }
    #endregion
    #region 滑鼠點擊按鈕的事件
    public void OnClick(GameObject target)
    {
        introduction.SetActive(false);
        MIntroductionP.SetActive(false);
        switch (target.name)
        {
            //action
            case "AtkBtn":
                TargetP.SetActive(true);//顯示選擇target選單
                Fight_menu.SetActive(false);//關閉行動選單
                action_type = "Atk";//紀錄行動方式
                break;
            case "DefBtn":
                Fight_menu.SetActive(false);//關閉行動選單
                action_type = "Def";
                break;
            case "SkillBtn":
                skill_switch = true;
                Fight_menu.SetActive(false);//關閉行動選單
                break;
            case "ItemBtn":
                ItemP.SetActive(true);//開啟道具選單
                Fight_menu.SetActive(false);//關閉行動選單
                break;
            //P1_skill
            case "FireBtn":
                PlayerSkillLimit("Character1", "Fire", 20);
                break;
            case "WaterBtn":
                PlayerSkillLimit("Character1", "Water", 15);
                break;
            case "HealBtn":
                PlayerSkillLimit("Character1", "Heal", 10);
                break;
            case "LightningBtn":
                PlayerSkillLimit("Character1", "Lightning", 25);
                break;
            //P2_skill
            case "P2Atk1":
                PlayerSkillLimit("Character2", "P2Atk1", 10);
                break;
            case "P2Atk2":
                PlayerSkillLimit("Character2", "P2Atk2", 20);
                break;
            case "P2Atk3":
                PlayerSkillLimit("Character2", "P2Atk3", 25);
                break;
            //item
            case "RedwaterBtn":
                ItemAmountLimit("RedWater");
                break;
            case "BluewaterBtn":
                ItemAmountLimit("BlueWater");
                break;
            case "ReviveBtn":
                ItemAmountLimit("ReviveItem");
                break;
            //back
            case "P1_S_Back":
                skill_switch = false;
                break;
            case "P2_S_Back":
                skill_switch = false;
                break;
            case "T_Back":
                Fight_menu.SetActive(true);//顯示行動選單
                SkillP.SetActive(false);
                SkillP2.SetActive(false);
                ItemP.SetActive(false);
                TargetP.SetActive(false);
                PlayerP.SetActive(false);
                break;
            case "P_Back":
                Fight_menu.SetActive(true);//顯示行動選單
                SkillP.SetActive(false);
                SkillP2.SetActive(false);
                ItemP.SetActive(false);
                TargetP.SetActive(false);
                PlayerP.SetActive(false);
                break;
            //MapUI
            case "MainMenuButton":
                //UIOpen();
                bool b_menu = MainMenuP.activeSelf;
                if (b_menu == false)
                {
                    MainMenuP.SetActive(true);
                    LV1.GetComponent<Text>().text = "Lv " + TotalStatic.Player1.GetComponent<CharacterState>().Level;
                    LV2.GetComponent<Text>().text = "Lv " + TotalStatic.Player2.GetComponent<CharacterState>().Level;
                    TotalStatic.FightMapUI.SetActive(true);
                    TotalStatic.State1.SetActive(true);
                    bUIopen = true;////////////////////////////
                    TotalStatic.bCameraMove = false;
                    Blood.BloodVariety(TotalStatic.Player1);
                    if (TotalStatic.Player2.activeSelf)
                    {
                        TotalStatic.State2.SetActive(true);
                        Blood.BloodVariety(TotalStatic.Player2);
                        P2stat.SetActive(true);
                        P2target.SetActive(true);
                    } 
                }  
                else
                {
                    MainMenuP.SetActive(false);

                    TotalStatic.FightMapUI.SetActive(false);
                    TotalStatic.State1.SetActive(false);
                    TotalStatic.State2.SetActive(false);
                    bUIopen = false;
                    TotalStatic.bCameraMove = true;//////////////////////////
                }
                MStatusP.SetActive(false);
                MSkillP.SetActive(false);
                MSkillP1.SetActive(false);
                MItemP.SetActive(false);
                MTargetP.SetActive(false);
                MIntroductionP.SetActive(false);
                break;
            case "MStatusBtn":
                MainMenuP.SetActive(false);
                MStatusP.SetActive(true);
                break;
            case "MSkillBtn":
                MSkillP.SetActive(true);
                MainMenuP.SetActive(false);
                break;
            case "MItemBtn":
                MItemP.SetActive(true);
                MainMenuP.SetActive(false);
                break;
            case "MSystemBtn":
                GetSomething.SetActive(true);
                GetText.GetComponent<Text>().text = "真的要離開嗎?!";
                ExitP.SetActive(true);
                break;
            case "ExitY"://y
                Application.Quit();
                break;
            case "ExitN"://n
                GetSomething.SetActive(false);
                ExitP.SetActive(false);
                break;
            case "P1stat"://P1狀態
                MStatusP.SetActive(false);
                break;
            case "P2stat"://P2狀態
                MStatusP.SetActive(false);
                break;
            case "MP1SName"://P1技能
                MSkillP.SetActive(false);
                MSkillP1.SetActive(true);
                break;
            case "MP1Heal"://P1技能補血
                if (TotalStatic.Player1.GetComponent<CharacterState>().Mp >= 15)
                {
                    MTargetP.SetActive(true);
                    mapAction_type = "Heal";
                }
                break;
            case "P1target"://選擇P1
                MTargetP.SetActive(false);
                MSkillP1.SetActive(false);
                MItemP.SetActive(false);
                TotalStatic.bCameraMove = true;
                Invoke("ColseState", 3f);//關閉寫條
                MapItemUse(TotalStatic.Player1);
                break;
            case "P2target"://選擇P2 
                MTargetP.SetActive(false);
                MSkillP1.SetActive(false);
                MItemP.SetActive(false);
                TotalStatic.bCameraMove = true;
                Invoke("ColseState", 3f);//關閉寫條
                MapItemUse(TotalStatic.Player2);

                break;
            case "mRedwaterBtn"://
                MTargetP.SetActive(true);
                mapAction_type = "Red";
                break;
            case "mBluewaterBtn"://
                MTargetP.SetActive(true);
                mapAction_type = "Blue";
                break;
            case "mReviveBtn"://
                break;
            case "mOPSeedBtn"://
                mapAction_type = "QQ";
                MapItemUse(TotalStatic.Player1);
                TotalStatic.bCameraMove = true;
                MItemP.SetActive(false);
                Invoke("ColseState", 2.5f);//關閉寫條
                break;
            default:
                break;
        }
    }
    #endregion

    /// <summary>
    /// 技能限定判斷
    /// </summary>
    void PlayerSkillLimit(string name, string type, int Mplimit)
    {
        foreach (var value in TotalStatic.PlayerList)
        {
            if (value.name == name
                && (value.GetComponent<CharacterState>().Mp >= Mplimit))
            {
                action_type = type;
                skill_switch = false;

                if (type == "Heal")
                    PlayerP.SetActive(true);
                else if(type != "Lightning")
                    TargetP.SetActive(true);

                if (value.name == "Character1")
                    SkillP.SetActive(false);
                else if(value.name == "Character2")
                    SkillP2.SetActive(false);

                return;
            }
        }
    }
    /// <summary>
    /// 道具數量判斷
    /// </summary>
    void ItemAmountLimit(string name)
    {
        switch (name)
        {
            case "RedWater":
                if(TotalStatic.iRedWater_item > 0)
                {
                    PlayerP.SetActive(true);
                    ItemP.SetActive(false);
                    action_type = "RedWater";
                }
                break;
            case "BlueWater":
                if (TotalStatic.iBlueWater_item > 0)
                {
                    PlayerP.SetActive(true);
                    ItemP.SetActive(false);
                    action_type = "BlueWater";
                }
                break;
            case "ReviveItem":
                if (TotalStatic.iRevive_item > 0)
                {
                    PlayerP.SetActive(true);
                    ItemP.SetActive(false);
                    action_type = "ReviveItem";
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 按鈕持續旋轉
    /// </summary>
    /// 
    private void ColseState() //MapUI延遲關閉血條
    {
        TotalStatic.State1.SetActive(false);
        TotalStatic.State2.SetActive(false);
    }

    private void MapItemUse(GameObject go)
    {
        switch(mapAction_type)
        {
            case "Heal":
                if (TotalStatic.Player1.GetComponent<CharacterState>().Mp >= 15)
                {
                    TotalStatic.Player1.GetComponent<CharacterState>().Mp -= 15;
                    go.GetComponent<CharacterState>().Hp += (int)(TotalStatic.Player1.GetComponent<CharacterState>().Matk * 1.6f);//
                    if (go.GetComponent<CharacterState>().Hp > go.GetComponent<CharacterState>().MaxHp)
                        go.GetComponent<CharacterState>().Hp = go.GetComponent<CharacterState>().MaxHp;
                    Blood.BloodVariety(go);
                    TotalStatic.AcqChan.GetComponent<Animator>().SetTrigger("Heal"); //補血動作

                    UnityEngine.Object o = Resources.Load("Heal");//從resources中取得技能
                    partical = GameObject.Instantiate(o) as GameObject;//生成傷害特效
                    partical.transform.position = TotalStatic.AcqChan.transform.position + TotalStatic.AcqChan.transform.up;
                }
                    break;
            case "Red":
                if (TotalStatic.iRedWater_item > 0)
                {
                    TotalStatic.iRedWater_item -= 1;
                    go.GetComponent<CharacterState>().Hp += 50;//?? 波特效
                    if (go.GetComponent<CharacterState>().Hp > go.GetComponent<CharacterState>().MaxHp)
                        go.GetComponent<CharacterState>().Hp = go.GetComponent<CharacterState>().MaxHp;
                    Blood.BloodVariety(go);
                    TotalStatic.AcqChan.GetComponent<Animator>().SetTrigger("Heal"); //補血動作

                    UnityEngine.Object o = Resources.Load("RedWater");//從resources中取得技能
                    partical = GameObject.Instantiate(o) as GameObject;//生成傷害特效
                    partical.transform.position = TotalStatic.AcqChan.transform.position + TotalStatic.AcqChan.transform.up;
                }
                break;
            case "Blue":
                if (TotalStatic.iBlueWater_item > 0)
                {
                    TotalStatic.iBlueWater_item -= 1;
                    go.GetComponent<CharacterState>().Mp += 60;//?? 波特效
                    if (go.GetComponent<CharacterState>().Mp > go.GetComponent<CharacterState>().MaxMp)
                        go.GetComponent<CharacterState>().Mp = go.GetComponent<CharacterState>().MaxMp;
                    Blood.BloodVariety(go);
                    TotalStatic.AcqChan.GetComponent<Animator>().SetTrigger("Heal"); //補血動作

                    UnityEngine.Object o = Resources.Load("BlueWater");//從resources中取得技能
                    partical = GameObject.Instantiate(o) as GameObject;//生成傷害特效
                    partical.transform.position = TotalStatic.AcqChan.transform.position + TotalStatic.AcqChan.transform.up;
                }
                break;
            case "Revive":
                if (TotalStatic.iRevive_item > 0)
                {
                    TotalStatic.AcqChan.GetComponent<Animator>().SetTrigger("Heal"); //補血動作
                }
                break;
            case "QQ":
                if(mOPSeed.activeSelf)
                {     
                    TotalStatic.AcqChan.GetComponent<Animator>().SetTrigger("Heal"); //補血動作
                    UnityEngine.Object o = Resources.Load("Critical");//從resources中取得技能
                    partical = GameObject.Instantiate(o) as GameObject;//生成傷害特效
                    partical.transform.position = TotalStatic.AcqChan.transform.position + TotalStatic.AcqChan.transform.up;
                    ///全能力增加
                    GameObject character1 = GameObject.Find("Character1");
                    GameObject character2 = GameObject.Find("Character2");
                    TotalStatic.PlayerList.Add(character1);
                    TotalStatic.PlayerList.Add(character2);
                    foreach (var value in TotalStatic.PlayerList)
                    {
                        Debug.Log("惡魔果實近來一次");
                        ValueState = value.GetComponent<CharacterState>();
                        if (value.name == "Character1")
                        {
                            ValueState.Exp += 150;
                            ValueState.MaxHp += 55;
                            ValueState.MaxMp += 45;
                            ValueState.Atk += 4;
                            ValueState.Matk += 25;
                            ValueState.Def += 8;
                            ValueState.Mdef += 8;
                        }
                        else
                        {
                            ValueState.Exp += 150;
                            ValueState.MaxHp += 70;
                            ValueState.MaxMp += 35;
                            ValueState.Atk += 25;
                            ValueState.Matk += 4;
                            ValueState.Def += 8;
                            ValueState.Mdef += 8;
                        }
                    }
                    Blood.BloodVariety(TotalStatic.Player1);
                    Blood.BloodVariety(TotalStatic.Player2);
                    TotalStatic.PlayerList.Clear();
                    mOPSeed.SetActive(false);
                    GetSomething.SetActive(true);
                    GetText.GetComponent<Text>().text = "兩人全能力提升！";
                    Invoke("CloseText", 2);
                }
                break;
            default:
                break;
        }
    }

    private void IconRotate(GameObject icon, string mode)
    {
        if (mode == "Horizontal")
            icon.transform.Rotate(-3, 0, 0);
        else
            icon.transform.Rotate(0, -3, 0);
    }
    void CloseText()
    {
        GetSomething.SetActive(false);
    }
}