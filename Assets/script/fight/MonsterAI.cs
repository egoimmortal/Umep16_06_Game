using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    /// <summary>
    /// 該script附加在Assets/Prefabs/的所有怪物身上
    /// script說明 : 
    ///     判斷現在要執行動作的怪物和目標角色
    /// </summary>

    /// <summary>
    /// 私有變數
    /// </summary>
    //選擇玩家的判斷，王的攻擊頻率
    private int iNum, iFrequency;
    private GameObject objTemTarget;
    private GameObject _partical;

    private void Start()
    {
        if (tag == "Boss")
        {
            _partical = transform.GetChild(3).gameObject;
            _partical.SetActive(false);
        }
    }

    private void Update()
    {
        ///現在的行動者是該怪物的名稱時執行
        if (TotalStatic.MonsterAI_switch && TimeTwig.now_turn == gameObject.name)
        {
            TotalStatic.MonsterAI_switch = false;
            TotalStatic.MonsterActionType = AttackType();//判斷攻擊模式

            ///判斷攻擊目標，兩個目標的HP都>0話就隨機，否則就挑存活的角色
            TotalStatic.TargetPlayer = ChangeTarget(TotalStatic.PlayerList);
        }
    }

    GameObject ChangeTarget(List<GameObject> list)
    {
        iNum = 0;
        for (int i = 0;i < list.Count;i++)
        {
            if (list[i].GetComponent<CharacterState>().Hp > 0)
                iNum++;
        }
        ///iNum == 1 血量大於0的只有player1或是player2，就只有選那個目標
        if (iNum == 1)
        {
            foreach (var value in list)
            {
                if (value.GetComponent<CharacterState>().Hp > 0)
                    objTemTarget = value;
            }
            return objTemTarget;
        }
        else if (iNum == 2)///iNum == 2 血量大於0的有player1和player2，隨機選一個目標
        {
            iNum = 0;
            if (Random.Range(iNum, iNum + 2) == iNum)
                return list[iNum];
            else
                return list[iNum + 1];
        }
        else
            return null;
    }

    string AttackType()
    {
        if (gameObject.tag == "Mushroom")
            return "Atk";//蘑菇只會施放移動攻擊
        else if (gameObject.tag == "Boss")
        {
            iFrequency++;//攻擊次數+1

            ///血量低於60%且沒有strong狀態時間的時候會施放strong
            if ((gameObject.GetComponent<CharacterState>().Hp <= (gameObject.GetComponent<CharacterState>().MaxHp)*0.6)
               && (gameObject.GetComponent<CharacterState>().iStrongTime <= 0))
            {
                _partical.SetActive(true);
                gameObject.GetComponent<CharacterState>().iStrongTime = 5;
                return "Strong";
            }

            if(gameObject.GetComponent<CharacterState>().iStrongTime == 0)
            {
                _partical.SetActive(false);
            }

            ///每三次攻擊後下一次判斷場上是否有小怪，沒有的話就招喚小怪
            if ((iFrequency % 4) == 0)
            {
                if(TotalStatic.MonsterList.Count == 1)
                {
                    return "Summon";
                }
            }
            //每兩次攻擊後下一次會有範圍攻擊
            if ((iFrequency % 3) == 0)
            {
                if (Random.Range(0, 2) == 0)
                    return "JumpAtk";
                else
                    return "WaveAtk";
            }
            //普通的單體攻擊
            return "Atk";
        }
        else
        {
            //火龍隨機施放移動攻擊跟吐火
            if (Random.Range(0, 2) == 0)
                return "Atk";
            else
                return "FireballExplode";
        }
    }
}