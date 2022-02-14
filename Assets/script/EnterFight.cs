using UnityEngine;

public class EnterFight : MonoBehaviour
{
    void Update()
    {
        ///進入戰鬥
        if (NewBehaviour.attacking == true && !TotalStatic.bFight)
        {
            TotalStatic.bFight = true;//進入戰鬥
            TotalStatic.MapUI.SetActive(false);//關閉MapUI
            MenuClick.MiniMap.SetActive(false);//關閉小地圖
            FadeInOut.FadeIn();//淡入
            Invoke("SuccessFight", 2f);//等淡入的效果結束後(2秒)進入戰鬥判斷
            Invoke("AIfalse", 0.6f);
            TotalStatic.MouseChange_icon.SetActive(false);
        }
    }
    public void AIfalse()
    {
        TotalStatic.MapAI.SetActive(false);//關閉地圖怪物AI
    }

    public void SuccessFight()
    {

        NewBehaviour.attacking = false;//大地圖怪物AI變成沒攻擊
        FightCamera.camera2.SetActive(true);//開啟戰鬥攝影機
        TotalStatic.objCamera1.SetActive(false);//關閉大地圖攝影機
        TotalStatic.StateChangeTime.SetActive(true);//開啟戰鬥用UI
        TotalStatic.FightMapUI.SetActive(true);//開啟共用UI
        MonsterPool.EnterFight(NewBehaviour.monster_type);//判斷敵人類型並叫出隨機類型敵人和隨機數量
        if(NewBehaviour.monster_type == "Boss")
        {
            Sounds.Instance().PlayNowMusic("BossFight");//播放Boss戰鬥音效
            //91.46997  -32.80472  347.75
            //92.46997 -32.77786 350.01
            FightCamera.camera2.transform.position = GameObject.Find("BossCameraPosition").transform.position;//王關戰鬥攝影機的位置
            FightCamera.camera2.transform.rotation = GameObject.Find("BossCameraPosition").transform.rotation;//王關戰鬥攝影機的位置
            TotalStatic.Player1.transform.position = new Vector3(
                91.46997f,
                -32.78f,
                347.75f);
            TotalStatic.Player2.transform.position = new Vector3(
                92.46997f,
                -32.788f,
                350.01f);
        }
        else
        {
            Sounds.Instance().PlayNowMusic("Fight");//播放戰鬥音效
            ///一般怪物戰鬥攝影機的位置//185.2728 3.801971 242.95
            FightCamera.camera2.transform.position = GameObject.Find("SampleCameraPosition").transform.position;//一般戰鬥戰鬥攝影機的位置
            FightCamera.camera2.transform.rotation = GameObject.Find("SampleCameraPosition").transform.rotation;//一般戰鬥攝影機的轉向
            TotalStatic.Player1.transform.position = new Vector3(
                190.90f,
                1.11f,//1.122406
                246.1299f);
            TotalStatic.Player2.transform.position = new Vector3(
                194.15f,
                1.22f,//1.245239
                245.7998f);
        }
        TotalStatic.bStartFight = true;//MainFight的戰鬥開啟
    }
}