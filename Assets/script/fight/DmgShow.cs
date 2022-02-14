using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgShow : MonoBehaviour
{
    private GameObject Dmg;  //傷害
    private GameObject Def; //防禦特效
    private GameObject Hp;
    private GameObject result;
    private Vector3 vpos;
    private string sTemDotDmg;

    public void DOTDmg(GameObject target, CharacterState state)
    {
        if(state.iburn_turn > 0)
        {
            Object o = Resources.Load("Fireeee");        
            Dmg = GameObject.Instantiate(o) as GameObject;
            Dmg.transform.SetParent(GameObject.Find("Canvas").transform);
            Dmg.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
            Dmg.GetComponent<Image>().CrossFadeAlpha(1, 1f, false); //淡入效果

            if (state.Hp <= state.burn_damage)
                sTemDotDmg = (state.Hp - 1).ToString();
            else
                sTemDotDmg = state.burn_damage.ToString();
            if (state.Hp == 1)
                sTemDotDmg = "0";

            result = Dmg.transform.GetChild(1).gameObject;//取得Text
            result.GetComponent<Text>().text = sTemDotDmg;//此處放入DOT傷害

            vpos = Camera.main.WorldToScreenPoint(target.transform.position);
            vpos.y += 200;
            Dmg.transform.position = vpos;
            sTemDotDmg = "";
        }
        if(state.ipoison_turn > 0)
        {
            Object o = Resources.Load("Dmg");

            Dmg = GameObject.Instantiate(o) as GameObject;
            Dmg.transform.SetParent(GameObject.Find("Canvas").transform);
            Dmg.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
            Dmg.GetComponent<Image>().CrossFadeAlpha(1, 1f, false); //淡入效果

            if (state.Hp <= state.poison_damage)//現在血量小於毒傷
            {
                if (state.iburn_turn > 0)
                    sTemDotDmg = (state.Hp - state.burn_damage - 1).ToString();//有燒傷
                else
                    sTemDotDmg = (state.poison_damage - 1).ToString();//沒燒傷
            }
            else
                sTemDotDmg = state.poison_damage.ToString();
            if (state.Hp == 1)
                sTemDotDmg = "0";

            result = Dmg.transform.GetChild(1).gameObject;//取得Text
            result.GetComponent<Text>().text = sTemDotDmg;//此處放入DOT傷害

            vpos = Camera.main.WorldToScreenPoint(target.transform.position);
            vpos.x += 100;
            vpos.y += 200;
            Dmg.transform.position = vpos;
            sTemDotDmg = "";
        }
    }

    public void ShowDmg(string skillName, GameObject target)
    {
        if (target.GetComponent<CharacterState>().bDefense)
        {
            Object o2 = Resources.Load("Def");
            Def = GameObject.Instantiate(o2) as GameObject;
            Def.transform.SetParent(GameObject.Find("Canvas").transform);
            Def.transform.position = new Vector3(
                target.transform.position.x,
                target.transform.position.y + 1f,
                target.transform.position.z + 0.6f);
        }
        Object o = Resources.Load("Dmg");
        Dmg = GameObject.Instantiate(o) as GameObject;
        Dmg.transform.SetParent(GameObject.Find("Canvas").transform);
        Dmg.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        Dmg.GetComponent<Image>().CrossFadeAlpha(1, 1f, false); //淡入效果

        result = Dmg.transform.GetChild(1).gameObject;//取得Text

        if(MoveToTarget.damage_result == "Miss")
            result.GetComponent<Text>().text = "Miss";
        else if (MoveToTarget.damage_result == "Critical")
            result.GetComponent<Text>().text = MoveToTarget.damage.ToString();//此處放入計算後的傷害
        else
            result.GetComponent<Text>().text = MoveToTarget.damage.ToString();//此處放入計算後的傷害

        vpos = Camera.main.WorldToScreenPoint(target.transform.position);
        vpos.y += 200;
        Dmg.transform.position = vpos;
    }

    public void ShowAddHp(string skillName, GameObject target)
    {
        Object o = Resources.Load("AddHp");
        Hp = GameObject.Instantiate(o) as GameObject;
        Hp.transform.SetParent(GameObject.Find("Canvas").transform);
        Hp.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        Hp.GetComponent<Image>().CrossFadeAlpha(1, 1f, false); //淡入效果

        result = Hp.transform.GetChild(1).gameObject;//取得Text
        result.GetComponent<Text>().text = (-MoveToTarget.damage).ToString();//此處放入計算後的傷害

        vpos = Camera.main.WorldToScreenPoint(target.transform.position);
        vpos.y += 200;
        Hp.transform.position = vpos;
    }

    public void ShowAddMp(string skillName, GameObject target)
    {
        Object o = Resources.Load("AddMp");
        Hp = GameObject.Instantiate(o) as GameObject;
        Hp.transform.SetParent(GameObject.Find("Canvas").transform);
        Hp.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        Hp.GetComponent<Image>().CrossFadeAlpha(1, 1f, false); //淡入效果

        result = Hp.transform.GetChild(0).gameObject;//取得Text
        result.GetComponent<Text>().text = (-MoveToTarget.damage).ToString();//此處放入計算後的傷害

        vpos = Camera.main.WorldToScreenPoint(target.transform.position);
        vpos.y += 200;
        Hp.transform.position = vpos;
    }
}
