using UnityEngine;

public class EventControl : MonoBehaviour
{
    /// <summary>
    /// 該script附加在DemoScene中的FightScript上
    /// script說明 : 
    ///     判斷玩家是否有在UI選擇了技能和目標，有的話就進入戰鬥判斷
    ///     判斷怪物AI是否有選擇好攻擊方式和目標，有的話就進入戰鬥判斷
    /// </summary>

    /// <summy>
    /// 公有變數
    /// <summy>
    //宣告傷害訊息，選擇目標，被選擇目標，選擇模式
    public static string fight_message, change_user, change_be_user, action_type;
    //宣告判斷回合，行動開關
    public static bool event_switch;
    //宣告傷害數值
    public static int tem_damage;
    //顯示傷害的樣式
    public static string display_damage;
    //現在怪物狀態
    private CharacterState MonsterState;
    /// <summy>
    /// 私有變數
    /// <summy>
    //判斷使用者與被使用者
    private GameObject user, be_user;

    HurtJudge damage = new HurtJudge();

    void Update()
    {
        #region 執行一次判斷玩家回合玩家是否有指定目標，然後玩家進行攻擊
        if (MenuClick.Target_Monster != null)
        {
            be_user = MenuClick.Target_Monster;
            MoveToTarget.m_Target = MenuClick.Target_Monster;//設定執行目標
            MenuClick.Target_Monster = null;
        }
        else if (MenuClick.action_type == "Def"
            || MenuClick.action_type == "Lightning")
        {
            be_user = MenuClick.Now_Character;
            MoveToTarget.m_Target = MenuClick.Now_Character;
        }

        if(TotalStatic.bPlayer_turn && be_user != null)
        {
            user = MenuClick.Now_Character;//取得現在的角色
            MoveToTarget.m_Go = MenuClick.Now_Character;//設定行動者
            action_type = MenuClick.action_type;//設定執行的動作

            if (action_type == "Def")
            {
                user.GetComponent<CharacterState>().fTime -= 100;//防禦的時間減去100
            }
            else if (action_type != "Lightning")
            {
                tem_damage = damage.JudgeHurt(user, be_user, action_type);//判斷傷害
                DisplayDamage();//判斷MISS和爆擊
            }

            ///現在的角色和被選擇的角色清空
            user = null;
            be_user = null;
            Destroy(TotalStatic.PlayerCircle, 0f);//刪除腳下光圈

            event_switch = true;//開啟動作開關
        }
        #endregion

        #region 執行一次判斷怪物回合怪物是否有指定目標，然後怪物進行攻擊
        if (TotalStatic.bMonster_turn && TotalStatic.TargetPlayer)
        {
            MoveToTarget.m_Go = TotalStatic.Monster_now;//取得現在的角色
            MoveToTarget.m_Target = TotalStatic.TargetPlayer;//設定執行目標
            action_type = TotalStatic.MonsterActionType;//設定執行的動作
            MonsterState = TotalStatic.Monster_now.GetComponent<CharacterState>();

            //如果動作是strong的話就把目標變成自己，不是的話strong的回合數-1
            if (action_type == "Strong")
                MoveToTarget.m_Target = TotalStatic.Monster_now;
            else
                MonsterState.iStrongTime--;

            //如果現在BOSS的BUFF回合為0時，就將狀態減回去
            if(MonsterState.iStrongTime == 0)
            {
                MonsterState.Def -= 10;
                MonsterState.Mdef -= 10;
                MonsterState.iSpeed -= 10;

            }

            //非範圍攻擊時進入判斷
            if(action_type != "WaveAtk" || action_type != "JumpAtk" || action_type != "Summon")
            {
                //判斷傷害
                tem_damage = damage.JudgeHurt(TotalStatic.Monster_now, TotalStatic.TargetPlayer, action_type);
                //判斷MISS和爆擊
                DisplayDamage();
            }

            ///清空怪物AI中的攻擊方和被攻擊方和攻擊方式
            TotalStatic.Monster_now = null;
            TotalStatic.TargetPlayer = null;

            event_switch = true;//開啟動作開關
        }
        #endregion
    }

    private void DisplayDamage()
    {
        if (tem_damage == -1)
        {
            display_damage = "Miss";
        }
        if (HurtJudge.is_critical)
        {
            HurtJudge.is_critical = false;
            display_damage = "Critical";
        }
    }
}