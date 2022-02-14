using System;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] private AudioSource _MainSound;
    ///背景音樂
    [SerializeField] private AudioClip _MainMenu = null;//主選單背影音樂
    [SerializeField] private AudioClip _MainMusic = null;//主要背景音樂
    [SerializeField] private AudioClip _FightMusic = null;//戰鬥背景音樂
    [SerializeField] private AudioClip _FightWinMusic = null;//戰鬥勝利背景音樂
    [SerializeField] private AudioClip _FightFailMusic = null;//戰鬥失敗背景音樂
    [SerializeField] private AudioClip _AnimationFindUnityMusic = null;//一開始動畫的背景音樂
    [SerializeField] private AudioClip _GateDoorMusic = null;//傳送門的背景音樂
    [SerializeField] private AudioClip _BossFightMusic = null;//Boss戰鬥音樂
    [SerializeField] private AudioClip _BossAppearMusic = null;//王出現動畫的背景音樂
    [SerializeField] private AudioClip _BossOverMusic = null;//打贏王的背景音樂
    ///一般音效
    [SerializeField] private AudioClip _Run = null;//跑步
    [SerializeField] private AudioClip _LearnSkill = null;//學習技能
    [SerializeField] private AudioClip _OpenBag = null;//打開包包
    [SerializeField] private AudioClip _MouseClick = null;//按下按鈕
    [SerializeField] private AudioClip _NPCHello = null;//NPC
    ///戰鬥音效
    [SerializeField] private AudioClip _MushroomHit = null;//蘑菇頭槌攻擊
    [SerializeField] private AudioClip _DragonSampleHit = null;//小火龍尾巴攻擊
    [SerializeField] private AudioClip _DragonFire = null;//小火龍火焰攻擊
    [SerializeField] private AudioClip _BossSampleHit = null;//骷髏王進戰攻擊
    [SerializeField] private AudioClip _BossWaveHit = null;//骷髏王刀波範圍攻擊
    [SerializeField] private AudioClip _BossJumpHit = null;//骷髏王跳躍範圍攻擊
    [SerializeField] private AudioClip _BossBuff = null;//骷髏王幫自己上BUFF
    [SerializeField] private AudioClip _BossSummon = null;//骷髏王召喚怪物

    [SerializeField] private AudioClip _UnitychanSampleHit = null;//UnityChan普通攻擊
    [SerializeField] private AudioClip _UnitychanNoShadowFeet = null;//UnityChan無影腳
    [SerializeField] private AudioClip _UnitychanWindLeg = null;//UnityChan旋風腿
    [SerializeField] private AudioClip _UnitychanDragonFist = null;//UnityChan昇龍拳
    [SerializeField] private AudioClip _UnitychanJump = null;//UnityChan跳躍

    [SerializeField] private AudioClip _AcquirechanSampleHit = null;//AcquireChan頭槌
    [SerializeField] private AudioClip _AcquirechanFire = null;//AcquireChan火焰
    [SerializeField] private AudioClip _AcquirechanIce = null;//AcquireChan冰錐
    [SerializeField] private AudioClip _AcquirechanHeal = null;//AcquireChan治療
    [SerializeField] private AudioClip _AcquirechanLightning = null;//AcquireChan閃電

    [SerializeField] private AudioClip _UHit = null;//UnityChan普通攻擊
    [SerializeField] private AudioClip _UA1 = null;//UnityChan無影腳
    [SerializeField] private AudioClip _UA2 = null;//UnityChan旋風腿
    [SerializeField] private AudioClip _UA3 = null;//UnityChan昇龍拳

    [SerializeField] private AudioClip _AA1 = null;//AcquireChan頭槌
    [SerializeField] private AudioClip _AFire = null;//AcquireChan火焰
    [SerializeField] private AudioClip _AIce = null;//AcquireChan冰錐
    [SerializeField] private AudioClip _AHeal = null;//AcquireChan治療
    [SerializeField] private AudioClip _ALightning = null;//AcquireChan閃電
    [SerializeField] private AudioClip _ABC = null;//AcquireChan閃電

    [SerializeField] private AudioClip _Redwater = null;//藥草
    [SerializeField] private AudioClip _Bludewater = null;//魔力水滴
    [SerializeField] private AudioClip _Revive = null;//世界樹之葉

    [SerializeField] private AudioClip _DieM = null;//死亡
    [SerializeField] private AudioClip _DieD = null;//死亡
    [SerializeField] private AudioClip _DieB = null;//死亡
    [SerializeField] private AudioClip _DieU = null;//死亡
    [SerializeField] private AudioClip _DieA = null;//死亡
    [SerializeField] private AudioClip _Levelup = null;//升級

    private GameObject Character;
    private AudioSource EffectSound;
    private AudioSource DieSound;

    private static Sounds m_instance;
    public static Sounds Instance()
    {
        return m_instance;
    }

    private void Awake()
    {
        m_instance = this;
        EffectSound = GameObject.Find("SoundsManager").GetComponent<AudioSource>();
        EffectSound.volume = 1.0f;
        _MainSound = GameObject.Find("MainMusic").GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayNowMusic("Menu");
    }
    /// <summary>
    /// 缺少的音樂判斷:
    /// 判斷進入兩段動畫的背景音樂
    /// 離開第一段動畫後開啟的大地圖音樂
    /// 進入BOSS動畫的音樂
    /// 打贏BOSS後動畫的音樂
    /// 接任務的音效
    /// 完成任務的音效
    /// 回報任務的音效
    /// 大地圖UI音效
    /// 跑步音效不知道要不要
    /// </summary>

    #region 背景音樂
    public void PlayNowMusic(string type)
    {
        switch (type)
        {
            case "Menu":
                _MainSound.clip = _MainMenu;
                break;
            case "Main":
                _MainSound.clip = _MainMusic;
                break;
            case "Fight":
                _MainSound.clip = _FightMusic;
                break;
            case "FindUnity":
                _MainSound.clip = _AnimationFindUnityMusic;
                break;
            case "GateOpen":
                _MainSound.clip = _GateDoorMusic;
                break;
            case "BossFight":
                _MainSound.clip = _BossFightMusic;
                break;
            case "BossAppear":
                _MainSound.clip = _BossAppearMusic;
                break;
            case "BossOver":
                _MainSound.clip = _BossOverMusic;
                break;
            default:
                break;
        }
        _MainSound.Play();
    }

    public void PlayFightWinMusic()
    {
        _MainSound.clip = _FightWinMusic;
        _MainSound.Play();
    }

    public void PlayFightFailMusic()
    {
        _MainSound.clip = _FightFailMusic;
        _MainSound.Play();
    }
    #endregion

    #region 一般音效
    public void PlayRun()
    {
        EffectSound.clip = _Run;
        EffectSound.Play();
    }

    public void PlayOpenBag()
    {
        EffectSound.clip = _OpenBag;
        EffectSound.Play();
    }

    public void PlayMouseClick()
    {
        EffectSound.clip = _MouseClick;
        EffectSound.Play();
    }
    public void NPCHello()
    {
        EffectSound.clip = _NPCHello;
        EffectSound.Play();
    }
    
    #endregion

    #region 戰鬥音效
    public void PlayMoveHit(string type)
    {
        switch (type)
        {
            case "Character1":
                EffectSound.clip = _AcquirechanSampleHit;
                break;
            case "Character2":
                EffectSound.clip = _UnitychanSampleHit;
                break;
            case "Mushroom":
                EffectSound.clip = _MushroomHit;
                break;
            case "FireDragon":
                EffectSound.clip = _DragonSampleHit;
                break;
            case "Boss":
                EffectSound.clip = _BossSampleHit;
                break;
            default:
                break;
        }
        
        EffectSound.Play();
    }

    public void PlayNoMove(string type)
    {
        switch (type)
        {
            //Player
            case "Fire":
                EffectSound.clip = _AcquirechanFire;
                break;
            case "Water":
                EffectSound.clip = _AcquirechanIce;
                break;
            case "Heal":
                EffectSound.clip = _AcquirechanHeal;
                break;
            case "Lightning":
                EffectSound.clip = _AcquirechanLightning;
                break;
            case "P2Atk1":
                EffectSound.clip = _UnitychanNoShadowFeet;
                break;
            case "P2Atk2":
                EffectSound.clip = _UnitychanWindLeg;
                break;
            case "P2Atk3":
                EffectSound.clip = _UnitychanDragonFist;
                break;
            //Monster
            case "FireballExplode":
                EffectSound.clip = _DragonFire;
                break;
            case "WaveAtk":
                EffectSound.clip = _BossWaveHit;
                break;
            case "JumpAtk":
                EffectSound.clip = _BossJumpHit;
                break;
            case "Strong":
                EffectSound.clip = _BossBuff;
                break;
            //Item
            case "RedWater":
                EffectSound.clip = _Redwater;
                break;
            case "BlueWater":
                EffectSound.clip = _Bludewater;
                break;
            case "ReviveItem":
                EffectSound.clip = _Revive;
                break;
                
            default:
                break;
        }

        EffectSound.Play();
    }

    public void PlayDie(GameObject Character)
    {
        DieSound = Character.GetComponent<AudioSource>();
        switch (Character.tag)
        {
            case "Mushroom":
                DieSound.clip = _DieM;
                break;
            case "FireDragon":
                DieSound.clip = _DieD;
                break;
            case "Boss":
                DieSound.clip = _DieB;
                break;
            case "Character1":
                DieSound.clip = _DieA;
                break;
            case "Character2":
                DieSound.clip = _DieU;
                break;
            default:
                DieSound.clip = null;
                break;
        }
        DieSound.Play();
    }

    public void PlaySkill(string type)
    {
        switch (type)
        {
            //Player
            case "Fire":
                EffectSound.clip = _AFire;
                break;
            case "Water":
                EffectSound.clip = _AIce;
                break;
            case "Heal":
                EffectSound.clip = _AHeal;
                break;
            case "Lightning":
                EffectSound.clip = _ALightning;
                break;
            case "P2Atk1":
                EffectSound.clip = _UA1;
                break;
            case "P2Atk2":
                EffectSound.clip = _UA2;
                break;
            case "P2Atk3":
                EffectSound.clip = _UA3;
                break;
            //Monster
            case "FireballExplode":
                EffectSound.clip = _ABC;
                break;
            case "WaveAtk":
                EffectSound.clip = _ABC;
                break;
            case "JumpAtk":
                EffectSound.clip = _ABC;
                break;
            case "Strong":
                EffectSound.clip = _ABC;
                break;
            //Item
            case "RedWater":
                EffectSound.clip = _ABC;
                break;
            case "BlueWater":
                EffectSound.clip = _ABC;
                break;
            case "ReviveItem":
                EffectSound.clip = _ABC;
                break;
            /////////////////////
            case "Character1":
                EffectSound.clip = _AA1;
                break;
            case "Character2":
                EffectSound.clip = _UHit;
                break;
            case "Mushroom":
                EffectSound.clip = _ABC;
                break;
            case "FireDragon":
                EffectSound.clip = _ABC;
                break;
            case "Boss":
                EffectSound.clip = _ABC;
                break;
            default:
                break;
        }

        EffectSound.Play();
    }
    public void PlayBossSummon()
    {
        EffectSound.clip = _BossSummon;
        EffectSound.Play();
    }

    public void PlayUnitychanJump()
    {
        EffectSound.clip = _UnitychanJump;
        EffectSound.Play();
    }

    public void PlayLevelup()
    {
        EffectSound.clip = _Levelup;
        EffectSound.Play();
    }
    #endregion

    //停止目前音樂
    public void StopMusic()
    {
        _MainSound.Stop();
    }
    //停止目前音效
    public void StopEffect()
    {
        EffectSound.Stop();
    }
}