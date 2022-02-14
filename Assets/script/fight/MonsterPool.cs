using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterPool : MonoBehaviour
{
    public GameObject Mushroom_type, Dragon_type, Boss_type;

    private GameObject item;
    private int monster_count;

    /// <summary>
    /// 建立怪物
    /// </summary>
    private void Start()
    {
        monster_count = 3;//建立怪物物件池的數量
        while(monster_count != 0)
        {
            item = ObjectPool.Intance.CreateObject("Mushroom", Mushroom_type, Vector3.zero, new Quaternion(0, 0, 0, 0));
            item = ObjectPool.Intance.CreateObject("FireDragon", Dragon_type, Vector3.zero, new Quaternion(0, 0, 0, 0));
            item = ObjectPool.Intance.CreateObject("Boss", Boss_type, Vector3.zero, new Quaternion(0, 0, 0, 0));
            monster_count--;
        }
        ObjectPool.Intance.CloseMonster();
    }
    /// <summary>
    /// 進入戰鬥取得碰到的怪物的TAG名稱去抓取相對的怪物
    /// </summary>
    /// <param name="monster_name"></param>
    public static void EnterFight(string monster_name)
    {
        if(monster_name == "Boss")//Boss的戰鬥
        {
            TotalStatic.Monster1 = ObjectPool.Intance.GetMonster(monster_name, 0);
            TotalStatic.Monster1.transform.position = new Vector3(85.93f, -32.778f, 351.95f);
            Quaternion q = TotalStatic.Monster1.transform.rotation;
            TotalStatic.Monster1.transform.localRotation = new Quaternion(0, q.y, q.z, q.w);
            TotalStatic.Monster1.name = "Monster1";
            TotalStatic.Monster1.SetActive(true);
        }
        else if(monster_name == "Mushroom" || monster_name == "FireDragon")
        {
            if(Random.Range(1, 4) == 1)
            {
                TotalStatic.Monster1 = ObjectPool.Intance.GetMonster(monster_name, 0);
                TotalStatic.Monster1.transform.position = new Vector3(193.79f, 1.03f, 254.41f);
                TotalStatic.Monster1.name = "Monster1";
                TotalStatic.Monster1.SetActive(true);
            }
            else if (Random.Range(1, 4) == 2)
            {
                TotalStatic.Monster1 = ObjectPool.Intance.GetMonster(monster_name, 0);
                monster_name = RandomMonster();
                TotalStatic.Monster2 = ObjectPool.Intance.GetMonster(monster_name, 1);
                TotalStatic.Monster1.transform.position = new Vector3(191.32f, 1.035f, 254.29f);
                TotalStatic.Monster2.transform.position = new Vector3(195.86f, 1.085f, 253.18f);
                TotalStatic.Monster1.name = "Monster1";
                TotalStatic.Monster2.name = "Monster2";
                TotalStatic.Monster1.SetActive(true);
                TotalStatic.Monster2.SetActive(true);
            }
            else
            {
                TotalStatic.Monster1 = ObjectPool.Intance.GetMonster(monster_name, 0);
                monster_name = RandomMonster();
                TotalStatic.Monster2 = ObjectPool.Intance.GetMonster(monster_name, 1);
                monster_name = RandomMonster();
                TotalStatic.Monster3 = ObjectPool.Intance.GetMonster(monster_name, 2);
                TotalStatic.Monster1.transform.position = new Vector3(191.32f, 1.035f, 254.29f);
                TotalStatic.Monster2.transform.position = new Vector3(193.79f, 1.03f, 254.41f);
                TotalStatic.Monster3.transform.position = new Vector3(195.86f, 1.085f, 253.18f);
                TotalStatic.Monster1.name = "Monster1";
                TotalStatic.Monster2.name = "Monster2";
                TotalStatic.Monster3.name = "Monster3";
                TotalStatic.Monster1.SetActive(true);
                TotalStatic.Monster2.SetActive(true);
                TotalStatic.Monster3.SetActive(true);
            }
        }
        else if (monster_name == "FirstFight")
        {
            TotalStatic.Monster1 = ObjectPool.Intance.GetMonster("Mushroom", 0);
 
            TotalStatic.Monster2 = ObjectPool.Intance.GetMonster("Mushroom", 1);
            TotalStatic.Monster1.transform.position = new Vector3(191.32f, 1.035f, 254.29f);
            TotalStatic.Monster2.transform.position = new Vector3(195.86f, 1.085f, 253.18f);
            TotalStatic.Monster1.name = "Monster1";
            TotalStatic.Monster2.name = "Monster2";
            TotalStatic.Monster1.SetActive(true);
            TotalStatic.Monster2.SetActive(true);

        }
        else if (monster_name == "Summon")
        {
            monster_name = RandomMonster();
            TotalStatic.Monster2 = ObjectPool.Intance.GetMonster(monster_name, 0);
            monster_name = RandomMonster();
            TotalStatic.Monster3 = ObjectPool.Intance.GetMonster(monster_name, 1);
            TotalStatic.Monster2.transform.position = new Vector3(85.18f, -32.778f, 349.44f);
            TotalStatic.Monster3.transform.position = new Vector3(87.09f, -32.778f, 354.49f);
            TotalStatic.Monster2.name = "Monster2";
            TotalStatic.Monster3.name = "Monster3";
            TotalStatic.Monster2.SetActive(true);
            TotalStatic.Monster3.SetActive(true);
            TotalStatic.MonsterList.Add(TotalStatic.Monster2);
            TotalStatic.MonsterList.Add(TotalStatic.Monster3);
        }
    }

    static string RandomMonster()
    {
        if (Random.Range(1, 3) == 1)
            return "Mushroom";
        else
            return "FireDragon";
    }
    /// <summary>
    /// 將怪物回收到物件池
    /// </summary>
    /// <param name="monster_type"></param>
    public static void ExitFight(GameObject monster_type)
    {
        if(monster_type.tag != "Boss")
            monster_type.transform.GetChild(1).gameObject.GetComponent<Dissolve>().dissolve();
        else
            ObjectPool.Intance.CollectObject(monster_type, 2);
    }
}