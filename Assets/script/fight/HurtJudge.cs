using UnityEngine;

public class HurtJudge
{
    //是否爆擊
    public static bool is_critical;
    //燒傷，暫存結果，暫存燒傷，暫存毒傷
    private float damage, tem_result, tem_burn_damage, tem_poison_damage;
    //是否命中
    private bool is_hit;

    #region 是否命中
        private bool HitOrMiss(float u_hit, float be_dodge)
    {
        if (Random.Range(1, 101) <= u_hit - be_dodge)
            return true;
        else
            return false;
    }
    #endregion

    #region 是否爆擊
    private bool Critical(float u_cri)
    {
        if (Random.Range(1, 101) <= u_cri)
            return true;
        else
            return false;
    }
    #endregion

    #region 普通攻擊計算
    private float Atk(string user_name, CharacterState operation, CharacterState be_operation)
    {
        switch (user_name)
        {
            case "Character1":
                if (operation.Atk * 1.2f - be_operation.Def <= 1)
                    tem_result = 1;
                else
                    tem_result = operation.Atk * 1.2f - be_operation.Def;
                break;
            case "Character2":
                if (operation.Atk * 1.2f - be_operation.Def <= 1)
                    tem_result = 1;
                else
                    tem_result = operation.Atk * 1.2f - be_operation.Def;
                break;
            case "Monster1":
                if (operation.Atk * 1.2f - be_operation.Def <= 1)
                    tem_result = 1;
                else
                    tem_result = operation.Atk * 1.2f - be_operation.Def;
                break;
            case "Monster2":
                if (operation.Atk * 1.2f - be_operation.Def <= 1)
                    tem_result = 1;
                else
                    tem_result = operation.Atk * 1.2f - be_operation.Def;
                break;
            case "Monster3":
                if (operation.Atk * 1.2f - be_operation.Def <= 1)
                    tem_result = 1;
                else
                    tem_result = operation.Atk * 1.2f - be_operation.Def;
                break;
            default:
                break;
        }

        return tem_result;
    }
    #endregion

    #region 技能火焰計算
    private float Fire(CharacterState operation, CharacterState be_operation)
    {
        return (float)(operation.Matk * 1.9 - be_operation.Mdef);
    }
    #endregion
    #region 技能冰雞計算
    private float Water(CharacterState operation, CharacterState be_operation)
    {
        return (float)(operation.Matk * 1.7 - be_operation.Mdef);
    }
    #endregion
    #region 技能治療計算
    private float Heal(CharacterState operation, CharacterState be_operation)
    {
        return (float)(operation.Matk * 1.6);
    }
    #endregion
    #region 技能閃電計算
    private float Lightning(CharacterState operation, CharacterState be_operation)
    {
        return (float)(operation.Matk * 1.6 - be_operation.Mdef);
    }
    #endregion

    #region 技能無影腳計算
    private float P2Atk1(CharacterState operation, CharacterState be_operation)
    {
        return (float)(operation.Atk * 1.6 - be_operation.Def);
    }
    #endregion
    #region 技能旋風腿計算
    private float P2Atk2(CharacterState operation, CharacterState be_operation)
    {
        return (float)(operation.Atk * 1.7 - be_operation.Def);
    }
    #endregion
    #region 技能昇龍腿計算
    private float P2Atk3(CharacterState operation, CharacterState be_operation)
    {
        return (float)(operation.Atk * 1.8 - be_operation.Def);
    }
    #endregion

    #region 範圍攻擊計算
    float AreaAtk(string action, CharacterState operation, CharacterState be_operation)
    {
        switch (action)
        {
            case "WaveAtk":
                tem_result = operation.Atk * 1.2f - be_operation.Def;
                break;
            case "JumpAtk":
                tem_result = operation.Atk * 1.3f - be_operation.Def;
                break;
            default:
                break;
        }

        return (int)tem_result;
    }
    #endregion

    #region 持續傷害計算
    public static void IsDOT(CharacterState user_state)
    {
        ///燒傷計算
        if (user_state.iburn_turn > 0)
        {
            if (user_state.Hp <= user_state.burn_damage)
                user_state.Hp = 1;
            else
                user_state.Hp -= user_state.burn_damage;
            user_state.iburn_turn--;
        }
        ///毒傷計算
        if (user_state.ipoison_turn > 0)
        {
            if (user_state.Hp <= user_state.poison_damage)
                user_state.Hp = 1;
            else
                user_state.Hp -= user_state.poison_damage;
            user_state.ipoison_turn--;
        }
    }
    #endregion

