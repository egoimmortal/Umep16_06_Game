using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject Mushroom_type;
    public static bool create_switch;
    public static string monster_type, monster1_name, monster2_name;

    private int monster_num;

    private void Update()
    {
        if (create_switch)
        {
            create_switch = false;
            monster_num = Random.Range(1,3);
        }
        if (monster_num != 0)
        {
            create(monster_type);
            monster_num--;
        }
    }

    private void create(string monster_type)
    {
        GameObject test;
        test = Instantiate(Mushroom_type);

        if (!GameObject.Find("Monster1"))
            test.name = "Monster1";
        else if (!GameObject.Find("Monster2"))
            test.name = "Monster2";
        else
            Destroy(test);
    }
}
