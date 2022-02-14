using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToTarget : MonoBehaviour
{
    /// <summary>
    /// 該script附加在DemoScene中的FightScript上
    /// script說明 : 
    ///     執行動作判斷並執行動作
    ///     攻擊動作分移動攻擊和不移動攻擊
    /// </summary>

    /// <summy>
    /// 公有變數
    /// <summy>
    public static GameObject m_Go;          //行動方
    private CharacterState m_GoState;       //行動方狀態
    private Animator m_GoAnimator;          //行動方動作
    public static GameObject m_Target;      //目標
    private CharacterState m_TargetState;   //目標狀態
    public static GameObject partical;      //粒子系統

    public static bool back_move;
    public static string SkillName;         //技能名稱
    public static string damage_result;     //公用傷害結果
    public static int damage;               //公用傷害數值
    public float MoveSpeed = 8;            //移動速度

    public static bool DmgGo;               //新增判斷 跳傷害 播特效 受傷害動畫時間 開關
    /// <summy>
    /// 私有變數
    /// <summy>
    private float DmgTime;                  //新增判斷 跳傷害 播特效 受傷害動畫時間
    private float DistanceToTarget;         //和目標距離
    private float DistanceToBasic;          //和初始位置距離
    private Vector3 m_BasicPosition;        //初始點
    private Vector3 m_TargetPoition;        //目標位置
    private Quaternion m_BasicRotation;     //初始面向
    private bool Ismove;                    //移動去目標
    private bool Isback;                    //移動回原位
    private float AniLength = 0;            //動作長度
    private List<GameObject> TemList = new List<GameObject>();//範圍攻擊的List，存放怪物或角色的List
    private CharacterState ValueState;//暫存List中的狀態
    DmgShow dmg_show = new DmgShow();
    HurtJudge damage_judge = new HurtJudge();

    void Update()
    {
        //VcameraGo();//測試Cinemachine
        #region 攻擊者、目標、攻擊方式設定好，攻擊傷害判定好後執行
        if (EventControl.event_switch)
        {
            EventControl.event_switch = false;//關閉執行一次的開關
            m_GoState = m_Go.GetComponent<CharacterState>();
            m_GoAnimator = m_Go.GetComponent<Animator>();
            m_TargetState = m_Target.GetComponent<CharacterState>();

            //判斷玩家或怪物的回合執行的動作名稱
            if (TotalStatic.bMonster_turn)
            {
                SkillName = TotalStatic.MonsterActionType;

                dmg_show.DOTDmg(m_Go, m_GoState);//跳傷害數字
                HurtJudge.IsDOT(m_GoState);//持續傷害的判斷
                if (m_GoState.Hp <= 0)
                    m_GoState.Hp = 1;
            }
            else
            {
                SkillName = MenuClick.action_type;
                MenuClick.action_type = "";
                if(SkillName != "Atk")
                Sounds.Instance().PlaySkill(SkillName);///////////////////

                if (m_Go.name == "Character2" && SkillName == "Atk")
                    Sounds.Instance().PlaySkill(m_Go.name);///////////////////
            }

            ///將傷害數值與傷害判定放入變數
            damage_result = EventControl.display_damage;
            damage = EventControl.tem_damage;
            m_BasicPosition = m_Go.transform.position;//設定行動者初始位置
            m_BasicRotation = m_Go.transform.rotation;//設定行動者初始轉向
            m_TargetPoition = m_Target.transform.position;//設定目標初始位置
            m_BasicRotation.x = 0;//看前方

            ///不移動攻擊執行NoMove
            if (SkillName == "Fire"
                || SkillName == "Water"
                || SkillName == "Heal"
                || SkillName == "RedWater"
                || SkillName == "BlueWater"
                || SkillName == "ReviveItem"
                || SkillName == "FireballExplode"
                || SkillName == "Strong")
            {
                switch (SkillName)
                {
                    case "Fire":
                        DmgTime = 0.625f;
                        break;
                    case "Water":
                        DmgTime = 1f;
                        break;
                    case "Heal":
                        DmgTime = 0.67f;
                        break;
                    case "RedWater":
                        DmgTime = 0.75f;
                        break;
                    case "BlueWater":
                        DmgTime = 0.75f;
                        break;
                    case "ReviveItem":
                        DmgTime = 0.83f;
                        break;
                    case "FireballExplode":
                        DmgTime = 0.625f;
                        break;
                    case "Strong":
                        DmgTime = 0.75f;
                        break;
                    default:
                        break;
                }
                
                m_Go.transform.LookAt(m_TargetPoition);//行動者看著目標的位置
                m_Go.GetComponent<Animator>().SetTrigger(SkillName);//行動者將技能動作判定打開並執行技能動作
                StartCoroutine(NoMove(DmgTime));//執行不移動function
                StartCoroutine(LookBack());//同時判斷動作結束時間 看向原本位置
            }
            else if (SkillName == "JumpAtk"
                || SkillName == "WaveAtk"
                || SkillName == "Lightning")//範圍攻擊
            {
                ///判斷受到範圍攻擊的是怪物還是玩家
                if (m_Go.name.Substring(0, m_Go.name.Length - 1) == "Character")
                {
                    TemList = TotalStatic.MonsterList;
                }
                else if (m_Go.name.Substring(0, m_Go.name.Length - 1) == "Monster")
                {
                    TemList = TotalStatic.PlayerList;
                }

                DmgTime = 1f;//動作施放時間
                m_Go.transform.LookAt(m_TargetPoition);//行動者看著目標的位置
                m_Go.GetComponent<Animator>().SetTrigger(SkillName);//行動者將技能動作判定打開並執行技能動作
                StartCoroutine(AreaSkill(DmgTime));//執行不移動範圍攻擊function
                StartCoroutine(LookBack());//同時判斷動作結束時間 看向原本位置
            }
            else if(SkillName == "Def")//防禦
            {
                m_GoState.bDefense = true;
                m_GoState.Def += 10;
                m_GoState.Mdef += 10;
                m_Target.GetComponent<Animator>().SetTrigger("Def");
                m_Go.transform.rotation = m_BasicRotation;
                TurnOver();//回合結束
            }
            else if (SkillName == "Summon")
            {
                DmgTime = 1.75f;
                TemList = TotalStatic.MonsterList;
                m_Go.GetComponent<Animator>().SetTrigger(SkillName);//行動者將技能動作判定打開並執行技能動作
                StartCoroutine(Summon(DmgTime));//執行召喚怪物
                StartCoroutine(LookBack());//同時判斷動作結束時間 看向原本位置
            }
            else///移動攻擊執行Move
            {
                //Sounds.Instance().PlaySkill(m_Go.name);
                Ismove = true;//移動攻擊的開關打開    
            }      
        }
        #endregion

        #region 移動的攻擊
        if (Ismove)
        {
            DistanceToTarget = Vector3.Distance(m_Target.transform.position, m_Go.transform.position);//設定行動者現在位置與目標現在位置的距離

            //如果雙方距離大於2，行動者會看著目標並持續朝著目標位置移動，並且播放跑步動作
            if (DistanceToTarget > 2)
            {
                m_Go.transform.LookAt(m_TargetPoition);//行動者看著目標位置
                if (m_Go == GameObject.Find("Character2"))//UnityChan移動攻擊技能
                {
                    MoveSpeed = 10;
                    if(SkillName == "P2Atk1" || SkillName == "Atk")
                    {
                        m_Go.GetComponent<Animator>().SetBool("Run", true);
                    }
                    else if(SkillName == "P2Atk2"|| SkillName == "P2Atk3")//旋風腿 升龍拳 隱身攻擊
                    {
                        m_Go.GetComponent<Animator>().SetBool("Run", true);
                        Object o = Resources.Load("P2Smoke");//P2消失特效
                        partical = GameObject.Instantiate(o) as GameObject;
                        partical.transform.position = m_Go.transform.position;
                        foreach (var value in TotalStatic.PlayerList)
                        {
                            if(value.name == "Character2")
                                value.SetActive(false);//P2消失
                        }
                    }
                    //else 
                    //{
                    //    if (m_Go.name == "Character2")
                    //    {
                    //        m_Go.GetComponent<Animator>().SetBool("Jump", true);//UnityChan跳
                    //        Sounds.Instance().PlayUnitychanJump();
                    //    }
                    //}
                }
                else //其他人的近戰移動動作
                {
                    MoveSpeed = 8; 
                    m_Go.GetComponent<Animator>().SetBool("Run", true);//行動者的RUN動作打開
                }
                m_Go.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);//行動者位置朝目標前進
            }
            else//如果雙方距離小於2
            {
                foreach (var value in TotalStatic.PlayerList)
                {
                    if (value.name == "Character2")
                        value.SetActive(true);//P2出現
                }
                //if (m_Go.name == "Character2")
                    //m_Go.GetComponent<Animator>().SetBool("Jump", false);//停止跳動作
                m_Go.GetComponent<Animator>().SetBool("Run", false);//停止跑步動作
                m_Go.GetComponent<Animator>().SetBool(SkillName, true);//行動者播放攻擊動作
                //判斷顯示受傷動作的時間
                if (m_Go == GameObject.Find("Character1"))
                {
                    DmgTime = 0.8f;
                }
                else if (m_Go == GameObject.Find("Character2"))
                {
                    if (SkillName == "Atk") DmgTime = 0.6f;
                    else if (SkillName == "P2Atk1") DmgTime = 0.22f;
                    else if (SkillName == "P2Atk2") DmgTime = 0.07f;
                    else if (SkillName == "P2Atk3") DmgTime = 0.1f;
                }
                else//(未增加其他怪物動作時間)
                {
                    DmgTime = 0.45f;
                }
                
                StartCoroutine(NearDmgPlay(DmgTime));//受傷者播放受傷動作，跳出傷害數字
                back_move = true;//播放回去動作的開關開啟
                Ismove = false;//將移動攻擊的開關關閉
                StartCoroutine(Back());//開啟回到原點的開關
            }
        }
        #endregion

        #region 回到原點
        if (Isback)
        {
            //回去原點的動作
            if (back_move)
            {
                DistanceToBasic = Vector3.Distance(m_BasicPosition, m_Go.transform.position);//取得行動者初始位置和行動在現在位置的距離
                ///如果距離大於0.1f
                if (DistanceToBasic > 0.1f)
                {
                    m_Go.transform.LookAt(m_BasicPosition);//行動者面相行動者初始位置
                    if (m_Go == GameObject.Find("Character2"))//UnityChan行動
                    {
                        if (SkillName == "P2Atk1" || SkillName == "Atk")
                        {
                            m_Go.GetComponent<Animator>().SetBool("Run", true);
                        }
                        else if (SkillName == "P2Atk2" || SkillName == "P2Atk3")
                        {
                            m_Go.GetComponent<Animator>().SetBool("Run", true);
                            Object o = Resources.Load("P2Smoke");//P2消失特效
                            partical = GameObject.Instantiate(o) as GameObject;
                            partical.transform.position = m_Go.transform.position;
                            foreach (var value in TotalStatic.PlayerList)
                            {
                                if (value.name == "Character2")
                                    value.SetActive(false);//P2消失
                            }
                        }
                        //else
                        //{
                        //    if (m_Go.name == "Character2")
                        //    {
                        //        m_Go.GetComponent<Animator>().SetBool("Jump", true);//UnityChan跳
                        //        Sounds.Instance().PlayUnitychanJump();
                        //    }
                        //}
                    }
                    else//其他人的近戰移動
                    {
                        m_Go.GetComponent<Animator>().SetBool("Run", true);//行動者播放RUN動作
                    }
                    m_Go.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);//行動者位置朝行動者初始位置前進
                }
                else///如果距離小於0.1f
                {
                    foreach (var value in TotalStatic.PlayerList)
                    {
                        if (value.name == "Character2")
                            value.SetActive(true);//P2出現
                    }
                    back_move = false;//回到原點的動作開關關閉
                    //if(m_Go.name == "Character2")
                    //    m_Go.GetComponent<Animator>().SetBool("Jump", false);//UnityChan結束Jump動作
                    m_Go.GetComponent<Animator>().SetBool("Run", false);//行動者結束RUN動作
                    m_Go.transform.position = m_BasicPosition;//行動者位置設為行動者初始位置
                    //m_Go.transform.rotation = m_BasicRotation;//行動者轉向設為行動者初始轉向
                    m_Go.transform.LookAt(m_TargetPoition);
                    Quaternion q = m_Go.transform.rotation;
                    m_Go.transform.localRotation = new Quaternion(0, q.y, q.z, q.w);
                }
            }
            else//回到原點後執行回合結束的事情
            {
                Isback = false;//只執行一次攻擊結束的動作
                TurnOver();//回合結束判定
            }
        }
        #endregion
    }

    #region 近距離特效
    IEnumerator NearDmgPlay(float time)
    {
        yield return new WaitForSeconds(time);
        DamageJudge();//傷害判斷
        DamageAction();//傷害動作判斷

        if(m_Go.tag == "Player")
            Sounds.Instance().PlayMoveHit(m_Go.name);//音效判斷
        else
            Sounds.Instance().PlayMoveHit(m_Go.tag);//音效判斷

        dmg_show.ShowDmg(SkillName, m_Target);//顯示傷害數字
        if(m_Go.tag == "Boss")
        {
            Object o = Resources.Load("HitBoss");//讀取傷害特效
            partical = GameObject.Instantiate(o) as GameObject;//生成傷害特效
            partical.transform.position = m_TargetPoition + m_Target.transform.up;
            //傷害特效的位置設在初始目標位置的前面
        }
        else
        {
            Object o = Resources.Load("Hit_C");//讀取傷害特效
            partical = GameObject.Instantiate(o) as GameObject;//生成傷害特效
            partical.transform.position = m_TargetPoition + m_Target.transform.forward + m_Target.transform.up;
            //傷害特效的位置設在初始目標位置的前面
        }

    }
    #endregion

    #region 攻擊者做出攻擊動作後，開啟回到原點的開關
    IEnumerator Back()
    {
        AnimationClip[] clips = m_Go.GetComponent<Animator>().runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(SkillName))
            {
                AniLength = clip.length / 1.35f;  //加速過的把動畫名字改掉抓時間，沒加速的用預設時間回去
                break;
            }
            else if (!clip.name.Equals(SkillName))
            {
                AniLength = 2.4f;
            }
        }
        Debug.Log("普攻時間" + AniLength);
        yield return new WaitForSeconds(AniLength);
        Isback = true;//開啟回到原點的開關
    }
    #endregion

    #region 原地攻擊
    IEnumerator NoMove(float time)
    {
        yield return new WaitForSeconds(time);//在等待輸入參數的秒後執行後續

        Object o = Resources.Load(SkillName);//從resources中取得技能
        partical = GameObject.Instantiate(o) as GameObject;//生成技能
        DamageJudge();//傷害判斷

        ///若是補血or道具不播放受傷動畫
        if (SkillName == "Heal" || SkillName == "RedWater")
        {
            if (SkillName == "RedWater")
                TotalStatic.iRedWater_item -= 1;
            partical.transform.position = m_TargetPoition + m_Target.transform.up;
            dmg_show.ShowAddHp(SkillName, m_Target);//補HP數字
        }
        else if (SkillName == "BlueWater")
        {
            TotalStatic.iBlueWater_item -= 1;
            partical.transform.position = m_TargetPoition + m_Target.transform.up;
            dmg_show.ShowAddMp(SkillName, m_Target);//補MP數字
        }
        else if (SkillName == "ReviveItem")
        {
            TotalStatic.iRevive_item -= 1;
            partical.transform.position = m_TargetPoition + m_Target.transform.up * 0.1f;
            dmg_show.ShowAddHp(SkillName, m_Target);//補HP數字
            m_Target.GetComponent<Animator>().SetTrigger("Idle");
            m_Target.GetComponent<CharacterState>().TimeBarDisplay();//顯示目標時間條
        }
        else if (SkillName == "Strong")
        {
            partical.transform.position = m_TargetPoition + m_Target.transform.up;

        }
        else
        {
            partical.transform.position = m_TargetPoition;//技能特效在目標位置上出現
            dmg_show.ShowDmg(SkillName, m_Target);//跳傷害數字
            
            DamageAction();//傷害動作判斷
        }

        Sounds.Instance().PlayNoMove(SkillName);

        Destroy(partical.gameObject, 3);//消除技能特效
    }
    #endregion

    #region 轉回正面後 回合結束
    IEnumerator LookBack()
    {
        AnimationClip[] clips = m_Go.GetComponent<Animator>().runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(SkillName))
            {
                AniLength = clip.length / 1.2f;
                break;
            }
            else if (!clip.name.Equals(SkillName))
            {
                AniLength = 2.5f;
            }
        }
        Debug.Log("技能時間"+AniLength);
        yield return new WaitForSeconds(AniLength);
        m_Go.transform.rotation = m_BasicRotation;
        Quaternion q = m_Go.transform.rotation;
        m_Go.transform.localRotation = new Quaternion(0, q.y, q.z, q.w);
        TurnOver();//回合結束
    }
    #endregion

    #region 傷害判斷
    void DamageJudge()
    {
        if(SkillName == "BlueWater")
        {
            m_TargetState.Mp -= damage;//選擇藍水的話就補魔
        }
        else if (SkillName == "Strong")
        {
            m_TargetState.Hp -= damage;//增加BUFF
            m_TargetState.Def += 10;
            m_TargetState.Mdef += 10;
            m_TargetState.iSpeed += 10;
        }
        else
        {
            ///扣除傷害
            if (damage_result == "Miss")//攻擊Miss的UI
                m_TargetState.Hp -= 0;
            else if (damage_result == "Critical")//爆擊的話傷害UI會不一樣
            {
                damage *= 2;
                m_TargetState.Hp -= damage;
            }
            else//普通攻擊的UI
                m_TargetState.Hp -= damage;
        }

        ///如果血量超過上限會變成血量上限，血量低於0會變成0
        if (m_TargetState.Hp >= m_TargetState.MaxHp)
            m_TargetState.Hp = m_TargetState.MaxHp;
        else if (m_TargetState.Hp <= 0)
            m_TargetState.Hp = 0;
        ///如果魔量超過上限會變成魔量上限
        if (m_TargetState.Mp >= m_TargetState.MaxMp)
            m_TargetState.Mp = m_TargetState.MaxMp;

        //如果攻擊者是角色的話就更新角色血魔
        if (m_Go.name.Substring(0, m_Go.name.Length - 1) == "Character")
            Blood.BloodVariety(m_Go);
        //如果目標是角色受到傷害就更新目標血魔
        if (m_Target.name.Substring(0, m_Target.name.Length - 1) == "Character")
            Blood.BloodVariety(m_Target);
    }
    #endregion

    #region 受傷或死亡動作判斷
    void DamageAction()
    {
        if (m_Target.GetComponent<CharacterState>().Hp > 0)
        {
            m_Target.GetComponent<Animator>().SetTrigger("Dmg");//受傷者播放受傷動作
        }
        else
        {
            Object o = Resources.Load("Die");//從resources中取得技能
            partical = GameObject.Instantiate(o) as GameObject;//生成技能
            partical.transform.position = m_TargetPoition;//技能特效在目標位置上出現

            m_Target.GetComponent<Animator>().SetBool("Die", true);//目標播放死亡動作
            m_TargetState.fTime = 0;//目標時間歸零
            m_TargetState.TimeBackOrigin();//時間條的位置回到原位
            m_TargetState.TimeBarHide();//隱藏該目標時間條
            Sounds.Instance().PlayDie(m_Target);//播放死亡聲音

            if (TimeTwig.now_turn == m_Target.name)//如果輪到該角色行動則將行動回合清空
                TimeTwig.now_turn = null;

            if (m_Target.tag == "Boss")
                TotalStatic.BossFight = true;
            Debug.Log("TotalStatic.BossFight = " + TotalStatic.BossFight);
            if (m_Target.name.Substring(0, m_Target.name.Length - 1) == "Monster")
            {
                TotalStatic.iTemExp += m_TargetState.Exp;//將死亡的怪物經驗值放入暫存經驗值
                TotalStatic.StateList.Remove(m_TargetState);//將怪物從狀態列表中移除
                TotalStatic.MonsterList.Remove(m_Target);//將怪物從怪物列表中移除
                DropItem();//掉落物品判斷
                Invoke("MonsterDie",0.5f);//直接讓怪物播放完死亡動畫後回收物件池
            }

            Destroy(partical.gameObject, 3);//消除技能特效
        }
    }
    #endregion

    #region 掉落物品判斷
    void DropItem()
    {
        if (Random.Range(1, 3) == 1)//掉落紅水
        {
            if(Random.Range(0, 101) <= 40)
                TotalStatic.lTemItem.Add("RedWater"); 
        }
        else//掉落藍水
        {
            if (Random.Range(0, 101) <= 30)
                TotalStatic.lTemItem.Add("BlueWater");
        }
    }
    #endregion

    #region 怪物死亡收回物件池
    void MonsterDie()
    {
        MonsterPool.ExitFight(m_Target);
        if (m_Target.tag == "Mushroom") NPCtest.Instance().iMsr++; //計算殺了哪種怪物
        if (m_Target.tag == "FireDragon") NPCtest.Instance().iDragon++;
        NPCtest.Instance().MissionUpdate();
    }
    #endregion

    #region 範圍攻擊
    IEnumerator AreaSkill(float time)
    {
        yield return new WaitForSeconds(time);//在等待輸入參數的秒後執行後續
        Sounds.Instance().PlayNoMove(SkillName);//播放技能聲音
        if(SkillName == "Lightning")
            m_GoState.Mp -= 25;
        m_GoState.fTime -= 100;

        foreach (var value in TemList)
        {
            m_Target = value;
            ValueState = value.GetComponent<CharacterState>();
            if(ValueState.Hp > 0)
            {
                Object o = Resources.Load(SkillName);//從resources中取得技能
                partical = GameObject.Instantiate(o) as GameObject;//生成技能

                if (SkillName == "WaveAtk")
                {
                    partical.transform.position = m_Go.transform.position;//技能特效在自己位置上出現
                    partical.transform.LookAt(value.transform.position);//特效朝向目標位置
                }
                else
                    partical.transform.position = value.transform.position + value.transform.up;//技能特效在目標位置上出現

                damage = damage_judge.JudgeHurt(m_Go, value, SkillName);//計算傷害

                if (damage == -1)
                {
                    damage_result = "Miss";
                }
                if (HurtJudge.is_critical)
                {
                    HurtJudge.is_critical = false;
                    damage_result = "Critical";
                }

                ///目標扣除傷害
                if (damage_result == "Miss")//攻擊Miss的UI
                    ValueState.Hp -= 0;
                else if (damage_result == "Critical")//爆擊的話傷害UI會不一樣
                {
                    damage *= 2;
                    ValueState.Hp -= damage;
                }
                else//普通攻擊的UI
                    ValueState.Hp -= damage;

                if (ValueState.Hp <= 0)
                    ValueState.Hp = 0;//受到傷害使血量小於等於零的時候強制設零

                Destroy(partical.gameObject, 3);//消除技能特效
                dmg_show.ShowDmg(SkillName, value);//顯示傷害數字

                if (ValueState.Hp > 0)
                {
                    value.GetComponent<Animator>().SetTrigger("Dmg");//受傷者播放受傷動作
                }
                else
                {
                    value.GetComponent<Animator>().SetBool("Die", true);//目標播放死亡動作
                    ValueState.fTime = 0;//目標時間歸零
                    ValueState.TimeBackOrigin();//時間條的位置回到原位
                    ValueState.TimeBarHide();//隱藏該目標時間條
                    if (TimeTwig.now_turn == value.name)//如果輪到該角色行動則將行動回合清空
                        TimeTwig.now_turn = null;
                }
            }
        }

        foreach (var value in TotalStatic.PlayerList)
        {
            Blood.BloodVariety(value);//更新角色血魔
        }

        if (m_Target.name.Substring(0, m_Target.name.Length - 1) == "Monster")
        {
            ManyMonsterDie();//怪物播放完死亡動畫後回收物件池
        }
    }
    #endregion

    #region 範圍攻擊後怪物死亡回收
    void ManyMonsterDie()
    {
        for (int i = TemList.Count - 1;i > -1;i--)
        {
            if (TotalStatic.MonsterList[i])
            {
                ValueState = TotalStatic.MonsterList[i].GetComponent<CharacterState>();
                if (ValueState.Hp <= 0)
                {
                    if (TotalStatic.MonsterList[i].tag == "Boss")
                        TotalStatic.BossFight = true;
                    Debug.Log("TotalStatic.BossFight = " + TotalStatic.BossFight);
                    Object o = Resources.Load("Die");//從resources中取得技能
                    partical = GameObject.Instantiate(o) as GameObject;//生成技能
                    partical.transform.position = TotalStatic.MonsterList[i].transform.position;//技能特效在目標位置上出現

                    TotalStatic.iTemExp += ValueState.Exp;//將死亡的怪物經驗值放入暫存經驗值
                    DropItem();//掉落物品判斷
                    MonsterPool.ExitFight(TotalStatic.MonsterList[i]);
                    TotalStatic.StateList.Remove(ValueState);//將怪物從狀態列表中移除
                    TotalStatic.MonsterList.Remove(TotalStatic.MonsterList[i]);//將怪物從怪物列表中移除
                    Destroy(partical.gameObject, 3);//消除技能特效
                }
            }
        }
    }
    #endregion

    #region 招喚怪物
    IEnumerator Summon(float time)
    {
        yield return new WaitForSeconds(time);

        MonsterPool.EnterFight("Summon");//叫出2個隨機類型敵人
        Sounds.Instance().PlayBossSummon();//召喚怪物音效
        foreach (var value in TotalStatic.MonsterList)
        {
            //怪物1(王)以外的怪物初始化
            if (value.name != "Monster1")
            {
                Object o = Resources.Load(SkillName);//從resources中取得技能
                partical = GameObject.Instantiate(o) as GameObject;//生成技能
                partical.transform.position = value.transform.position;//技能特效在目標位置上出現

                GameObject.Find(value.name + "_TimeBar").GetComponent<Image>().enabled = true;//列表有誰就顯示誰的時間表頭像
                GameObject.Find(value.name + "_TimeBar").GetComponent<Image>().sprite = Resources.Load<Sprite>(value.tag);//根據tag切換時間條頭像
                GameObject.Find(value.name + "_TimeBar").transform.GetChild(0).GetComponent<Image>().enabled = true;//顯示時間表頭像的移動點
                TotalStatic.StateList.Add(value.GetComponent<CharacterState>());//怪物加入狀態列表
                value.GetComponent<CharacterState>().FightSet();
                value.GetComponent<CharacterState>().MaxHp += 100;
                value.GetComponent<CharacterState>().Hp += 100;
                value.GetComponent<CharacterState>().Mdef += 15;
                value.GetComponent<CharacterState>().Atk += 10;
                value.GetComponent<CharacterState>().Matk += 10;

                value.transform.LookAt(GameObject.Find("Character1").transform.position);
                Quaternion q = value.transform.rotation;
                value.transform.localRotation = new Quaternion(0, q.y, q.z, q.w);
                Destroy(partical.gameObject, 3);//消除技能特效
            }
        }
        yield return new WaitForSeconds(time);
    }
    #endregion

    #region 戰鬥結束判斷
    void JudgeGameOver()
    {
        //計算角色死亡數量用
        int CharacterDieCount = 0;

        //怪物數量小於等於0
        if (TotalStatic.MonsterList.Count <= 0)
        {
            TotalStatic.bOverFight = true;//戰鬥結束
            TotalStatic.bFightResult = "win";
            TotalStatic.Time_switch = false;//時間條開關關閉
        }
        foreach (var value in TotalStatic.PlayerList)
        {
            if (value.GetComponent<CharacterState>().Hp <= 0)
                CharacterDieCount++;
            if (CharacterDieCount == TotalStatic.PlayerList.Count)//角色血量都小於0的數量跟角色的數量一樣
            {
                TotalStatic.bOverFight = true;//戰鬥結束
                TotalStatic.bFightResult = "lose";
                TotalStatic.Time_switch = false;//時間條開關關閉
            }
        }
    }
    #endregion

    #region 回合結束
    void TurnOver()
    {
        ///將行動者、目標、技能、傷害、傷害判定清空
        m_Go = null;
        m_Target = null;
        SkillName = "";
        damage = 0;
        damage_result = "";

        TotalStatic.bPlayer_turn = false;//將玩家回合false
        TotalStatic.bMonster_turn = false;//將怪物回合false
        TotalStatic.Turn_switch = true;//開啟回合判斷
        TotalStatic.TurnOver();//角色UI歸位
        FightCamera.target_monster = null;//清空戰鬥攝影機的目標怪物
        FightCamera.fight_ui_switch = true;//戰鬥攝影機中的UI開關開啟
        MessageControl.message_clear = true;//清空戰鬥資訊
        EventControl.display_damage = "";//清空傷害判斷
        EventControl.tem_damage = 0;//清空傷害
        MenuClick.action_type = "";//清空玩家攻擊方式
        TotalStatic.MonsterActionType = "";//清空怪物攻擊方式

        TimeTwig.now_turn = null;//現在回合清空
        JudgeGameOver();
        if(!TotalStatic.bOverFight)
            TotalStatic.Time_switch = true;//開啟時間表計時
    }
    #endregion
}