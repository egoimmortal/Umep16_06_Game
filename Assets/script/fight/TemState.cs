using System.Collections.Generic;
using UnityEngine;

public class TemState
{
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
}