    public int JudgeHurt(GameObject user, GameObject be_user, string action_type)
    {
        var user_state = user.GetComponent<CharacterState>();
        var be_user_state = be_user.GetComponent<CharacterState>();

        is_hit = HitOrMiss(user_state.Hit, be_user_state.Dodge);//判斷是否命中
        
        is_critical = Critical(user_state.Critical);//判斷是否爆擊

        if(action_type != "Lightning"
            && action_type != "WaveAtk"
            && action_type != "JumpAtk")
        {
            user_state.fTime -= 100;//時間歸零
        }

        #region 計算攻擊方式造成的傷害，扣去相對應的時間
        switch (action_type)
        {
            //通用普攻
            case "Atk":
                damage = Atk(user.name, user_state, be_user_state);
                break;
            //P1技能組
            case "Fire":
                user_state.Mp -= 20;
                ///沒燒傷就有機率燒傷
                if (Random.Range(1, 11) <= 3)
                {
                    be_user_state.iburn_turn = 3;
                    ///計算燒傷，最少會扣1
                    tem_burn_damage = user_state.Matk * 0.4f;
                    if (tem_burn_damage < 1)
                        be_user_state.burn_damage = 1;
                    else
                        be_user_state.burn_damage = (int)tem_burn_damage;
                }

                damage = Fire(user_state, be_user_state);
                break;
            case "Water":
                user_state.Mp -= 15;
                /*
                ///沒中毒就有機率中毒
                if (user_state.ipoison_turn != 0
                    && Random.Range(1, 11) == 1)
                {
                    be_user_state.ipoison_turn = 5;
                    ///計算中毒傷害，最少會扣1
                    tem_poison_damage = user_state.Matk * 0.2f;
                    if (tem_poison_damage < 1)
                        be_user_state.poison_damage = 1;
                    else
                        be_user_state.poison_damage = (int)tem_poison_damage;
                }
                */
                damage = Water(user_state, be_user_state);
                break;
            case "Heal":
                user_state.Mp -= 10;
                damage = Heal(user_state, be_user_state);
                damage = -damage;//從正數變成負數(變成補血)
                break;
            case "Lightning":
                damage = Lightning(user_state, be_user_state);
                break;
            //P2技能組
            case "P2Atk1":
                user_state.Mp -= 10;
                damage = P2Atk1(user_state, be_user_state);
                break;
            case "P2Atk2":
                user_state.Mp -= 15;
                damage = P2Atk2(user_state, be_user_state);
                break;
            case "P2Atk3":
                user_state.Mp -= 20;
                damage = P2Atk3(user_state, be_user_state);
                break;
            case "P2Atk4":
                user_state.Mp -= 25;
                break;
            //道具
            case "RedWater":
                damage = -50;
                break;
            case "BlueWater":
                damage = -60;
                break;
            case "ReviveItem":
                damage = -50;
                break;
            //火龍的噴火
            case "FireballExplode":
                damage = Fire(user_state, be_user_state);
                break;
            //王的技能組
            case "WaveAtk":
                //範圍攻擊1
                damage = AreaAtk(action_type, user_state, be_user_state);
                damage += 5;
                break;
            case "JumpAtk":
                //範圍攻擊2
                damage = AreaAtk(action_type, user_state, be_user_state);
                damage += 10;
                break;
            case "Strong":
                //幫自己上BUFF
                damage = -50;
                break;
            case "Summon":
                //招喚小怪
                break;
            default:
                break;
        }
        #endregion

        #region 如果是使用道具的話就直接回傳數值，將爆擊取消
        if (action_type == "Heal"
            || action_type == "RedWater"
            || action_type == "BlueWater"
            || action_type == "ReviveItem"
            || action_type == "Strong")
        {
            is_critical = false;
            return (int)damage;
        }
        #endregion

        #region 將傷害做成0.8倍到1.2倍之間的跳動值
        damage *= Random.Range(8, 13);
        damage *= 0.1f;
        //最小的傷害為1
        if (damage < 1)
            damage = 1;
        #endregion

        #region 沒命中就回傳-1，接收方那邊就可以判斷為miss
        if (!is_hit)
        {
            is_critical = false;
            return -1;
        }
        else
            return (int)damage;
        #endregion
    }
    #region 技能設計說明
    /// <summary>
    /// P1攻擊方式:
    /// 普通攻擊(Atk * 1.2)(消耗時間50)
    /// 火焰(Matk * 1.9)(30%機率造成燒傷 持續3回合 每回合造成Matk * 0.4的傷害)(消耗時間80)(消耗MP20)
    /// 水流(Matk * 1.7)(10%機率造成毒傷 持續5回合 每回合造成Matk * 0.2的傷害)(消耗時間70)(消耗MP15)
    /// 補血(Matk * 1.6)(消耗時間60)(消耗MP15)
    /// 
    /// P2攻擊方式:
    /// 普通攻擊(Atk * 1.5)(消耗時間50)
    /// 無影腳(Atk * 1.6)(消耗時間60)(消耗MP10)
    /// 旋風腿(Atk * 1.7)(消耗時間70)(消耗MP20)
    /// 昇龍拳(Atk * 1.8)(消耗時間80)(消耗MP25)
    /// 
    /// 使用道具:
    /// 統一增加50點生命或魔力
    /// 消耗時間60
    /// 
    /// 蘑菇攻擊方式:
    /// 普通攻擊(Atk * 1.0)(消耗時間50)
    /// 
    /// </summary>
    #endregion
}