using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FightCamera : MonoBehaviour
{
    public static GameObject camera1, camera2, camera3,
        Character1, Character2, target_monster;
    public static bool fight_ui_switch, start_switch;
    public static GameObject over_cube;
    public static Vector3 over_point;

    private GameObject monster1, monster2, cube1, cube2, cube3;
    private Vector3 start_border_left, start_border_right, screen_center,
        c_t_p, c_t_l, tm_t_p_1, tm_t_l_1;
    private bool fight_over_switch;
    private float fight_screen_change = 0.1f, start_monster_animation = 2f;

    RectTransform monster_width_1, monster_width_2;

    void Start()
    {
        camera1 = GameObject.Find("FightCamera1");
        camera2 = GameObject.Find("FightCamera2");
        camera3 = GameObject.Find("FightCamera3");

        StartSet();

        camera2.SetActive(false);
        /*
        StartSet();
        fight_ui_switch = true;

        var m_t_l_1 = monster1.transform.lossyScale;
        var m_t_p_1 = monster1.transform.position;
        start_border_left = new Vector3(m_t_p_1.x - m_t_l_1.x * 3 / 2, m_t_p_1.y + m_t_l_1.y / 2, m_t_p_1.z);//攝影機一開始的左邊框點

        if (monster2)
        {
            var m_t_l_2 = monster2.transform.lossyScale;
            var m_t_p_2 = monster2.transform.position;

            start_border_right = new Vector3(m_t_p_2.x + m_t_l_2.x, m_t_p_2.y + m_t_l_2.y / 2, m_t_p_2.z);//攝影機一開始的右邊框點
        }
        else
        {
            start_border_right = new Vector3(m_t_p_1.x + m_t_l_1.x, m_t_p_1.y + m_t_l_1.y / 2, m_t_p_1.z);//攝影機一開始的右邊框點
        }
        
        var distance = (start_border_left.x + start_border_right.x) / 2;
        screen_center = new Vector3(distance, start_border_left.y, start_border_left.z);
        
        if(!monster2)//一個敵人
            camera1.transform.position = new Vector3(screen_center.x, start_border_left.y, screen_center.z - distance * 4);
        else//兩個敵人
            camera1.transform.position = new Vector3(screen_center.x, start_border_left.y, screen_center.z - distance * 3 / 2);

        camera1.transform.LookAt(screen_center);
        //start_switch = true;
        camera1.SetActive(false);
        //atack_switch();
        */
    }
    /*
    void Update()
    {
        
        if (start_switch
            && Character)
        {
            OverCameraSet();
            Invoke("fightstart", start_monster_animation);
        }
        
        if (MoveToTarget.animator_over)
            StartCoroutine(fightover());
        
    }
    */
    /*
    private void LateUpdate()
    {
        
        //Debug.Log("NowCharacter = " + Character);
        ///<summy>
        ///目前暫定一種，之後要製作多種角度的攝影機進行Random去調整攝影機位置
        ///</summy>
        /*
        //選擇戰鬥目標後攝影機會跳到以被指定的敵人為中心，距離要將操控角色包含進去，位置要在操控角色的左後方
        if (target_monster)
        {
            //Debug.Log("進入選擇目標的攝影機");
            fight_ui_switch = false;
            atack_switch();

            //攻擊時候的視角(如果跟敵人的X軸太近的話視角就不會太歪)
            camera2.transform.position = new Vector3((c_t_p.x + screen_center.x) / 2, screen_center.y, c_t_p.z);
            camera2.transform.LookAt(screen_center);
        }
        *//*
        //選擇動作按鈕時的攝影機
        if (fight_ui_switch)
        {
            atack_switch();
            //Debug.Log("進入選擇按鈕的攝影機");
            
            //if (tm_t_p_1 == monster1.transform.position
            //    || !monster2)
            //    camera2.transform.position = new Vector3(start_border_left.x, screen_center.y + 1f, c_t_p.z - (screen_center.z - c_t_p.z) / 2);
            //else if (tm_t_p_1 == monster2.transform.position
            //    || !monster1)
            //    camera2.transform.position = new Vector3(start_border_left.x, screen_center.y + 1f, c_t_p.z - (screen_center.z - c_t_p.z) / 2);
            
            camera2.transform.position = new Vector3(start_border_left.x, screen_center.y * 2f, start_border_right.z);
            camera2.transform.LookAt(screen_center);
        }
        //Debug.Log(camera2.GetComponent<Camera>().pixelWidth);//攝影機寬度
    }
    */
    public void StartSet()
    {
        Character1 = GameObject.Find("Character1");
        Character2 = GameObject.Find("Character2");
        //monster1 = GameObject.Find("Monster1");
        //monster2 = GameObject.Find("Monster2");
        over_cube = GameObject.Find("OverCube");//兩個角色中間的CUBE，用於戰鬥結束的攝影機看向的位置

        if(Character2)
            over_point = (Character1.transform.position + Character2.transform.position) / 2;
        else
            over_point = Character1.transform.position;

        //over_cube.transform.position = over_point;
        camera3.transform.position = new Vector3(
            over_point.x - 3,
            over_point.y * 1.8f,
            over_point.z + 3);
        //camera3.LookAt(over_cube);
        /*
        c_t_l = Character1.transform.lossyScale;
        c_t_p = Character1.transform.position;
        tm_t_l_1 = monster1.transform.lossyScale;
        tm_t_p_1 = monster1.transform.position;
        */
        camera1.SetActive(false);
        //camera2.SetActive(false);
        camera3.SetActive(false);
        //camera4.SetActive(false);
    }

    void atack_switch()
    {
        if (Character1)
        {
            c_t_l = Character1.transform.lossyScale;
            c_t_p = Character1.transform.position;
            start_border_right = new Vector3(c_t_p.x + c_t_l.x * 3 / 2, c_t_p.y + c_t_l.y / 2, c_t_p.z);//攝影機的右邊框點
            Character1 = null;
        }
        if (target_monster)
        {
            tm_t_l_1 = target_monster.transform.lossyScale;
            tm_t_p_1 = target_monster.transform.position;
            start_border_left = new Vector3(tm_t_p_1.x - tm_t_l_1.x, tm_t_p_1.y + tm_t_l_1.y / 2, tm_t_p_1.z);//攝影機的左邊框點
            target_monster = null;
        }

        if (fight_ui_switch)
        {
            start_border_left = new Vector3(c_t_p.x - c_t_l.x * 3 / 2, c_t_p.y + c_t_l.y / 2, c_t_p.z);//攝影機的左邊框點
            start_border_right = new Vector3(tm_t_p_1.x + tm_t_l_1.x / 2, tm_t_p_1.y + tm_t_l_1.y / 2, tm_t_p_1.z);//攝影機的右邊框點
        }

        c_t_l = GameObject.Find("Monster1").transform.lossyScale;
        c_t_p = GameObject.Find("Monster1").transform.position;
        tm_t_l_1 = GameObject.Find("Character2").transform.lossyScale;
        tm_t_p_1 = GameObject.Find("Character2").transform.position;
        start_border_left = new Vector3(c_t_p.x - c_t_l.x * 2f, c_t_p.y + c_t_l.y * 0.5f, c_t_p.z);
        start_border_right = new Vector3(tm_t_p_1.x + tm_t_l_1.x * 2f, tm_t_p_1.y + tm_t_l_1.y * 0.5f, tm_t_p_1.z);

        var distance_x = (start_border_left.x + start_border_right.x) * 0.5f;
        var distance_z = (start_border_left.z + start_border_right.z) * 0.5f;
        var distance_y = (start_border_left.y + start_border_right.y) * 0.5f;
        screen_center = new Vector3(distance_x, distance_y, distance_z);
    }

    void OverCameraSet()
    {
        //Character = GameObject.Find("Character1");
        c_t_p = Character1.transform.position;
        c_t_l = Character1.transform.lossyScale;
        camera3.transform.position = new Vector3(c_t_p.x - 3 * c_t_l.x, c_t_p.y + c_t_l.y, c_t_p.z);
    }
    /*
    private void fightstart()
    {
        fight_over_switch = true;
        //start_switch = false;
        camera1.SetActive(false);
        camera2.SetActive(true);
    }
    */
    IEnumerator fightchange()
    {
        yield return new WaitForSeconds(fight_screen_change);
    }

    IEnumerator fightover()
    {
        yield return 0;
        camera2.SetActive(false);
        camera3.SetActive(true);
    }

    public void resetCamera3()
    {
       camera3.transform.position = new Vector3(
       over_point.x - 3,
       over_point.y * 1.8f,
       over_point.z + 3);
    }

    public void CameraSet()
    {

    }
    /// <summary>
    /// 勝利畫面
    /// </summary>
    public void FightOver()
    {
        ///結算畫面角色會圍著角色緩緩順時針轉動
        camera3.transform.RotateAround(over_point, over_cube.transform.up, 2f * Time.deltaTime);
        camera3.transform.LookAt(new Vector3(over_point.x, over_point.y + over_point.y, over_point.z));
    }

    ///<summary>
    /// 森林戰鬥場景 : 
    /// X Y Z
    /// P1 : 191.67 1.36 241.87
    /// P2 : 193.88 1.52 240.98
    /// M1 : 191.9 1.08 248.9
    /// M2 : 196.5 1.25 247.45
    /// M3 : 195.08 1.11 252.36
    /// camera看的cube : 
    ///     position -853.4478 -384.4345 220.625
    /// canera : 
    ///     position 189.124 3.967 241.34
    ///     rotation 26.916 54.477 1.773
    /// 
    /// </summary>
}