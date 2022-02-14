using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainFight : MonoBehaviour
{
    /// <summary>
    /// 該script附加在DemoScene中的FightScript上
    /// script說明 : 
    ///     現在是誰的回合的判斷
    ///     還有進入戰鬥時的怪物初始化設定
    ///     進入戰鬥和離開戰鬥的FadeIn FadeOut
    ///     進入戰鬥和離開戰鬥的UI開關
    ///     判斷怪物回合一開始是否會受到持續傷害
    ///     判斷怪物回合一開始是否死亡
    /// </summary>

    /// <summary>
    /// 私有變數
    /// </summary>
    //判斷勝利，判斷失敗，是否戰鬥結束，戰鬥結束的攝影機
    private bool bWin, bLose, bOverFight, bFightOverCamera;
    //計算暫存道具數量
    private int iTemRedWater, iTemBlueWater;
    //戰鬥結束的經驗值，道具，升級後是否學到技能
    private int stage;
    //角色圓圈特效來源
    private UnityEngine.Object PlayerCircle_resources;
    //暫存升等用的dictionary
    private Dictionary<string, int> DTemAbilityData = new Dictionary<string, int>();
    ///使用傷害判斷
    FightCamera Fcamera = new FightCamera();
    TimeTwig timebar = new TimeTwig();

    void Update()
    {
        #region 開啟一次戰鬥開關
        if (TotalStatic.bStartFight)
        {
            TotalStatic.bStartFight = false;//關閉戰鬥初始化開關
            FadeOut();//淡出
            FightStartSet();//戰鬥數值初始化
            TotalStatic.UI_switch = true;//開啟戰鬥結束判定的ui開關
            TotalStatic.Time_switch = true;//時間條開啟

            foreach (var value in TotalStatic.PlayerList)
            {
                value.GetComponent<Animator>().SetTrigger("Ready");//Ready to fight
            }
            Fcamera.resetCamera3();//reset camera3 position
            Fcamera.StartSet();//reset overcube position
        }
        #endregion

        #region 時間條移動
        if (TotalStatic.Time_switch)
        {
            timebar.TimeJudge();//時間條判斷與時間條增加
            foreach (var value in TotalStatic.StateList)
            {
                value.TimeBarAdd();//時間條頭像跟時間對齊
            }
        }
        #endregion

        #region 設定戰鬥回合
        if (!TotalStatic.Time_switch && TimeTwig.now_turn != null && TotalStatic.Turn_switch)
        {
            TotalStatic.Turn_switch = false;
            ///判斷玩家回合和怪物回合的true跟false
            if (TimeTwig.now_turn.Substring(0, TimeTwig.now_turn.Length - 1) == "Character")
            {
                NowTurn(TimeTwig.now_turn);//將現在行動的角色UI介面往左移動，角色腳下顯示光圈
                TotalStatic.PlayerAction_switch = true;//開啟玩家行動開關
                ///設定玩家回合
                TotalStatic.bPlayer_turn = true;
                TotalStatic.bMonster_turn = false;
            }
            else
            {
                TotalStatic.Monster_now = GameObject.Find(TimeTwig.now_turn);
                TotalStatic.MonsterAI_switch = true;//開啟怪物AI判斷
                ///設定怪物回合
                TotalStatic.bPlayer_turn = false;
                TotalStatic.bMonster_turn = true;
            }
        }
        #endregion

        #region 遊戲結束將UI隱藏，然後停留在角色IDEL上，滑鼠點 第一下左鍵可以顯示經驗值，第二~三下可以顯示升等，第三~四下左鍵可以FADEIN FADEOUT後離開戰鬥
        if (bOverFight)
        {
            Destroy(TotalStatic.PlayerCircle, 0f);//關閉角色腳下光環
            TotalStatic.MouseChange_icon.SetActive(false);//關閉角色選擇的icon
            TotalStatic.StateChangeTime.SetActive(false);//關閉戰鬥用UI
            TotalStatic.FightMapUI.SetActive(false);//關閉共用UI

            ///戰鬥失敗的話進入GAMEOVER後按左鍵離開遊戲
            if (bLose)
                if (Input.GetMouseButtonDown(0))
                    Application.Quit();

            if (TotalStatic.BossFight)
                TotalStatic.BossFightOver = true;//判斷BOSS戰勝利

            if (Input.GetMouseButtonDown(0))
            {
                if (!TotalStatic.bGameOverSettlement)
                {
                    TotalStatic.DropItem.SetActive(true);//顯示掉落物品的UI
                    stage++;
                    Settlement();//顯示得到的經驗值
                    if (TotalStatic.lTemItem.Count <= 0)
                        TotalStatic.bGameOverSettlement = true;
                }
                else
                {
                    TotalStatic.GetExp_AfterFight.GetComponent<Text>().text = "";//清空獲得經驗
                    TotalStatic.GetItem_AfterFight.GetComponent<Text>().text = "";//清空獲得物品
                    TotalStatic.DropItem.SetActive(false);//關閉掉落物品的UI
                    if (TotalStatic.bLevelUp)
                    {
                        foreach (var value in TotalStatic.PlayerList)
                        {
                            if (value.GetComponent<CharacterState>().bLevelUp)
                            {
                                Sounds.Instance().PlayLevelup();//播放升級音效
                                value.GetComponent<CharacterState>().bLevelUp = false;
                                TotalStatic.LvUpState.SetActive(true);
                                DTemAbilityData = value.GetComponent<CharacterState>().GetDataFromDictionary("before");
                                GameObject.Find("LvUpName").GetComponent<Text>().text = value.GetComponent<CharacterState>().sName;
                                GameObject.Find("BeforeLevel").GetComponent<Text>().text = "等級由" + FindInDictionary(DTemAbilityData, "Level");
                                GameObject.Find("BeforeMaxHp").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "MaxHp");
                                GameObject.Find("BeforeMaxMp").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "MaxMp");
                                GameObject.Find("BeforeAtk").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "Atk");
                                GameObject.Find("BeforeMatk").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "Matk");
                                GameObject.Find("BeforeDef").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "Def");
                                GameObject.Find("BeforeMdef").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "Mdef");
                                GameObject.Find("BeforeSpeed").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "iSpeed");
                                DTemAbilityData = value.GetComponent<CharacterState>().GetDataFromDictionary("after");
                                GameObject.Find("AfterLevel").GetComponent<Text>().text = "提升為" + FindInDictionary(DTemAbilityData, "Level") + "了";
                                GameObject.Find("AfterMaxHp").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "MaxHp");
                                GameObject.Find("AfterMaxMp").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "MaxMp");
                                GameObject.Find("AfterAtk").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "Atk");
                                GameObject.Find("AfterMatk").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "Matk");
                                GameObject.Find("AfterDef").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "Def");
                                GameObject.Find("AfterMdef").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "Mdef");
                                GameObject.Find("AfterSpeed").GetComponent<Text>().text = FindInDictionary(DTemAbilityData, "iSpeed");

                                if((!MenuClick.Water.activeSelf || !MenuClick.P2Atk2.activeSelf)
                                    && (Int32.Parse(FindInDictionary(DTemAbilityData, "Level")) >= 2))
                                {
                                    if(!MenuClick.Water.activeSelf
                                        && (value.name == "Character1"))
                                    {
                                        TotalStatic.DropItem.SetActive(true);//開啟掉落物品的UI
                                        TotalStatic.GetExp_AfterFight.GetComponent<Text>().text = "AcquireChan習得了 冰錐";
                                        MenuClick.SkillP.SetActive(true);
                                        MenuClick.Water.SetActive(true);
                                        MenuClick.SkillP.SetActive(false);
                                    }
                                    else if (!MenuClick.P2Atk2.activeSelf
                                        && (value.name == "Character2"))
                                    {
                                        TotalStatic.DropItem.SetActive(true);//開啟掉落物品的UI
                                        TotalStatic.GetExp_AfterFight.GetComponent<Text>().text = "UnityChan習得了 旋風腿";
                                        MenuClick.SkillP2.SetActive(true);
                                        MenuClick.P2Atk2.SetActive(true);
                                        MenuClick.SkillP2.SetActive(false);
                                    }
                                }
                                else if ((!MenuClick.Heal.activeSelf || !MenuClick.P2Atk3.activeSelf)
                                    && (Int32.Parse(FindInDictionary(DTemAbilityData, "Level")) >= 3))
                                {
                                    if (!MenuClick.Heal.activeSelf
                                        && (value.name == "Character1"))
                                    {
                                        TotalStatic.DropItem.SetActive(true);//開啟掉落物品的UI
                                        TotalStatic.GetExp_AfterFight.GetComponent<Text>().text = "AcquireChan習得了 治癒";
                                        MenuClick.SkillP.SetActive(true);
                                        MenuClick.Heal.SetActive(true);
                                        MenuClick.SkillP.SetActive(false);
                                    }
                                    else if (!MenuClick.P2Atk3.activeSelf
                                        && (value.name == "Character2"))
                                    {
                                        TotalStatic.DropItem.SetActive(true);//開啟掉落物品的UI
                                        TotalStatic.GetExp_AfterFight.GetComponent<Text>().text = "UnityChan習得了 升龍拳";
                                        MenuClick.SkillP2.SetActive(true);
                                        MenuClick.P2Atk3.SetActive(true);
                                        MenuClick.SkillP2.SetActive(false);
                                    }
                                }
                                return;
                            }
                            else
                            {
                                TotalStatic.iLvUpOver++;
                                TotalStatic.LvUpState.SetActive(false);
                            }
                            if (TotalStatic.iLvUpOver == TotalStatic.PlayerList.Count)
                                TotalStatic.bLevelUp = false;
                        }
                    }
                    ///離開戰鬥
                    if (!TotalStatic.bLevelUp)
                    {
                        bOverFight = false;
                        TotalStatic.bFight = false;//進入戰鬥設為false
                        TotalStatic.iTemExp = 0;//暫存的經驗值歸0
                        iTemRedWater = 0;//清空暫存紅水
                        iTemBlueWater = 0;//清空暫存藍水
                        bFightOverCamera = false;//結束攝影機環繞
                        game_over();
                        foreach (var value in TotalStatic.PlayerList)
                        {
                            value.GetComponent<Animator>().SetTrigger("Exit");
                        }
                        FightCamera.camera3.SetActive(false);//關閉環繞攝影機
                        Sounds.Instance().PlayNowMusic("Main");//停止目前音樂
                        TotalStatic.Player1.transform.position = new Vector3(
                            50f,
                            50f,
                            50f);
                        TotalStatic.Player2.transform.position = new Vector3(
                            50f,
                            50f,
                            50f);
                    }
                }
            }
        }
        #endregion
    }

    private void LateUpdate()
    {
        #region 戰鬥結束的判斷(怪物血量都小於等於0 or 玩家血量都小於等於0)
        if (TotalStatic.bOverFight)
        {
            if (TotalStatic.bFightResult == "win")
            {
                Invoke("FightOverDelay", 1);//延遲2秒後進入戰鬥攝影機環繞
                GameOverBeforeCamera("win");
                Sounds.Instance().PlayFightWinMusic();//播放勝利音效
            }
            else if(TotalStatic.bFightResult == "lose")
            {
                Invoke("FightOverDelay", 1);//延遲2秒後進入戰鬥攝影機環繞
                GameOverBeforeCamera("lose");
                Sounds.Instance().PlayFightFailMusic();//播放勝利音效
            }
            TotalStatic.bOverFight = false;//戰鬥結束
            TotalStatic.bFightResult = "";
        }
        #endregion

        #region 攝影機環繞前延遲1秒
        if (bFightOverCamera)
        {
            StartCoroutine(CameraDelay(1));
        }
        #endregion
    }

    #region 戰鬥資料初始化
    void FightStartSet()
    {
        ///清空List
        TotalStatic.PlayerList.Clear();
        TotalStatic.MonsterList.Clear();
        TotalStatic.StateList.Clear();
        ///隱藏時間表頭像
        TotalStatic.Character1_TimeBar.GetComponent<Image>().enabled = false;//隱藏角色1時間表頭像
        TotalStatic.Character1_TimeBar.transform.GetChild(0).GetComponent<Image>().enabled = false;//隱藏角色1時間表頭像的移動點
        TotalStatic.Character2_TimeBar.GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像
        TotalStatic.Character2_TimeBar.transform.GetChild(0).GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像的移動點
        TotalStatic.Monster1_TimeBar.GetComponent<Image>().enabled = false;//隱藏角色1時間表頭像
        TotalStatic.Monster1_TimeBar.transform.GetChild(0).GetComponent<Image>().enabled = false;//隱藏角色1時間表頭像的移動點
        TotalStatic.Monster2_TimeBar.GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像
        TotalStatic.Monster2_TimeBar.transform.GetChild(0).GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像的移動點
        TotalStatic.Monster3_TimeBar.GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像
        TotalStatic.Monster3_TimeBar.transform.GetChild(0).GetComponent<Image>().enabled = false;//隱藏角色2時間表頭像的移動點
        ///取得角色1角色2怪物1怪物2的資料
        GameObject character1 = GameObject.Find("Character1");
        GameObject character2 = GameObject.Find("Character2");
        GameObject monster1 = GameObject.Find("Monster1");
        GameObject monster2 = GameObject.Find("Monster2");
        GameObject monster3 = GameObject.Find("Monster3");
        ///如果角色和怪物存在就放入List
        if (character1)
            TotalStatic.PlayerList.Add(character1);
        if (character2)
            TotalStatic.PlayerList.Add(character2);
        if (monster1)
            TotalStatic.MonsterList.Add(monster1);
        if (monster2)
            TotalStatic.MonsterList.Add(monster2);
        if (monster3)
            TotalStatic.MonsterList.Add(monster3);
        ///將存在的怪物狀態放入StateList
        foreach (var value in TotalStatic.MonsterList)
        {
            GameObject.Find(value.name + "_TimeBar").GetComponent<Image>().enabled = true;//列表有誰就顯示誰的時間表頭像
            GameObject.Find(value.name + "_TimeBar").GetComponent<Image>().sprite = Resources.Load<Sprite>(value.tag);//根據tag切換時間條頭像
            GameObject.Find(value.name + "_TimeBar").transform.GetChild(0).GetComponent<Image>().enabled = true;//顯示時間表頭像的移動點
            TotalStatic.StateList.Add(value.GetComponent<CharacterState>());//怪物加入狀態列表
        }
        ///將存在的角色放入PlayerList，將狀態放入StateList
        foreach (var value in TotalStatic.PlayerList)
        {
            value.GetComponent<CharacterState>().objState.SetActive(true);//顯示狀態欄
            GameObject.Find(value.name + "_TimeBar").GetComponent<Image>().enabled = true;//列表有誰就顯示誰的時間表頭像
            GameObject.Find(value.name + "_TimeBar").transform.GetChild(0).GetComponent<Image>().enabled = true;//顯示時間表頭像的移動點
            TotalStatic.StateList.Add(value.GetComponent<CharacterState>());//角色加入狀態列表
            Blood.BloodVariety(value);//血魔初始化和血魔顯示判斷
            value.transform.LookAt(TotalStatic.StateList[0].transform);//角色看向第一個怪物的位置
            Quaternion q = value.transform.rotation;
            value.transform.localRotation = new Quaternion(0, q.y, q.z, q.w);
        }
        ///戰鬥數值初始化
        foreach (var value in TotalStatic.StateList)
        {
            value.FightSet();
        }
        foreach (var value in TotalStatic.MonsterList)
        {
            value.transform.LookAt(GameObject.Find("Character1").transform.position);
            Quaternion q = value.transform.rotation;
            value.transform.localRotation = new Quaternion(0, q.y, q.z, q.w);
        }
        ///將玩家與目標的按鈕開啟
        MenuClick.TargetP.SetActive(true);
        MenuClick.Target1.SetActive(true);
        if(GameObject.Find("Monster2"))
            MenuClick.Target2.SetActive(true);
        else
            MenuClick.Target2.SetActive(false);
        if (GameObject.Find("Monster3"))
            MenuClick.Target3.SetActive(true);
        else
            MenuClick.Target3.SetActive(false);
        MenuClick.TargetP.SetActive(false);

        MenuClick.PlayerP.SetActive(true);
        MenuClick.Player1.SetActive(true);
        if(GameObject.Find("Character2"))
            MenuClick.Player2.SetActive(true);
        else
            MenuClick.Player2.SetActive(false);
        MenuClick.PlayerP.SetActive(false);

        FightCamera.camera3.SetActive(false);//關閉環繞攝影機
        TotalStatic.Turn_switch = true;//判斷回合開關開啟
        TotalStatic.bGameOverSettlement = false;//戰鬥結算關閉
        TotalStatic.iLvUpOver = 0;//升等數量設為0
        stage = 0;//掉落經驗值物品升等技能判斷，初始化
    }
    #endregion

    #region 畫面淡入淡出
    void FadeIn()
    {
        FadeInOut.FadeIn();
    }
    void FadeOut()
    {
        FadeInOut.FadeOut();
    }
    #endregion

    #region 輪到誰，誰的UI就會往左移，然後腳下會顯示光圈
    void NowTurn(string now_character)
    {
        PlayerCircle_resources = Resources.Load("CharacterCircle");
        TotalStatic.PlayerCircle = GameObject.Instantiate(PlayerCircle_resources) as GameObject;
        TotalStatic.PlayerCircle.transform.SetParent(TotalStatic.UI_canvas.transform);

        if (now_character == "Character1")
        {
            TotalStatic.State1_position.x -= 100;
            TotalStatic.State1.transform.position = TotalStatic.State1_position;
        }
        else if (now_character == "Character2")
        {
            TotalStatic.State2_position.x -= 100;
            TotalStatic.State2.transform.position = TotalStatic.State2_position;
        }

        TotalStatic.PlayerCircle.transform.position = new Vector3(
            GameObject.Find(now_character).transform.position.x,
            GameObject.Find(now_character).transform.position.y + 0.07f,
            GameObject.Find(now_character).transform.position.z);
    }
    #endregion

    #region 戰鬥結束後延遲，要讓動作播完
    void FightOverDelay()
    {
        bOverFight = true;
        bFightOverCamera = true;
    }
    #endregion

    #region 戰鬥結束
    void game_over()
    {
        FadeIn();
        Invoke("FadeOut", 2f);
        Invoke("GameOverAfterCamera", 2f);
    }
    #endregion

    #region 戰鬥結束後攝影機前判斷
    void GameOverBeforeCamera(string result)
    {
        TotalStatic.UI_switch = false;

        ///時間條回到原位
        foreach (var value in TotalStatic.StateList)
        {
            value.TimeBackOrigin();
        }

        if (result == "win")
            bWin = true;
        else if (result == "lose")
            bLose = true;
    }
    #endregion

    #region 戰鬥結束後攝影機後判斷
    void GameOverAfterCamera()
    {
        TotalStatic.objCamera1.SetActive(true);
        TimeLineM.bUChanTLtrigger = true;
        if (NPCtest.Instance().bUnityChanJoined == true)//第一場戰鬥結束不開啓
        {
            TotalStatic.MapAI.SetActive(true);//戰鬥結束開啟地圖AI
            MenuClick.MiniMap.SetActive(true);//戰鬥結束後開啟小地圖
            TotalStatic.MapUI.SetActive(true);//開啓MapUI
        }  
    }
    #endregion

    #region 攝影機環繞前的延遲
    IEnumerator CameraDelay(float time)
    {
        yield return new WaitForSeconds(time);
        FightCamera.camera2.SetActive(false);
        FightCamera.camera3.SetActive(true);
        Fcamera.FightOver();
        if (bWin) Win();//播放勝利動作
        if (bLose) Invoke("Lose", 2f);//延遲2秒進入戰鬥失敗
    }
    #endregion

    #region 戰鬥結束後經驗值結算和顯示掉落物品和升級技能
    void Settlement()
    {
        if (stage == 1)
        {
            foreach (var value in TotalStatic.PlayerList)
            {
                value.GetComponent<CharacterState>().Settlement(TotalStatic.iTemExp);
            }
            TotalStatic.GetExp_AfterFight.GetComponent<Text>().text = "獲得經驗值 : " + (TotalStatic.iTemExp).ToString();
        }
        if (stage == 2)
        {
            TotalStatic.GetExp_AfterFight.GetComponent<Text>().text = "";

            if (TotalStatic.lTemItem.Count > 0)
            {
                foreach (var value in TotalStatic.lTemItem)
                {
                    switch (value)
                    {
                        case "RedWater":
                            TotalStatic.iRedWater_item++;
                            iTemRedWater++;
                            break;
                        case "BlueWater":
                            TotalStatic.iBlueWater_item++;
                            iTemBlueWater++;
                            break;
                        default:
                            break;
                    }
                }
                TotalStatic.GetItem_AfterFight.GetComponent<Text>().text += "獲得道具 : ";
                if (iTemRedWater > 0)
                    TotalStatic.GetItem_AfterFight.GetComponent<Text>().text += "\n藥草 : " + iTemRedWater + "個";
                if (iTemBlueWater > 0)
                    TotalStatic.GetItem_AfterFight.GetComponent<Text>().text += "\n魔力水滴 : " + iTemBlueWater + "個";
            }
            TotalStatic.lTemItem.Clear();//清空暫存物品
        }
    }
    #endregion

    #region 尋找dictionary中的資料
    private string FindInDictionary(Dictionary<string, int> book, string key)
    {
        if (book.ContainsKey(key))
            return book[key].ToString();
        else
            return "0";
    }
    #endregion

    #region 勝利動作
    void Win()
    {
        bWin = false;
        foreach (var value in TotalStatic.PlayerList)
        {
            value.GetComponent<Animator>().SetTrigger("Win");
        }
    }
    #endregion

    #region 戰鬥失敗
    void Lose()
    {
        FadeInOut.GameOverFadeIn();
    }
    #endregion
}