using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterState : MonoBehaviour
{
    /// <summary>
    /// 該script附加在Assets/Prefabs/的所有角色和怪物身上
    /// script說明 : 
    ///     宣告所有屬性
    ///     PlayerFightSet 進入戰鬥時將角色時間歸零
    ///     MonsterFightSet 進入戰鬥時將怪物血條補滿和時間歸零
    ///     SAVE用資料
    /// </summary>

    /// <summary>
    /// 公有變數
    /// </summary>
    public string sName;
    public int Level;
    public int Hp;
    public int MaxHp;
    public int Mp;
    public int MaxMp;
    public int Exp;
    public int MaxExp = 100;
    public int Atk;
    public int Matk;
    public int Def;
    public int Mdef;
    public int iSpeed;
    public float fTime = 0;
    public int Dodge;//迴避
    public int Hit;//命中
    public int Critical;//爆擊
    public Vector3 vPos;//存檔用位置
    public List<int> ItemList = new List<int>();//存檔用道具數量(0紅水,1藍水,2復活)
    public int iburn_turn = 0;//燒傷的回合
    public int ipoison_turn = 0;//中毒的回合
    public int burn_damage = 0;//燒傷的傷害
    public int poison_damage = 0;//中毒的傷害
    public int iStrongTime;//strong狀態的回合數
    public bool bDefense;//是否正在防禦
    public bool bLevelUp;//是否有升等
    public GameObject objState;//戰鬥UI用的State
    public GameObject objLv;//戰鬥UI用的LV
    private Dictionary<int, int> LevelOfSkillLock = new Dictionary<int, int>();//技能鎖
    private Dictionary<string, int> DOriginData = new Dictionary<string, int>();//初始資訊
    private Dictionary<string, int> DBeforeLevelup = new Dictionary<string, int>();//升等前的資訊
    private Dictionary<string, int> DAfterLevelup = new Dictionary<string, int>();//升等後的資訊

    private string search_time_bar;
    private GameObject tem_time_bar;
    private Vector2 origin_position;

    public void FightSet()
    {
        if (gameObject.tag == "Player")
        {
            if (Hp <= 0)
                Hp = 1;
            objLv.GetComponent<Text>().text = "Lv " + Level.ToString();
            DBeforeLevelup.Clear();
            DAfterLevelup.Clear();
        }
        else
        {
            Hp = MaxHp;
            Mp = MaxMp;
            iStrongTime = 0;
        }
        bDefense = false;//防禦歸無
        fTime = 0;//時間歸0
        iburn_turn = 0;//燒傷的回合
        ipoison_turn = 0;//中毒的回合
        burn_damage = 0;//燒傷的傷害
        poison_damage = 0;//中毒的傷害
        OriginTimeBarSet();//取得現在的時間表
        TimeBarDisplay();
        DOriginData.Clear();//清空現在的資料
        SaveDataToDictionary(DOriginData);//將現在的資料存入DOriginData
    }

    public void OriginTimeBarSet()
    {
        search_time_bar = gameObject.name + "_TimeBar";
        tem_time_bar = GameObject.Find(search_time_bar);
        origin_position = tem_time_bar.GetComponent<RectTransform>().anchoredPosition;
    }

    public void TimeBarAdd()
    {
        if(GameObject.Find(search_time_bar))
            GameObject.Find(search_time_bar).GetComponent<RectTransform>().anchoredPosition = new Vector2(
                origin_position.x,
                origin_position.y - fTime);
    }

    public void TimeBackOrigin()
    {
        if(GameObject.Find(search_time_bar))
            GameObject.Find(search_time_bar).GetComponent<RectTransform>().anchoredPosition = origin_position;
    }

    public void TimeBarDisplay()
    {
        tem_time_bar.GetComponent<Image>().enabled = true;
        tem_time_bar.transform.GetChild(0).GetComponent<Image>().enabled = true;
    }

    public void TimeBarHide()
    {
        tem_time_bar.GetComponent<Image>().enabled = false;
        tem_time_bar.transform.GetChild(0).GetComponent<Image>().enabled = false;
    }

    public void Settlement(int tem_exp)
    {
        Exp += tem_exp;
        if(Exp >= MaxExp)
        {
            SaveDataToDictionary(DBeforeLevelup);
            if (gameObject.name == "Character1")
            {    
                Exp -= MaxExp;
                MaxExp += 20;
                Level += 1;
                MaxHp += 20;
                Hp = MaxHp;
                MaxMp += 15;
                Mp = MaxMp;
                Atk += 2;
                Matk += 10;
                Def += 3;
                Mdef += 3;
            }
            else
            {
                Exp -= MaxExp;
                MaxExp += 20;
                Level += 1;
                MaxHp += 25;
                Hp = MaxHp;
                MaxMp += 10;
                Mp = MaxMp;
                Atk += 10;
                Matk += 2;
                Def += 3;
                Mdef += 3;
            }
            SaveDataToDictionary(DAfterLevelup);
            bLevelUp = true;
            TotalStatic.bLevelUp = true;
        }
    }

    private void SaveDataToDictionary(Dictionary<string, int> book)
    {
        book.Add("Level", Level);
        book.Add("MaxHp", MaxHp);
        book.Add("MaxMp", MaxMp);
        book.Add("Atk", Atk);
        book.Add("Matk", Matk);
        book.Add("Def", Def);
        book.Add("Mdef", Mdef);
        book.Add("iSpeed", iSpeed);
    }

    public Dictionary<string, int> GetDataFromDictionary(string book)
    {
        if (book == "before")
            return DBeforeLevelup;
        else if (book == "after")
            return DAfterLevelup;
        else if (book == "now")
            return DOriginData;
        else
            return null;
    }
}