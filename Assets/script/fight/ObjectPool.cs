using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    private Dictionary<string, List<GameObject>> monster_pool =
        new Dictionary<string, List<GameObject>>();

    public GameObject CreateObject(string key, GameObject go, Vector3 position, Quaternion quaternion)
    {
        //從池中取出可以使用的物件
        GameObject tempgo = FindUseObj(key);
        if (tempgo != null)
        {
            tempgo.transform.position = position;
            tempgo.transform.rotation = quaternion;
        }
        else
        {
            tempgo = Instantiate(go, position, quaternion) as GameObject;
            Add(key, tempgo);
        }
        tempgo.SetActive(true);
        return tempgo;
    }

    public void Add(string key, GameObject tempGo)
    {
        //物件池當中如果沒有鍵（新增鍵）
        if (!monster_pool.ContainsKey(key))
        {
            monster_pool.Add(key, new List<GameObject>());
        }
        //找到相應的值，為這個列表中新增tempGo
        //List中的Add方法
        //物件池中有鍵就直接在其list中新增值
        monster_pool[key].Add(tempGo);
    }

    public GameObject FindUseObj(string key)
    {
        //在池中且未被使用的物件
        if (monster_pool.ContainsKey(key))
        {
            //返回找到的第一個可用的物件（沒有返回 null）
            return monster_pool[key].Find(p => !p.activeSelf);
            /*上面(泛型委託Lambda)這句話的意思
             *p=cache[key][i]
             *if(!cache[key][i].activeSelf=!p.activeSelf)
             *
             *實現的方法如下
             * for(int i;i<cache[key].Count;i++)
             * {
             * if(!cache[key][i].activeSelf)
             *     return cache[key][i];
             * }
            */
        }
        return null;
    }

    public GameObject GetMonster(string key, int num)
    {
        return monster_pool[key][num];
    }

    public void CloseMonster()
    {
        var list = new List<string>(monster_pool.Keys);
        for (int i = 0; i < list.Count; i++)
        {
            if (monster_pool.ContainsKey(list[i]))
            {
                //把所有的物件setactive false掉
                for (int j = 0; j < monster_pool[list[i]].Count; j++)
                {
                    monster_pool[list[i]][j].SetActive(false);
                }
            }
        }
    }

    public void CollectObject(GameObject go)
    {
        go.SetActive(false);
    }

    public void CollectObject(GameObject go, float delay)
    {
        StartCoroutine(Collect(go, delay));
    }

    private IEnumerator Collect(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        CollectObject(go);
    }

    public void Clear(string key)
    {
        if (monster_pool.ContainsKey(key))
        {
            //Destroy當中所有的物件
            for (int i = 0; i < monster_pool[key].Count; i++)
            {
                Destroy(monster_pool[key][i]);
            }
            //清除鍵當中的所有值
            //cache[key].Clear();
            //清除這個鍵（鍵值一起清除）
            monster_pool.Remove(key);
        }
    }

    public void ClearAll()
    {
        var list = new List<string>(monster_pool.Keys);
        for (int i = 0; i < list.Count; i++)
        {
            Clear(list[i]);
        }
    }
}
